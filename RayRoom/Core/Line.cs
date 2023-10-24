using System.Numerics;

namespace RayRoom.Core
{
    public class Line : IStructure
    {
        public readonly Vector2 a;
        public readonly Vector2 b;
        
        public Line(Vector2 a, Vector2 b)
        {
            this.a = a;
            this.b = b;
        }

        public bool CastRay(Ray ray, out CastInfo info)
        {
            return CollisionHelpers.RaycastRayToLine(this, ray, out info);
        }
    }
}
