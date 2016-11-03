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
    //Creo que esta es la clase más hardcodeada de todo el juego...
    public class Player : MoveableTile
    {
        //protected new LevelState state;
        private Animation animation;
        private Direction nextStep;
        private bool isDeath;
        private float health;
        public int Health { get { return (int)health; } }
        public Vector2 OtherPlayerGrid { get; set; }
        //No me gusta esta implementación
        private Keys right, left, up, down, shot;
        private string currentFaceDirection;
        Direction bulletDirection;
        public Player(Texture2D sprite, Keys right, Keys left, Keys up, Keys down, Keys shot, int tileX = 250, int tileY = 250) : base(sprite, tileX, tileY, "player")
        {
            this.right = right;
            this.left = left;
            this.up = up;
            this.down = down;
            this.shot = shot;
            currentFaceDirection = "Down";
            health = 100.0f;
        }
        public override void Added()
        {
            base.Added();
            //state = (LevelState)POIGame.CurrentState;
            animation = new Animation(sprite, 24, 24);
            animation.AddAnimation("stopSide", new int[] { 0 });
            animation.AddAnimation("moveSide", new int[] { 1 });
            animation.AddAnimation("stopDown", new int[] { 2 });
            animation.AddAnimation("moveDown", new int[] { 3 });
            animation.AddAnimation("stopUp", new int[] { 4 });
            animation.AddAnimation("moveUp", new int[] { 5 });
            animation.SetAnimation("stopDown");
            state.AddAnimation(animation);
            MoveEase = 5.0f;//7.0f
            nextStep = Direction.None;
            bulletDirection = Direction.Down;
            state.camera.Value.Location = position.Value.ToPoint();
        }
        internal override void Update()
        {
            if(!isDeath && health < 100)
            {
                health += 0.015f;
            }
            if (Velocity > 1.5f)
            {
                animation.SetAnimation("move" + currentFaceDirection);
            }
            else {
                animation.SetAnimation("stop" + currentFaceDirection);
            }
            if (right != Keys.Escape)
            {
                if(state.playerNumber == 0)
                {
                    POIGame.MapKeyPress(delegate { return GetGamePadSide(Direction.Right); }, right);
                    POIGame.MapKeyPress(delegate { return GetGamePadSide(Direction.Left); }, left);
                    POIGame.MapKeyPress(delegate { return GetGamePadSide(Direction.Up); }, up);
                    POIGame.MapKeyPress(delegate { return GetGamePadSide(Direction.Down); }, down);
                    POIGame.MapKeyPress(delegate { return GetGamePadInput(Buttons.B); }, shot);
                }
                if (POIGame.GetKeyPressed(shot) && !isDeath)
                {
                    state.PlayerShot();
                    Assets.bulletSound.Play(0.6f, 0.2f, 0.0f);
                    state.AddInstance(new Bullet(
                        "playerBullet",
                        Assets.playerBullet, 
                        (int)position.Value.X + 12, 
                        (int)position.Value.Y + 12,
                        8.0f,
                        bulletDirection));
                }
                //move logic
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
                        if (new Vector2(GridPosition.X + 1, GridPosition.Y) != OtherPlayerGrid)
                        {
                            MoveRight();
                            currentFaceDirection = "Side";
                            state.PlayerInput(Direction.Right, (int)GridPosition.X, (int)GridPosition.Y);
                            bulletDirection = Direction.Right;
                        }
                    }
                    else if (POIGame.GetKeyPressed(left))
                    {
                        sprite.effects = SpriteEffects.None;
                        if (new Vector2(GridPosition.X - 1, GridPosition.Y) != OtherPlayerGrid)
                        {
                            MoveLeft();
                            currentFaceDirection = "Side";
                            state.PlayerInput(Direction.Left, (int)GridPosition.X, (int)GridPosition.Y);
                            bulletDirection = Direction.Left;
                        }
                    }
                    else if (POIGame.GetKeyPressed(up))
                    {
                        if (new Vector2(GridPosition.X, GridPosition.Y - 1) != OtherPlayerGrid)
                        {
                            MoveUp();
                            currentFaceDirection = "Up";
                            state.PlayerInput(Direction.Up, (int)GridPosition.X, (int)GridPosition.Y);
                            bulletDirection = Direction.Up;
                        }
                    }
                    else if (POIGame.GetKeyPressed(down))
                    {
                        if (new Vector2(GridPosition.X, GridPosition.Y + 1) != OtherPlayerGrid)
                        {
                            MoveDown();
                            currentFaceDirection = "Down";
                            state.PlayerInput(Direction.Down, (int)GridPosition.X, (int)GridPosition.Y);
                            bulletDirection = Direction.Down;
                        }
                    }
                }
                else
                {
                    if (POIGame.GetKeyPressed(right) || nextStep == Direction.Right)
                    {
                        sprite.effects = SpriteEffects.FlipHorizontally;
                        if (new Vector2(GridPosition.X + 1, GridPosition.Y) != OtherPlayerGrid)
                        {
                            MoveRight();
                            currentFaceDirection = "Side";
                            state.PlayerInput(Direction.Right, (int)GridPosition.X, (int)GridPosition.Y);
                            bulletDirection = Direction.Right;
                            Assets.stepSound.Play(0.6f, 0.5f, 0.0f);
                        }
                    }
                    else if (POIGame.GetKeyPressed(left) || nextStep == Direction.Left)
                    {
                        sprite.effects = SpriteEffects.None;
                        if (new Vector2(GridPosition.X - 1, GridPosition.Y) != OtherPlayerGrid)
                        {
                            MoveLeft();
                            currentFaceDirection = "Side";
                            state.PlayerInput(Direction.Left, (int)GridPosition.X, (int)GridPosition.Y);
                            bulletDirection = Direction.Left;
                            Assets.stepSound.Play(0.6f, 0.5f, 0.0f);
                        }
                    }
                    else if (POIGame.GetKeyPressed(up) || nextStep == Direction.Up)
                    {
                        if (new Vector2(GridPosition.X, GridPosition.Y - 1) != OtherPlayerGrid)
                        {
                            MoveUp();
                            currentFaceDirection = "Up";
                            state.PlayerInput(Direction.Up, (int)GridPosition.X, (int)GridPosition.Y);
                            bulletDirection = Direction.Up;
                            Assets.stepSound.Play(0.6f, 0.5f, 0.0f);
                        }
                    }
                    else if (POIGame.GetKeyPressed(down) || nextStep == Direction.Down)
                    {
                        if (new Vector2(GridPosition.X, GridPosition.Y + 1) != OtherPlayerGrid)
                        {
                            MoveDown();
                            currentFaceDirection = "Down";
                            state.PlayerInput(Direction.Down, (int)GridPosition.X, (int)GridPosition.Y);
                            bulletDirection = Direction.Down;
                            Assets.stepSound.Play(0.6f, 0.5f, 0.0f);
                        }
                    }
                    nextStep = Direction.None;
                }
            }
            base.Update();
        }
        public void Move(Direction direction)
        {
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
                    if (new Vector2(GridPosition.X + 1, GridPosition.Y) != OtherPlayerGrid)
                    {
                        MoveRight();
                        currentFaceDirection = "Side";
                        bulletDirection = Direction.Right;
                    }
                }
                else if (direction == Direction.Left)
                {
                    sprite.effects = SpriteEffects.None;
                    if (new Vector2(GridPosition.X - 1, GridPosition.Y) != OtherPlayerGrid)
                    {
                        MoveLeft();
                        currentFaceDirection = "Side";
                        bulletDirection = Direction.Left;
                    }
                }
                else if (direction == Direction.Up)
                {
                    if (new Vector2(GridPosition.X, GridPosition.Y - 1) != OtherPlayerGrid)
                    {
                        MoveUp();
                        currentFaceDirection = "Up";
                        bulletDirection = Direction.Up;
                    }
                }
                else if (direction == Direction.Down)
                {
                    if (new Vector2(GridPosition.X, GridPosition.Y + 1) != OtherPlayerGrid)
                    {
                        MoveDown();
                        currentFaceDirection = "Down";
                        bulletDirection = Direction.Down;
                    }
                }
            }
            else
            {
                if (direction == Direction.Right || nextStep == Direction.Right)
                {
                    sprite.effects = SpriteEffects.FlipHorizontally;
                    if (new Vector2(GridPosition.X + 1, GridPosition.Y) != OtherPlayerGrid)
                    {
                        MoveRight();
                        currentFaceDirection = "Side";
                        bulletDirection = Direction.Right;
                        Assets.stepSound.Play(0.6f, 0.5f, 0.0f);
                    }
                }
                else if (direction == Direction.Left || nextStep == Direction.Left)
                {
                    sprite.effects = SpriteEffects.None;
                    if (new Vector2(GridPosition.X - 1, GridPosition.Y) != OtherPlayerGrid)
                    {
                        MoveLeft();
                        currentFaceDirection = "Side";
                        bulletDirection = Direction.Left;
                        Assets.stepSound.Play(0.6f, 0.5f, 0.0f);
                    }
                }
                else if (direction == Direction.Up || nextStep == Direction.Up)
                {
                    if (new Vector2(GridPosition.X, GridPosition.Y - 1) != OtherPlayerGrid)
                    {
                        MoveUp();
                        currentFaceDirection = "Up";
                        bulletDirection = Direction.Up;
                        Assets.stepSound.Play(0.6f, 0.5f, 0.0f);
                    }
                }
                else if (direction == Direction.Down || nextStep == Direction.Down)
                {
                    if (new Vector2(GridPosition.X, GridPosition.Y + 1) != OtherPlayerGrid)
                    {
                        MoveDown();
                        currentFaceDirection = "Down";
                        bulletDirection = Direction.Down;
                        Assets.stepSound.Play(0.6f, 0.5f, 0.0f);
                    }
                }
                nextStep = Direction.None;
            }
            //base.Update();
        }
        public void Shot()
        {
            Assets.bulletSound.Play(0.6f, 0.2f, 0.0f);
            state.AddInstance(new Bullet(
                "playerBullet",
                Assets.playerBullet,
                (int)position.Value.X + 12,
                (int)position.Value.Y + 12,
                8.0f,
                bulletDirection));
        }
        public override void Removed()
        {
            state.RemoveAnimation(animation);
            base.Removed();
        }
        //http://www.gamefromscratch.com/post/2015/06/28/MonoGame-Tutorial-Handling-Keyboard-Mouse-and-GamePad-Input.aspx
        private bool GetGamePadSide(Direction direction)
        {
            GamePadCapabilities gPadCapabilities = GamePad.GetCapabilities(PlayerIndex.One);
            if (!gPadCapabilities.IsConnected)
                return false;
            GamePadState gPadState = GamePad.GetState(PlayerIndex.One);
            bool flag = false;
            if (gPadCapabilities.HasLeftXThumbStick &&
                gPadCapabilities.HasLeftYThumbStick)
            {
                switch(direction)
                {
                    case Direction.Left:
                        flag = gPadState.ThumbSticks.Left.X < -0.5f;
                        break;
                    case Direction.Right:
                        flag = gPadState.ThumbSticks.Left.X > 0.5f;
                        break;
                    case Direction.Up:
                        flag = gPadState.ThumbSticks.Left.Y > 0.5f;
                        break;
                    case Direction.Down:
                        flag = gPadState.ThumbSticks.Left.Y < -0.5f;
                        break;
                }
            }
            if(flag == false &&
                gPadCapabilities.HasDPadRightButton &&
                gPadCapabilities.HasDPadLeftButton &&
                gPadCapabilities.HasDPadUpButton &&
                gPadCapabilities.HasDPadDownButton)
            {
                switch (direction)
                {
                    case Direction.Left:
                        flag = gPadState.IsButtonDown(Buttons.DPadLeft);
                        break;
                    case Direction.Right:
                        flag = gPadState.IsButtonDown(Buttons.DPadRight);
                        break;
                    case Direction.Up:
                        flag = gPadState.IsButtonDown(Buttons.DPadUp);
                        break;
                    case Direction.Down:
                        flag = gPadState.IsButtonDown(Buttons.DPadDown);
                        break;
                }
            }
            return flag;
        }
        private bool GetGamePadInput(Buttons button)
        {
            GamePadCapabilities gPadCapabilities = GamePad.GetCapabilities(PlayerIndex.One);
            if (!gPadCapabilities.IsConnected)
                return false;
            GamePadState gPadState = GamePad.GetState(PlayerIndex.One);
            return gPadState.IsButtonDown(button);
        }
        protected override void OnCollide(string group1, string group2)
        {
            if(group1 =="randomBot")
            {
                health--;
            }
            else if(group1 == "enemyBullet")
            {
                health -= 10.0f;
            }
            if(health <= 0)
            {
                health = 0;
                sprite.colorMask = new Color(0.0f, 1.0f, 1.0f, 0.0f);
                isDeath = true;
            }
            base.OnCollide(group1, group2);
        }
    }
}
