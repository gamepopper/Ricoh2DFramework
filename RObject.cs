using System;
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

namespace Ricoh2DFramework
{
    public class RObject : RBasic
    {
        public Matrix Transform;

        protected virtual Vector2 centre
        {
            get
            {
                if (origin.X == width / 2 && origin.Y == height / 2)
                {
                    return origin;
                }

                Vector2 centre = new Vector2(width / 2, height / 2);

                Matrix transform = Matrix.Identity *
                    Matrix.CreateTranslation(-origin.X, -origin.Y, 0) *
                    Matrix.CreateScale(new Vector3(scale, 1)) *
                    Matrix.CreateRotationZ(rotation) *
                    Matrix.CreateTranslation(origin.X, origin.Y, 0);

                return Vector2.Transform(centre, transform);
            }
        }
        public Vector2 PositionCentre
        {
            get
            {
                return position - origin + centre;
            }
        }

        public Vector2 Velocity;
        public Vector2 Acceleration;

        protected Rectangle collisionBox;
        protected Circle collisionCircle;
        protected Polygon collisionPolygon;
        protected Color[] colourData;

        public virtual Rectangle RenderBox
        {
            get
            {
                return new Rectangle(0, 0, width, height);
            }
        }
        public virtual Rectangle Box
        {
            get
            {
                if (rotation == 0 && scale.X == 1 && scale.Y == 1)
                {
                    return collisionBox;
                }
                else
                {
                    Matrix boxTransform = Matrix.Identity;
                    boxTransform *= Matrix.CreateTranslation(new Vector3(-position, 0));
                    boxTransform *= Matrix.CreateScale(new Vector3(scale, 1));
                    boxTransform *= Matrix.CreateRotationZ(rotation);
                    boxTransform *= Matrix.CreateTranslation(new Vector3(position, 0));

                    // From AustinCC.edu
                    // Get all four corners in local space
                    Vector2 leftTop = new Vector2(collisionBox.Left, collisionBox.Top);
                    Vector2 rightTop = new Vector2(collisionBox.Right, collisionBox.Top);
                    Vector2 leftBottom = new Vector2(collisionBox.Left, collisionBox.Bottom);
                    Vector2 rightBottom = new Vector2(collisionBox.Right, collisionBox.Bottom);

                    // Transform all four corners into work space
                    Vector2.Transform(ref leftTop, ref boxTransform, out leftTop);
                    Vector2.Transform(ref rightTop, ref boxTransform, out rightTop);
                    Vector2.Transform(ref leftBottom, ref boxTransform, out leftBottom);
                    Vector2.Transform(ref rightBottom, ref boxTransform, out rightBottom);

                    // Find the minimum and maximum extents of the rectangle in world space
                    Vector2 min = Vector2.Min(Vector2.Min(leftTop, rightTop),
                                              Vector2.Min(leftBottom, rightBottom));
                    Vector2 max = Vector2.Max(Vector2.Max(leftTop, rightTop),
                                              Vector2.Max(leftBottom, rightBottom));

                    // Return as a rectangle
                    return new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
                }
            }
        }
        public virtual Circle Circle
        {
            get { return collisionCircle; }
        }
        public virtual Polygon Polygon
        {
            get { return collisionPolygon; }
        }
        public virtual Color[] ColourData
        {
            get { return colourData; }
        }

        protected int collisionOffset = 0;
        public virtual int CollisionOffset
        {
            get { return collisionOffset; }
            set
            {
                collisionOffset = value;
                dirtyTransform = true;
            }
        }

        public bool immoveable = false;
        public bool solid = false;

        public RObject()
            : base()
        {
            Transform = Matrix.Identity;

            collisionBox = new Rectangle((int)(position.X - origin.X), (int)(position.Y - origin.Y), width, height);
            collisionCircle = new Circle(position, width / 2);
            collisionPolygon = new Polygon(this.width, this.height);
            colourData = new Color[0];

            Velocity = Vector2.Zero;
            Acceleration = Vector2.Zero;
        }

        public override void Update(GameTime gameTime)
        {
            if (!immoveable)
            {
                Velocity += Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                Velocity = Vector2.Zero;
                Acceleration = Vector2.Zero;
            }

            base.Update(gameTime);
        }
    }
}
