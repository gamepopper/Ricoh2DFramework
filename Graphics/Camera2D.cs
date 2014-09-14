using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        public SamplerState samplerState = SamplerState.LinearWrap;
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
            if (dirtyTransform) //Only converts the matrix when coordinates, angle or zoom has changed.
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
