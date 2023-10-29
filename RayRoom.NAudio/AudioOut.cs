using NAudio.Wave;
using RayRoom.Core;
using RayRoom.Structures;

namespace RayRoom.NAudioEngine
{
    public class AudioOut : ISampleProvider
    {
        public WaveFormat WaveFormat { get; }

        public ResultContainer Container
        { 
            get => container;
            
            set
            {
                updatedPosition = value;
            }
        }

        private ResultContainer updatedPosition;

        private ShiftArray<float> leftBuffer;
        private ShiftArray<float> rightBuffer;
        private Settings settings;

        private Dictionary<ISampleHandler, List<AudioSourceCollision>> sources;
        private ResultContainer container;

        public AudioOut(Settings settings)
        {
            this.settings = settings;
            sources = new Dictionary<ISampleHandler, List<AudioSourceCollision>>();
            WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(settings.frequency, 2);

            leftBuffer = new ShiftArray<float>((int)(settings.frequency * (settings.maxDistanceMeters / settings.speedOfSound + 1)));
            rightBuffer = new ShiftArray<float>((int)(settings.frequency * (settings.maxDistanceMeters / settings.speedOfSound + 1)));
        }

        public int Read(float[] buffer, int offset, int count)
        {
            container = updatedPosition;

            float velocity = 1f / container.countOfRays;

            sources.Clear();

            foreach (var collision in container.distances)
                if (sources.ContainsKey(collision.source.Handler))
                    sources[collision.source.Handler].Add(collision);
                else
                    sources.Add(collision.source.Handler, new List<AudioSourceCollision>(10) { collision });

            foreach (var source in sources)
            {
                float[] samples = new float[count / 2];
                
                int read = source.Key.WriteTo(samples, 0, count / 2);

                foreach (var collision in source.Value)
                {
                    int offsetSamples = settings.GetSamplesDelay(collision.distance) * 4; 
                    Parallel.For(0, count / 2, i =>
                    {
                        if (collision.channel == 0)
                            leftBuffer[i + offsetSamples] += samples[i] * velocity * (collision.source.Loudness / MathF.Pow(collision.distance, 2));
                        else
                            rightBuffer[i + offsetSamples] += samples[i] * velocity * (collision.source.Loudness / MathF.Pow(collision.distance, 2));

                    });
                }
            }

            Parallel.For(0, count, i =>
            {
                if (i % 2 == 0)
                {
                    buffer[i] = leftBuffer[i / 2];
                    leftBuffer[i / 2] = 0;
                }
                else
                {
                    buffer[i] = rightBuffer[(i - 1) / 2];
                    rightBuffer[(i - 1) / 2] = 0;
                }

            });

            leftBuffer.Offset += count / 2;
            rightBuffer.Offset += count / 2;

            return count;
        }
    }
}
