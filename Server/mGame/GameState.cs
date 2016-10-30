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
        private List<Tuple<string, string>> collisionListeners;
        private Dictionary<string, List<Hitbox>> hitboxes;
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
            collisionListeners = new List<Tuple<string, string>>();
            hitboxes = new Dictionary<string, List<Hitbox>>();
        }
        public virtual void Update()
        {
            lock(this)
            {
                instanceManager.Update();
                animationManager.Update();
                CheckCollisions();
            }
        }
        public virtual void Draw()
        {
            lock (this)
            graphicManager.Draw();
        }
        public void CheckCollisions()
        {
            foreach(var group in collisionListeners)
            {
                var hitboxGroup1 = hitboxes[group.Item1];
                var hitboxGroup2 = hitboxes[group.Item2];
                for (int i = 0; i < hitboxGroup1.Count; i++)
                {
                    for (int j = 0; j < hitboxGroup2.Count; j++)
                    {
                        var hitbox1 = hitboxGroup1[i];
                        var hitbox2 = hitboxGroup2[j];
                        if (Hitbox.Collide(hitbox1, hitbox2))
                        {
                            hitbox1.callback(group.Item1,group.Item2);
                            hitbox2.callback(group.Item1, group.Item2);
                            i = hitboxGroup1.Count;
                            break;
                        }
                    }
                }
            }
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
        public bool AddCollisionGroup(string groupName)
        {
            if (hitboxes.ContainsKey(groupName))
                return false;
            hitboxes.Add(groupName, new List<Hitbox>());
            return true;
        }
        public bool AddHitbox(Hitbox hitbox)
        {
            if (!hitboxes.ContainsKey(hitbox.group))
                return false;
            hitboxes[hitbox.group].Add(hitbox);
            return true;
        }
        public void AddCollisionListener(string group1, string group2)
        {
            collisionListeners.Add(new Tuple<string, string>(group1, group2));
        }
        public void RemoveHitbox(Hitbox hitbox)
        {
            hitboxes[hitbox.group].Remove(hitbox);
        }
        //public virtual void NullDrawCall() { }
        public virtual void Init() { initialized = true; }
        public virtual void Out() { }
    }
}
