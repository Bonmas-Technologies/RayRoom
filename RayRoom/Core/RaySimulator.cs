using System.Numerics;

namespace RayRoom.Core
{
    public class RaySimulator
    {
        private Settings simulationSettings;

        public RaySimulator(Settings simulationSettings)
        {
            this.simulationSettings = simulationSettings;
        }

        public ResultContainer Simulate(List<ICastObject> structures, Vector2 position, int channel, int count)
        {
            count += 1;

            Ray[] startupRays = new Ray[count];

            for (int i = 0; i < count; i++)
            {
                float angle = i / (float)count * MathF.PI * 2f;
                startupRays[i] = new Ray(position, UnitVectorFromAngle(angle));
            }

            List<Ray> rays = new(startupRays);

            List<AudioSourceCollision> distances = new(count);

            while (rays.Count > 0)
            {
                var ray = rays[0];

                rays.Remove(ray);

                if (ray.bounces >= simulationSettings.maxRayReflections)
                    continue;
                if (ray.distance >= simulationSettings.maxDistanceMeters)
                    continue;
                
                CastInfo closest = CastInfo.Default;

                foreach (var structure in structures)
                    if (structure.CastRay(ray, out CastInfo info) & closest.distance > info.distance)
                        closest = info;

                if (closest.collided)
                {
                    if (closest.castObject == null)
                        throw new NullReferenceException("CastInfo collided but castObject is null");

                    if (closest.castObject.IsAudioSource)
                        distances.Add(new(closest.distance, (AudioSource)closest.castObject, channel));
                    else
                        rays.Add(new Ray(closest.point, ray.direction - 2 * Vector2.Dot(ray.direction, closest.normal) * closest.normal, closest.distance, ray.bounces + 1));
                }
            }

            return new ResultContainer(count, distances.ToArray());
        }

        private Vector2 UnitVectorFromAngle(float angle)
        {
            return new Vector2(MathF.Cos(angle), MathF.Sin(angle));
        }
    }
}
