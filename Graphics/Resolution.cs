using System;
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
    public class Resolution  //code based off RoboBlob's method for solving resolution independance
    {
        private readonly Game game;
        private Viewport viewport;
        private Vector2 ratio;
        private Vector2 vMouse = Vector2.Zero;

        public Color BackgroundColor = Color.Black;

        public int VirtualWidth, VirtualHeight;
        public int ScreenWidth, ScreenHeight;

        private bool dirtyMatrix;
        public bool RenderingToScreenIsFinished;
        private static Matrix scaleMatrix;
        private float scale;

        public Resolution(Game _game)
        {
            game = _game;

            //Resolution we are emulating
            VirtualWidth = 1366;
            VirtualHeight = 768;

            //Actual Screen Resolution
            ScreenWidth = 1024;
            ScreenHeight = 768;

            //Clear Screen Colour
            BackgroundColor = Color.Black;
        }

        public Resolution(Game _game, int virtualWidth = 1366, int virtualHeight = 768, int screenWidth = 1024, int screenHeight = 768)
        {
            game = _game;

            VirtualWidth = virtualWidth;
            VirtualHeight = virtualHeight;

            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;

            BackgroundColor = Color.Black;
        }

        public void Initialise()
        {
            SetupVirtualScreenViewport();

            ratio = new Vector2(
                (float)viewport.Width / VirtualWidth,
                (float)viewport.Height / VirtualHeight);

            dirtyMatrix = true;
        }

        public void SetupFullViewport()
        {
            // Reset's viewport to screen resolution            
            Viewport vport = new Viewport();

            vport.X = 0;
            vport.Y = 0;

            vport.Width = ScreenWidth;
            vport.Height = ScreenHeight;
            vport.MinDepth = 0;
            vport.MaxDepth = 1;

            game.GraphicsDevice.Viewport = vport;

            dirtyMatrix = true;
        }

        public void BeginDraw()
        {
            SetupFullViewport();
            game.GraphicsDevice.Clear(BackgroundColor);
            SetupVirtualScreenViewport();
        }

        public Matrix GetTransformedMatrix()
        {
            if (dirtyMatrix)
                RecreateScaleMatrix();

            return scaleMatrix;
        }

        private void RecreateScaleMatrix()
        {
            //Scale's the graphical components to fit the virtual resolution
            Matrix.CreateScale((float)scale, (float)scale, 1f, out scaleMatrix);
            dirtyMatrix = false;
        }

        public Vector2 ScaleMouseToScreenCoordinates(Vector2 screenPosition)
        {
            //Converts Mouse Coordinates from Screen to Virtual Screen

            Vector2 real = new Vector2(
                screenPosition.X - viewport.X,
                screenPosition.Y - viewport.Y);

            real.X /= ratio.X;
            real.Y /= ratio.Y;

            return real;
        }

        public void SetupVirtualScreenViewport()
        {
            //Sets the viewport to fit with the virtual resolution
            int height, width;
            float widthScale = 0, heightScale = 0;
            widthScale = (float)ScreenWidth / (float)VirtualWidth;
            heightScale = (float)ScreenHeight / (float)VirtualHeight;

            scale = Math.Min(widthScale, heightScale);

            width = (int)(VirtualWidth * scale);
            height = (int)(VirtualHeight * scale);

            // set up the new viewport centered in the backbuffer
            viewport = new Viewport
            {
                X = (ScreenWidth - width) / 2,
                Y = (ScreenHeight - height) / 2,
                Width = width,
                Height = height
            };

            game.GraphicsDevice.Viewport = viewport;
        }
    }
}