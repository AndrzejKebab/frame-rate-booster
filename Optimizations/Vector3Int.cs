using System;
using System.Runtime.CompilerServices;

namespace ToolBuddy.FrameRateBooster.Optimizations
{
	internal struct Vector3Int
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

		public int z
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => m_Z;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => m_Z = value;
		}

		public int m_X;
		public int m_Y;
		public int m_Z;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int operator +(Vector3Int a, Vector3Int b)
		{
			a.m_X += b.m_X;
			a.m_Y += b.m_Y;
			a.m_Z += b.m_Z;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int operator -(Vector3Int a, Vector3Int b)
		{
			a.m_X -= b.m_X;
			a.m_Y -= b.m_Y;
			a.m_Z -= b.m_Z;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int operator *(Vector3Int a, int b)
		{
			a.m_X *= b;
			a.m_Y *= b;
			a.m_Z *= b;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int operator /(Vector3Int a, int b)
		{
			a.m_X /= b;
			a.m_Y /= b;
			a.m_Z /= b;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector3Int a, Vector3Int b)
		{
			float dx = a.m_X - b.m_X;
			float dy = a.m_Y - b.m_Y;
			float dz = a.m_Z - b.m_Z;
			return MathF.Sqrt(dx * dx + dy * dy + dz * dz);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int Min(Vector3Int lhs, Vector3Int rhs)
		{
			lhs.m_X = Math.Min(lhs.m_X, rhs.m_X);
			lhs.m_Y = Math.Min(lhs.m_Y, rhs.m_Y);
			lhs.m_Z = Math.Min(lhs.m_Z, rhs.m_Z);
			return lhs;
		}
	}
}