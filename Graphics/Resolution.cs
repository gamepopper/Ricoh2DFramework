using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ricoh2DFramework.Graphics
{
    public class Resolution  //code based off RoboBlob's method for solving resolution independance
    {
        private readonly Game game;
        private Viewport viewport;
        private Vector2 ratio;
        private Vector2 vMouse = Vector2.Zero;

        public Color bgColor = Color.Black;

        public int VirtualWidth, VirtualHeight;
        public int ScreenWidth, ScreenHeight;

        private bool dirtyMatrix;
        public bool RenderingToScreenIsFinished;
        private static Matrix scaleMatrix;

        public Resolution(Game _game, int virtualWidth = 1366, int virtualHeight = 768, int screenWidth = 1920, int screenHeight = 1080)
        {
            game = _game;

            VirtualWidth = virtualWidth;
            VirtualHeight = virtualHeight;

            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
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
            game.GraphicsDevice.Clear(bgColor);
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

            Matrix.CreateScale(
                (float)ScreenWidth / VirtualWidth,
                (float)ScreenWidth / VirtualWidth,
                1f, out scaleMatrix);

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

            float targetAspectRatio = VirtualWidth / (float)VirtualHeight;

            float width = ScreenWidth;
            float height = (int)(width / targetAspectRatio + 0.5f);

            if (height > ScreenHeight)
            {
                height = ScreenHeight;
                width = (int)(height * targetAspectRatio + 0.5f);
            }

            viewport = new Viewport(
                    (int)((ScreenWidth / 2) - (width / 2)),
                    (int)((ScreenHeight / 2) - (height / 2)),
                    (int)width,
                    (int)height);

            game.GraphicsDevice.Viewport = viewport;
        }
    }
}