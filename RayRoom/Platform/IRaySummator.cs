using System;
using RayRoom.Core;

namespace RayRoom.Platform
{
    internal interface IRaySummator
    {
        float[] SumRays(Ray[] rays, float[] data);
    }
}
