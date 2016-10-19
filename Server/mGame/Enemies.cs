using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mGame
{
    class RandomBot : MoveableTile
    {
        public RandomBot(int tileX, int tileY) : base(Assets.randomBot, tileX, tileY) { }
        public void Move()
        {
            switch (new Random().Next(4))
            {
                case 0:
                    MoveRight();
                    break;
                case 1:
                    MoveLeft();
                    break;
                case 2:
                    MoveUp();
                    break;
                case 3:
                    MoveDown();
                    break;
            }
        }
        public override void Added()
        {
            BaseSpeed = 0.5f;
            MoveEase = 20.0f;
            Move();
            base.Added();
        }
        protected override void GoalReached()
        {
            Move();
            base.GoalReached();
        }
    }
}
