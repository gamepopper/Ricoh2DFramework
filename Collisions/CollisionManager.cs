using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ricoh2DFramework.Collisions
{
    public class CollisionManager
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

        public static bool PolygonCollision(Rectangle boxA, Rectangle boxB, Polygon polygonA, Polygon polygonB)
        {
            if (BoxCollision(boxA, boxB))
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
            }

            return false;
        }

        private static bool LineIntersect(Line lineA, Line lineB)
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

        public static bool PixelCollision(Rectangle rectangleA, Color[] dataA, Rectangle rectangleB, Color[] dataB)
        {
            if (BoxCollision(rectangleA, rectangleB))
            {
                //Original Code by Rob Carr

                // Find the bounds of the rectangle intersection
                int top = Math.Max(rectangleA.Top, rectangleB.Top);
                int bottom = Math.Min(rectangleA.Bottom, rectangleB.Bottom);
                int left = Math.Max(rectangleA.Left, rectangleB.Left);
                int right = Math.Min(rectangleA.Right, rectangleB.Right);

                // Check every point within the intersection bounds
                for (int y = top; y < bottom; y++)
                {
                    for (int x = left; x < right; x++)
                    {
                        // Get the color of both pixels at this point
                        Color colorA = dataA[(x - rectangleA.Left) +
                                             (y - rectangleA.Top) * rectangleA.Width];
                        Color colorB = dataB[(x - rectangleB.Left) +
                                             (y - rectangleB.Top) * rectangleB.Width];

                        // If both pixels are not completely transparent,
                        if (colorA.A != 0 && colorB.A != 0)
                        {
                            // then an intersection has been found
                            return true;
                        }
                    }
                }
            }
            // No intersection found
            return false;
        }

        public static bool PixelCollision(Matrix transformA, Rectangle boxA, Color[] dataA, Matrix transformB, Rectangle boxB, Color[] dataB)
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

            // For each row of pixels in A
            for (int yA = 0; yA < boxA.Height; yA++)
            {
                // Start at the beginning of the row
                Vector2 posInB = yPosInB;

                // For each pixel in this row
                for (int xA = 0; xA < boxA.Width; xA++)
                {
                    // Round to the nearest pixel
                    int xB = (int)Math.Round(posInB.X);
                    int yB = (int)Math.Round(posInB.Y);

                    // If the pixel lies within the bounds of B
                    if (0 <= xB && xB < boxB.Width &&
                        0 <= yB && yB < boxB.Height)
                    {
                        // Get the colors of the overlapping pixels
                        Color colorA = dataA[xA + yA * boxA.Width];
                        Color colorB = dataB[xB + yB * boxB.Width];

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

                // Move to the next row
                yPosInB += stepY;
            }
            // No intersection found
            return false;
        }
    }
}
