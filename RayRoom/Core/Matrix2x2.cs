using System.Numerics;

namespace AudioTester
{
    public struct Matrix2x2
    {
        float a, b, c, d;

        public static Matrix2x2 Identity => new Matrix2x2(Vector2.UnitX, Vector2.UnitY);
        
        public Matrix2x2(float a, float b, float c, float d)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
        }

        public Matrix2x2(Vector2 a, Vector2 b)
        {
            this.a = a.X;
            this.b = b.X;
            this.c = a.Y;
            this.d = b.Y;
        }

        public readonly float Determinant => (a*d) - (b*c);

        public static Vector2 operator*(Matrix2x2 matrix, Vector2 vector)
        {
            return new Vector2(matrix.a, matrix.c) * vector.X + new Vector2(matrix.b, matrix.d) * vector.Y;
        }

        public static Matrix2x2 operator*(Matrix2x2 a, Matrix2x2 b)
        {
            return new Matrix2x2(b * new Vector2(a.a, a.c), b * new Vector2(a.b, a.d));
        }
    }
}