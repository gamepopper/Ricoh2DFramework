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
    public class Animation
    {
        private int[] frames = {}; //Frames to use, based on sprite sheet left-to-right and top-to-bottom.
        private int frameNo = 0; //Current Frame to Render
        private int frameCount = 0; //Total amount of frames.
        private bool looped = false; //Looping or not.
        private bool finished = false; //If not looping, returns true if current frame is on last frame.
        private float frameRate = 0; //Total frames to render per second.
        private float frameTime = 0; //Total time between frames.
        private AnimationStates state = AnimationStates.FORWARD; //Current State of Animation
        private float timer = 0.0f;

        public int FrameCount
        {
            get { return frameCount; }
        }
        public bool Looping
        {
            get { return looped; }
            set { looped = value; }
        }
        public bool Finished
        {
            get { return finished; }
        }
        public AnimationStates State
        {
            get { return state; }
            set { state = value; }
        }

        public Animation(int[] frames, bool looped = false, float frameRate = 30)
        {
            if (frames != null)
            {
                this.frames = frames;
            }
            else
            {
                int[] defaultFrame = { 0 };
                this.frames = defaultFrame;
            }
            this.looped = looped;
            this.frameRate = frameRate;

            frameNo = 0;
            frameCount = frames.Length;
            frameTime = 1 / frameRate;
        }

        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > frameTime)
            {
                if (state == AnimationStates.FORWARD)
                {
                    frameNo++;
                }
                else if (state == AnimationStates.BACKWARD)
                {
                    frameNo--;
                }

                if (looped)
                {
                    if (frameNo >= frameCount)
                    {
                        frameNo -= frameCount;
                    }
                    else if (frameNo < 0)
                    {
                        frameNo += frameCount;
                    }
                }
                else
                {
                    if (frameNo >= frameCount)
                    {
                        frameNo = frameCount - 1;
                        finished = true;
                    }
                    else if (frameNo < 0)
                    {
                        frameNo = 0;
                        finished = true;
                    }
                }

                timer = 0;
            }
        }

        public void ResetAnimation()
        {
            frameNo = 0;
            timer = 0;
            finished = false;
            state = AnimationStates.FORWARD;
        }

        public int getCurrentFrame()
        {
            return frames[frameNo];
        }
    }
}
