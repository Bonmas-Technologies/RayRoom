namespace RayRoom.Core
{
    public struct Settings
    {
        public readonly int frequency;
        public readonly int maxRayReflections;
        public readonly float speedOfSound;
        public readonly float maxDistanceMeters;
        public static Settings Default => new Settings(44100, 10, 330f, 10f);

        public Settings(int frequency, int maxRayReflections, float speedOfSound, float maxDistanceMeters)
        {
            this.frequency = frequency;
            this.maxRayReflections = maxRayReflections;
            this.speedOfSound = speedOfSound;
            this.maxDistanceMeters = maxDistanceMeters;
        }

        public float GetSecoundsDelay(float distance)
        {
            return distance / speedOfSound;
        }

        public int GetSamplesDelay(float distance)
        {
            return (int)(GetSecoundsDelay(distance) * frequency);
        }
    }
}
