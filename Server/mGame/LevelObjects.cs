using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//Component composing test
namespace mGame
{
    public class MoveableTile : Updateable
    {
        //private Position positionReference;
        protected GraphicInstance sprite;
        protected new LevelState state;
        protected Position position;
        private const int TileSize = 24;
        private float baseSpeed;
        protected float BaseSpeed {
            get { return baseSpeed; }
            set { baseSpeed = Math.Abs(value) > 1.0f ? 1.0f: Math.Abs(value); }
        }
        private Vector2
            gridPosition,
            goal, 
            previousGoal, 
            speed;
        //default 5.0, highter means slower
        public Vector2 GridPosition{
            get { return gridPosition; }
        }
        public Position Position{
            get { return position; }
        }
        public Vector2 Speed {
            get { return speed; }
        }
        public float MoveEase { get; set; }
        public float Velocity {
            get { return speed.Length(); }
        }
        protected bool isGoalReached{
            get { return goalReached; }
        }
        private bool goalReached;
        private Texture2D texture;
        public MoveableTile(Texture2D texture, int tileX, int tileY) {
            state = (LevelState)POIGame.CurrentState;
            position = new Position();
            position.Value = new Vector2(tileX * TileSize, tileY * TileSize);
            this.texture = texture;
            goal = position.Value;
            previousGoal = goal;
            goalReached = true;
            MoveEase = 5.0f;
            baseSpeed = 1.0f;
            gridPosition = this.position.Value / 24;
        }
        public override void Added()
        {
            sprite = new GraphicInstance(texture, position);
            state.AddGraphic(sprite);
            base.Added();
        }
        internal override void Update() {
            speed.X = (goal.X - position.Value.X) / MoveEase;
            if (position.ClampValue.X != goal.X) {
                speed.X += Math.Sign(speed.X) * baseSpeed;
            }
            else {
                speed.X = 0;
            }
            speed.Y = (goal.Y - position.Value.Y) / MoveEase;
            if (position.ClampValue.Y != goal.Y) {
                speed.Y += Math.Sign(speed.Y) * baseSpeed;
            }
            else {
                speed.Y = 0;
            }
            position.Value += speed;
            if (position.ClampValue == goal)
            {
                goalReached = true;
                GoalReached();
            }
            else {
            }
            base.Update();
        }
        protected void MoveRight() {
            if((goalReached || speed.X < 0) && speed.Y == 0 && CheckEmpty(1, 0)) {
                goal.X += TileSize;
                goalReached = false;
                gridPosition.X++;
            }
        }
        protected void MoveLeft() {
            if ((goalReached || speed.X > 0) && speed.Y == 0 && CheckEmpty(-1, 0)) {
                goal.X -= TileSize;
                goalReached = false;
                gridPosition.X--;
            }
        }
        protected void MoveUp() {
            if ((goalReached || speed.Y > 0) && speed.X == 0 && CheckEmpty(0, -1)) {
                goal.Y -= TileSize;
                goalReached = false;
                gridPosition.Y--;
            }
        }
        protected void MoveDown() {
            if ((goalReached || speed.Y < 0) && speed.X == 0 && CheckEmpty(0, 1)) {
                goal.Y += TileSize;
                goalReached = false;
                gridPosition.Y++;
            }
        }
        private bool CheckEmpty(int x, int y)
        {
            int index = ((int)(gridPosition.Y + y) * LevelState.LevelHeight) + (int)gridPosition.X + x;
            return state.LevelData[index] != 0;
        }
        protected virtual void GoalReached() {
            speed = new Vector2();
        }
        public override void Removed()
        {
            state.RemoveGraphic(sprite);
            base.Removed();
        }
    }
    //Custom Tilemap component
    public class TileMap : Drawable
    {
        protected new LevelState state;
        struct TileInfo
        {
            public Vector2 tilePosition;
            public Point textureCoord;
        }
        private TileInfo[] tiles;
        private GraphicInstance tile;
        private int tileWidth;
        private int tileHeight;
        //Texture2D texture;
        public TileMap(Texture2D texture, int tileWidth = 24, int tileHeight = 24)
        {
            state = (LevelState)POIGame.CurrentState;
            //tiles = new TileInfo[mapWidth * mapHeight];
            tile = new GraphicInstance(texture, new Position());
            tile.frame.rectangle = new Rectangle(0, 0, tileWidth, tileHeight);
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            //test locals
            const int mapWidth = LevelState.LevelWidth;
            const int mapHeight = LevelState.LevelHeight;
            tile.layerDepth = 0.001f;
            tiles = new TileInfo[state.LevelData.Length];
            int index = 501;// = i * mapWidth + j;
            for (int i = 1; i < mapHeight - 1; i++)
            {
                for (int j = 1; j < mapWidth - 1; j++)
                {
                    tiles[i * mapWidth + j].tilePosition = new Vector2(j * tileWidth, i * tileHeight);
                    if (state.LevelData[index] == 0)
                    {
                        bool isWall = false;
                        if (
                            //Side check
                            state.LevelData[index + 1] != 0 ||
                            state.LevelData[index - 1] != 0 ||
                            state.LevelData[index - mapWidth] != 0 ||
                            state.LevelData[index + mapWidth] != 0 ||
                            //Corner check
                            state.LevelData[index + mapWidth + 1] != 0 ||
                            state.LevelData[index + mapWidth - 1] != 0 ||
                            state.LevelData[index - mapWidth + 1] != 0 ||
                            state.LevelData[index - mapWidth - 1] != 0
                            )
                        {
                            isWall = true;
                        }
                        tiles[index].textureCoord = isWall ? new Point(48, 0) : new Point(24, 0);
                    }
                    else {
                        tiles[index].textureCoord = new Point();
                    }
                    index++;
                }
            }
            //this.texture = texture;
        }
        /// <summary>
        /// Loads the tiles from an array, it just evaluates
        /// if the value is different from 0
        /// </summary>
        public void GenerateFromArray(int mapWidth, int mapHeight)
        {
        }
        internal override void Draw()
        {
            //Just draw current on screen tiles...
            //TODO: Make tile size a global static const
            Point cameraGrid = new Point(state.camera.Value.X / 24, state.camera.Value.Y / 24);
            int index = (cameraGrid.Y) * 500 + (cameraGrid.X);// = i * mapWidth + j;
            for (int i = 0; i < state.camera.Value.Height / 24 + 1; i++)
            {
                for (int j = 0; j < state.camera.Value.Width / 24 + 1; j++)
                {
                    TileInfo tileInfo = tiles[(cameraGrid.Y + i) * 500 + (cameraGrid.X + j)];
                    tile.position.Value = tileInfo.tilePosition;
                    tile.frame.rectangle = new Rectangle(tileInfo.textureCoord, new Point(24, 24));
                    tile.Draw();
                    index++;
                }
                index -= state.camera.Value.Width / 24 + 1;
                index += i * state.camera.Value.Width / 24 + 1;
            }
            base.Draw();
        }
    }
}
