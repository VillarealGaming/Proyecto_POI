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
        public RandomBot(int tileX, int tileY) : base(Assets.randomBot, tileX, tileY) { }
        public void Move()
        {
            switch (new Random().Next(4))
            {
                case 0:
                    if(MoveRight())
                        state.RandomBotInput(Direction.Right, id);
                    break;
                case 1:
                    if(MoveLeft())
                        state.RandomBotInput(Direction.Left, id);
                    break;
                case 2:
                    if(MoveUp())
                        state.RandomBotInput(Direction.Up, id);
                    break;
                case 3:
                    if(MoveDown())
                        state.RandomBotInput(Direction.Down, id);
                    break;
            }
        }
        public override void Added()
        {
            //state = (LevelState)POIGame.CurrentState;
            base.Added();
            BaseSpeed = 0.5f;
            MoveEase = 20.0f;
            Move();
        }
        protected override void GoalReached()
        {
            //Solo movemos los randombots en el player host(el primero en iniciar la sesión de juego)
            //... hardcode
            if(state.playerNumber == 0)
            {
                Move();
                state.RandomBotAllign(id, (int)GridPosition.X, (int)GridPosition.Y);
            }
            base.GoalReached();
        }
    }
}
