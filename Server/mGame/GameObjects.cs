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
    public class Player : Updateable
    {
        GraphicInstance sprite;
        Animation animation;
        Position position;
        MoveableTile moveComponent;
        public override void Added() {
            position = new Position();
            sprite = new GraphicInstance(Assets.playerSprite, position);
            animation = new Animation(sprite, 24, 24);
            animation.AddAnimation("walk", new int[] {0,1});
            animation.AddAnimation("stop", new int[] { 0 });
            animation.AddAnimation("move", new int[] { 1 });
            animation.SetAnimation("stop");
            moveComponent = new MoveableTile(position, 250, 250);
            moveComponent.MoveEase = 7.0f;
            POIGame.GraphicManager.AddGraphic(sprite);
            POIGame.AnimationManager.AddInstance(animation);
            POIGame.MoveableManager.AddInstance(moveComponent);
            base.Added();
        }
        internal override void Update() {
            if(moveComponent.Velocity > 1.0f) {
                animation.SetAnimation("move");
            }
            else {
                animation.SetAnimation("stop");
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Right)) {
                sprite.effects = SpriteEffects.FlipHorizontally;
                moveComponent.MoveRight();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) {
                sprite.effects = SpriteEffects.None;
                moveComponent.MoveLeft();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up)) {
                moveComponent.MoveUp();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down)) {
                moveComponent.MoveDown();
            }
            base.Update();
            POIGame.Camera.Value.Location = position.Value.ToPoint();
            POIGame.Camera.Value.X -= POIGame.Camera.Value.Width / 2;
            POIGame.Camera.Value.Y -= POIGame.Camera.Value.Height / 2;
        }
        public override void Removed() {
            POIGame.GraphicManager.Remove(sprite);
            POIGame.AnimationManager.Remove(animation);
            base.Removed();
        }
    }
    public abstract class Behavior : Updateable
    {
        protected MoveableTile movable;
        public Behavior(MoveableTile movableComponent) { this.movable = movableComponent; }
    }
    public class MoveableTile : Updateable
    {
        public event EventHandler OnGoalReached;
        private Position positionReference;
        private Vector2 position;
        private float ease;
        private const int TileSize = 24;
        private Vector2 goal;
        private Vector2 previousGoal;
        private Vector2 speed;
        public float MoveEase { get; set; }//default 5.0, highter means slower
        public float Velocity {
            get { return speed.Length(); }
        }
        private bool goalReached;
        public MoveableTile(Position position, int tileX, int tileY) {
            this.positionReference = position;
            this.position = new Vector2(tileX * TileSize, tileY * TileSize);
            position.Value = this.position;
            goal = this.position;
            previousGoal = goal;
            goalReached = true;
            MoveEase = 5.0f;
        }
        internal override void Update() {
            speed.X = (goal.X - position.X) / MoveEase;
            if (positionReference.ClampValue.X != goal.X) {
                speed.X += Math.Sign(speed.X);
            }
            else {
                speed.X = 0;
            }
            speed.Y = (goal.Y - position.Y) / MoveEase;
            if (positionReference.ClampValue.Y != goal.Y) {
                speed.Y += Math.Sign(speed.Y);
            }
            else {
                speed.Y = 0;
            }
            position += speed;
            positionReference.Value = this.position;
            if (positionReference.ClampValue != goal) {
            }
            else {
                goalReached = true;
            }
            base.Update();
        }
        public void MoveRight() {
            if((goalReached || speed.X < 0) && speed.Y == 0) {
                goal.X += TileSize;
                goalReached = false;
            }
        }
        public void MoveLeft() {
            if ((goalReached || speed.X > 0) && speed.Y == 0) {
                goal.X -= TileSize;
                goalReached = false;
            }
        }
        public void MoveUp() {
            if ((goalReached || speed.Y > 0) && speed.X == 0) {
                goal.Y -= TileSize;
                goalReached = false;
            }
        }
        public void MoveDown() {
            if ((goalReached || speed.Y < 0) && speed.X == 0) {
                goal.Y += TileSize;
                goalReached = false;
            }
        }
    }
}
