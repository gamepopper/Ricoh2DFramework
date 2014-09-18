using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Ricoh2DFramework.Collisions;

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
    public class Primitives
    {
        private static Texture2D debugTexture;
        
        public static void Initialise(GraphicsDevice graphicsDevice)
        {
            debugTexture = new Texture2D(graphicsDevice, 1, 1);
            debugTexture.SetData<Color>(new Color[] { Color.White });
        }

        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle Rectangle, Color color, bool filled=false, int thickness=1)
        {
            if (filled)
                spriteBatch.Draw(debugTexture, Rectangle, color);
            else
            {
                Vector2 leftTop = new Vector2(Rectangle.Left, Rectangle.Top);
                Vector2 rightTop = new Vector2(Rectangle.Right, Rectangle.Top);
                Vector2 leftBottom = new Vector2(Rectangle.Left, Rectangle.Bottom);
                Vector2 rightBottom = new Vector2(Rectangle.Right, Rectangle.Bottom);

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

        public static void DrawLine(SpriteBatch spriteBatch, Line Line, Color color, int thickness = 1)
        {
            DrawLine(spriteBatch, Line.StartPoint, Line.EndPoint, color, thickness);
        }
        public static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color, int thickness=1)
        {
            Vector2 edge = end - start;
            float angle = (float)Math.Atan2(edge.Y, edge.X);

            spriteBatch.Draw(debugTexture, new Rectangle((int)start.X,(int)start.Y,(int)edge.Length(), thickness), null, color, angle, new Vector2(0, 0), SpriteEffects.None, 0);
        }
        public static void DrawPolygon(SpriteBatch spriteBatch, Polygon Polygon, Color color, int thickness = 1)
        {
            List<Line> lines = Polygon.getTransformedLines();

            foreach (Line l in lines)
            {
                DrawLine(spriteBatch, l, color, thickness);
            }
        }
    }
}
