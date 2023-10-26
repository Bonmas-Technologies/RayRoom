using System.Numerics;

namespace RayRoom.Core
{
    public class AudioSimulator
    {
        private Settings simulationSettings;

        public AudioSimulator(Settings simulationSettings)
        {
            this.simulationSettings = simulationSettings;
        }

        public Line[] Simulate(Vector2 position, int count)
        {
            count += 1;
            Ray[] startupRays = new Ray[count];
            for (int i = 0; i < count; i++)
            {
                float angle = i / (float)count * MathF.PI * 2f;
                startupRays[i] = new Ray(position, UnitVectorFromAngle(angle));
            }

            List<ICastObject> structures = new List<ICastObject>
            {
                new Circle(new Vector2(0, -3), 2f),
                new Circle(new Vector2(5, 3), 2f),
                new Circle(new Vector2(-0.5f, 0.5f), 0.5f),
                new Line(new Vector2(-10, -10), new Vector2(0, 10)),
                new Line(new Vector2(5, 3), new Vector2(0, 10)),
                new Line(new Vector2(-10, -10), new Vector2(0, -3)),
                new Line(new Vector2(0, -3), new Vector2(5, 3)),
            };

            List<Ray> rays = new(startupRays);

            List<Line> output = new(count);

            int castCount = count * 10;

            while (rays.Count > 0 && castCount-- > 0)
            {
                var ray = rays[0];

                rays.Remove(ray);

                if (ray.bounces >= simulationSettings.maxRayReflections)
                    continue;

                CastInfo closest = CastInfo.Default;

                foreach (var structure in structures)
                {
                    if (structure.CastRay(ray, out CastInfo info))
                        if (closest.distance > info.distance)
                            closest = info;
                }

                if (closest.collided)
                {
                    output.Add(new Line(ray.position, closest.point));
                    rays.Add(new Ray(closest.point, ray.direction - 2 * Vector2.Dot(ray.direction, closest.normal) * closest.normal, closest.distance, ray.bounces + 1));
                }
            }

            return output.ToArray();
        }

        private Vector2 UnitVectorFromAngle(float angle)
        {
            return new Vector2(MathF.Cos(angle), MathF.Sin(angle));
        }
    }
}
