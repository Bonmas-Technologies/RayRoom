using RayRoom.Core;
using System.Numerics;

namespace RayRoom.Tests
{
    [TestClass]
    public class MathTest
    {
        [TestMethod]
        public void TestRayCollision()
        {
            Ray a = new Ray(Vector2.Zero, Vector2.UnitY);
            Ray b = new Ray(new Vector2(4, 5), -Vector2.UnitX);
            Ray c = new Ray(Vector2.One, -Vector2.UnitX);
            Ray d = new Ray(Vector2.One, Vector2.UnitX);

            Assert.IsTrue(CollisionHelpers.RaycastRayToRay(a, b, out CastInfo info));
            Console.WriteLine(info.ToString());
            Assert.IsTrue(CollisionHelpers.RaycastRayToRay(a, c, out info));
            Console.WriteLine(info.ToString());
            Assert.IsFalse(CollisionHelpers.RaycastRayToRay(a, d, out info));
            Console.WriteLine(info.ToString());
        }

        [TestMethod]
        public void TestRayToLineCollision()
        {
            Line a = new Line(Vector2.Zero, new Vector2(0, 4));
            Ray b = new Ray(new Vector2(4, 5), -Vector2.UnitX);
            Ray c = new Ray(Vector2.One, -Vector2.UnitX);
            Ray d = new Ray(Vector2.One, Vector2.UnitX);

            Assert.IsFalse(CollisionHelpers.RaycastRayToLine(a, b, out CastInfo info));
            Console.WriteLine(info.ToString());
            Assert.IsTrue(CollisionHelpers.RaycastRayToLine(a, c, out info));
            Console.WriteLine(info.ToString());
            Assert.IsFalse(CollisionHelpers.RaycastRayToLine(a, d, out info));
            Console.WriteLine(info.ToString());
        }
    }
}