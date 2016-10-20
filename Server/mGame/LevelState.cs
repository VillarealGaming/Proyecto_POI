using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace mGame
{
    public class LevelState : GameState
    {
        public const int LevelWidth = 500, LevelHeight = 500;
        protected Player[] players;
        private Player playerA, playerB;
        protected Dictionary<int, RandomBot> randomBots;
        private TileMap tiles;
        //private UpdateableContainer moveableContainer;
        private UInt32[] levelData;
        //level generation test
        Rectangle levelDimensions;
        private TextSprite text;
        //Texture2D level;
        public int playerNumber;
        public UInt32[] LevelData
        {
            get { return levelData; }
        }
        public void SetLevelData(UInt32[] data)
        {
            levelData = data;
        }
        public LevelState(int playerNumber = 0)
        {
            this.playerNumber = playerNumber - 1;
            randomBots = new Dictionary<int, RandomBot>();
            levelDimensions = new Rectangle(0, 0, 500, 500);
            //level = new Texture2D(GraphicsDevice, levelDimensions.Width, levelDimensions.Height);
            //levelData = new RoomGenerator(500, 500).Generate(6, 14, 4, 10, 2);//6,24, 2, 4 //6, 24, 4, 10
            //level.SetData(LevelData);
            players = new Player[2];
            players[this.playerNumber] = new Player(Keys.Right, Keys.Left, Keys.Up, Keys.Down);
            players[this.playerNumber == 0 ? 1 : 0] = new Player(Keys.Escape, Keys.Escape, Keys.Escape, Keys.Escape);
            playerA = players[0];
            playerB = players[1];
            playerB.SetTile(251, 250);
            //tiles.Generate();
            text = new TextSprite(Assets.retroFont, new Vector2(24, 24));
        }
        public override void Init()
        {
            lock(this)
            {
                AddInstance(playerA);
                AddInstance(playerB);
                AddGraphic(text);
                tiles = new TileMap(Assets.mapTiles);
                tiles.Generate();
                AddGraphic(tiles);
                foreach (var randomBot in randomBots)
                    AddInstance(randomBot.Value);
                base.Init();
                //FullyInitialized();
            }
        }
        public void GenerateLevelData()
        {
            levelData = new RoomGenerator(500, 500).Generate(6, 14, 4, 10, 2);
        }
        //generamos un nuevo robot
        public void GenerateRandomBot()
        {
            RandomBot newRandomBot = new RandomBot(255, 255);
            //AddInstance(newRandomBot);
            randomBots.Add(newRandomBot.id, newRandomBot);
        }
        //Creamos un robot con id fija remota
        public void GenerateRandomBot(Tuple<int, int[]>[] data)
        {
            foreach(var d in data)
            {
                RandomBot newRandomBot = new RandomBot((int)d.Item2[0], (int)d.Item2[1]);
                //AddInstance(newRandomBot);
                randomBots.Add(d.Item1, newRandomBot);
            }
        }
        public Tuple<int, int[]>[] GetRandomBotData()
        {
            List<Tuple<int, int[]>> randomBotData = new List<Tuple<int, int[]>>();
            foreach(var randomBot in randomBots)
            {
                int[] position = new int[2];
                position[0] = (int)randomBot.Value.GridPosition.X;
                position[1] = (int)randomBot.Value.GridPosition.Y;
                randomBotData.Add(new Tuple<int, int[]>(randomBot.Key, position));
            }
            return randomBotData.ToArray();
        }
        public override void Update()
        {
            int playerA_X = playerA.Position.Value.ToPoint().X - camera.Value.Width / 2;
            int playerB_X = playerB.Position.Value.ToPoint().X - camera.Value.Width / 2;
            int playerA_Y = playerA.Position.Value.ToPoint().Y - camera.Value.Height / 2;
            int playerB_Y = playerB.Position.Value.ToPoint().Y - camera.Value.Height / 2;
            camera.Value.X -= (int)(camera.Value.X - ((playerA_X + playerB_X) / 2)) / 20;
            camera.Value.Y -= (int)(camera.Value.Y - ((playerA_Y + playerB_Y) / 2)) / 20;
            playerA.OtherPlayerGrid = playerB.GridPosition;
            playerB.OtherPlayerGrid = playerA.GridPosition;
            base.Update();
            text.text = camera.Value.Location.X + ", " + camera.Value.Location.Y;
        }
        public virtual void PlayerInput(Direction direction) { }
        public virtual void RandomBotInput(Direction direction, int robotID) { }
        public virtual void RandomBotAllign(int robotID, int gridX, int gridY) { }
    }
}
