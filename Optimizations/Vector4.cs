using System;
using System.Runtime.CompilerServices;

namespace ToolBuddy.FrameRateBooster.Optimizations
{
	internal struct Vector4
	{
		public float x;
		public float y;
		public float z;
		public float w;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator +(Vector4 a, Vector4 b)
		{
			a.x += b.x;
			a.y += b.y;
			a.z += b.z;
			a.w += b.w;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator -(Vector4 a, Vector4 b)
		{
			a.x -= b.x;
			a.y -= b.y;
			a.z -= b.z;
			a.w -= b.w;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator -(Vector4 a)
		{
			a.x = -a.x;
			a.y = -a.y;
			a.z = -a.z;
			a.w = -a.w;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator *(Vector4 a, float d)
		{
			a.x *= d;
			a.y *= d;
			a.z *= d;
			a.w *= d;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator *(float d, Vector4 a)
		{
			a.x *= d;
			a.y *= d;
			a.z *= d;
			a.w *= d;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator /(Vector4 a, float d)
		{
			var inv = 1f / d;
			a.x *= inv;
			a.y *= inv;
			a.z *= inv;
			a.w *= inv;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Lerp(Vector4 a, Vector4 b, float t)
		{
			t   =  MathF.Max(0f, MathF.Min(1f, t));
			a.x += (b.x - a.x) * t;
			a.y += (b.y - a.y) * t;
			a.z += (b.z - a.z) * t;
			a.w += (b.w - a.w) * t;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector4 a, Vector4 b)
		{
			var dx = a.x - b.x;
			var dy = a.y - b.y;
			var dz = a.z - b.z;
			var dw = a.w - b.w;
			return MathF.Sqrt(dx * dx + dy * dy + dz * dz + dw * dw);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Min(Vector4 lhs, Vector4 rhs)
		{
			lhs.x = MathF.Min(lhs.x, rhs.x);
			lhs.y = MathF.Min(lhs.y, rhs.y);
			lhs.z = MathF.Min(lhs.z, rhs.z);
			lhs.w = MathF.Min(lhs.w, rhs.w);
			return lhs;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Max(Vector4 lhs, Vector4 rhs)
		{
			lhs.x = MathF.Max(lhs.x, rhs.x);
			lhs.y = MathF.Max(lhs.y, rhs.y);
			lhs.z = MathF.Max(lhs.z, rhs.z);
			lhs.w = MathF.Max(lhs.w, rhs.w);
			return lhs;
		}
	}
}