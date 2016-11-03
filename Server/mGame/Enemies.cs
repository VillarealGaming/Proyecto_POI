using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mGame
{
    public class RandomBot : MoveableTile
    {
        private const float BulletSpeed = 3.0f;
        private int health;
        private Color colorMask;
        public RandomBot(int tileX, int tileY) : base(Assets.randomBot, tileX, tileY, "randomBot") {
            Random rand = new Random(DateTime.Now.Millisecond);
            BaseSpeed = 0.24f;//0.5
            MoveEase = rand.Next(20, 40);//20.0f;//10-20
            health = 5;
            colorMask = Color.White;
        }
        public RandomBot(int tileX, int tileY, float baseSpeed, float moveEase) : base(Assets.randomBot, tileX, tileY, "randomBot")
        {
            BaseSpeed = baseSpeed;
            MoveEase = moveEase;//20.0f;
            health = 5;
            colorMask = Color.White;
        }
        internal override void Update()
        {
            if (Vector2.Distance(position.Value, state.camera.Value.Center.ToVector2()) < (POIGame.GameWidth / 2) + (POIGame.GameWidth / 6))
            {
                base.Update();
                sprite.colorMask = colorMask;
            }
        }
        public void Move()
        {
            switch (new Random().Next(4))
            {
                case 0:
                    if(MoveLeft())
                        state.RandomBotInput(Direction.Left, id,(int)GridPosition.X, (int)GridPosition.Y);
                    break;
                case 1:
                    if(MoveRight())
                        state.RandomBotInput(Direction.Right, id, (int)GridPosition.X, (int)GridPosition.Y);
                    break;
                case 2:
                    if(MoveUp())
                        state.RandomBotInput(Direction.Up, id, (int)GridPosition.X, (int)GridPosition.Y);
                    break;
                case 3:
                    if(MoveDown())
                        state.RandomBotInput(Direction.Down, id, (int)GridPosition.X, (int)GridPosition.Y);
                    break;
            }
        }
        public override void Added()
        {
            //state = (LevelState)POIGame.CurrentState;
            base.Added();
            //if(state.playerNumber == 0)
            //{
            //}
            //else
            //{
            //    BaseSpeed = 0.5f;
            //    MoveEase = rand.Next(10, 20);//20.0f;
            //}
            //Move();
        }
        protected override void GoalReached()
        {
            //Solo movemos los randombots en el player host(el primero en iniciar la sesión de juego)
            //... hardcode
            if (state.playerNumber == 0)
                Move();
            base.GoalReached();
            //if (Vector2.Distance(position.Value, state.camera.Value.Center.ToVector2()) < (POIGame.GameWidth / 2) + (POIGame.GameWidth / 6))

        }
        public override bool MoveLeft()
        {
            if(base.MoveLeft())
            {
                state.AddInstance(new Bullet(
                   "enemyBullet",
                   Assets.enemyBullet,
                   (int)position.Value.X + 12,
                   (int)position.Value.Y + 12,
                   BulletSpeed,
                   Direction.Up
                   ));
                state.AddInstance(new Bullet(
                   "enemyBullet",
                   Assets.enemyBullet,
                   (int)position.Value.X + 12,
                   (int)position.Value.Y + 12,
                   BulletSpeed,
                   Direction.Down
                   ));
                return true;
            }
            return false;
        }
        public override bool MoveRight()
        {
            if (base.MoveRight())
            {
                state.AddInstance(new Bullet(
                   "enemyBullet",
                   Assets.enemyBullet,
                   (int)position.Value.X + 12,
                   (int)position.Value.Y + 12,
                   BulletSpeed,
                   Direction.Up
                   ));
                state.AddInstance(new Bullet(
                   "enemyBullet",
                   Assets.enemyBullet,
                   (int)position.Value.X + 12,
                   (int)position.Value.Y + 12,
                   BulletSpeed,
                   Direction.Down
                   ));
                return true;
            }
            return false;
        }
        public override bool MoveUp()
        {
            if (base.MoveUp())
            {
                state.AddInstance(new Bullet(
                   "enemyBullet",
                   Assets.enemyBullet,
                   (int)position.Value.X + 12,
                   (int)position.Value.Y + 12,
                   BulletSpeed,
                   Direction.Left
                   ));
                state.AddInstance(new Bullet(
                   "enemyBullet",
                   Assets.enemyBullet,
                   (int)position.Value.X + 12,
                   (int)position.Value.Y + 12,
                   BulletSpeed,
                   Direction.Right
                   ));
                return true;
            }
            return false;
        }
        public override bool MoveDown()
        {
            if (base.MoveDown())
            {
                state.AddInstance(new Bullet(
                   "enemyBullet",
                   Assets.enemyBullet,
                   (int)position.Value.X + 12,
                   (int)position.Value.Y + 12,
                   BulletSpeed,
                   Direction.Left
                   ));
                state.AddInstance(new Bullet(
                   "enemyBullet",
                   Assets.enemyBullet,
                   (int)position.Value.X + 12,
                   (int)position.Value.Y + 12,
                   BulletSpeed,
                   Direction.Right
                   ));
                return true;
            }
            return false;
        }
        protected override void OnCollide(string group1, string group2)
        {
            if(group1 == "playerBullet")
            {
                health--;
                if(health <= 0)
                {
                    state.EnemyKilled();
                    state.RemoveInstance(this);
                }
                else
                {
                    float redEffect = 0.25f + (health / 5.0f) *0.75f;
                    colorMask = new Color(1.0f, redEffect, redEffect, 1.0f);
                    sprite.colorMask = new Color(10.0f, 10.0f, 10.0f, 0.0f);
                }
                base.OnCollide(group1, group2);
            }
        }
        public override void Removed()
        {
            Assets.explosionSound.Play(0.5f, 0.0f, 0.0f);
            //state.AddInstance(
            //    new OnceAnimation(
            //        Assets.enemyExplode,
            //        (int)position.Value.X,
            //        (int)position.Value.Y,
            //        36,
            //        36,
            //        40.0f));
            base.Removed();
        }
    }
}
