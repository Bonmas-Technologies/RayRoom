using RayRoom.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

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
