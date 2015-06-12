using System;
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

        public virtual SpriteEffects SpriteEffects
        {
            get
            {
                return spriteEffects;
            }
            set
            {
                spriteEffects = value;
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
            scale = Vector2.One;
            spriteEffects = SpriteEffects.None;
            color = Color.White;
            opacity = 1;
            depth = 0;

            width = height = 0;

            origin = Vector2.Zero;

            dirtyTransform = true;
        }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}
