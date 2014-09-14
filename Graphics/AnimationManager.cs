using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Ricoh2DFramework.Graphics
{
    public class AnimationManager
    {
        public string currAnimName = "";
        public Animation currAnim;
        private int rows;
        private int columns;

        private Dictionary<string, Animation> AnimationList = new Dictionary<string, Animation>();

        public AnimationManager(int rows = 1, int columns = 1)
        {
            this.rows = rows;
            this.columns = columns;
        }

        public void Update(GameTime gameTime)
        {
            if (currAnim != null)
                currAnim.Update(gameTime);
        }

        public void Add(string name, int[] frames, bool looped = false, float frameRate = 30)
        {
            AnimationList.Add(name, new Animation(frames, looped, frameRate));
        }

        public Animation getAnimation(string name)
        {
            Animation anim;
            AnimationList.TryGetValue(name, out anim);
            return anim;
        }

        public void RemoveAnimation(string name)
        {
            AnimationList.Remove(name);
        }

        public void RemoveAll()
        {
            AnimationList.Clear();
        }

        public void Play(string name)
        {
            if (!currAnimName.Equals(name))
            {
                Animation anim = getAnimation(name);

                if (anim != null)
                {
                    currAnimName = name;
                    currAnim = anim;
                }
            }
        }

        public void Pause()
        {
            currAnim.State = AnimationStates.PAUSE;
        }

        public void Forward()
        {
            currAnim.State = AnimationStates.FORWARD;
        }

        public void Reverse()
        {
            currAnim.State = AnimationStates.BACKWARD;
        }

        public int getCurrentRow()
        {
            return (int)(currAnim.getCurrentFrame() / columns);
        }

        public int getCurrentColumn()
        {
            return (currAnim.getCurrentFrame() % columns);
        }

        public int Size()
        {
            return AnimationList.Count;
        }
    }
}
