using NAudio.Wave;
using RayRoom.Core;

namespace RayRoom.NAudioEngine
{
    public class AudioSampler : ISampleHandler, IDisposable
    {
        public bool Loop { get; set; }

        private bool disposed;

        private AudioFileReader reader;
        private ISampleProvider provider;

        public AudioSampler(string file, bool looping = false)
        {
            disposed = false;
            reader = new AudioFileReader(file);
            Loop = looping;

            provider = reader.ToSampleProvider();
            provider = provider.ToMono();
        }

        public int WriteTo(float[] buffer, int offset, int count)
        {
            if (disposed) throw new ObjectDisposedException("AudioSampler is disposed");

            int length = provider.Read(buffer, offset, count);

            if (Loop && length < count)
            {
                reader.Position = 0;
                length += provider.Read(buffer, length, count - length);
            }

            return length;
        }

        public void Dispose()
        {
            reader.Close();
            reader.Dispose();
        }
    }
}