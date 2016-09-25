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
    //graphic component and container
    public class GraphicInstance : Component
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
    }
    public class Graphics : ComponentContainer
    {
        public void AddGraphic(GraphicInstance graphic) {
            Add(graphic);
        }
        public void Draw() {
            foreach (var pair in components) {
                GraphicInstance graphic = (GraphicInstance)pair.Value;
                POIGame.spriteBatch.Draw(
                    graphic.texture, 
                    graphic.position.ClampValue,
                    null, 
                    graphic.frame.rectangle,
                    graphic.origin,
                    graphic.rotation,
                    graphic.scale,
                    Color.White,
                    graphic.effects
                    );
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
}
