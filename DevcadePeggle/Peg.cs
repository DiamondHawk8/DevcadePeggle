// Peg.cs
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Peg
{

    public Vector2 Position { get; private set; }

    private Texture2D texture;

    public bool IsHit { get; private set; }

    public Peg(Vector2 position)
    {
        Position = position;
        IsHit = false;
      
    }

    public void Update(GameTime gameTime)
    {
     
    }

    public void Draw(SpriteBatch spriteBatch)
    {
  
    }
}
