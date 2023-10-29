namespace RayRoom.Core
{
    public struct ResultContainer
    {
        public readonly int countOfRays;
        public AudioSourceCollision[] distances;

        public ResultContainer(int countOfRays, AudioSourceCollision[] distances)
        {
            this.countOfRays = countOfRays;
            this.distances = distances;
        }
    }

    public struct AudioSourceCollision
    {
        public readonly float distance;
        public readonly AudioSource source;
        public readonly int channel;

        public AudioSourceCollision(float distance, AudioSource source, int channel)
        {
            this.distance = distance;
            this.source = source;
            this.channel = channel;
        }
    }
}
