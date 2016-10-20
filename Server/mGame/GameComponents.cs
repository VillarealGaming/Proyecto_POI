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
        protected GameState state;
        private static int idIncrement = 0;
        public readonly int id;
        public Component(GameState state = null) {
            this.state = state == null ? POIGame.CurrentState : state;
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
            try
            {
                components.Add(component.id, component);
                component.Added();
            }
            catch { }
        }
        public void Remove(Component component) {
            component.Removed();
            components.Remove(component.id);
        }
        public void Clear()
        {
            components.Clear();
        }
        public T GetComponent<T>(int id) where T: Component{
            return (T)components[id];
        }
    }
    public class RefRectangle { public Rectangle Value; }
    //Drawable component and container
    public class Drawable : Component
    {
        internal virtual void Draw() { }
    }
    public class AdvancedDrawable : Drawable
    {
        public SpriteEffects effects;
        public Vector2 scale;
        public float rotation { get; set; }
        public Vector2 origin;
        protected RefRectangle offSet;
        //I don't like this layer implementation
        public float layerDepth;
        public AdvancedDrawable(bool fixedPosition = false)
        {
            effects = new SpriteEffects();
            scale.X = 1.0f;
            scale.Y = 1.0f;
            layerDepth = 0.5f;
            offSet = fixedPosition ? new RefRectangle() : state.camera;
        }
    }
    //graphic component and container
    //it draw itself based on the camera position
    public class GraphicInstance : AdvancedDrawable
    {
        internal Position position;
        internal Texture2D texture;
        internal AnimationFrame frame;
        public GraphicInstance(Texture2D texture, Position position, bool fixedPosition = false) : base(fixedPosition) {
            this.texture = texture;
            this.position = position;
            frame = new AnimationFrame();
        }
        internal override void Draw() {
            try
            {
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
            catch
            {
                throw new System.Exception("...");
            }
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
    //text component
    public class TextSprite : AdvancedDrawable
    {
        private SpriteFont font;
        public string text { get; set; }
        public Vector2 position { get; set; }
        public Color color { get; set; }
        public TextSprite(SpriteFont font, Vector2 position = new Vector2(), bool fixedPosition = true) : base(fixedPosition)
        {
            this.font = font;
            this.position = position;
            layerDepth = 1.0f;
            color = Color.White;
        }
        internal override void Draw()
        {
            try
            {
                POIGame.spriteBatch.DrawString(
                    font,
                    text,
                    position - offSet.Value.Location.ToVector2(),
                    color,
                    rotation,
                    origin,
                    scale,
                    effects,
                    layerDepth);
                base.Draw();
            }
            catch {
                throw new System.Exception("...");
            }
        }
    }
}
