using NAudio.Wave;
using RayRoom.Core;

namespace AudioTester
{
    internal static class RayRoomSettings
    {
        public static Settings GetSettingsFromWaveFromat(WaveFormat format, Settings baseSettings)
        {
            return new Settings(format.SampleRate, 
                baseSettings.maxRayReflections, 
                baseSettings.maxDiffusionRays, 
                baseSettings.fadeFactorPerMeter, 
                baseSettings.speedOfSoundMetersPerSec);
        }
    }
}
