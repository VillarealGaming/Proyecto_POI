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
        //Will define a lot of the draw behavior...
        public static Rectangle Camera;
        private const int GameWidth = 432;
        private const int GameHeight = 336;
        //level generation test
        Rectangle levelDimensions;
        Texture2D level;
        public static double DeltaTime {
            get { return deltaTime; }
        }
        private static double deltaTime;
        private Player player;
        public POIGame() {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GameWidth;
            graphics.PreferredBackBufferHeight = GameHeight;
            GraphicManager = new Graphics();
            AnimationManager = new UpdateableContainer();
            InstanceManager = new UpdateableContainer();
            MoveableManager = new UpdateableContainer();
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
            level.SetData(new RoomGenerator(500, 500).Generate(6, 24, 2, 4, 2));//6, 24, 4, 10
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
            player = new Player();
            InstanceManager.AddInstance(player);
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
            deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
            InstanceManager.Update();
            AnimationManager.Update();
            MoveableManager.Update();
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
            spriteBatch.Begin();
            GraphicManager.Draw();
            spriteBatch.Draw(level, new Vector2(), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
