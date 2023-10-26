using RayRoom.Core;
using System.Runtime.Intrinsics.X86;

namespace RayRoom.Platform
{
    internal class AVXSummator : IRaySummator
    {
        private float[] buffer;

        public AVXSummator()
        {
            buffer = new float[2048];

        }

        public float[] SumRays(Ray[] rays, float[] data)
        {
            if (!Avx.IsSupported)
                throw new NotSupportedException("Avx is not supported");

            throw new NotImplementedException("Avx is not implemented");
        }
    }
}
