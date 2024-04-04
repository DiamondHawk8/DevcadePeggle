using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


public class Ball
{
    public Vector2 Position { get; set; }

    public Vector2 Velocity { get; set; }

    public bool IsActive { get; set; } // Whether the ball is currently active

    private Texture2D texture;
    private float scale = 0.15f; // Scale factor for the ball texture
    private float gravity = 0f; // placeholder
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

    public void Update(GameTime gameTime)
    {
        if (!IsActive) return;
        //subject velocity to the effects of gravity
        Velocity = new Vector2(Velocity.X, Velocity.Y + gravity * (float)gameTime.ElapsedGameTime.TotalSeconds);
        //change position based on velocity
        Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!IsActive) return;

        spriteBatch.Draw(texture, Position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
    }
}
