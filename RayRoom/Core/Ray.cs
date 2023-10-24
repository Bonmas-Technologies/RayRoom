using System.ComponentModel;
using System.Numerics;

namespace RayRoom.Core
{
    public class Ray
    {
        public readonly Vector2 position;
        public readonly Vector2 direction;

        public Ray(Vector2 position, Vector2 direction)
        {
            this.position = position;
            this.direction = direction;
        }

        public override string? ToString()
        {
            return string.Format("p:{0} d:{1}", position.ToString(), direction.ToString());
        }
    }
}