// Peg.cs
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

public class Peg
{

    public Vector2 Position { get; private set; }

    public static Dictionary<PegType, Texture2D> pegTextures = new Dictionary<PegType, Texture2D>();

    private Texture2D texture;

    private PegType type;

    public bool IsHit { get; set; } = false;

    public bool IsActive { get; set; } = true;

    private static float scale = 0.15f;

    public float pegRadius = (200 / 2) * scale;


    public Peg(Vector2 position, PegType type)
    {
        Position = position;
        texture = pegTextures[type];
    }

    public void Update(GameTime gameTime)
    {
      if (IsHit)
        {
            this.switchTexture();
            System.Diagnostics.Debug.WriteLine($"PEGS NEW TYPE IS: {this.type}");
            this.texture = pegTextures[this.type];
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if(!IsActive) return;

        spriteBatch.Draw(texture, Position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
    }

    public void LoadContent(ContentManager content)
    {
        

    }
    public static void InitializeTextures(ContentManager content)
    {
        pegTextures.Add(PegType.GREEN, content.Load<Texture2D>("unlit_green_peg"));
        pegTextures.Add(PegType.GREENL, content.Load<Texture2D>("lit_green_peg"));
        pegTextures.Add(PegType.GREENG, content.Load<Texture2D>("glowing_green_peg"));
        pegTextures.Add(PegType.BLUE, content.Load<Texture2D>("unlit_blue_peg"));
        pegTextures.Add(PegType.BLUEL, content.Load<Texture2D>("lit_blue_peg"));
        pegTextures.Add(PegType.BLUEG, content.Load<Texture2D>("glowing_blue_peg"));
        pegTextures.Add(PegType.YELLOW, content.Load<Texture2D>("unlit_yellow_peg"));
        pegTextures.Add(PegType.YELLOWL, content.Load<Texture2D>("lit_yellow_peg"));
        pegTextures.Add(PegType.YELLOWG, content.Load<Texture2D>("glowing_yellow_peg"));
        pegTextures.Add(PegType.RED, content.Load<Texture2D>("unlit_red_peg"));
        pegTextures.Add(PegType.REDL, content.Load<Texture2D>("lit_red_peg"));
        pegTextures.Add(PegType.REDG, content.Load<Texture2D>("glowing_red_peg"));
        pegTextures.Add(PegType.ERROR, content.Load<Texture2D>("ball"));
    }
    public void switchTexture()
    {
        PegType type = this.type;
        System.Diagnostics.Debug.WriteLine($"TESTING WITH CASE: {type}");
        switch (type)
        {
            case PegType.GREEN:
                this.type = PegType.GREENG;
                break;
            case PegType.BLUE:
                this.type = PegType.BLUEG;
                break;
            case PegType.YELLOW:
                this.type = PegType.YELLOWG;
                break;
            case PegType.RED:
                this.type = PegType.REDG;
                break;
            default: 
                this.type = type;
                break;
        }
    }

}
