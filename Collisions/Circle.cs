using Microsoft.Xna.Framework;

namespace Ricoh2DFramework.Collisions
{
    public class Circle
    {
        private float radius;
        public float Radius
        {
            get
            {
                if (Scale == 1)
                    return radius;
                else
                    return radius * Scale;
            }
            set
            {
                radius = value;
            }
        }

        public Vector2 Position;
        public float Scale = 1;

        public Circle(float X, float Y, float radius)
        {
            Position = new Vector2(X, Y);
            this.radius = radius;
        }

        public Circle(Vector2 position, float radius)
        {
            this.Position = position;
            this.radius = radius;
        }
    }
}
