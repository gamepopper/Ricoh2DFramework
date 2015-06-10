using System;
using Microsoft.Xna.Framework;
using Ricoh2DFramework.Collisions;

namespace Ricoh2DFramework.Collisions
{
    public class Collision
    {
        private Matrix transform;
        private Rectangle collisionBox;
        private Circle collisionCircle;
        private Polygon collisionPolygon;
        private Color[] colourData;
        private Vector2 origin;
        private Vector2 position;
        private Vector2 scale;
        private float rotation;
        private int width;
        private int height;
        private bool dirtyTransform = false;

        public Collision()
        {
            collisionBox = new Rectangle();
            collisionCircle = new Circle();
            collisionPolygon = new Polygon();
        }

        public void SetSize(int height, int width)
        {
            if (this.width != width || this.height != height)
            {
                this.width = width;
                this.height = height;
                collisionPolygon.SetTextureSize(width, height);
                dirtyTransform = true;
            }
        }

        public void SetOrigin(Vector2 origin)
        {
            if (this.origin != origin)
            {
                this.origin = origin;
                dirtyTransform = true;
            }
        }

        public void SetTransform(Vector2 position, float rotation, Vector2 scale)
        {
            if (this.position != position || this.rotation != rotation || this.scale != scale)
            {
                this.position = position;
                this.rotation = rotation;
                this.scale = scale;
                dirtyTransform = true;
            }
        }

        public Matrix Transform
        {
            get
            {
                return transform;
            }
        }
        public bool IsDirty
        {
            get
            {
                return dirtyTransform;
            }
        }
        private Vector2 centre
        {
            get
            {
                if (origin.X == width / 2 && origin.Y == height / 2)
                {
                    return origin;
                }

                Vector2 centre = new Vector2(width / 2, height / 2);

                Matrix t = Matrix.Identity *
                    Matrix.CreateTranslation(-origin.X, -origin.Y, 0) *
                    Matrix.CreateScale(new Vector3(scale, 1)) *
                    Matrix.CreateRotationZ(rotation) *
                    Matrix.CreateTranslation(origin.X, origin.Y, 0);

                return Vector2.Transform(centre, t);
            }
        }
        public Vector2 PositionCentre
        {
            get
            {
                return position - origin + centre;
            }
        }
        public Rectangle RenderBox
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
                    Matrix t = Matrix.Identity;
                    t *= Matrix.CreateTranslation(new Vector3(-position, 0));
                    t *= Matrix.CreateScale(new Vector3(scale, 1));
                    t *= Matrix.CreateRotationZ(rotation);
                    t *= Matrix.CreateTranslation(new Vector3(position, 0));

                    // From AustinCC.edu
                    // Get all four corners in local space
                    Vector2 leftTop = new Vector2(collisionBox.Left, collisionBox.Top);
                    Vector2 rightTop = new Vector2(collisionBox.Right, collisionBox.Top);
                    Vector2 leftBottom = new Vector2(collisionBox.Left, collisionBox.Bottom);
                    Vector2 rightBottom = new Vector2(collisionBox.Right, collisionBox.Bottom);

                    // Transform all four corners into work space
                    Vector2.Transform(ref leftTop, ref t, out leftTop);
                    Vector2.Transform(ref rightTop, ref t, out rightTop);
                    Vector2.Transform(ref leftBottom, ref t, out leftBottom);
                    Vector2.Transform(ref rightBottom, ref t, out rightBottom);

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
            set { colourData = value; }
        }

        protected int offset = 0;
        public virtual int Offset
        {
            get { return offset; }
            set
            {
                offset = value;
            }
        }

        public void Update()
        {
            if (dirtyTransform)
            {
                transform = Matrix.Identity;
                transform *= Matrix.CreateTranslation(new Vector3(-origin, 0));
                transform *= Matrix.CreateScale(new Vector3(scale, 1));
                transform *= Matrix.CreateRotationZ(rotation);
                transform *= Matrix.CreateTranslation(new Vector3(position, 0));

                Vector2 centrePos = PositionCentre;

                collisionBox.X = (int)(position.X - origin.X);
                collisionBox.Y = (int)(position.Y - origin.Y);
                collisionBox.Width = width;
                collisionBox.Height = height;
                collisionBox.Inflate(offset, offset);

                collisionCircle.Position = centrePos;
                collisionCircle.Radius = (width / 2) + offset;
                collisionCircle.Radius *= (scale.X + scale.Y) / 2;

                collisionPolygon.Position = centrePos;
                collisionPolygon.Rotation = rotation;
                collisionPolygon.Scale = scale;

                dirtyTransform = false;
            }
        }

        public bool Immoveable = false;
        public bool Solid = false;
    }
}
