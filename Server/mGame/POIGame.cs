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
        public static Random Rand;
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
            Rand = new Random();
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
            level.SetData(new RoomGenerator(500, 500).Generate(6, 24, 4, 10));
            base.Initialize();
        }
        class RoomGenerator
        {
            private enum NodeType { Up, Left, Down, Right }
            private class Room
            {
                private static List<Room> totalRooms = new List<Room>();
                public static int MaxRoomSize { get; set; }
                public static int MinRoomSize { get; set; }
                public static int MaxDisplacement { get; set; }
                public static int MinDisplacement { get; set; }
                public static List<Room> TotalRooms { get { return totalRooms; } }
                private bool[] nodes;
                private Rectangle bounds;
                public Rectangle Bounds { get { return bounds; } }
                public Room(Rectangle rectangle) {
                    bounds = rectangle;
                    nodes = new bool[4];
                    totalRooms.Add(this);
                }
                //From which node came
                protected Room(Room parent, NodeType originNode) {
                    nodes = new bool[4];
                    nodes[(int)originNode] = true;
                    bounds = new Rectangle(0, 0, Rand.Next(MinRoomSize, MaxRoomSize), Rand.Next(MinRoomSize, MaxRoomSize));
                    bounds.Location = parent.bounds.Location;
                    int displacementLenght = Rand.Next(MinDisplacement, MaxDisplacement);
                    Point newLocation = bounds.Location;
                    //Set position displacement
                    switch (originNode) {
                        case NodeType.Up: {
                                newLocation.Y -= bounds.Height + displacementLenght;
                            }
                            break;
                        case NodeType.Left: {
                                newLocation.X -= bounds.Width + displacementLenght;
                            }
                            break;
                        case NodeType.Down: {
                                newLocation.Y += parent.bounds.Height + displacementLenght;
                            }
                            break;
                        case NodeType.Right: {
                                newLocation.X += parent.bounds.Width + displacementLenght;
                            }
                            break;
                    }
                    bounds.Location = newLocation;
                }
                private bool CollideRooms() {
                    Rectangle fixedBounds = bounds;
                    fixedBounds.X--;
                    fixedBounds.Y--;
                    fixedBounds.Width+=2;
                    fixedBounds.Height+=2;
                    foreach (var room in totalRooms) {
                        if (fixedBounds.Intersects(room.Bounds) && bounds != room.bounds)
                            return true;
                    }
                    return false;
                }
                public List<Room> GenerateChilds() {
                    List<Room> childs = new List<Room>();
                    int retries = 0;
                    //minimun one new child
                    int minChildIndex = Rand.Next(4);
                    while (nodes[minChildIndex]) { minChildIndex = Rand.Next(4); }
                    nodes[minChildIndex] = true;
                    //adding child...
                    Room minChild;
                    do { minChild = new Room(this, (NodeType)minChildIndex); retries++; }
                    while (minChild.CollideRooms() && retries < 100);
                    if(retries < 10)
                        childs.Add(minChild);
                    //
                    for (int i = 0; i < nodes.Length; i++) {
                        //If there's no node
                        if (!nodes[i]) {
                            //Random creation chance
                            if (Rand.Next(2) == 1) {
                                Room newRoom;
                                retries = 0;
                                do { newRoom = new Room(this, (NodeType)i); retries++; }
                                while (newRoom.CollideRooms() && retries < 100);
                                if(retries < 10)
                                    childs.Add(newRoom);
                            }
                        }
                    }
                    totalRooms.AddRange(childs);
                    return childs;
                }
                //Fills all neighbors
                public List<Room> GenerateAll() {
                    List<Room> childs = new List<Room>();
                    for (int i = 0; i < nodes.Length; i++) {
                        //If there's no node
                        if (!nodes[i]) {
                            Room newRoom = new Room(this, (NodeType)i);
                            childs.Add(newRoom);
                            
                        }
                    }
                    totalRooms.AddRange(childs);
                    return childs;
                }
                public static void Reset() {
                    totalRooms = new List<Room>();
                }
            }
            public static GraphicsDevice graphicsDevice { get; set; }
            public int TotalWidth { get; set; }
            public int TotalHeight { get; set; }
            public RoomGenerator(int totalWidth, int totalHeight) {
                TotalWidth = totalWidth;
                TotalHeight = totalHeight;
            }
            private T[] ArrayFill<T>(T value, int size) {
                T[] array = new T[size];
                for (int i = 0; i < array.Length; i++) { array[i] = value; }
                return array;
            }
            public UInt32[] Generate(int minRoomSize, int maxRoomSize, int minRoomSeparation, int maxRoomSeparation) {
                //Uses texture2d to fill the data...
                Texture2D levelData = new Texture2D(graphicsDevice, TotalWidth, TotalHeight);
                //UInt32[] levelData = new UInt32[TotalWidth * TotalHeight];
                //init level generation
                Room.MinRoomSize = minRoomSize;
                Room.MaxRoomSize = maxRoomSize;
                Room.MinDisplacement = minRoomSeparation;
                Room.MaxDisplacement = maxRoomSeparation;
                Room.Reset();
                //Random rand = new Random();
                //List<Rectangle> rooms = new List<Rectangle>();
                List<Room> lastGeneratedChilds = new List<Room>();
                Room originRoom = new Room(new Rectangle(TotalWidth / 2, TotalHeight / 2, 20, 20));
                //rooms.Add(originRoom.Bounds);
                lastGeneratedChilds.AddRange(originRoom.GenerateAll());
                //foreach (Room room in lastGeneratedChilds) {
                //    rooms.Add(room.Bounds);
                //}
                for (int i = 0; i < 20; i++) {
                    List<Room> newGeneratedChilds = new List<Room>();
                    foreach (Room room in lastGeneratedChilds) {
                        newGeneratedChilds.AddRange(room.GenerateChilds());
                    }
                    lastGeneratedChilds = newGeneratedChilds;
                    //Add rectangle to list of rooms...
                    //foreach (Room room in lastGeneratedChilds) {
                    //    rooms.Add(room.Bounds);
                    //}
                }
                //Rectangle newRoom = new Rectangle(0, 0, rand.Next(minRoomSize, maxRoomSize), rand.Next(minRoomSize, maxRoomSize));
                //Point newRoomCenter = newRoom.Center;
                //newRoom.Location = (levelDimensions.Size.ToVector2() / 2).ToPoint() - newRoomCenter;
                //rooms.Add(newRoom);
                foreach (var room in Room.TotalRooms) {
                    levelData.SetData(0, room.Bounds, ArrayFill(0xffffffff, TotalWidth * TotalHeight), 0, TotalWidth * TotalHeight);
                }
                UInt32[] output = new UInt32[TotalWidth * TotalHeight];
                levelData.GetData(output, 0, output.Length);
                return output;
            }
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
            GraphicsDevice.Clear(Color.CornflowerBlue);
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
