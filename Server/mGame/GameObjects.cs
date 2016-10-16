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
    public class Player : MoveableTile
    {
        private GraphicInstance sprite;
        private Animation animation;
        private enum NextStepDirection { Left, Right, Up, Down, None }
        private NextStepDirection nextStep;
        //FOR TESTS ONLY!!!
        private Keys right, left, up, down;
        public Player(Keys right, Keys left, Keys up, Keys down, int tileX = 250, int tileY = 250) : base(tileX, tileY)
        {
            this.right = right;
            this.left = left;
            this.up = up;
            this.down = down;
        }
        public override void Added() {
            sprite = new GraphicInstance(Assets.playerSprite, position);
            animation = new Animation(sprite, 24, 24);
            animation.AddAnimation("walk", new int[] {0,1});
            animation.AddAnimation("stop", new int[] { 0 });
            animation.AddAnimation("move", new int[] { 1 });
            animation.SetAnimation("stop");
            POIGame.GraphicManager.AddGraphic(sprite);
            POIGame.AnimationManager.AddInstance(animation);
            MoveEase = 7.0f;
            nextStep = NextStepDirection.None;
            POIGame.Camera.Value.Location = position.Value.ToPoint();
            base.Added();
        }
        internal override void Update() {
            if(Velocity > 1.0f) {
                animation.SetAnimation("move");
            }
            else {
                animation.SetAnimation("stop");
            }
            if (!isGoalReached)
            {
                if (POIGame.GetKeyPressed(right))
                    nextStep = Speed.X < 0 ? NextStepDirection.None : NextStepDirection.Right;
                else if (POIGame.GetKeyPressed(left))
                    nextStep = Speed.X > 0? NextStepDirection.None : NextStepDirection.Left;
                else if (POIGame.GetKeyPressed(up))
                    nextStep = Speed.Y > 0 ? NextStepDirection.None : NextStepDirection.Up;
                else if (POIGame.GetKeyPressed(down))
                    nextStep = Speed.Y < 0 ? NextStepDirection.None : NextStepDirection.Down;

                if (POIGame.GetKeyPressed(right))
                {
                    sprite.effects = SpriteEffects.FlipHorizontally;
                    MoveRight();
                }
                else if (POIGame.GetKeyPressed(left))
                {
                    sprite.effects = SpriteEffects.None;
                    MoveLeft();
                }
                else if (POIGame.GetKeyPressed(up))
                {
                    MoveUp();
                }
                else if (POIGame.GetKeyPressed(down))
                {
                    MoveDown();
                }
            }
            else
            {
                if (POIGame.GetKeyPressed(right) || nextStep == NextStepDirection.Right)
                {
                    sprite.effects = SpriteEffects.FlipHorizontally;
                    MoveRight();
                }
                else if (POIGame.GetKeyPressed(left) || nextStep == NextStepDirection.Left)
                {
                    sprite.effects = SpriteEffects.None;
                    MoveLeft();
                }
                else if (POIGame.GetKeyPressed(up) || nextStep == NextStepDirection.Up)
                {
                    MoveUp();
                }
                else if (POIGame.GetKeyPressed(down) || nextStep == NextStepDirection.Down)
                {
                    MoveDown();
                }
                nextStep = NextStepDirection.None;
            }
            base.Update();
            //POIGame.Camera.Value.Location = position.Value.ToPoint();
            //POIGame.Camera.Value.X -= POIGame.Camera.Value.Width / 2;
            //POIGame.Camera.Value.Y -= POIGame.Camera.Value.Height / 2;
        }
        public override void Removed() {
            POIGame.GraphicManager.Remove(sprite);
            POIGame.AnimationManager.Remove(animation);
            base.Removed();
        }
    }

    public class MoveableTile : Updateable
    {
        //private Position positionReference;
        protected Position position;
        private const int TileSize = 24;
        private Vector2
            gridPosition,
            goal, 
            previousGoal, 
            speed;
        //default 5.0, highter means slower
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
        public MoveableTile(int tileX, int tileY) {
            position = new Position();
            position.Value = new Vector2(tileX * TileSize, tileY * TileSize);
            goal = position.Value;
            previousGoal = goal;
            goalReached = true;
            MoveEase = 5.0f;
            gridPosition = this.position.Value / 24;
        }
        internal override void Update() {
            speed.X = (goal.X - position.Value.X) / MoveEase;
            if (position.ClampValue.X != goal.X) {
                speed.X += Math.Sign(speed.X);
            }
            else {
                speed.X = 0;
            }
            speed.Y = (goal.Y - position.Value.Y) / MoveEase;
            if (position.ClampValue.Y != goal.Y) {
                speed.Y += Math.Sign(speed.Y);
            }
            else {
                speed.Y = 0;
            }
            position.Value += speed;
            if (position.ClampValue == goal && !goalReached)
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
            int index = ((int)(gridPosition.Y + y) * POIGame.LevelHeight) + (int)gridPosition.X + x;
            return POIGame.LevelData[index] != 0;
        }
        protected virtual void GoalReached() {
            speed = new Vector2();
        }
    }
}
