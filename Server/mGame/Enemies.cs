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
        public RandomBot(int tileX, int tileY) : base(Assets.randomBot, tileX, tileY, "randomBot") {
            Random rand = new Random(DateTime.Now.Millisecond);
            BaseSpeed = 0.5f;
            MoveEase = rand.Next(10, 20);//20.0f;
        }
        public RandomBot(int tileX, int tileY, float baseSpeed, float moveEase) : base(Assets.randomBot, tileX, tileY, "randomBot")
        {
            BaseSpeed = baseSpeed;
            MoveEase = moveEase;//20.0f;
        }
        internal override void Update()
        {
            if(Vector2.Distance(position.Value, state.camera.Value.Center.ToVector2()) < (POIGame.GameWidth / 2) + (POIGame.GameWidth/6))
            {
                base.Update();
            }
        }
        public void Move()
        {
            switch (new Random().Next(4))
            {
                case 0:
                    if(MoveRight())
                        state.RandomBotInput(Direction.Right, id,(int)GridPosition.X, (int)GridPosition.Y);
                    break;
                case 1:
                    if(MoveLeft())
                        state.RandomBotInput(Direction.Left, id, (int)GridPosition.X, (int)GridPosition.Y);
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
            Move();
        }
        protected override void GoalReached()
        {
            //Solo movemos los randombots en el player host(el primero en iniciar la sesión de juego)
            //... hardcode
            if(state.playerNumber == 0)
            {
                Move();
                //state.RandomBotAllign(id, (int)GridPosition.X, (int)GridPosition.Y);
            }
            base.GoalReached();
        }
        protected override void OnCollide(string group1, string group2)
        {
            state.RemoveInstance(this);
            base.OnCollide(group1, group2);
        }
    }
}
