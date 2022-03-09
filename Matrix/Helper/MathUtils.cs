using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Matrix.Utilities
{
    public static class MathUtils
    {
        public static Vector2 FromPolar(float angle, float magnitude)
        {
            return magnitude * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }
    }
}
