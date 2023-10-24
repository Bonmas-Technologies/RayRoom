using NAudio.Codecs;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace AudioTester.Core
{
    internal class RenderStreamer : ISampleProvider
    {
        public WaveFormat WaveFormat { get; }

        private const int ReadBufferSize = 1024;
        private int sample;
        private Random rand;

        private AudioFileReader streamReader;
        private ISampleProvider provider;

        public RenderStreamer()
        {
            rand = new Random();
            WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(44100, 2);
            sample = 0;

            streamReader = new AudioFileReader(@"Resources\test.wav");
            
            provider = streamReader.ToSampleProvider();
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int length = provider.Read(buffer, offset, count); 
            
            Console.WriteLine("i: {0}; o:{1}", count, length);
            
            if (length < count)
            {
                streamReader.Position = 0;
                length += provider.Read(buffer, length, count - length);
            }

            return length;
        }
    }
}
