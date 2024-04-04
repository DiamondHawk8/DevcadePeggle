using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Reflection.Metadata.Ecma335;
using System;
using DevcadePeggle;


public class Ball
{
    public Vector2 Position { get; set; }

    public Vector2 Velocity { get; set; }

    public bool IsActive { get; set; } // Whether the ball is currently active

    private Texture2D texture;

    // Constants:

    private static float scale = 0.15f; // Scale factor for the ball texture
    private float gravity = 500f; // subject to change
    private float dampingFactor = 0.95f; // How much "energy" is lost upon collision, lower values means more loss
    private float ballDiameter = 200 * scale;

    public Action OnHitBottom { get; set; }


    public Ball()
    {   
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

        // Check for horizontal wall collision
        if (Position.X < 0 || Position.X > Game1.screenWidth - ballDiameter)
        {
            velocityX *= -1 * dampingFactor; // Invert X velocity and apply damping factor
        }

        // After collision checks and modifications, reassign the Velocity
        Velocity = new Vector2(velocityX, velocityY);

        // Change position based on velocity
        Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;




        if (this.Position.Y > Game1.screenHeight - ballDiameter)
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
}
