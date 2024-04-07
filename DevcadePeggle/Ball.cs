using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Linq;
using System;
using DevcadePeggle;


public class Ball
{
    public Vector2 Position { get; set; }

    public Vector2 Velocity { get; set; }

    public bool IsActive { get; set; } // Whether the ball is currently active

    private Texture2D texture;

    //private Level level;

    //temp peg list variable for testing
    private List<Peg> pegs;

    // Constants:

    private static float scale = 0.15f; // Scale factor for the ball texture
    private float gravity = 800f; // subject to change
    private float dampingFactor = 0.9f; // How much "energy" is lost upon collision, lower values means more loss
    private float diameter = 200 * scale;
    private float radius = (200 / 2) * scale;

    //delete
    public Action OnHitBottom { get; set; }


    public Ball(List<Peg> pegs)
    {   
        this.pegs = pegs;
        Position = Vector2.Zero;
        Velocity = Vector2.Zero;
        IsActive = false;
    }

    public void LoadContent(ContentManager content)
    {
        texture = content.Load<Texture2D>("ball"); 
    }

    //Delegate to handle the ball hitting the bottom of screen
    

    public void Update(GameTime gameTime)
    {

        if (!IsActive) return;


        float velocityX = Velocity.X;
        float velocityY = Velocity.Y;

        // Update velocity based on gravity effects and wall/ceiling collisions
        Velocity = UpdateVelocityWithGravityAndCollisions(velocityX, velocityY, gameTime);


        //ReCheckCollisionWithPegs(level.pegs);
        ReCheckCollisionWithPegs(pegs);

        // Change position based on velocity
        Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;


        if (this.Position.Y > Game1.screenHeight - diameter)
        {
            this.IsActive = false; // Allow for another ball to be fired
            OnHitBottom?.Invoke();
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!IsActive) return;

        spriteBatch.Draw(texture, Position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
    }


    //Deprecated, do not use
    public void CheckCollisionWithPegs(List<Peg> pegs)
    {
        foreach (var peg in pegs.Where(p => p.IsActive))
        {
            float distance = Vector2.Distance(this.Position, peg.Position);


            if (distance < this.radius + peg.pegRadius) // Assuming radius is defined for both ball and peg
            {
                peg.IsHit = true; // Deactivate the peg


                // Calculate the normal at the point of contact
                Vector2 normal = Vector2.Normalize(this.Position - peg.Position);


                // Calculate the dot product of the ball's velocity and the normal
                float dotProduct = Vector2.Dot(this.Velocity, normal);


                // Reflect the ball's velocity vector
                this.Velocity = this.Velocity - 2 * dotProduct * normal * dampingFactor;

            }
        }


      
    }


    public void ReCheckCollisionWithPegs(List<Peg> pegs)
    {
        foreach (var peg in pegs.Where(p => p.IsActive))
        {
            float distance = Vector2.Distance(this.Position, peg.Position);
            float overlap = this.radius + peg.pegRadius - distance;

            if (overlap > 0) // Collision detected
            {
                peg.IsHit = true;
                // Calculate the normal at the point of contact
                Vector2 normal = Vector2.Normalize(this.Position - peg.Position);

                // Reflect the ball's velocity vector
                float dotProduct = Vector2.Dot(this.Velocity, normal);
                this.Velocity = this.Velocity - 2 * dotProduct * normal * dampingFactor;

                // Reposition the ball to ensure it's not intersecting with the peg
                this.Position += normal * overlap;
            }
        }
    }


    public Vector2 UpdateVelocityWithGravityAndCollisions(float velocityX, float velocityY, GameTime gameTime)
    {
        float screenRightBoundary = Game1.screenWidth - diameter;
        float screenTopBoundary = 0; // Ceiling

        // Apply gravity to Y velocity before checking collisions
        velocityY += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Check for wall collisions (left and right)
        if (Position.X < 0 || Position.X > screenRightBoundary)
        {
            velocityX *= -1 * dampingFactor; // Invert X velocity and apply damping factor
            Position = new Vector2(MathHelper.Clamp(Position.X, 0, screenRightBoundary), Position.Y);
        }

        // Check for ceiling collision
        if (Position.Y < screenTopBoundary)
        {
            velocityY *= -1 * dampingFactor; // Invert Y velocity and apply damping factor
            Position = new Vector2(Position.X, screenTopBoundary);
        }

        return new Vector2(velocityX, velocityY);
    }


}
