using System.Numerics;

namespace RayRoom.Core
{
    public class Circle : ICastObject
    {
        public readonly Vector2 center;
        public readonly float radius;

        public Circle(Vector2 center, float radius)
        {
            this.center = center;
            this.radius = radius;
        }

        public bool IsAudioSource => false;

        public bool CastRay(Ray ray, out CastInfo info)
        {
            return CollisionHelpers.RaycastRayToCircle(this, ray, out info);
        }
    }
}