using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Devcade;

namespace DevcadePeggle
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D backgroundTexture; // Stores background

        // class instances 

        private Launcher launcher;
        private Ball ball;
        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false; // Consider setting true for debugging purposes
        }

        protected override void Initialize()
        {
            Input.Initialize(); // Initialize input, remove if unused

            // Standard resolution setup for debug and release modes
#if DEBUG
            _graphics.PreferredBackBufferWidth = 420; // Debug mode width
            _graphics.PreferredBackBufferHeight = 980; // Debug mode height
#else
            _graphics.PreferredBackBufferWidth = 1080; // Standard width for release
            _graphics.PreferredBackBufferHeight = 2560; // Standard height for release
#endif
            _graphics.ApplyChanges();

            // Ball Initialization
            ball = new Ball();

            // Launcher Initialization
            Vector2 launcherPosition = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height - 2460);
            launcher = new Launcher(launcherPosition, ball);

            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load background texture
            backgroundTexture = Content.Load<Texture2D>("TempBG");

            // Class loading
            launcher.LoadContent(Content);
            ball.LoadContent(Content);
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GameShouldExit())
            {
                Exit();
            }
            launcher.Update(gameTime);

            // Update game logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White); // Use Vector2.Zero for drawing at origin
            launcher.Draw(_spriteBatch);
            ball.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private bool GameShouldExit()
        {
            // Simplifies the exiting logic check
            var kbState = Keyboard.GetState();
            return kbState.IsKeyDown(Keys.Escape) ||
                   (Input.GetButton(1, Input.ArcadeButtons.Menu) && Input.GetButton(2, Input.ArcadeButtons.Menu));
        }
    }
}