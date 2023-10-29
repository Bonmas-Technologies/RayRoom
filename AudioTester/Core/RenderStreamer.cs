using NAudio.Wave;
using RayRoom.Core;

namespace AudioTester.Core
{
    internal class RenderStreamer : ISampleProvider
    {
        public WaveFormat WaveFormat { get; }

        private ISampleHandler provider;

        public RenderStreamer(ISampleHandler provider)
        {
            this.provider = provider;
            
            WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(44100, 1);
        }

        public int Read(float[] buffer, int offset, int count)
        {
            return provider.WriteTo(buffer, offset, count);
        }
    }
}
