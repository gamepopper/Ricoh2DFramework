using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Ricoh2DFramework;

namespace Ricoh2DFramework.Collisions
{
    public delegate void ResponseFunction();

    public class CollisionManager
    {
        public static bool Overlap(RObject object1, RObject object2, CollisionType Type = CollisionType.Box, ResponseFunction f = null)
        {
            switch (Type)
            {
                case CollisionType.Circle:
                    if (CollisionListener.CircleCollision(object1.Circle, object2.Circle))
                    {
                        callResponse(f);
                    }
                    break;
                case CollisionType.Polygon:
                    if (CollisionListener.BoxCollision(object1.Box, object2.Box))
                    {
                        if (CollisionListener.PolygonCollision(object1.Polygon, object2.Polygon))
                        {
                            callResponse(f);
                        }
                    }
                    break;
                case CollisionType.Pixel:
                    if (CollisionListener.BoxCollision(object1.Box, object2.Box))
                    {
                        if (CollisionListener.PixelCollision(object1.Transform, object1.RenderBox, object1.ColourData, object2.Transform, object2.RenderBox, object2.ColourData))
                        {
                            callResponse(f);
                        }
                    }
                    break;
                default:
                    if (CollisionListener.BoxCollision(object1.Box, object2.Box))
                    {
                        callResponse(f);
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
