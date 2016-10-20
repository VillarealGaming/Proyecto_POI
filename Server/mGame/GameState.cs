using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace mGame
{
    public class GameState
    {
        private Graphics graphicManager;
        private UpdateableContainer animationManager;
        private UpdateableContainer instanceManager;
        public readonly RefRectangle camera;
        private bool initialized;
        public bool Initialized
        {
            get { return initialized; }
        }
        public GameState()
        {
            graphicManager = new Graphics();
            animationManager = new UpdateableContainer();
            instanceManager = new UpdateableContainer();
            camera = new RefRectangle();
            camera.Value.Width = POIGame.GameWidth;
            camera.Value.Height = POIGame.GameHeight;
        }
        public virtual void Update()
        {
            lock(this)
            {
                instanceManager.Update();
                animationManager.Update();
            }
        }
        public virtual void Draw()
        {
            lock (this)
            graphicManager.Draw();
        }
        public void AddAnimation(Animation animation)
        {
            lock (this)
            animationManager.AddInstance(animation);
        }
        public void AddInstance(Updateable Instance)
        {
            lock (this)
            instanceManager.AddInstance(Instance);
        }
        public void AddGraphic(Drawable graphic)
        {
            lock (this)
            graphicManager.AddGraphic(graphic);
        }
        public void RemoveAnimation(Animation animation)
        {
            lock (this)
            animationManager.Remove(animation);
        }
        public void RemoveInstance(Updateable Instance)
        {
            lock (this)
            instanceManager.Remove(Instance);
        }
        public void RemoveGraphic(Drawable graphic)
        {
            lock (this)
            graphicManager.Remove(graphic);
        }
        public virtual void Clear()
        {
            instanceManager.Clear();
            animationManager.Clear();
            graphicManager.Clear();
        }
        public virtual void Init() { initialized = true; }
        public virtual void Out() { }
    }
}
