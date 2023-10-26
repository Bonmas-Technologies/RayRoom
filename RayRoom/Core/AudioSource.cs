using System.Numerics;

namespace RayRoom.Core
{
    public interface ISampleHandler
    {
        int Read(float[] buffer, int offset, int count);
    }


    public class AudioSource : ICastObject
    {
        public const float size = 1.0f;

        public bool IsAudioSource => true;

        private Circle source;
        private float strength;

        public AudioSource(Vector2 position, float strength)
        {
            source = new Circle(position, strength);
            this.strength = strength;
        }


        public bool CastRay(Ray ray, out CastInfo info)
        {
            return source.CastRay(ray, out info);
        }
    }
}
