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
            instanceManager.Update();
            animationManager.Update();
        }
        public virtual void Draw()
        {
            graphicManager.Draw();
        }
        public void AddAnimation(Animation animation)
        {
            animationManager.AddInstance(animation);
        }
        public void AddInstance(Updateable Instance)
        {
            instanceManager.AddInstance(Instance);
        }
        public void AddGraphic(Drawable graphic)
        {
            graphicManager.AddGraphic(graphic);
        }
        public void RemoveAnimation(Animation animation)
        {
            animationManager.Remove(animation);
        }
        public void RemoveInstance(Updateable Instance)
        {
            instanceManager.Remove(Instance);
        }
        public void RemoveGraphic(Drawable graphic)
        {
            graphicManager.Remove(graphic);
        }
        public virtual void Init() { }
        public virtual void Out() { }
    }
}
