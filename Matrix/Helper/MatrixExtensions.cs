using Matrix.Models;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace Matrix.Utilities
{
	public static class MatrixExtensions
	{
		public static float ToAngle(this Vector2 vector)
		{
			return (float)Math.Atan2(vector.Y, vector.X);
		}

		public static Vector2 ScaleTo(this Vector2 vector, float length)
		{
			return vector * (length / vector.Length());
		}

		public static Point ToPoint(this Vector2 vector)
		{
			return new Point((int)vector.X, (int)vector.Y);
		}

		public static float NextFloat(this Random rand, float minValue, float maxValue)
		{
			return (float)rand.NextDouble() * (maxValue - minValue) + minValue;
		}

		public static Vector2 NextVector2(this Random rand, float minLength, float maxLength)
		{
			double theta = rand.NextDouble() * 2 * Math.PI;
			float length = rand.NextFloat(minLength, maxLength);
			return new Vector2(length * (float)Math.Cos(theta), length * (float)Math.Sin(theta));
		}

		public static Texture2D GetRandomTexture()
		{
			Random random = new Random();
			List<Texture2D> textures = new List<Texture2D>()
			{
				Arts.EnemyBlack,
				Arts.EnemyBlood,
				Arts.EnemyBlue,
				Arts.EnemyGreen,
			};
			return textures[random.Next(0, textures.Count)];
		}
	}
}
