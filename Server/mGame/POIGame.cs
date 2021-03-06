using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Drawing;
namespace mGame {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class POIGame : Game
    {
        private enum KeyState { Idle, On, Off }
        GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        private static GameState state;
        private static Game currentGame;
        public static Game CurrentGame { get { return currentGame; } }
        public static GameState CurrentState
        {
            get { return state; }
        }
        public static void SetState(GameState gameState)
        {
            lock(state)
            state.Out();
            state = gameState;
            lock(state)
            {
                while(!state.Initialized)
                {
                    state.Clear();
                    state.Init();
                }
            }
        }
        //public static UInt32[] LevelData;
        public static bool GetKeyPressed(Keys key)
        {
            return keyPressed[key] == KeyState.On;
        }
        public static void MapKeyPress(Func<bool> condition, Keys keyMap)
        {
            if(condition())
            {
                if(keyMapsPressed[keyMap] == KeyState.Idle)
                {
                    keyMapsPressed[keyMap] = KeyState.On;
                    keyPressed[keyMap] = KeyState.On;
                }
                else if (keyMapsPressed[keyMap] == KeyState.On)
                {
                    keyMapsPressed[keyMap] = KeyState.Off;
                    keyPressed[keyMap] = KeyState.Off;
                }
            }
            else
            {
                keyMapsPressed[keyMap] = KeyState.Idle;
                //keyPressed[keyMap] = KeyState.Idle;
            }
        }
        private static Dictionary<Keys, KeyState> keyPressed;
        private static Dictionary<Keys, KeyState> keyMapsPressed;
        //Will define a lot of the draw behavior...
        public const int GameWidth = 432;
        public const int GameHeight = 336;
        public static double DeltaTime {
            get { return deltaTime; }
        }
        private static double deltaTime;
        public POIGame(GameState gameState) {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GameWidth;
            graphics.PreferredBackBufferHeight = GameHeight;
            state = gameState;
            keyPressed = new Dictionary<Keys, KeyState>();
            keyMapsPressed = new Dictionary<Keys, KeyState>();
            currentGame = this;
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
            Assets.cameraEffect = Content.Load<Texture2D>("Content/CameraEffect");
            Assets.playerSprite = Content.Load<Texture2D>("Content/Player");
            Assets.player2Sprite = Content.Load<Texture2D>("Content/Player2");
            Assets.mapTiles = Content.Load<Texture2D>("Content/tileSet");
            Assets.randomBot = Content.Load<Texture2D>("Content/Enemy1");
            Assets.playerBullet = Content.Load<Texture2D>("Content/playerBullet");
            Assets.enemyBullet = Content.Load<Texture2D>("Content/enemyBullet");
            Assets.bulletExplode = Content.Load<Texture2D>("Content/bulletExplode");
            Assets.hud = Content.Load<Texture2D>("Content/hud");
            //Assets.enemyExplode = Content.Load<Texture2D>("Content/enemyExplode");
            Assets.retroFont = Content.Load<SpriteFont>("Content/RetroFont");
            //sounds
            Assets.bulletSound = Content.Load<SoundEffect>("Content/bullet");
            Assets.bulletHitSound = Content.Load<SoundEffect>("Content/hit");
            Assets.explosionSound = Content.Load<SoundEffect>("Content/explosion");
            Assets.stepSound = Content.Load<SoundEffect>("Content/step");
            Assets.LevelSong = Content.Load<Song>("Content/3DGalax");
            //lock(this)
                state.Init();
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
            //lock(this)
                CurrentState.Update();
            base.Update(gameTime);
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);
            GraphicsDevice.Textures[0] = null;
            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp);
            CurrentState.Draw();
            //spriteBatch.Draw(level, new Vector2(), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        protected override void EndRun()
        {
            if (state != null)
                state.Out();
            MediaPlayer.Stop();
            currentGame = null;
            base.EndRun();
        }
    }
}
