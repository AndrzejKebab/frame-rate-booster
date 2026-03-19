using System;
using System.Runtime.CompilerServices;

namespace ToolBuddy.FrameRateBooster.Optimizations
{
	internal struct Vector3
	{
		public const float kEpsilon = 0.00001F;
		public       float x;
		public       float y;
		public       float z;

		public Vector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
		
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator +(Vector3 a, Vector3 b)
		{
			a.x += b.x;
			a.y += b.y;
			a.z += b.z;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
			a.x -= b.x;
			a.y -= b.y;
			a.z -= b.z;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator -(Vector3 a)
		{
			a.x = -a.x;
			a.y = -a.y;
			a.z = -a.z;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator *(Vector3 a, float d)
		{
			a.x *= d;
			a.y *= d;
			a.z *= d;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator *(float d, Vector3 a)
		{
			a.x *= d;
			a.y *= d;
			a.z *= d;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator /(Vector3 a, float d)
		{
			var inv = 1f / d;
			a.x *= inv;
			a.y *= inv;
			a.z *= inv;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
		{
			t   =  MathF.Max(0f, MathF.Min(1f, t));
			a.x += (b.x - a.x) * t;
			a.y += (b.y - a.y) * t;
			a.z += (b.z - a.z) * t;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector3 a, Vector3 b)
		{
			var dx = a.x - b.x;
			var dy = a.y - b.y;
			var dz = a.z - b.z;
			return MathF.Sqrt(dx * dx + dy * dy + dz * dz);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Min(Vector3 lhs, Vector3 rhs)
		{
			lhs.x = MathF.Min(lhs.x, rhs.x);
			lhs.y = MathF.Min(lhs.y, rhs.y);
			lhs.z = MathF.Min(lhs.z, rhs.z);
			return lhs;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Max(Vector3 lhs, Vector3 rhs)
		{
			lhs.x = MathF.Max(lhs.x, rhs.x);
			lhs.y = MathF.Max(lhs.y, rhs.y);
			lhs.z = MathF.Max(lhs.z, rhs.z);
			return lhs;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
		{
			Vector3 result = default;
			result.x = lhs.y * rhs.z - lhs.z * rhs.y;
			result.y = lhs.z * rhs.x - lhs.x * rhs.z;
			result.z = lhs.x * rhs.y - lhs.y * rhs.x;
			return result;
		}
		
		public Vector3 normalized
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => Vector3.Normalize(this);
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Normalize(Vector3 value)
		{
			var mag = Magnitude(value);
			if (mag > kEpsilon)
				return value / mag;
			return zero;
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Magnitude(Vector3 vector) { return (float)Math.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z); }
		public static Vector3 zero
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		} = new(0F, 0F, 0F);
	}
}