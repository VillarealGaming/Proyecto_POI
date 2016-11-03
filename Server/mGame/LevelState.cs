using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace mGame
{
    public class LevelState : GameState
    {
        public const int LevelWidth = 500, LevelHeight = 500;
        private GraphicInstance cameraEffect;
        private GraphicInstance hud;
        protected Player[] players;
        private Player playerA, playerB;
        protected Dictionary<int, RandomBot> randomBots;
        private TileMap tiles;
        private UInt32[] levelData;
        Rectangle levelDimensions;
        private TextSprite text;
        //Texture2D level;
        public int playerNumber;
        private RoomGenerator roomGenerator;
        protected int enemiesKill;
        private TextSprite player1NameSprite;
        private TextSprite player2NameSprite;
        private string player1Name;
        private string player2Name;
        public UInt32[] LevelData
        {
            get { return levelData; }
        }
        public void SetLevelData(UInt32[] data)
        {
            levelData = data;
        }
        public LevelState(int playerNumber, string player1name, string player2name)
        {
            this.playerNumber = playerNumber - 1;
            int indexOfplayer1 = player1name.IndexOf(" ");
            int indexOfplayer2 = player2name.IndexOf(" ");
            player1Name = indexOfplayer1 > 0 ? player1name.Substring(0, indexOfplayer1) : player1name;
            player2Name = indexOfplayer2 > 0 ? player2name.Substring(0, indexOfplayer2) : player2name;
            randomBots = new Dictionary<int, RandomBot>();
            levelDimensions = new Rectangle(0, 0, 500, 500);
            //level = new Texture2D(GraphicsDevice, levelDimensions.Width, levelDimensions.Height);
            //levelData = new RoomGenerator(500, 500).Generate(6, 14, 4, 10, 2);//6,24, 2, 4 //6, 24, 4, 10
            //level.SetData(LevelData);
        }
        public override void Init()
        {
            lock(this)
            {
                AddCollisionGroup("player");
                AddCollisionGroup("playerBullet");
                AddCollisionGroup("enemyBullet");
                AddCollisionGroup("randomBot");
                AddCollisionListener("playerBullet", "randomBot");
                AddCollisionListener("enemyBullet", "player");
                //tests only
                //if (playerNumber == 0)
                //{
                //    GenerateLevelData();
                //    GenerateRandomBot();
                //}
                players = new Player[2];
                players[this.playerNumber] = new Player(this.playerNumber == 0 ? Assets.playerSprite : Assets.player2Sprite, Keys.Right, Keys.Left, Keys.Up, Keys.Down, Keys.Z);
                players[this.playerNumber == 0 ? 1 : 0] = new Player(this.playerNumber == 0 ? Assets.player2Sprite : Assets.playerSprite, Keys.Escape, Keys.Escape, Keys.Escape, Keys.Escape, Keys.Escape);
                //For tests only 
                //players[this.playerNumber == 0 ? 1 : 0] = new Player(this.playerNumber == 0 ? Assets.player2Sprite : Assets.playerSprite, Keys.D, Keys.A, Keys.W, Keys.S, Keys.LeftShift);
                playerA = players[0];
                playerB = players[1];
                playerB.SetTile(251, 250);
                //tiles.Generate();
                text = new TextSprite(Assets.retroFont, new Vector2(4, 4));
                text.text = "Enemigos abatidos: " + enemiesKill.ToString();
                cameraEffect = new GraphicInstance(Assets.cameraEffect, new Position(), true);
                cameraEffect.layerDepth = 0.9f;
                hud = new GraphicInstance(Assets.hud, new Position(), true);
                hud.scale = new Vector2(POIGame.GameWidth, 16);
                hud.layerDepth = 0.99f;
                player1NameSprite = new TextSprite(Assets.retroFont, playerA.Position.Value, false);
                player2NameSprite = new TextSprite(Assets.retroFont, playerB.Position.Value, false);
                player1NameSprite.text = player1Name;
                player2NameSprite.text = player2Name;
                player1NameSprite.layerDepth = 1.0f;
                player2NameSprite.layerDepth = 1.0f;
                AddGraphic(hud);
                AddGraphic(cameraEffect);
                AddInstance(playerA);
                AddInstance(playerB);
                AddGraphic(text);
                AddGraphic(player1NameSprite);
                AddGraphic(player2NameSprite);
                tiles = new TileMap(Assets.mapTiles);
                tiles.Generate();
                AddGraphic(tiles);
                foreach (var randomBot in randomBots)
                    AddInstance(randomBot.Value);
                MediaPlayer.Volume = 0.4f;
                MediaPlayer.Play(Assets.LevelSong);
                MediaPlayer.IsRepeating = true;
                base.Init();
                //FullyInitialized();
            }
        }
        public void GenerateLevelData()
        {
            roomGenerator = new RoomGenerator(500, 500);
            levelData = roomGenerator.Generate(6, 14, 4, 10, 2);
        }
        //generamos un nuevo robot
        public void GenerateRandomBot()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            foreach (var room in roomGenerator.GetGeneratedRooms())
            {
                for(int i = 0; i < rand.Next(1,5); i++)
                {
                    RandomBot newRandomBot = new RandomBot(rand.Next(room.Left, room.Right), rand.Next(room.Top, room.Bottom));
                    randomBots.Add(newRandomBot.id, newRandomBot);
                }
            }
        }
        //Creamos un robot con id fija remota
        public void GenerateRandomBot(Tuple<int, float[]>[] data)
        {
            foreach(var d in data)
            {
                RandomBot newRandomBot = new RandomBot((int)d.Item2[0], (int)d.Item2[1], d.Item2[2], d.Item2[3]);
                //AddInstance(newRandomBot);
                randomBots.Add(d.Item1, newRandomBot);
            }
        }
        public Tuple<int, float[]>[] GetRandomBotData()
        {
            List<Tuple<int, float[]>> randomBotData = new List<Tuple<int, float[]>>();
            foreach(var randomBot in randomBots)
            {
                float[] data = new float[4];
                data[0] = randomBot.Value.GridPosition.X;
                data[1] = randomBot.Value.GridPosition.Y;
                data[2] = randomBot.Value.BaseSpeed;
                data[3] = randomBot.Value.MoveEase;
                randomBotData.Add(new Tuple<int, float[]>(randomBot.Key, data));
            }
            return randomBotData.ToArray();
        }
        public override void Update()
        {
            try
            {
                int playerA_X = playerA.Position.Value.ToPoint().X - camera.Value.Width / 2;
                int playerB_X = playerB.Position.Value.ToPoint().X - camera.Value.Width / 2;
                int playerA_Y = playerA.Position.Value.ToPoint().Y - camera.Value.Height / 2;
                int playerB_Y = playerB.Position.Value.ToPoint().Y - camera.Value.Height / 2;
                camera.Value.X -= (int)(camera.Value.X - ((playerA_X + playerB_X) / 2)) / 20;
                camera.Value.Y -= (int)(camera.Value.Y - ((playerA_Y + playerB_Y) / 2)) / 20;
                playerA.OtherPlayerGrid = playerB.GridPosition;
                playerB.OtherPlayerGrid = playerA.GridPosition;
                player1NameSprite.position = playerA.Position.Value - new Vector2(0.0f, 16.0f);
                player2NameSprite.position = playerB.Position.Value - new Vector2(0.0f, 16.0f);
            }
            catch{ }
            base.Update();
            //text.text = camera.Value.Location.X + ", " + camera.Value.Location.Y;
        }
        public virtual void PlayerInput(Direction direction, int gridX, int gridY) { }
        public virtual void PlayerShot() { }
        public virtual void EnemyKilled()
        {
            enemiesKill++;
            text.text = "Enemigos abatidos: " + enemiesKill.ToString();
        }
        public virtual void RandomBotInput(Direction direction, int robotID, int gridX, int gridY) { }
        //public virtual void RandomBotAllign(int robotID, int gridX, int gridY) { }
    }
}
