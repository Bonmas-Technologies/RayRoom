using System.Numerics;

namespace RayRoom.Core
{
    public struct CastInfo
    {
        public readonly Vector2 point;
        public readonly Vector2 normal;
        public readonly float distance;
        public readonly bool collided;
        public static CastInfo Default => new CastInfo(Vector2.Zero, Vector2.Zero, float.MaxValue, false);

        public CastInfo(Vector2 point, Vector2 normal, float distance, bool collided)
        {
            this.point = point;
            this.normal = normal;
            this.distance = distance;
            this.collided = collided;
        }

        public override string ToString()
        {
            return string.Format("p:{0} n:{1} d:{2} c:{3}", point.ToString(), normal.ToString(), distance.ToString(), collided.ToString());
        }
    }
}