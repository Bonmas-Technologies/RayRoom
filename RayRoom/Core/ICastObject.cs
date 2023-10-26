namespace RayRoom.Core
{
    public interface ICastObject
    {
        bool IsAudioSource { get; }

        bool CastRay(Ray ray, out CastInfo info);
    }
}