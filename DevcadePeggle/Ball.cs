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
    private float gravity = 600f; // subject to change
    private float dampingFactor = 0.95f; // How much "energy" is lost upon collision, lower values means more loss
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


        // Apply gravity to the velocity's Y component
        float velocityX = Velocity.X;
        float velocityY = Velocity.Y + gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;


        // Check for wall collision
        if (Position.X < 0 || Position.X > Game1.screenWidth - diameter)
        {
            velocityX *= -1 * dampingFactor; // Invert X velocity and apply damping factor
        }
        if (Position.Y < 0 || Position.Y > Game1.screenHeight - diameter)
        {
            velocityY *= -1 * dampingFactor; // Invert Y velocity and apply damping factor
        }

        // After Wall collision checks and modifications, reassign the Velocity
        Velocity = new Vector2(velocityX, velocityY);



        //CheckCollisionWithPegs(level.pegs);
        CheckCollisionWithPegs(pegs);
        // Change position based on velocity
        System.Diagnostics.Debug.WriteLine($"VELOCITY BEING UPDATED: {this.Velocity}");
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

    public void CheckCollisionWithPegs(List<Peg> pegs)
    {
        foreach (var peg in pegs.Where(p => p.IsActive))
        {
            float distance = Vector2.Distance(this.Position, peg.Position);

            System.Diagnostics.Debug.WriteLine($"Checking collision... Distance to peg: {distance}, Required: {this.radius + peg.pegRadius}");

            if (distance < this.radius + peg.pegRadius) // Assuming radius is defined for both ball and peg
            {
                peg.IsHit = true; // Deactivate the peg

                System.Diagnostics.Debug.WriteLine("Collision detected with peg. Peg marked as hit.-----------------------------------------------------------------------------");

                // Calculate the normal at the point of contact
                Vector2 normal = Vector2.Normalize(this.Position - peg.Position);
                System.Diagnostics.Debug.WriteLine($"Normal: {normal}");

                // Calculate the dot product of the ball's velocity and the normal
                float dotProduct = Vector2.Dot(this.Velocity, normal);
                System.Diagnostics.Debug.WriteLine($"Dot Product: {dotProduct}");

                // Reflect the ball's velocity vector
                System.Diagnostics.Debug.WriteLine($"Current Velocity: {this.Velocity}");
                this.Velocity = this.Velocity - 2 * dotProduct * normal;
                System.Diagnostics.Debug.WriteLine($"New Velocity: {this.Velocity}");
            }
        }
    }


}
