using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Ricoh2DFramework;
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

namespace Ricoh2DFramework.Collisions
{
    public enum CollisionType
    {
        Box,
        Circle,
        Polygon,
        Pixel
    }

    public delegate void ResponseFunction();

    public class CollisionManager
    {
        public static bool Overlap(Collision collider1, Collision collider2, CollisionType Type = CollisionType.Box, ResponseFunction f = null)
        {
            switch (Type)
            {
                case CollisionType.Circle:
                    if (CollisionListener.CircleCollision(collider1.Circle, collider2.Circle))
                    {
                        return callResponse(f);
                    }
                    break;
                case CollisionType.Polygon:
                    if (CollisionListener.BoxCollision(collider1.Box, collider2.Box))
                    {
                        if (CollisionListener.PolygonCollision(collider1.Polygon, collider2.Polygon))
                        {
                            return callResponse(f);
                        }
                    }
                    break;
                case CollisionType.Pixel:
                    if (CollisionListener.BoxCollision(collider1.Box, collider2.Box))
                    {
                        if (CollisionListener.PixelCollision(collider1.Transform, collider1.RenderBox, collider1.ColourData, collider2.Transform, collider2.RenderBox, collider2.ColourData))
                        {
                            return callResponse(f);
                        }
                    }
                    break;
                default:
                    if (CollisionListener.BoxCollision(collider1.Box, collider2.Box))
                    {
                        return callResponse(f);
                    }
                    break;
            }
            
            return false;
        }

        public static bool Collide(Collision collider1, Collision collider2, CollisionType Type = CollisionType.Box, ResponseFunction f = null)
        {
            switch (Type)
            {
                case CollisionType.Circle:
                    if (CollisionListener.CircleCollision(collider1.Circle, collider2.Circle))
                    {
                        return callResponse(f);
                    }
                    break;
                case CollisionType.Polygon:
                    if (CollisionListener.BoxCollision(collider1.Box, collider2.Box))
                    {
                        if (CollisionListener.PolygonCollision(collider1.Polygon, collider2.Polygon))
                        {
                            return callResponse(f);
                        }
                    }
                    break;
                case CollisionType.Pixel:
                    if (CollisionListener.BoxCollision(collider1.Box, collider2.Box))
                    {
                        if (CollisionListener.PixelCollision(collider1.Transform, collider1.RenderBox, collider1.ColourData, collider2.Transform, collider2.RenderBox, collider2.ColourData))
                        {
                            return callResponse(f);
                        }
                    }
                    break;
                default:
                    if (CollisionListener.BoxCollision(collider1.Box, collider2.Box))
                    {
                        return callResponse(f);
                    }
                    break;
            }

            return false;
        }

        private static bool callResponse(ResponseFunction f)
        {
            if (f != null)
                f();

            return true;
        }
    }
}
