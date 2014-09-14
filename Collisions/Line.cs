using Microsoft.Xna.Framework;

namespace Ricoh2DFramework.Collisions
{
    public class Line
    {
        public Vector2 StartPoint;
        public Vector2 EndPoint;

        public Line()
        {
            StartPoint = Vector2.Zero;
            EndPoint = Vector2.Zero;
        }

        public Line(Vector2 Start, Vector2 End)
        {
            StartPoint = Start;
            EndPoint = End;
        }

        public void Transform(Matrix Transform)
        {
            StartPoint = Vector2.Transform(StartPoint, Transform);
            EndPoint = Vector2.Transform(EndPoint, Transform);
        }
    }
}
