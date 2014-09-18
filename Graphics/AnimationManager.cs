using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

/*The MIT License (MIT)

Copyright (c) 2014 Gamepopper

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

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
