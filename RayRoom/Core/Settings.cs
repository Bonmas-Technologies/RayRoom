namespace RayRoom.Core
{
    public struct Settings
    {
        public readonly int frequency;
        public readonly int maxRayReflections;
        public readonly float speedOfSound;

        public static Settings Default => new Settings(44100, 10, 330f);

        public Settings(int frequency, int maxRayReflections, float speedOfSound)
        {
            this.frequency = frequency;
            this.maxRayReflections = maxRayReflections;
            this.speedOfSound = speedOfSound;
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
