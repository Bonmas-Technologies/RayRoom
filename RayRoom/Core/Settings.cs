using System;

namespace RayRoom.Core
{
    public struct Settings
    {
        public readonly int frequency;
        public readonly int maxRayReflections;
        public readonly int maxDiffusionRays;

        public readonly float fadeFactorPerMeter; //curve
        public readonly float speedOfSoundMetersPerSec;

        public static Settings Default => new Settings(44100, 10, 15, 0.001f, 330f);

        public Settings(int frequency, int maxRayReflections, int maxDiffusionRays, float fadeFactorPerMeter, float speedOfSoundMetersPerSec)
        {
            this.frequency = frequency;
            this.maxRayReflections = maxRayReflections;
            this.maxDiffusionRays = maxDiffusionRays;
            this.fadeFactorPerMeter = fadeFactorPerMeter;
            this.speedOfSoundMetersPerSec = speedOfSoundMetersPerSec;
        }
    }
}
