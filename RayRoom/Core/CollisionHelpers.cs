using System.Numerics;

namespace RayRoom.Core
{
    public static class CollisionHelpers
    {
        private static float minimalThreshold = 0.001f;

        public struct Distances
        {
            public readonly float a;
            public readonly float b;

            public Distances(float a, float b)
            {
                this.a = a;
                this.b = b;
            }
        }

        /// <summary>
        /// Calculates ray collision
        /// </summary>
        /// <param name="a">static base ray</param>
        /// <param name="b">raycaster</param>
        /// <param name="info">collision info for b ray</param>
        /// <returns>Distance modifiers for ray elements</returns>
        public static Distances TestRays(Ray a, Ray b, out CastInfo info)
        {
            a = new Ray(a.position, Vector2.Normalize(a.direction));
            b = new Ray(b.position, Vector2.Normalize(b.direction));

            float dx = b.position.X - a.position.X;
            float dy = b.position.Y - a.position.Y;

            float det = b.direction.X * a.direction.Y - b.direction.Y * a.direction.X;

            float u = (dy * b.direction.X - dx * b.direction.Y) / det;
            float v = (dy * a.direction.X - dx * a.direction.Y) / det;

            Vector2 normal = GetClosestNormal(a.direction, b.direction);

            info = new CastInfo(b.position + b.direction * v, normal, (v * b.direction).Length(), true);

            return new Distances(u, v);
        }

        public static Distances TestSphereToRay(Circle a, Ray b, out CastInfo info)
        {
            Vector2 position = b.position - a.center;

            float vb = 2 * b.direction.X * position.X + 2 * b.direction.Y * position.Y;
            float va = MathF.Pow(b.direction.X, 2) + MathF.Pow(b.direction.Y, 2);
            float vc = MathF.Pow(position.X, 2) + MathF.Pow(position.Y, 2) - MathF.Pow(a.radius, 2);

            float discr = MathF.Pow(vb, 2) - 4 * va * vc;

            Distances result;

            if (discr >= 0)
            {
                float t0 = (-vb - MathF.Sqrt(discr)) / (va * 2);
                float t1 = (-vb + MathF.Sqrt(discr)) / (va * 2);

                float distance;

                if (MathF.Abs(t0) < MathF.Abs(t1))
                    distance = t0;
                else
                    distance = t1;

                Vector2 normal = (b.position + b.direction * distance - a.center) / a.radius * -1f;

                info = new CastInfo(b.position + b.direction * distance, normal, (b.direction * distance).Length(), true);
                result = new Distances(t0, t1);
            }
            else
            {
                info = CastInfo.Default;
                result = new Distances(0, 0);
            }

            return result;
        }

        private static Vector2 GetClosestNormal(Vector2 collision, Vector2 direction)
        {
            Vector2 n0 = new Vector2(collision.Y, -collision.X);
            Vector2 n1 = new Vector2(-collision.Y, collision.X);

            float t0 = Vector2.Dot(n0, direction);
            float t1 = Vector2.Dot(n1, direction);

            if (t0 < t1)
                return n0;
            else
                return n1;
        }

        public static bool RaycastRayToSphere(Circle sphere, Ray b, out CastInfo info)
        {
            var distances = TestSphereToRay(sphere, b, out info);

            if (distances.a < minimalThreshold || distances.b < minimalThreshold)
            {
                info = CastInfo.Default;
                return false;
            }

            return info.collided;
        }

        public static bool RaycastRayToLine(Line aLine, Ray b, out CastInfo info)
        {
            Ray a = new Ray(aLine.a, aLine.b - aLine.a);

            var result = TestRays(a, b, out info);

            bool output = result.b >= 0 && result.a >= 0 && result.a <= (aLine.b - aLine.a).Length();

            if (result.a < minimalThreshold || result.b < minimalThreshold)
                output = false;

            if (!output)
                info = CastInfo.Default;

            return output;
        }

        public static bool RaycastRayToRay(Ray a, Ray b, out CastInfo info)
        {
            var result = TestRays(a, b, out info);

            bool output = result.b >= 0 && result.a >= 0;

            if (result.a < minimalThreshold || result.b < minimalThreshold)
                output = false;

            if (!output)
                info = CastInfo.Default;

            return output;

        }
    }
}