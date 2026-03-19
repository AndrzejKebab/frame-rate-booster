using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.Mathematics;

namespace ToolBuddy.FrameRateBooster.Optimizations
{
	[BurstCompile]
	[StructLayout(LayoutKind.Sequential, Size = 8, Pack = 4)]
	public struct Vector2
	{
		public float x;
		public float y;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 op_Addition(Vector2 a, Vector2 b)
		{
			a.x += b.x;
			a.y += b.y;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 op_UnaryNegation(Vector2 a)
		{
			a.x = -a.x;
			a.y = -a.y;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 op_Subtraction(Vector2 a, Vector2 b)
		{
			a.x -= b.x;
			a.y -= b.y;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 op_Multiply(Vector2 a, float d)
		{
			a.x *= d;
			a.y *= d;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 op_Multiply(float d, Vector2 a)
		{
			a.x *= d;
			a.y *= d;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 op_Multiply(Vector2 a, Vector2 b)
		{
			a.x *= b.x;
			a.y *= b.y;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 op_Division(Vector2 a, Vector2 b)
		{
			a.x /= b.x;
			a.y /= b.y;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 op_Division(Vector2 a, float d)
		{
			var inversed = 1 / d;
			a.x *= inversed;
			a.y *= inversed;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 op_Implicit(Vector3 v)
		{
			Vector2 result;
			result.x = v.x;
			result.y = v.y;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 op_Implicit(Vector2 v)
		{
			Vector3 result;
			result.x = v.x;
			result.y = v.y;
			result.z = 0.0f;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
		{
			t   =  math.max(0.0f, math.min(1f, t));
			a.x += (b.x - a.x) * t;
			a.y += (b.y - a.y) * t;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 LerpUnclamped(Vector2 a, Vector2 b, float t)
		{
			a.x += (b.x - a.x) * t;
			a.y += (b.y - a.y) * t;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Scale(Vector2 a, Vector2 b)
		{
			a.x *= b.x;
			a.y *= b.y;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Perpendicular(Vector2 inDirection)
		{
			Vector2 result;
			result.x = -inDirection.y;
			result.y = inDirection.x;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Min(Vector2 lhs, Vector2 rhs)
		{
			if (lhs.x >= rhs.x)
				lhs.x = rhs.x;
			if (lhs.y >= rhs.y)
				lhs.y = rhs.y;
			return lhs;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Max(Vector2 lhs, Vector2 rhs)
		{
			if (lhs.x <= rhs.x)
				lhs.x = rhs.x;
			if (lhs.y <= rhs.y)
				lhs.y = rhs.y;
			return lhs;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float get_magnitude()
		{
			return math.sqrt(x * x + y * y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Normalize()
		{
			var num = math.sqrt(x * x + y * y);
			if (num > 9.99999974737875E-06)
			{
				var inversed = 1 / num;
				x *= inversed;
				y *= inversed;
			}
			else
			{
				x = 0;
				y = 0;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector2 get_normalized()
		{
			Vector2 result;
			var     num = math.sqrt(x * x + y * y);
			if (num > 9.99999974737875E-06)
			{
				var inversed = 1 / num;
				result.x = x * inversed;
				result.y = y * inversed;
			}
			else
			{
				result.x = 0;
				result.y = 0;
			}

			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector2 a, Vector2 b)
		{
			a.x -= b.x;
			a.y -= b.y;
			return math.sqrt(a.x * a.x + a.y * a.y);
		}
	}
}