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
        }
        public override void Init()
        {
            levelDimensions = new Rectangle(0, 0, 500, 500);
            //level = new Texture2D(GraphicsDevice, levelDimensions.Width, levelDimensions.Height);
            //levelData = new RoomGenerator(500, 500).Generate(6, 14, 4, 10, 2);//6,24, 2, 4 //6, 24, 4, 10
            //level.SetData(LevelData);
            players = new Player[2];
            players[playerNumber == 1 ? 0 : 1] = new Player(Keys.Right, Keys.Left, Keys.Up, Keys.Down);
            players[playerNumber == 1 ? 1 : 0] = new Player(Keys.Escape, Keys.Escape, Keys.Escape, Keys.Escape);
            playerA = players[0];
            playerB = players[1];
            playerB.SetTile(251, 250);
            //tiles.Generate();
            text = new TextSprite(Assets.retroFont, new Vector2(24, 24));
            AddInstance(playerA);
            AddInstance(playerB);
            AddInstance(new RandomBot(254, 254));
            AddGraphic(text);
            tiles = new TileMap(Assets.mapTiles);
            tiles.Generate();
            AddGraphic(tiles);
            base.Init();
        }
        public void GenerateLevelData()
        {
            levelData = new RoomGenerator(500, 500).Generate(6, 14, 4, 10, 2);
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
        public virtual void PlayerInput(Direction direction)
        {

        }
    }
}
