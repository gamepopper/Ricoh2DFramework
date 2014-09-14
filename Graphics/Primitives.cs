using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Ricoh2DFramework.Collisions;

namespace Ricoh2DFramework.Graphics
{
    public class Primitives
    {
        private static Texture2D debugTexture;
        
        public static void Initialise(GraphicsDevice graphicsDevice)
        {
            debugTexture = new Texture2D(graphicsDevice, 1, 1);
            debugTexture.SetData<Color>(new Color[] { Color.White });
        }

        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color, bool filled=false, int thickness=1)
        {
            if (filled)
                spriteBatch.Draw(debugTexture, rectangle, color);
            else
            {
                Vector2 leftTop = new Vector2(rectangle.Left, rectangle.Top);
                Vector2 rightTop = new Vector2(rectangle.Right, rectangle.Top);
                Vector2 leftBottom = new Vector2(rectangle.Left, rectangle.Bottom);
                Vector2 rightBottom = new Vector2(rectangle.Right, rectangle.Bottom);

                DrawLine(spriteBatch, leftTop, rightTop, color, thickness);
                DrawLine(spriteBatch, rightTop, rightBottom, color, thickness);
                DrawLine(spriteBatch, rightBottom, leftBottom, color, thickness);
                DrawLine(spriteBatch, leftBottom, leftTop, color, thickness);
            }
        }

        public static void DrawCircle(SpriteBatch spriteBatch, Circle Circle, Color color, bool filled=false, int thickness=1)
        {
            DrawCircle(spriteBatch, Circle.Position, (int)Circle.Radius, color, filled, thickness);
        }
        public static void DrawCircle(SpriteBatch spriteBatch, Vector2 Position, int Radius, Color color, bool filled=false, int thickness=1)
        {
            if (filled)
            {

            }
            else
            {
                int x = Radius;
                int y = 0;
                int RadiusError = 1 - x;

                while (x >= y)
                {
                    DrawPixel(spriteBatch, new Vector2(x + Position.X, y + Position.Y), color);
                    DrawPixel(spriteBatch, new Vector2(y + Position.X, x + Position.Y), color);
                    DrawPixel(spriteBatch, new Vector2(-x + Position.X, y + Position.Y), color);
                    DrawPixel(spriteBatch, new Vector2(-y + Position.X, x + Position.Y), color);
                    DrawPixel(spriteBatch, new Vector2(-x + Position.X, -y + Position.Y), color);
                    DrawPixel(spriteBatch, new Vector2(-y + Position.X, -x + Position.Y), color);
                    DrawPixel(spriteBatch, new Vector2(x + Position.X, -y + Position.Y), color);
                    DrawPixel(spriteBatch, new Vector2(y + Position.X, -x + Position.Y), color);

                    y++;
                    if (RadiusError < 0)
                    {
                        RadiusError += 2 * y + 1;
                    }
                    else
                    {
                        x--;
                        RadiusError += 2 * (y - x + 1);
                    }
                }

                if (thickness > 1)
                {
                    DrawCircle(spriteBatch, Position, Radius - 1, color, false, thickness - 1);
                }
            }
        }

        public static void DrawPixel(SpriteBatch spriteBatch, Vector2 Position, Color color)
        {
            DrawRectangle(spriteBatch, new Rectangle((int)Position.X, (int)Position.Y, 1, 1), color, true);
        }

        public static void DrawLine(SpriteBatch spriteBatch, Line line, Color color, int thickness = 1)
        {
            DrawLine(spriteBatch, line.StartPoint, line.EndPoint, color, thickness);
        }
        public static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color, int thickness=1)
        {
            Vector2 edge = end - start;
            float angle = (float)Math.Atan2(edge.Y, edge.X);

            spriteBatch.Draw(debugTexture, new Rectangle((int)start.X,(int)start.Y,(int)edge.Length(), thickness), null, color, angle, new Vector2(0, 0), SpriteEffects.None, 0);
        }
    }
}
