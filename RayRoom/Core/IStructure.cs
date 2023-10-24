namespace RayRoom.Core
{
    public interface IStructure
    {
        bool CastRay(Ray ray, out CastInfo info);
    }
}