using System;
using Microsoft.Xna.Framework;
using Ricoh2DFramework.Graphics;
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
        public Collision Collider = new Collision();
        public Vector2 Velocity;
        public Vector2 Acceleration;
        private Vector2 drag;
        public Vector2 Drag
        {
            get
            {
                return drag;
            }
            set
            {
                drag = value;
                if (drag.X < 1) drag.X = 1;
                if (drag.Y < 1) drag.Y = 1;
            }
        }

        public RObject() : base()
        {
            Velocity = Vector2.Zero;
            Acceleration = Vector2.Zero;
            Drag = Vector2.One;
        }

        public override void Update(GameTime gameTime)
        {
            if (!Collider.Immoveable)
            {
                Velocity += Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Velocity /= Drag;
                Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                Velocity = Vector2.Zero;
                Acceleration = Vector2.Zero;
                Drag = Vector2.One;
            }

            Collider.SetSize(width, height);
            Collider.SetOrigin(origin);
            Collider.SetTransform(position, rotation, scale);

            Collider.Update();

            base.Update(gameTime);
        }
    }
}
