using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
namespace mGame
{
    public enum Direction { Left, Right, Up, Down, None }
    //I believe this is the most hardcoded class from the whole game...
    public class Player : MoveableTile
    {
        private Animation animation;
        private Direction nextStep;
        public Vector2 OtherPlayerGrid { get; set; }
        //FOR TESTS ONLY!!!
        private Keys right, left, up, down;
        public Player(Keys right, Keys left, Keys up, Keys down, int tileX = 250, int tileY = 250) : base(Assets.playerSprite, tileX, tileY)
        {
            this.right = right;
            this.left = left;
            this.up = up;
            this.down = down;
        }
        public override void Added()
        {
            base.Added();
            animation = new Animation(sprite, 24, 24);
            animation.AddAnimation("walk", new int[] { 0, 1 });
            animation.AddAnimation("stop", new int[] { 0 });
            animation.AddAnimation("move", new int[] { 1 });
            animation.SetAnimation("stop");
            state.AddAnimation(animation);
            MoveEase = 7.0f;
            nextStep = Direction.None;
            state.camera.Value.Location = position.Value.ToPoint();
        }
        internal override void Update()
        {
            if (Velocity > 1.0f)
            {
                animation.SetAnimation("move");
            }
            else {
                animation.SetAnimation("stop");
            }
            if (!isGoalReached)
            {
                if (POIGame.GetKeyPressed(right))
                    nextStep = Speed.X < 0 ? Direction.None : Direction.Right;
                else if (POIGame.GetKeyPressed(left))
                    nextStep = Speed.X > 0 ? Direction.None : Direction.Left;
                else if (POIGame.GetKeyPressed(up))
                    nextStep = Speed.Y > 0 ? Direction.None : Direction.Up;
                else if (POIGame.GetKeyPressed(down))
                    nextStep = Speed.Y < 0 ? Direction.None : Direction.Down;

                if (POIGame.GetKeyPressed(right))
                {
                    sprite.effects = SpriteEffects.FlipHorizontally;
                    if (new Vector2(GridPosition.X + 1, GridPosition.Y) != OtherPlayerGrid) MoveRight();
                }
                else if (POIGame.GetKeyPressed(left))
                {
                    sprite.effects = SpriteEffects.None;
                    if (new Vector2(GridPosition.X - 1, GridPosition.Y) != OtherPlayerGrid) MoveLeft();
                }
                else if (POIGame.GetKeyPressed(up))
                {
                    if (new Vector2(GridPosition.X, GridPosition.Y - 1) != OtherPlayerGrid) MoveUp();
                }
                else if (POIGame.GetKeyPressed(down))
                {
                    if (new Vector2(GridPosition.X, GridPosition.Y + 1) != OtherPlayerGrid) MoveDown();
                }
            }
            else
            {
                if (POIGame.GetKeyPressed(right) || nextStep == Direction.Right)
                {
                    sprite.effects = SpriteEffects.FlipHorizontally;
                    if (new Vector2(GridPosition.X + 1, GridPosition.Y) != OtherPlayerGrid) MoveRight();
                }
                else if (POIGame.GetKeyPressed(left) || nextStep == Direction.Left)
                {
                    sprite.effects = SpriteEffects.None;
                    if (new Vector2(GridPosition.X - 1, GridPosition.Y) != OtherPlayerGrid) MoveLeft();
                }
                else if (POIGame.GetKeyPressed(up) || nextStep == Direction.Up)
                {
                    if (new Vector2(GridPosition.X, GridPosition.Y - 1) != OtherPlayerGrid) MoveUp();
                }
                else if (POIGame.GetKeyPressed(down) || nextStep == Direction.Down)
                {
                    if (new Vector2(GridPosition.X, GridPosition.Y + 1) != OtherPlayerGrid) MoveDown();
                }
                nextStep = Direction.None;
            }
            base.Update();
        }
        public void Move(Direction direction)
        {
            if (Velocity > 1.0f)
            {
                animation.SetAnimation("move");
            }
            else {
                animation.SetAnimation("stop");
            }
            if (!isGoalReached)
            {
                if (direction == Direction.Right)
                    nextStep = Speed.X < 0 ? Direction.None : Direction.Right;
                else if (direction == Direction.Left)
                    nextStep = Speed.X > 0 ? Direction.None : Direction.Left;
                else if (direction == Direction.Up)
                    nextStep = Speed.Y > 0 ? Direction.None : Direction.Up;
                else if (direction == Direction.Down)
                    nextStep = Speed.Y < 0 ? Direction.None : Direction.Down;

                if (direction == Direction.Right)
                {
                    sprite.effects = SpriteEffects.FlipHorizontally;
                    if (new Vector2(GridPosition.X + 1, GridPosition.Y) != OtherPlayerGrid) MoveRight();
                }
                else if (direction == Direction.Left)
                {
                    sprite.effects = SpriteEffects.None;
                    if (new Vector2(GridPosition.X - 1, GridPosition.Y) != OtherPlayerGrid) MoveLeft();
                }
                else if (direction == Direction.Up)
                {
                    if (new Vector2(GridPosition.X, GridPosition.Y - 1) != OtherPlayerGrid) MoveUp();
                }
                else if (direction == Direction.Down)
                {
                    if (new Vector2(GridPosition.X, GridPosition.Y + 1) != OtherPlayerGrid) MoveDown();
                }
            }
            else
            {
                if (direction == Direction.Right || nextStep == Direction.Right)
                {
                    sprite.effects = SpriteEffects.FlipHorizontally;
                    if (new Vector2(GridPosition.X + 1, GridPosition.Y) != OtherPlayerGrid) MoveRight();
                }
                else if (direction == Direction.Left || nextStep == Direction.Left)
                {
                    sprite.effects = SpriteEffects.None;
                    if (new Vector2(GridPosition.X - 1, GridPosition.Y) != OtherPlayerGrid) MoveLeft();
                }
                else if (direction == Direction.Up || nextStep == Direction.Up)
                {
                    if (new Vector2(GridPosition.X, GridPosition.Y - 1) != OtherPlayerGrid) MoveUp();
                }
                else if (direction == Direction.Down || nextStep == Direction.Down)
                {
                    if (new Vector2(GridPosition.X, GridPosition.Y + 1) != OtherPlayerGrid) MoveDown();
                }
                nextStep = Direction.None;
            }
            base.Update();
        }
        public override void Removed()
        {
            state.RemoveAnimation(animation);
            base.Removed();
        }
    }
}
