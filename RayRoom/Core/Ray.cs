﻿using System.Numerics;

namespace RayRoom.Core
{
    public class Ray
    {
        public readonly Vector2 position;
        public readonly Vector2 direction;
        public readonly float distance;
        public readonly int bounces;

        public Ray(Vector2 position, Vector2 direction, float distance = 0, int bounces = 0)
        {
            this.position = position;
            this.direction = direction;
            this.distance = distance;
            this.bounces = bounces;
        }

        public override string? ToString()
        {
            return string.Format("pos:{0} dir:{1} dist:{2}", position.ToString(), direction.ToString(), distance);
        }
    }
}