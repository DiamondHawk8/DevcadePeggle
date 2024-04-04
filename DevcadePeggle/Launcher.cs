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
    public Texture2D loadedTexture;
    public Texture2D unloadedTexture;
    public Texture2D currentTexture; // This will point to the texture currently being used.

    // Ball object reference for use in FireBall method
    private Ball ball;

    // Bool to prevent double firing
    private bool isFiring = false;

    private float scale = 0.5f; // Scale factor for the launcher texture

    private float launchSpeed = 640f; // Speed of the ball at launch (just kinda eyeballed it)
    private float launcherLength = 85f; // Distance from launcher's base to tip


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

        /*
        //debug code for determining good launch speeds
        if (kstate.IsKeyDown(Keys.Up))
            this.launchSpeed -= 0.5f;
        else if (kstate.IsKeyDown(Keys.Down))
            this.launchSpeed += 0.5f;
        System.Diagnostics.Debug.WriteLine("Current launch speed: " + this.launchSpeed);
        */
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


        Vector2 ballStartPosition = new Vector2(
                Position.X + (float)Math.Cos(Angle + MathHelper.PiOver2) * this.launcherLength,
                Position.Y + (float)Math.Sin(Angle + MathHelper.PiOver2) * this.launcherLength
            );

            // Set ball's initial position and velocity
            ball.Position = ballStartPosition;

            Vector2 ballStartVelocity = new Vector2(
               (float)Math.Cos(Angle + MathHelper.PiOver2) * this.launchSpeed,
               (float)Math.Sin(Angle + MathHelper.PiOver2) * this.launchSpeed
            );


            /*
            System.Diagnostics.Debug.WriteLine("The X value is the cos of [" + (Angle + MathHelper.PiOver2) + "], which is [" + (float)Math.Cos(Angle + MathHelper.PiOver2) + "]");
            System.Diagnostics.Debug.WriteLine("The Y Value is the sin of [" + (Angle + MathHelper.PiOver2) + "], which is [" + (float)Math.Sin(Angle + MathHelper.PiOver2) + "]");
            System.Diagnostics.Debug.WriteLine("The final vector is [" + (float)Math.Cos(Angle + MathHelper.PiOver2) * launchSpeed + "," + (float)Math.Sin(Angle + MathHelper.PiOver2) * launchSpeed + "]");
            System.Diagnostics.Debug.WriteLine("--------------");
            */

            ball.Velocity = ballStartVelocity;

            ball.IsActive = true; // Mark the ball as active
        }
        
   

    }






