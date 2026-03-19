using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.Mathematics;

namespace ToolBuddy.FrameRateBooster.Optimizations
{
	[BurstCompile]
	[StructLayout(LayoutKind.Sequential, Size = 12, Pack = 4)]
	public struct Vector3
	{
		public float x;
		public float y;
		public float z;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 op_Addition(Vector3 a, Vector3 b)
		{
			a.x += b.x;
			a.y += b.y;
			a.z += b.z;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 op_UnaryNegation(Vector3 a)
		{
			Vector3 result;
			result.x = -a.x;
			result.y = -a.y;
			result.z = -a.z;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 op_Subtraction(Vector3 a, Vector3 b)
		{
			a.x -= b.x;
			a.y -= b.y;
			a.z -= b.z;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 op_Multiply(Vector3 a, float d)
		{
			a.x *= d;
			a.y *= d;
			a.z *= d;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 op_Multiply(float d, Vector3 a)
		{
			a.x *= d;
			a.y *= d;
			a.z *= d;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 op_Division(Vector3 a, float d)
		{
			var inversed = 1 / d;
			a.x *= inversed;
			a.y *= inversed;
			a.z *= inversed;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Magnitude(Vector3 vector)
		{
			return math.sqrt(vector.x * vector.x + vector.y * vector.y +
			                 vector.z * vector.z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float get_magnitude()
		{
			return math.sqrt(x * x + y * y + z * z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Normalize()
		{
			var num = math.sqrt(x * x + y * y + z * z);
			if (num > 9.99999974737875E-06)
			{
				var inversed = 1 / num;
				x *= inversed;
				y *= inversed;
				z *= inversed;
			}
			else
			{
				x = 0;
				y = 0;
				z = 0;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Normalize(Vector3 value)
		{
			Vector3 result;
			var     num = math.sqrt(value.x * value.x + value.y * value.y + value.z * value.z);
			if (num > 9.99999974737875E-06)
			{
				var inversed = 1 / num;
				result.x = value.x * inversed;
				result.y = value.y * inversed;
				result.z = value.z * inversed;
			}
			else
			{
				result.x = 0;
				result.y = 0;
				result.z = 0;
			}

			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector3 get_normalized()
		{
			Vector3 result;
			var     num = math.sqrt(x * x + y * y + z * z);
			if (num > 9.99999974737875E-06)
			{
				var inversed = 1 / num;
				result.x = x * inversed;
				result.y = y * inversed;
				result.z = z * inversed;
			}
			else
			{
				result.x = 0;
				result.y = 0;
				result.z = 0;
			}

			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector3 a, Vector3 b)
		{
			a.x -= b.x;
			a.y -= b.y;
			a.z -= b.z;
			return math.sqrt(a.x * a.x + a.y * a.y + a.z * a.z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
		{
			t   =  math.max(0.0f, math.min(1f, t));
			a.x += (b.x - a.x) * t;
			a.y += (b.y - a.y) * t;
			a.z += (b.z - a.z) * t;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 LerpUnclamped(Vector3 a, Vector3 b, float t)
		{
			a.x += (b.x - a.x) * t;
			a.y += (b.y - a.y) * t;
			a.z += (b.z - a.z) * t;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Scale(Vector3 a, Vector3 b)
		{
			a.x *= b.x;
			a.y *= b.y;
			a.z *= b.z;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
		{
			Vector3 result;
			result.x = lhs.y * rhs.z - lhs.z * rhs.y;
			result.y = lhs.z * rhs.x - lhs.x * rhs.z;
			result.z = lhs.x * rhs.y - lhs.y * rhs.x;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Min(Vector3 lhs, Vector3 rhs)
		{
			if (lhs.x >= rhs.x)
				lhs.x = rhs.x;
			if (lhs.y >= rhs.y)
				lhs.y = rhs.y;
			if (lhs.z >= rhs.z)
				lhs.z = rhs.z;
			return lhs;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Max(Vector3 lhs, Vector3 rhs)
		{
			if (lhs.x <= rhs.x)
				lhs.x = rhs.x;
			if (lhs.y <= rhs.y)
				lhs.y = rhs.y;
			if (lhs.z <= rhs.z)
				lhs.z = rhs.z;
			return lhs;
		}

		//TODO https://forum.unity.com/threads/c-performance-tips.533831/  ?
	}
}