using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ricoh2DFramework
{
    public class RBasic
    {
        protected Vector2 position;
        protected float rotation;
        protected Vector2 scale;
        protected SpriteEffects spriteEffects;
        protected Color color;
        protected float opacity;
        protected float depth;

        protected int width;
        protected int height;
        protected Vector2 origin;

        public virtual Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                dirtyTransform = true;
            }
        }
        public virtual float Rotation
        {
            get { return rotation; }
            set
            {
                rotation = value;

                if (rotation >= 2 * MathHelper.Pi)
                    rotation -= 2 * MathHelper.Pi;
                else if (rotation < 0)
                    rotation += 2 * MathHelper.Pi;

                dirtyTransform = true;
            }
        }
        public virtual Vector2 Scale
        {
            get { return scale; }
            set
            {
                scale = value;
                dirtyTransform = true;
            }
        }

        public virtual Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public virtual float Opacity
        {
            get { return opacity; }
            set { opacity = value; }
        }
        public virtual float Depth
        {
            get { return depth; }
            set { depth = value; }
        }

        public virtual int Width
        {
            get { return width; }
            set
            {
                width = value;
                dirtyTransform = true;
            }
        }
        public virtual int Height
        {
            get { return height; }
        }
        public virtual Vector2 Origin
        {
            get { return origin; }
            set
            {
                origin = value;
            }
        }

        protected bool dirtyTransform = true;

        public RBasic()
        {
            position = Vector2.Zero;
            rotation = 0;
            scale = new Vector2(1, 1);
            spriteEffects = SpriteEffects.None;
            color = Color.White;
            opacity = 1;
            depth = 0;

            width = height = 0;

            origin = Vector2.Zero;

            dirtyTransform = true;
        }

        protected virtual void DirtyTransform()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            if (dirtyTransform)
            {
                DirtyTransform();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
