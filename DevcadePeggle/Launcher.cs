using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

public class Launcher
{
    public Vector2 Position { get; private set; }
    public float Angle { get; set; } // In radians

    private const float MaxAngle = MathHelper.PiOver2; // 90 degrees
    private const float MinAngle = -MathHelper.PiOver2; // -90 degrees

    // Textures to represent state of the launcher
    private Texture2D loadedTexture;
    private Texture2D unloadedTexture;
    private Texture2D currentTexture; // This will point to the texture currently being used.

    // Ball object reference for use in FireBall method
    private Ball ball;

    // Bool to prevent double firing
    private bool isFiring = false;

    private float scale = 0.5f; // Scale factor for the launcher texture

    public Launcher(Vector2 position, Ball ball)
    {
        Position = position;
        this.ball = ball;
        Angle = 0; // Initial angle pointing straight up
    }

    public void LoadContent(ContentManager content)
    {
        // Assumes "cannon-loaded" is the name of your launcher texture in the Content project
        loadedTexture = content.Load<Texture2D>("cannon-loaded");
        unloadedTexture = content.Load<Texture2D>("cannon-unloaded");
        currentTexture = loadedTexture; // Assume the cannon starts loaded.
    }

    public void Update(GameTime gameTime)
    {
        var kstate = Keyboard.GetState();

        // Adjust launcher angle based on input
        if (kstate.IsKeyDown(Keys.Right))
            Angle -= 0.05f; // Rotate counter-clockwise
        else if (kstate.IsKeyDown(Keys.Left))
            Angle += 0.05f; // Rotate clockwise

        // Clamp the angle within allowed bounds
        Angle = MathHelper.Clamp(Angle, MinAngle, MaxAngle);

        // Handle firing logic
        if (kstate.IsKeyDown(Keys.Space) && !isFiring)
        {
            FireBall();
            isFiring = true;
        }
        else if (kstate.IsKeyUp(Keys.Space))
        {
            isFiring = false; // Reset firing state when space is released
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // Define the origin for rotation around the bottom center of the texture
        Vector2 origin = new Vector2(currentTexture.Width / 2, currentTexture.Height / 2);
        // Draw the launcher with rotation and scaling
        spriteBatch.Draw(currentTexture, Position, null, Color.White, Angle, origin, scale, SpriteEffects.None, 0f);
    }




    public void FireBall()
    {
        // Prevent firing if a ball is already active
        if (ball.IsActive) return;

        // Swap texture between loaded and unloaded states
        currentTexture = (currentTexture == loadedTexture) ? unloadedTexture : loadedTexture;

        // Define launch speed and launcher length
        float launchSpeed = 500f; // Speed of the ball at launch
        float launcherLength = 0f; // Distance from launcher's base to tip

        // Calculate starting position of the ball at the launcher's tip
        Vector2 ballStartPosition = new Vector2(
            Position.X + (float)Math.Cos(Angle) * launcherLength,
            Position.Y + (float)Math.Sin(Angle) * launcherLength
        );

        // Set ball's initial position and velocity
        ball.Position = ballStartPosition;

        UpdateVelocityBasedOnAngle();
        ball.IsActive = true; // Mark the ball as active
    }

    public void UpdateVelocityBasedOnAngle()
    {
        float launchSpeed = 500f; 

        // Directly down
        if (Angle == 0)
        {
            ball.Velocity = new Vector2(0, launchSpeed); 
        }
        // Angles less than 0 (right side)
        else if (Angle < 0)
        {
            ball.Velocity = new Vector2(-launchSpeed, 0);
        }
        // Directly to the left
        else if (Angle > 0)
        {
            ball.Velocity = new Vector2(launchSpeed, 0); 
        }

    }


}



