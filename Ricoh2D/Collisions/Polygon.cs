using System.Collections.Generic;
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

namespace Ricoh2DFramework.Collisions
{
    public class Polygon
    {
        public Vector2 Position = Vector2.Zero;
        public float Rotation = 0;
        public Vector2 Scale = new Vector2(1, 1);
        public List<Line> Lines = new List<Line>();
        public int Width = 0, Height = 0;
        private int texWidth, texHeight;
        private Vector2 StartPoint = new Vector2(-999999, -999999);

        public Polygon()
        {
            
        }

        public Polygon(int width, int height)
        {
            SetTextureSize(width, height);
        }

        public void SetTextureSize(int width, int height)
        {
            texWidth = width;
            texHeight = height;
        }

        public void AddPoint(Vector2 Point)
        {
            if (StartPoint.X == -999999 && StartPoint.Y == -999999)
            {
                StartPoint = Point;
            }
            if (Lines.Count == 0)
            {
                Lines.Add(new Line(StartPoint, Point));
            }
            else if (Lines.Count > 0)
            {
                Vector2 LastEndPoint = Lines[Lines.Count - 1].EndPoint;
                if (LastEndPoint == StartPoint)
                {
                    Lines.RemoveAt(Lines.Count - 1);
                    
                    if (Lines.Count > 0)
                    {
                        LastEndPoint = Lines[Lines.Count - 1].EndPoint;
                    }
                }

                Lines.Add(new Line(LastEndPoint, Point));

                Vector2 min = Lines[0].StartPoint;
                Vector2 max = Lines[0].EndPoint;

                foreach (Line l in Lines)
                {
                    min = Vector2.Min(min, l.StartPoint);
                    min = Vector2.Min(min, l.EndPoint);
                    max = Vector2.Max(max, l.StartPoint);
                    max = Vector2.Max(max, l.EndPoint);
                }

                Width = (int)(max.X);
                Height = (int)(max.Y);
            }
        }

        public List<Line> getLines()
        {
            if (Lines[Lines.Count - 1].EndPoint != StartPoint)
            {
                Lines.Add(new Line(Lines[Lines.Count - 1].EndPoint, StartPoint));
            }

            return Lines;
        }

        public List<Line> getTransformedLines()
        {
            List<Line> nLines = getLines();
            List<Line> tLines = new List<Line>();

            for (int i = 0; i < Lines.Count; i++)
            {
                Line tLine = new Line();

                tLine.StartPoint = nLines[i].StartPoint;
                tLine.EndPoint = nLines[i].EndPoint;

                Vector2 centre = new Vector2(texWidth/2, texHeight/2);

                Matrix Transform = Matrix.Identity;
                Transform *= Matrix.CreateTranslation(new Vector3(-centre, 0));
                Transform *= Matrix.CreateScale(new Vector3(Scale, 1));
                Transform *= Matrix.CreateRotationZ(Rotation);
                Transform *= Matrix.CreateTranslation(new Vector3(Position, 0));

                tLine.Transform(Transform);
                tLines.Add(tLine);
            }

            return tLines;
        }
    }
}
