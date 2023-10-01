using System;

namespace RayRoom.Core
{
    public struct Settings
    {
        public readonly int frequency;
        public readonly int maxRayReflections;
        public readonly int maxDiffusionRays;

        public readonly float fadeFactorPerMeter; //qurve
        public readonly float speedOfSoundMetersPerSec;

        public static Settings GetDefault()
        {
            return new Settings(441000, 10, 15, 0.001f, 330);
        }

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
