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
    public class Camera2D //code based off RoboBlob's method for solving resolution independance camera class
    {
        private float zoom;
        private float rotation;
        private Vector2 position;
        private Matrix transform = Matrix.Identity;
        private bool dirtyTransform = true;

        private Matrix camTranslate = Matrix.Identity;
        private Matrix camRotation = Matrix.Identity;
        private Matrix camScale = Matrix.Identity;
        private Matrix resTranslate = Matrix.Identity;

        protected Resolution ResIndependantRenderer;
        private Vector3 camTranslateVector = Vector3.Zero;
        private Vector3 camScaleVector = Vector3.Zero;
        private Vector3 resTranslateVector = Vector3.Zero;

        public SpriteSortMode sortMode = SpriteSortMode.Deferred;
        public BlendState blendState = BlendState.AlphaBlend;
        public SamplerState samplerState = SamplerState.LinearClamp;
        public DepthStencilState depthStencil = DepthStencilState.None;
        public RasterizerState rasterizer = RasterizerState.CullNone;
        public Effect effect = null;

        public Camera2D(Resolution resIndependant)
        {
            ResIndependantRenderer = resIndependant;

            zoom = 1f;
            rotation = 0f;
            position = Vector2.Zero;
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                dirtyTransform = true;
            }
        }

        public float Zoom
        {
            get
            {
                return zoom;
            }
            set
            {
                zoom = value;

                if (zoom < 0.1f)
                {
                    zoom = 0.1f;
                }
                dirtyTransform = true;
            }
        }

        public float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
                dirtyTransform = true;
            }
        }

        public Matrix GetViewTransformationMatrix()
        {
            if (dirtyTransform)
            {
                camTranslateVector.X = -position.X;
                camTranslateVector.Y = -position.Y;

                Matrix.CreateTranslation(ref camTranslateVector, out camTranslate);
                Matrix.CreateRotationZ(rotation, out camRotation);

                camScaleVector.X = zoom;
                camScaleVector.Y = zoom;
                camScaleVector.Z = 1;

                Matrix.CreateScale(ref camScaleVector, out camScale);

                resTranslateVector.X = ResIndependantRenderer.VirtualWidth * 0.5f;
                resTranslateVector.Y = ResIndependantRenderer.VirtualHeight * 0.5f;
                resTranslateVector.Z = 0;

                Matrix.CreateTranslation(ref resTranslateVector, out resTranslate);

                transform =
                    camTranslate *
                    camRotation *
                    camScale *
                    resTranslate *
                    ResIndependantRenderer.GetTransformedMatrix();

                dirtyTransform = false;
            }

            return transform;
        }

        public void RecalculateTransformationMatrix()
        {
            dirtyTransform = true;
        }

        public void BeginDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(sortMode, blendState, samplerState, depthStencil, rasterizer, effect, GetViewTransformationMatrix());
        }
    }
}
