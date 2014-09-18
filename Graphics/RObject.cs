﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Ricoh2DFramework.Collisions;

namespace Ricoh2DFramework.Graphics
{
    public class RObject : RBasic
    {
        public Matrix Transform;

        public Vector2 Velocity;
        public Vector2 Acceleration;

        protected Rectangle collisionBox;
        protected Circle collisionCircle;
        protected Polygon collisionPolygon;

        public virtual Rectangle RenderBox
        {
            get
            {
                return new Rectangle((int)(position.X - origin.X), (int)(position.Y - origin.Y), width, height);
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
                    return new Rectangle((int)min.X, (int)min.Y,
                                         (int)(max.X - min.X), (int)(max.Y - min.Y));
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
        public Color[] ColourData;

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
            ColourData = new Color[0];

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
