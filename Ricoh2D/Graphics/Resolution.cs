using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
    public class Resolution
    {
        private readonly Game game;
        private Viewport viewport;
        private Vector2 ratio;

        public int VirtualWidth, VirtualHeight;
        public int ScreenWidth, ScreenHeight;

        public Resolution(Game _game, int virtualWidth = 1366, int virtualHeight = 768, int screenWidth = 1024, int screenHeight = 768)
        {
            game = _game;

            VirtualWidth = virtualWidth;
            VirtualHeight = virtualHeight;

            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
        }

        public void Initialise()
        {
            ratio = new Vector2(
                (float)viewport.Width / VirtualWidth,
                (float)viewport.Height / VirtualHeight);
        }

        public Vector2 ConvertActualToScreen(Vector2 screenPosition)
        {
            //Converts Mouse Coordinates from Screen to Virtual Screen
            Vector2 real = new Vector2(
                screenPosition.X - viewport.X,
                screenPosition.Y - viewport.Y);

            real.X /= ratio.X;
            real.Y /= ratio.Y;

            return real;
        }
    }
}