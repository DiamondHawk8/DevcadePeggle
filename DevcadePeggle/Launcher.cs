using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Launcher
{
    public Vector2 Position { get; private set; }
    public float Angle { get; set; } // In radians

    private const float MaxAngle = MathHelper.PiOver2; // 90 degrees
    private const float MinAngle = -MathHelper.PiOver2; // -90 degrees

    //textures to represent state of the launcher
    private Texture2D loadedTexture;
    private Texture2D unloadedTexture;
    private Texture2D currentTexture; // This will point to the texture currently being used.


    private float scale = 0.5f; // Scale factor for the launcher texture

    public Launcher(Vector2 position)
    {
        Position = position;
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
        if (kstate.IsKeyDown(Keys.Left))
            Angle -= 0.05f; // Rotate counter-clockwise
        else if (kstate.IsKeyDown(Keys.Right))
            Angle += 0.05f; // Rotate clockwise

        // Ensure the launcher angle stays within bounds
        Angle = MathHelper.Clamp(Angle, MinAngle, MaxAngle);

        if (kstate.IsKeyDown(Keys.Space)) // Assuming space bar is used to fire
        {
            ToggleLauncherState();
            // Include logic to "fire" the ball here.
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // Define the origin for rotation around the bottom center of the texture
        Vector2 origin = new Vector2(currentTexture.Width / 2, currentTexture.Height / 2);
        // Draw the launcher with rotation and scaling
        spriteBatch.Draw(currentTexture, Position, null, Color.White, Angle, origin, scale, SpriteEffects.None, 0f);
    }

    // Helper method for swapping texture
    public void ToggleLauncherState()
    {
        if (currentTexture == loadedTexture)
        {
            currentTexture = unloadedTexture;
        }
        else
        {
            currentTexture = loadedTexture;
        }
    }

}

