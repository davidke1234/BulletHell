using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Matrix.Controllers
{
    static class ProjectileManager
    {
        public static List<Vector2> GetCircleCoordinates(float startX, float startY, float radius)
        {
            List<Vector2> coordinates = new List<Vector2>();
            const float PI = 3.1415926535f;
            float angle, x1, y1;

            for (float i = 0; i < 360; i += 20f)
            {
                angle = i;
                x1 = (float)radius * (float)Math.Cos(angle * PI / 180);
                y1 = radius * (float)Math.Sin(angle * PI / 180);

                Vector2 vector2 = new Vector2(startX + x1, startY + y1);
                coordinates.Add(vector2);
            }

            return coordinates;
        }
    }
}
