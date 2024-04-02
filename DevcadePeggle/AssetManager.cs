using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class AssetManager
{
    private ContentManager content;
    private Dictionary<string, Texture2D> textures;
    //private Dictionary<string, SoundEffect> soundEffects;

    public AssetManager(ContentManager content)
    {
        this.content = content;
        textures = new Dictionary<string, Texture2D>();
        //soundEffects = new Dictionary<string, SoundEffect>();
    }
    /*
    public Texture2D GetTexture(string assetName)
    {
        
    }
    */

}
