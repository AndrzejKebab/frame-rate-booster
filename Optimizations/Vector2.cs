using System;
using System.Runtime.CompilerServices;

namespace ToolBuddy.FrameRateBooster.Optimizations
{
	internal struct Vector2
	{
		public float x;
		public float y;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator +(Vector2 a, Vector2 b)
		{
			a.x += b.x;
			a.y += b.y;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator -(Vector2 a, Vector2 b)
		{
			a.x -= b.x;
			a.y -= b.y;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator *(Vector2 a, float d)
		{
			a.x *= d;
			a.y *= d;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator /(Vector2 a, float d)
		{
			var inv = 1f / d;
			a.x *= inv;
			a.y *= inv;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
		{
			t   =  MathF.Max(0f, MathF.Min(1f, t));
			a.x += (b.x - a.x) * t;
			a.y += (b.y - a.y) * t;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector2 a, Vector2 b)
		{
			var dx = a.x - b.x;
			var dy = a.y - b.y;
			return MathF.Sqrt(dx * dx + dy * dy);
		}
	}
}