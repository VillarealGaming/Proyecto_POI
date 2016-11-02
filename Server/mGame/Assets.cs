using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
namespace mGame
{
    public static class Assets
    {
        public static Texture2D cameraEffect;
        public static Texture2D playerSprite;
        public static Texture2D player2Sprite;
        public static Texture2D mapTiles;
        public static Texture2D randomBot;
        public static Texture2D playerBullet;
        public static Texture2D enemyBullet;
        public static Texture2D bulletExplode;
        public static Texture2D hud;
        //public static Texture2D enemyExplode;
        public static SpriteFont retroFont;
        //sounds
        public static SoundEffect bulletSound;
        public static SoundEffect bulletHitSound;
        public static SoundEffect explosionSound;
        public static SoundEffect stepSound;
        public static Song LevelSong;
    }
}
