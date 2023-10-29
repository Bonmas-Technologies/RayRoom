using System.Numerics;

namespace RayRoom.Core
{
    public interface ISampleHandler
    {
        int WriteTo(float[] buffer, int offset, int count);
    }


    public class AudioSource : ICastObject
    {
        public const float size = 1f;

        public bool IsAudioSource => true;
        public float Loudness => loudness;
        public ISampleHandler Handler => handler;

        private Circle source;
        private float loudness;
        private ISampleHandler handler;

        public AudioSource(Vector2 position, float loudness, ISampleHandler handler)
        {
            source = new Circle(position, size);
            this.loudness = loudness;
            this.handler = handler;
        }

        public bool CastRay(Ray ray, out CastInfo info)
        {
            var result = CollisionHelpers.RaycastRayToCircle(source, ray, out info);

            info = new CastInfo(info.point, info.normal, info.distance, info.collided, this);

            return result;
        }
    }
}
