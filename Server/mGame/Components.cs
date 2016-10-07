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
        private RefRectangle offSet;
        public SpriteEffects effects;
        public Vector2 scale;
        public float rotation { get; set; }
        public Vector2 origin;
        //I don't like this layer implementation
        public float layerDepth;
        public GraphicInstance(Texture2D texture, Position position, bool fixedPosition = false) {
            this.texture = texture;
            this.position = position;
            scale.X = 1.0f;
            scale.Y = 1.0f;
            frame = new AnimationFrame();
            effects = new SpriteEffects();
            offSet = fixedPosition ? new RefRectangle() : POIGame.Camera;
            layerDepth = 1.0f;
        }
        internal override void Draw() {
            POIGame.spriteBatch.Draw(
                texture,
                position.ClampValue - offSet.Value.Location.ToVector2(),
                null,
                frame.rectangle,
                origin,
                rotation,
                scale,
                Color.White,
                effects,
                layerDepth
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
    //position and rectangle component, they just wrap an struct so
    //it can be handled by reference
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
        public Vector2 Value;
    }
    public class RefRectangle{ public Rectangle Value; }
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
            public Point textureCoord;
        }
        private TileInfo[] tiles;
        private GraphicInstance tile;
        private int tileWidth;
        private int tileHeight;
        //Texture2D texture;
        public TileMap(Texture2D texture, int tileWidth = 24, int tileHeight = 24) {
            //tiles = new TileInfo[mapWidth * mapHeight];
            tile = new GraphicInstance(texture, new Position());
            tile.frame.rectangle = new Rectangle(0, 0, tileWidth, tileHeight);
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            //test locals
            const int mapWidth = 500;
            const int mapHeight = 500;
            tile.layerDepth = 0.001f;
            tiles = new TileInfo[POIGame.LevelData.Length];
            int index = 501;// = i * mapWidth + j;
            for (int i = 1; i < mapHeight-1; i++) {
                for (int j = 1; j < mapWidth-1; j++) {
                    tiles[i * mapWidth + j].tilePosition = new Vector2(j * tileWidth, i * tileHeight);
                    if(POIGame.LevelData[index] == 0) {
                        bool isWall = false;
                        if(
                            //Side check
                            POIGame.LevelData[index + 1] != 0 ||
                            POIGame.LevelData[index - 1] != 0 ||
                            POIGame.LevelData[index - mapWidth] != 0 ||
                            POIGame.LevelData[index + mapWidth] != 0 ||
                            //Corner check
                            POIGame.LevelData[index + mapWidth + 1] != 0 ||
                            POIGame.LevelData[index + mapWidth - 1] != 0 ||
                            POIGame.LevelData[index - mapWidth + 1] != 0 ||
                            POIGame.LevelData[index - mapWidth - 1] != 0
                            ) {
                            isWall = true;
                        }
                        tiles[index].textureCoord = isWall? new Point(48, 0) : new Point(24, 0);
                    }
                    else {
                        tiles[index].textureCoord = new Point();
                    }
                    index++;
                }
            }
            //this.texture = texture;
        }
        /// <summary>
        /// Loads the tiles from an array, it just evaluates
        /// if the value is different from 0
        /// </summary>
        public void GenerateFromArray(int mapWidth, int mapHeight) {
        }
        internal override void Draw() {
            //Just draw current on screen tiles...
            //TODO: Make tile size a global static const
            Point cameraGrid = new Point(POIGame.Camera.Value.X / 24, POIGame.Camera.Value.Y / 24);
            int index = (cameraGrid.Y) * 500 + (cameraGrid.X);// = i * mapWidth + j;
            for (int i = 0; i < POIGame.Camera.Value.Height / 24 + 1; i ++) {
                for(int j = 0; j < POIGame.Camera.Value.Width / 24 + 1; j++) {
                    TileInfo tileInfo = tiles[(cameraGrid.Y + i) * 500 + (cameraGrid.X + j)];
                    tile.position.Value = tileInfo.tilePosition;
                    tile.frame.rectangle = new Rectangle(tileInfo.textureCoord, new Point(24, 24));
                    tile.Draw();
                    index++;
                }
                index -= POIGame.Camera.Value.Width / 24 + 1;
                index += i * POIGame.Camera.Value.Width / 24 + 1;
            }
            //foreach(var tileInfo in tiles) {
            //    tile.position.Value = tileInfo.tilePosition;
            //    tile.Draw();
            //    //POIGame.spriteBatch.Draw(
            //    //    texture,
            //    //    tile.tilePosition,
            //    //    tile.textureRect,
            //    //    Color.White
            //    //    );
            //}
            base.Draw();
        }
    }
}
