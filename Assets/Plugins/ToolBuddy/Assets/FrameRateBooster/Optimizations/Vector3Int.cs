using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.Mathematics;

namespace ToolBuddy.FrameRateBooster.Optimizations
{
	[BurstCompile]
	[StructLayout(LayoutKind.Sequential, Size = 12, Pack = 4)]
	public struct Vector3Int
	{
		private int m_X;
		private int m_Y;
		private int m_Z;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int op_Addition(Vector3Int a, Vector3Int b)
		{
			a.m_X += b.m_X;
			a.m_Y += b.m_Y;
			a.m_Z += b.m_Z;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int op_Subtraction(Vector3Int a, Vector3Int b)
		{
			a.m_X -= b.m_X;
			a.m_Y -= b.m_Y;
			a.m_Z -= b.m_Z;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int op_Multiply(Vector3Int a, int d)
		{
			a.m_X *= d;
			a.m_Y *= d;
			a.m_Z *= d;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int op_Multiply(Vector3Int a, Vector3Int b)
		{
			a.m_X *= b.m_X;
			a.m_Y *= b.m_Y;
			a.m_Z *= b.m_Z;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float get_magnitude()
		{
			return math.sqrt(m_X * m_X + m_Y * m_Y + m_Z * m_Z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector3Int a, Vector3Int b)
		{
			a.m_X -= b.m_X;
			a.m_Y -= b.m_Y;
			a.m_Z -= b.m_Z;
			return math.sqrt(a.m_X * a.m_X + a.m_Y * a.m_Y + a.m_Z * a.m_Z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int Scale(Vector3Int a, Vector3Int b)
		{
			a.m_X *= b.m_X;
			a.m_Y *= b.m_Y;
			a.m_Z *= b.m_Z;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int Min(Vector3Int lhs, Vector3Int rhs)
		{
			if (lhs.m_X >= rhs.m_X)
				lhs.m_X = rhs.m_X;
			if (lhs.m_Y >= rhs.m_Y)
				lhs.m_Y = rhs.m_Y;
			if (lhs.m_Z >= rhs.m_Z)
				lhs.m_Z = rhs.m_Z;
			return lhs;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int Max(Vector3Int lhs, Vector3Int rhs)
		{
			if (lhs.m_X <= rhs.m_X)
				lhs.m_X = rhs.m_X;
			if (lhs.m_Y <= rhs.m_Y)
				lhs.m_Y = rhs.m_Y;
			if (lhs.m_Z <= rhs.m_Z)
				lhs.m_Z = rhs.m_Z;
			return lhs;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int FloorToInt(Vector3 v)
		{
			Vector3Int result;
			result.m_X = (int)math.floor(v.x);
			result.m_Y = (int)math.floor(v.y);
			result.m_Z = (int)math.floor(v.z);
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int CeilToInt(Vector3 v)
		{
			Vector3Int result;
			result.m_X = (int)math.ceil(v.x);
			result.m_Y = (int)math.ceil(v.y);
			result.m_Z = (int)math.ceil(v.z);
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int RoundToInt(Vector3 v)
		{
			Vector3Int result;
			result.m_X = (int)math.round(v.x);
			result.m_Y = (int)math.round(v.y);
			result.m_Z = (int)math.round(v.z);
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 op_Implicit(Vector3Int v)
		{
			Vector3 result;
			result.x = v.m_X;
			result.y = v.m_Y;
			result.z = v.m_Z;
			return result;
		}
	}
}