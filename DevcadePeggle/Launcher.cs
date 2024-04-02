using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Launcher
{
    public Vector2 Position { get; private set; }
    public float Angle { get; set; } //radians

    private const float MaxAngle = MathHelper.PiOver2; // 90 degrees

    private const float MinAngle = -MathHelper.PiOver4; // -45 degrees

    float scale = 0.5f; // variable for 

    private Texture2D texture; 

    public Launcher(Vector2 position)
    {
        Position = position;
        Angle = 0; // Initial angle pointing straight up
    }

    public void LoadContent(ContentManager content)
    {
        texture = content.Load<Texture2D>("cannon-loaded"); 
    }

    public void Update(GameTime gameTime)
    {
        var kstate = Keyboard.GetState();

        if (kstate.IsKeyDown(Keys.Left))
            Angle -= 0.05f; // Rotate counter-clockwise
        else if (kstate.IsKeyDown(Keys.Right))
            Angle += 0.05f; // Rotate clockwise

        // Clamp the angle within allowed bounds
        Angle = MathHelper.Clamp(Angle, MinAngle, MaxAngle);
    }


    public void Draw(SpriteBatch spriteBatch)
    {
        // Calculate the origin to rotate around
        Vector2 origin = new Vector2(texture.Width / 2, texture.Height);
        spriteBatch.Draw(texture, Position, null, Color.White, Angle, origin, 1f, SpriteEffects.None, 0f);
    }
}