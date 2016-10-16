using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Drawing;
namespace mGame {
    enum KeyState
    {
        Idle,
        On,
        Off
    }
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class POIGame : Game {
        GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static Graphics GraphicManager;
        public static UpdateableContainer AnimationManager;
        public static UpdateableContainer InstanceManager;
        public static UpdateableContainer MoveableManager;
        public static UInt32[] LevelData;
        public static bool GetKeyPressed(Keys key)
        {
            return keyPressed[key] == KeyState.On;
        }
        private static Dictionary<Keys, KeyState> keyPressed;
        public const int LevelWidth = 500, LevelHeight = 500;
        //Will define a lot of the draw behavior...
        public static RefRectangle Camera;
        private const int GameWidth = 432;
        private const int GameHeight = 336;
        //level generation test
        Rectangle levelDimensions;
        Texture2D level;
        public static double DeltaTime {
            get { return deltaTime; }
        }
        private static double deltaTime;
        private Player playerA, playerB;
        private TileMap tiles;
        public POIGame() {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GameWidth;
            graphics.PreferredBackBufferHeight = GameHeight;
            GraphicManager = new Graphics();
            AnimationManager = new UpdateableContainer();
            InstanceManager = new UpdateableContainer();
            MoveableManager = new UpdateableContainer();
            Camera = new RefRectangle();
            Camera.Value.Width = GameWidth;
            Camera.Value.Height = GameHeight;
            keyPressed = new Dictionary<Keys, KeyState>();
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here
            RoomGenerator.graphicsDevice = GraphicsDevice;
            //level generation test
            levelDimensions = new Rectangle(0, 0, 500, 500);
            level = new Texture2D(GraphicsDevice, levelDimensions.Width, levelDimensions.Height);
            LevelData = new RoomGenerator(500, 500).Generate(6, 14, 4, 10, 2);//6,24, 2, 4 //6, 24, 4, 10
            level.SetData(LevelData);
            base.Initialize();
        }
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            Assets.playerSprite = Content.Load<Texture2D>("Content/Player");
            Assets.mapTiles = Content.Load<Texture2D>("Content/tileSet");
            playerA = new Player(Keys.Right, Keys.Left,Keys.Up, Keys.Down);
            playerB = new Player(Keys.D, Keys.A, Keys.W, Keys.S, 251, 250);
            tiles = new TileMap(Assets.mapTiles);
            InstanceManager.AddInstance(playerA);
            InstanceManager.AddInstance(playerB);
            GraphicManager.AddGraphic(tiles);
            
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            foreach(Keys key in Enum.GetValues(typeof(Keys)))
            {
                if (Keyboard.GetState().IsKeyDown(key))
                {
                    if (keyPressed[key] == KeyState.Idle)
                        keyPressed[key] = KeyState.On;
                    else if (keyPressed[key] == KeyState.On)
                        keyPressed[key] = KeyState.Off;
                }
                else
                {
                    keyPressed[key] = KeyState.Idle;
                }
            }
            deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
            InstanceManager.Update();
            AnimationManager.Update();
            MoveableManager.Update();
            //Camera logic
            int playerA_X = playerA.Position.Value.ToPoint().X - Camera.Value.Width / 2;
            int playerB_X = playerB.Position.Value.ToPoint().X - Camera.Value.Width / 2;
            int playerA_Y = playerA.Position.Value.ToPoint().Y - Camera.Value.Height / 2;
            int playerB_Y = playerB.Position.Value.ToPoint().Y - Camera.Value.Height / 2;
            Camera.Value.X -= (int)(Camera.Value.X - ((playerA_X + playerB_X) / 2)) / 20;
            Camera.Value.Y -= (int)(Camera.Value.Y - ((playerA_Y + playerB_Y) / 2)) / 20;
            base.Update(gameTime);
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.DarkSlateGray);
            GraphicsDevice.Textures[0] = null;
            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.FrontToBack);
            GraphicManager.Draw();
            //spriteBatch.Draw(level, new Vector2(), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
