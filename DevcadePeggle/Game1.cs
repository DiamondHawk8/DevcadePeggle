using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Devcade;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace DevcadePeggle
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D backgroundTexture; // Stores background

        //testing
        private List<Peg> Testpegs;

        // class instances 

        private Launcher launcher;
        private Ball ball;

        public static int screenWidth = 1080;
        public static int screenHeight = 2560;

        public static int score = 0;

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
            //#if DEBUG
            //_graphics.PreferredBackBufferWidth = 420; // Debug mode width
            //_graphics.PreferredBackBufferHeight = 980; // Debug mode height
            //#else
            _graphics.PreferredBackBufferWidth = screenWidth; // Standard width for release
            _graphics.PreferredBackBufferHeight = screenHeight; // Standard height for release
                                                                //#endif
            _graphics.ApplyChanges();

            Peg.InitializeTextures(Content);

            Testpegs = createTestList();

            // Ball Initialization
            ball = new Ball(Testpegs);


            // Launcher Initialization
            Vector2 launcherPosition = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 25.6f); // y arg is set to 100
            launcher = new Launcher(launcherPosition, ball);

            //Delegate method for ball hading colliding with the bottom
            ball.OnHitBottom = () =>
            {
                launcher.currentTexture = (launcher.currentTexture == launcher.loadedTexture) ? launcher.unloadedTexture : launcher.loadedTexture;
            };



            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load background texture
            backgroundTexture = Content.Load<Texture2D>("TempBG");

            // Class loading
            ball.LoadContent(Content);
            launcher.LoadContent(Content);
            

        }

        protected override void Update(GameTime gameTime)
        {
            if (GameShouldExit())
            {
                Exit();
            }

            foreach (var peg in Testpegs)
            {
                peg.Update(gameTime); // Calls the Draw method of each Peg, passing in the SpriteBatch
            }

            launcher.Update(gameTime);
            
            ball.Update(gameTime);

            base.Update(gameTime);

            foreach(var peg in Testpegs)
            {
                peg.Update(gameTime);
            }

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White); // Use Vector2.Zero for drawing at origin
            launcher.Draw(_spriteBatch);

            foreach (var peg in Testpegs) 
            {
                peg.Draw(_spriteBatch); // Calls the Draw method of each Peg, passing in the SpriteBatch
            }

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

        public static List<Peg> createTestList()
        {
            List<Peg> pegs = new List<Peg>();
            Vector2 startPosition = new Vector2(550, 550); // Starting position for the first peg
            int numberOfRows = 3; // Number of rows of pegs
            int numberOfColumns = 3; // Number of columns of pegs
            int spacing = 100; // Space between pegs

            // Populate the peg list with positions in a grid
            for (int row = 0; row < numberOfRows; row++)
            {
                for (int col = 0; col < numberOfColumns; col++)
                {
                    Vector2 pegPosition = new Vector2(startPosition.X + col * spacing, startPosition.Y + row * spacing);
                    Peg newPeg = new Peg(pegPosition, PegType.GREEN); 
                    pegs.Add(newPeg);
                }
            }
            return pegs;
        }

    }
}
