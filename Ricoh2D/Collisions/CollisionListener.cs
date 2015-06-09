using System;
using System.Collections.Generic;
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

namespace Ricoh2DFramework.Collisions
{
    public class CollisionListener
    {
        public static bool BoxCollision(Rectangle boxA, Rectangle boxB)
        {
            return boxA.Intersects(boxB);
        }

        public static bool CircleCollision(Circle circleA, Circle circleB)
        {
            float distance = Vector2.Distance(circleA.Position, circleB.Position);
            float radiusTotal = circleA.Radius + circleB.Radius;

            return (distance < radiusTotal);
        }

        public static bool PolygonCollision(Polygon polygonA, Polygon polygonB)
        {
            List<Line> LinesA = polygonA.getTransformedLines();
            List<Line> LinesB = polygonB.getTransformedLines();
            
            foreach (Line lineA in LinesA)
            {
                foreach (Line lineB in LinesB)
                {
                    if (LineIntersect(lineA, lineB))
                    {
                        return true;
                    }
                }
            }

            foreach (Line lineA in LinesA)
            {
                if (PointOverlap(lineA.StartPoint, LinesB))
                {
                    return true;
                }
            }

            foreach (Line lineB in LinesB)
            {
                if (PointOverlap(lineB.StartPoint, LinesA))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool LineIntersect(Line lineA, Line lineB)
        {
            float denom = ((lineA.StartPoint.X - lineA.EndPoint.X) * (lineB.StartPoint.Y - lineB.EndPoint.Y))
                - ((lineA.StartPoint.Y - lineA.EndPoint.Y)*(lineB.StartPoint.X - lineB.EndPoint.X));

            if (denom == 0) return false;

            float ua = (((lineB.EndPoint.X - lineB.StartPoint.X) * (lineA.StartPoint.Y - lineB.StartPoint.Y))
                - ((lineB.EndPoint.Y - lineB.StartPoint.Y) * (lineA.StartPoint.X - lineB.StartPoint.X))) / denom;

            float ub = (((lineA.EndPoint.X - lineA.StartPoint.X) * (lineA.StartPoint.Y - lineB.StartPoint.Y))
                - ((lineA.EndPoint.Y - lineA.StartPoint.Y) * (lineA.StartPoint.X - lineB.StartPoint.X))) / denom;

            if (ua < 0 || ua > 1 || ub < 0 || ub > 1) return false;

            return true;
        }

        public static bool LineIntersect(Line lineA, Line lineB, out Vector2 position)
        {
            position = Vector2.Zero;
            float denom = ((lineA.StartPoint.X - lineA.EndPoint.X) * (lineB.StartPoint.Y - lineB.EndPoint.Y))
                - ((lineA.StartPoint.Y - lineA.EndPoint.Y) * (lineB.StartPoint.X - lineB.EndPoint.X));

            if (denom == 0) return false;

            float ua = (((lineB.EndPoint.X - lineB.StartPoint.X) * (lineA.StartPoint.Y - lineB.StartPoint.Y))
                - ((lineB.EndPoint.Y - lineB.StartPoint.Y) * (lineA.StartPoint.X - lineB.StartPoint.X))) / denom;

            float ub = (((lineA.EndPoint.X - lineA.StartPoint.X) * (lineA.StartPoint.Y - lineB.StartPoint.Y))
                - ((lineA.EndPoint.Y - lineA.StartPoint.Y) * (lineA.StartPoint.X - lineB.StartPoint.X))) / denom;

            if (ua < 0 || ua > 1 || ub < 0 || ub > 1) return false;

            position = lineA.StartPoint + ((lineA.EndPoint - lineA.StartPoint) * ua);
            return true;
        }

        public static bool PointOverlap(Vector2 Point, List<Line> Polygon)
        {
            int PointIntersectCount = 0;

            Line test = new Line(Point, new Vector2(Point.X + 10000, Point.Y));

            foreach (Line L in Polygon)
            {
                if (LineIntersect(test, L))
                {
                    PointIntersectCount++;
                }
            }
            
            if (PointIntersectCount % 2 != 0)
            {
                return true;
            }

            return false;
        }

        public static bool PixelCollision(Matrix transformA, Rectangle renderBoxA, Color[] dataA, Matrix transformB, Rectangle renderBoxB, Color[] dataB)
        {
            //Original Code by Rob Carr

            // Calculate a matrix which transforms from A's local space into
            // world space and then into B's local space
            Matrix transformAToB = transformA * Matrix.Invert(transformB);

            // When a point moves in A's local space, it moves in B's local space with a
            // fixed direction and distance proportional to the movement in A.
            // This algorithm steps through A one pixel at a time along A's X and Y axes
            // Calculate the analogous steps in B:
            Vector2 stepX = Vector2.TransformNormal(Vector2.UnitX, transformAToB);
            Vector2 stepY = Vector2.TransformNormal(Vector2.UnitY, transformAToB);

            // Calculate the top left corner of A in B's local space
            // This variable will be reused to keep track of the start of each row
            Vector2 yPosInB = Vector2.Transform(Vector2.Zero, transformAToB);

            // For each column of pixels in A
            for (int yA = 0; yA < renderBoxA.Height; yA++)
            {
                // Start at the beginning of the row
                Vector2 posInB = yPosInB;

                // For each pixel in this row
                for (int xA = 0; xA < renderBoxA.Width; xA++)
                {
                    // Round to the nearest pixel
                    int xB = (int)Math.Round(posInB.X);
                    int yB = (int)Math.Round(posInB.Y);

                    // If the pixel lies within the bounds of B
                    if (0 <= xB && xB < renderBoxB.Width &&
                        0 <= yB && yB < renderBoxB.Height)
                    {
                        // Get the colors of the overlapping pixels
                        Color colorA = dataA[xA + yA * renderBoxA.Width];
                        Color colorB = dataB[xB + yB * renderBoxB.Width];

                        // If both pixels are not completely transparent,
                        if (colorA.A != 0 && colorB.A != 0)
                        {
                            // then an intersection has been found
                            return true;
                        }
                    }

                    // Move to the next pixel in the row
                    posInB += stepX;
                }

                // Move to the next column
                yPosInB += stepY;
            }
            // No intersection found
            return false;
        }
    }
}
