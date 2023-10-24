using System.Drawing;
using System.Numerics;

namespace AudioTester.Core.Extensions
{
    internal static class PointExtensions
    {
        public static Point GetPoint(this Vector2 vector)
        {
            return new Point((int)MathF.Round(vector.X), (int)MathF.Round(vector.Y));
        }
    }

}
