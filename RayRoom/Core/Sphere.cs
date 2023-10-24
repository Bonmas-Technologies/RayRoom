using System.Numerics;

namespace RayRoom.Core
{
    public class Sphere : IStructure
    {
        public readonly Vector2 center; 
        public readonly float radius;

        public Sphere(Vector2 center, float radius)
        {
            this.center = center;
            this.radius = radius;
        }

        public bool CastRay(Ray ray, out CastInfo info)
        {
            return CollisionHelpers.RaycastRayToSphere(this, ray, out info);
        }
    }
}