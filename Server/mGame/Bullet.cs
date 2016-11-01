using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace mGame
{
    class Bullet : Updateable
    {
        protected GraphicInstance sprite;
        protected new LevelState state;
        protected Position position;
        protected Hitbox hitbox;
        private Texture2D texture;
        private Vector2 direction;
        float speed;
        float rotation;
        public Bullet(Texture2D texture, int x, int y, float speed, Direction direction)
        {
            position = new Position();
            position.Value = new Vector2(x, y);
            this.speed = speed;
            this.texture = texture;
            switch(direction)
            {
                case Direction.Left:
                    this.direction = new Vector2(-1.0f, 0.0f);
                    rotation = 180.0f;
                    break;
                case Direction.Right:
                    this.direction = new Vector2(1.0f, 0.0f);
                    break;
                case Direction.Up:
                    this.direction = new Vector2(0.0f, -1.0f);
                    rotation = 270.0f;
                    break;
                case Direction.Down:
                    this.direction = new Vector2(0.0f, 1.0f);
                    rotation = 90.0f;
                    break;
            }
            hitbox = new Hitbox("playerBullet", OnCollide, 24, 24, position, new Vector2(-12, -12));
        }
        public override void Added()
        {
            state = (LevelState)POIGame.CurrentState;
            sprite = new GraphicInstance(texture, position);
            state.AddGraphic(sprite);
            sprite.rotation = MathHelper.ToRadians(rotation);
            sprite.origin = new Vector2(12.0f, 12.0f);
            base.Added();
            state.AddHitbox(hitbox);
        }
        internal override void Update()
        {
            position.Value += direction * speed;
            base.Update();
            if (Vector2.Distance(position.Value, state.camera.Value.Center.ToVector2()) > (POIGame.GameWidth / 2) + (POIGame.GameWidth / 6))
                state.RemoveInstance(this);
            if(!CheckEmpty())
                state.RemoveInstance(this);
        }
        private bool CheckEmpty()
        {
            int index = ((int)(position.Value.Y / 24) * LevelState.LevelHeight) + (int)(position.Value.X / 24);
            return state.LevelData[index] != 0;
        }
        protected void OnCollide(string group1, string group2)
        {
            state.RemoveInstance(this);
        }
        public override void Removed()
        {
            Assets.bulletHitSound.Play(0.6f, 0.0f, 0.0f);
            state.AddInstance(
                new OnceAnimation(
                    Assets.bulletExplode,
                    (int)position.Value.X -24,
                    (int)position.Value.Y -24,
                    48,
                    48));
            state.RemoveHitbox(hitbox);
            state.RemoveGraphic(sprite);
            base.Removed();
        }
    }
}
