using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.Mathematics;

namespace ToolBuddy.FrameRateBooster.Optimizations
{
	[BurstCompile]
	[StructLayout(LayoutKind.Sequential, Size = 8, Pack = 4)]
	public struct Vector2Int
	{
		public int m_X;
		public int m_Y;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int op_Addition(Vector2Int a, Vector2Int b)
		{
			a.m_X += b.m_X;
			a.m_Y += b.m_Y;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int op_Subtraction(Vector2Int a, Vector2Int b)
		{
			a.m_X -= b.m_X;
			a.m_Y -= b.m_Y;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int op_Multiply(Vector2Int a, int d)
		{
			a.m_X *= d;
			a.m_Y *= d;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int op_Multiply(Vector2Int a, Vector2Int b)
		{
			a.m_X *= b.m_X;
			a.m_Y *= b.m_Y;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float get_magnitude()
		{
			return math.sqrt(m_X * m_X + m_Y * m_Y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector2Int a, Vector2Int b)
		{
			a.m_X -= b.m_X;
			a.m_Y -= b.m_Y;
			return math.sqrt(a.m_X * a.m_X + a.m_Y * a.m_Y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int Scale(Vector2Int a, Vector2Int b)
		{
			a.m_X *= b.m_X;
			a.m_Y *= b.m_Y;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int Min(Vector2Int lhs, Vector2Int rhs)
		{
			if (lhs.m_X >= rhs.m_X)
				lhs.m_X = rhs.m_X;
			if (lhs.m_Y >= rhs.m_Y)
				lhs.m_Y = rhs.m_Y;
			return lhs;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int Max(Vector2Int lhs, Vector2Int rhs)
		{
			if (lhs.m_X <= rhs.m_X)
				lhs.m_X = rhs.m_X;
			if (lhs.m_Y <= rhs.m_Y)
				lhs.m_Y = rhs.m_Y;
			return lhs;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int FloorToInt(Vector2 v)
		{
			Vector2Int result;
			result.m_X = (int)math.floor(v.x);
			result.m_Y = (int)math.floor(v.y);
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int CeilToInt(Vector2 v)
		{
			Vector2Int result;
			result.m_X = (int)math.ceil(v.x);
			result.m_Y = (int)math.ceil(v.y);
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int RoundToInt(Vector2 v)
		{
			Vector2Int result;
			result.m_X = (int)math.round(v.x);
			result.m_Y = (int)math.round(v.y);
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 op_Implicit(Vector2Int v)
		{
			Vector2 result;
			result.x = v.m_X;
			result.y = v.m_Y;
			return result;
		}
	}
}