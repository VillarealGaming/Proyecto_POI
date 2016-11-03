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
            components.Add(component.id, component);
            component.Added();
        }
        public void Remove(Component component) {
            component.Removed();
            components.Remove(component.id);
        }
        public void Clear()
        {
            components = new Dictionary<int, Component>();
            //components.Clear();
        }
        public T GetComponent<T>(int id) where T : Component {
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
        public Color colorMask;
        public AdvancedDrawable(bool fixedPosition = false)
        {
            effects = new SpriteEffects();
            scale.X = 1.0f;
            scale.Y = 1.0f;
            layerDepth = 0.5f;
            offSet = fixedPosition ? new RefRectangle() : state.camera;
            colorMask = Color.White;
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
                    colorMask,//Color.White,
                    effects,
                    layerDepth
                    );
                base.Draw();
            }
            catch
            {
                //state.NullDrawCall();
            }
        }
    }
    public class Graphics : ComponentContainer
    {
        public void AddGraphic(Drawable graphic) {
            Add(graphic);
        }
        public void Draw() {
            foreach (var pair in components.ToList()) {
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
            foreach (var pair in components.ToList()) {
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
        private bool animationEnd;
        public bool AnimationEnd { get { return animationEnd; } }
        public Rectangle Frame { get { return frame; } }
        public Animation(GraphicInstance graphic, int frameWidth, int frameHeight) {
            frameReference = graphic.frame;
            frame = new Rectangle(0, 0, frameWidth, frameHeight);
            frameReference.rectangle = frame;
            animations = new Dictionary<string, int[]>();
            animationSpeed = 1;
        }
        public void SetAnimation(string animationName) {
            if (animations.ContainsKey(animationName)) {
                currentAnimation = animations[animationName];
            }
        }
        public void SetAnimation(string animationName, float animationSpeed) {
            SetAnimation(animationName);
            this.animationSpeed = animationSpeed;
        }
        internal override void Update() {
            currentFrame += animationSpeed * (float)POIGame.DeltaTime;
            animationEnd = currentFrame > currentAnimation.Length - 1;
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
            catch
            {
                //state.NullDrawCall();
            }
        }
    }
    //hitbox class
    public class Hitbox
    {
        public static bool Collide(Hitbox hitbox1, Hitbox hitbox2)
        {
            return !Rectangle.Intersect(hitbox1.Value, hitbox2.Value).IsEmpty;
        }
        //private static int idCount;
        private Rectangle rect;
        private Position position;
        private Vector2 offset;
        public readonly string group;
        public readonly Action<string, string> callback;
        //public readonly int id;
        //returns a rectangle with the position offset applied
        public Rectangle Value
        {
            get
            {
                return new Rectangle(
                    (int)(position.Value.X + offset.X),
                    (int)(position.Value.Y + offset.Y),
                    rect.Width, rect.Height);
            }
        }
        public Hitbox(
            string group,
            Action<string, string> callback,
            int width,
            int height,
            Position position = null,
            Vector2 offset = new Vector2())
        {
            //idCount++;
            //id = idCount;
            this.group = group;
            this.callback = callback;
            this.offset = offset;
            rect = new Rectangle(0, 0, width, height);
            if (position == null)
                this.position = new Position();
            else
                this.position = position;
        }
    }
    //component that remove itself when animation ends
    public class OnceAnimation : Updateable
    {
        private GraphicInstance graphic;
        private Position position;
        private Animation animation;
        private Texture2D texture;
        private float animationSpeed;
        private int frameWidth, frameHeight;
        public OnceAnimation(Texture2D texture, int x, int y, int frameWidth, int frameHeight, float speed = 30.0f)
        {
            animationSpeed = speed;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.texture = texture;
            position = new Position();
            position.Value = new Vector2(x, y);
        }
        public override void Added()
        {
            base.Added();
            graphic = new GraphicInstance(texture, position);
            animation = new Animation(graphic, frameWidth, frameWidth);
            int[] frames = new int[texture.Width / animation.Frame.Width];
            for (int i = 0; i < frames.Length; i++) { frames[i] = i; }
            animation.AddAnimation("base", frames);
            animation.SetAnimation("base", animationSpeed);
            state.AddGraphic(graphic);
            state.AddAnimation(animation);
        }
        internal override void Update()
        {
            base.Update();
            if (animation.AnimationEnd)
                state.RemoveInstance(this);
        }
        public override void Removed()
        {
            state.RemoveGraphic(graphic);
            state.RemoveAnimation(animation);
            base.Removed();
        }
    }
}
