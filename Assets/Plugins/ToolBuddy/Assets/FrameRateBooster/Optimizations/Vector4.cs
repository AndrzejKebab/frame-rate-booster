using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.Mathematics;

namespace ToolBuddy.FrameRateBooster.Optimizations
{
	[BurstCompile]
	[StructLayout(LayoutKind.Sequential, Size = 16, Pack = 4)]
	public struct Vector4
	{
		public float x;
		public float y;
		public float z;
		public float w;

		//TODO some implicit methods are slower than original

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 op_Implicit(Vector3 v)
		{
			//TODO remove assignation to 0 here and other implicit methods
			Vector4 result;
			result.x = v.x;
			result.y = v.y;
			result.z = v.z;
			result.w = 0.0f;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 op_Implicit(Vector4 v)
		{
			Vector3 result;
			result.x = v.x;
			result.y = v.y;
			result.z = v.z;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 op_Implicit(Vector2 v)
		{
			Vector4 result;
			result.x = v.x;
			result.y = v.y;
			result.z = 0.0f;
			result.w = 0.0f;
			return result;
		}

		//TODO Vector2 op_Implicit(UnityEngine.Vector4 v)
		//[MethodImpl(MethodImplOptions.AggressiveInlining)]
		//public static Vector2 op_Implicit(UnityEngine.Vector4 v)
		//{
		//    Vector2 result;
		//    result.x = v.x;
		//    result.y = v.y;
		//    return result;
		//}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 op_Addition(Vector4 a, Vector4 b)
		{
			a.x += b.x;
			a.y += b.y;
			a.z += b.z;
			a.w += b.w;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 op_UnaryNegation(Vector4 a)
		{
			Vector4 result;
			result.x = -a.x;
			result.y = -a.y;
			result.z = -a.z;
			result.w = -a.w;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 op_Subtraction(Vector4 a, Vector4 b)
		{
			a.x -= b.x;
			a.y -= b.y;
			a.z -= b.z;
			a.w -= b.w;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 op_Multiply(Vector4 a, float d)
		{
			a.x *= d;
			a.y *= d;
			a.z *= d;
			a.w *= d;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 op_Multiply(float d, Vector4 a)
		{
			a.x *= d;
			a.y *= d;
			a.z *= d;
			a.w *= d;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 op_Division(Vector4 a, float d)
		{
			var inversed = 1 / d;
			a.x *= inversed;
			a.y *= inversed;
			a.z *= inversed;
			a.w *= inversed;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Magnitude(Vector4 vector)
		{
			return math.sqrt(vector.x * vector.x + vector.y * vector.y +
			                 vector.z * vector.z + vector.w * vector.w);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float get_magnitude()
		{
			return math.sqrt(x * x + y * y + z * z + w * w);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Normalize()
		{
			var num = math.sqrt(x * x + y * y + z * z + w * w);
			if (num > 9.99999974737875E-06)
			{
				var inversed = 1 / num;
				x *= inversed;
				y *= inversed;
				z *= inversed;
				w *= inversed;
			}
			else
			{
				x = 0;
				y = 0;
				z = 0;
				w = 0;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Normalize(Vector4 value)
		{
			Vector4 result;
			var num = math.sqrt(value.x * value.x + value.y * value.y + value.z * value.z +
			                    value.w * value.w);
			if (num > 9.99999974737875E-06)
			{
				var inversed = 1 / num;
				result.x = value.x * inversed;
				result.y = value.y * inversed;
				result.z = value.z * inversed;
				result.w = value.w * inversed;
			}
			else
			{
				result.x = 0;
				result.y = 0;
				result.z = 0;
				result.w = 0;
			}

			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector4 get_normalized()
		{
			Vector4 result;
			var     num = math.sqrt(x * x + y * y + z * z + w * w);
			if (num > 9.99999974737875E-06)
			{
				var inversed = 1 / num;
				result.x = x * inversed;
				result.y = y * inversed;
				result.z = z * inversed;
				result.w = w * inversed;
			}
			else
			{
				result.x = 0;
				result.y = 0;
				result.z = 0;
				result.w = 0;
			}

			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector4 a, Vector4 b)
		{
			a.x -= b.x;
			a.y -= b.y;
			a.z -= b.z;
			a.w -= b.w;
			return math.sqrt(a.x * a.x + a.y * a.y + a.z * a.z + a.w * a.w);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Lerp(Vector4 a, Vector4 b, float t)
		{
			t   =  math.max(0.0f, math.min(1f, t));
			a.x += (b.x - a.x) * t;
			a.y += (b.y - a.y) * t;
			a.z += (b.z - a.z) * t;
			a.w += (b.w - a.w) * t;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 LerpUnclamped(Vector4 a, Vector4 b, float t)
		{
			a.x += (b.x - a.x) * t;
			a.y += (b.y - a.y) * t;
			a.z += (b.z - a.z) * t;
			a.w += (b.w - a.w) * t;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Scale(Vector4 a, Vector4 b)
		{
			a.x *= b.x;
			a.y *= b.y;
			a.z *= b.z;
			a.w *= b.w;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Min(Vector4 lhs, Vector4 rhs)
		{
			if (lhs.x >= rhs.x)
				lhs.x = rhs.x;
			if (lhs.y >= rhs.y)
				lhs.y = rhs.y;
			if (lhs.z >= rhs.z)
				lhs.z = rhs.z;
			if (lhs.w >= rhs.w)
				lhs.w = rhs.w;
			return lhs;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Max(Vector4 lhs, Vector4 rhs)
		{
			if (lhs.x <= rhs.x)
				lhs.x = rhs.x;
			if (lhs.y <= rhs.y)
				lhs.y = rhs.y;
			if (lhs.z <= rhs.z)
				lhs.z = rhs.z;
			if (lhs.w <= rhs.w)
				lhs.w = rhs.w;
			return lhs;
		}
	}
}