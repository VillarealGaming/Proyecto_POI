using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace mGame
{ 
    public class Component
    {
        private static int idIncrement = 0;
        public readonly int id;
        public Component() {
            id = idIncrement++;
        }
        //method called when a component is added to a container
        public virtual void Added() {

        }
        //method called when a component is removed from a container
        public virtual void Removed() {

        }
    }
    public abstract class ComponentContainer
    {
        internal Dictionary<int, Component> components = new Dictionary<int, Component>();
        protected void Add(Component component) {
            components.Add(component.id, component);
            component.Added();
        }
        public void Remove(Component component) {
            component.Removed();
            components.Remove(component.id);
        }
    }
    //Drawable component and container
    public class Drawable : Component
    {
        internal virtual void Draw() { }
    }
    //graphic component and container
    //it draw itself based on the camera position
    public class GraphicInstance : Drawable
    {
        internal Position position;
        internal Texture2D texture;
        internal AnimationFrame frame;
        public SpriteEffects effects;
        public Vector2 scale;
        public float rotation { get; set; }
        public Vector2 origin;
        public GraphicInstance(Texture2D texture, Position position) {
            this.texture = texture;
            this.position = position;
            scale.X = 1.0f;
            scale.Y = 1.0f;
            frame = new AnimationFrame();
            effects = new SpriteEffects();
        }
        internal override void Draw() {
            POIGame.spriteBatch.Draw(
                texture,
                position.ClampValue - POIGame.Camera.Location.ToVector2(),
                null,
                frame.rectangle,
                origin,
                rotation,
                scale,
                Color.White,
                effects
                );
            base.Draw();
        }
    }
    //TODO: GUI graphic
    //class...
    //
    public class Graphics : ComponentContainer
    {
        public void AddGraphic(Drawable graphic) {
            Add(graphic);
        }
        public void Draw() {
            foreach (var pair in components) {
                Drawable graphic = (Drawable)pair.Value;
                graphic.Draw();
            }
        }
    }
    //updatable component and container
    public class Updateable : Component
    {
        internal virtual void Update() { }
    }
    public class UpdateableContainer : ComponentContainer
    {
        public void AddInstance(Updateable updatable) {
            Add(updatable);
        }
        public void Update() {
            foreach (var pair in components) {
                Updateable updatable = (Updateable)pair.Value;
                updatable.Update();
            }
        }
    }
    //position component
    public class Position
    {
        private Vector2 clampValue;
        public Vector2 ClampValue {
            get {
                clampValue.X = (int)Value.X;
                clampValue.Y = (int)Value.Y;
                return clampValue;
            }
        }
        public Vector2 Value { get; set; }
    }
    //frame component
    public class AnimationFrame
    {
        public Rectangle? rectangle;
    }
    //animation component
    public class Animation : Updateable
    {
        private Dictionary<string, int[]> animations;
        private AnimationFrame frameReference;
        private Rectangle frame;
        public float frameSpeed { get; set; }
        private int[] currentAnimation;
        private float currentFrame;
        private float animationSpeed;
        public Animation(GraphicInstance graphic, int frameWidth, int frameHeight) {
            frameReference = graphic.frame;
            frame = new Rectangle(0, 0, frameWidth, frameHeight);
            frameReference.rectangle = frame;
            animations = new Dictionary<string, int[]>();
            animationSpeed = 1;
        }
        public void SetAnimation(string animationName) {
            if(animations.ContainsKey(animationName)) {
                currentAnimation = animations[animationName];
            }
        }
        public void SetAnimation(string animationName, float animationSpeed) {
            SetAnimation(animationName);
            this.animationSpeed = animationSpeed;
        }
        internal override void Update() {
            currentFrame += animationSpeed * (float)POIGame.DeltaTime;
            currentFrame %= currentAnimation.Length;
            frame.X = currentAnimation[(int)currentFrame] * frame.Width;
            //Crappy reasignation thanks to Rectangle struct type...
            frameReference.rectangle = frame;
            base.Update();
        }
        public void AddAnimation(string animationName, int[] frames) {
            animations.Add(animationName, frames);
        }
        public void RemoveAnimation(string animationName) {
            animations.Remove(animationName);
        }
    }
    //Custom Tilemap component
    public class TileMap : Drawable
    {
        struct TileInfo {
            public Vector2 tilePosition;
            public Rectangle textureRect;
        }
        private TileInfo[] tiles;
        private GraphicInstance tile;
        //Texture2D texture;
        public TileMap(Texture2D texture, int tileWidth = 24, int tileHeight = 24) {
            //tiles = new TileInfo[mapWidth * mapHeight];
            tile = new GraphicInstance(texture, new Position());
            tile.frame.rectangle = new Rectangle(0, 0, tileWidth, tileHeight);
            //this.texture = texture;
        }
        /// <summary>
        /// Loads the tiles from an array, it just evaluates
        /// if the value is different from 0
        /// </summary>
        public void GenerateFromArray(UInt32[] data, int mapWidth, int mapHeight) {
            tiles = new TileInfo[data.Length];

        }
        internal override void Draw() {
            foreach(var tile in tiles) {
                //POIGame.spriteBatch.Draw(
                //    texture,
                //    tile.tilePosition,
                //    tile.textureRect,
                //    Color.White
                //    );
            }
            base.Draw();
        }
    }
}
