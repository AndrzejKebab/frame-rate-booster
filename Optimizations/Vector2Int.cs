using System;
using System.Runtime.CompilerServices;

namespace ToolBuddy.FrameRateBooster.Optimizations
{
	internal struct Vector2Int
	{
		public int x
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => m_X;

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => m_X = value;
		}


		public int y
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => m_Y;

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => m_Y = value;
		}

		public int m_X;
		public int m_Y;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int operator +(Vector2Int a, Vector2Int b)
		{
			a.m_X += b.m_X;
			a.m_Y += b.m_Y;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int operator -(Vector2Int a, Vector2Int b)
		{
			a.m_X -= b.m_X;
			a.m_Y -= b.m_Y;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int operator *(Vector2Int a, int b)
		{
			a.m_X *= b;
			a.m_Y *= b;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int operator /(Vector2Int a, int b)
		{
			a.m_X /= b;
			a.m_Y /= b;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector2Int a, Vector2Int b)
		{
			float dx = a.m_X - b.m_X;
			float dy = a.m_Y - b.m_Y;
			return MathF.Sqrt(dx * dx + dy * dy);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int Min(Vector2Int lhs, Vector2Int rhs)
		{
			lhs.m_X = Math.Min(lhs.m_X, rhs.m_X);
			lhs.m_Y = Math.Min(lhs.m_Y, rhs.m_Y);
			return lhs;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int Max(Vector2Int lhs, Vector2Int rhs)
		{
			lhs.m_X = Math.Max(lhs.m_X, rhs.m_X);
			lhs.m_Y = Math.Max(lhs.m_Y, rhs.m_Y);
			return lhs;
		}
	}
}