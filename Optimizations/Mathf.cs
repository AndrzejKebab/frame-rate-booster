using System;
using System.Runtime.CompilerServices;

namespace ToolBuddy.FrameRateBooster.Optimizations
{
	internal struct Mathf
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Clamp(float value, float min, float max)
		{
			return MathF.Max(min, MathF.Min(max, value));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Clamp(int value, int min, int max)
		{
			return Math.Max(min, Math.Min(max, value));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Clamp01(float value)
		{
			return MathF.Max(0f, MathF.Min(1f, value));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Lerp(float a, float b, float t)
		{
			return a + (b - a) * MathF.Max(0f, MathF.Min(1f, t));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Min(float a, float b)
		{
			return MathF.Min(a, b);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Max(float a, float b)
		{
			return MathF.Max(a, b);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Abs(float f)
		{
			return MathF.Abs(f);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Sign(float f)
		{
			return f >= 0f ? 1f : -1f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Round(float f)
		{
			return MathF.Round(f);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Ceil(float f)
		{
			return MathF.Ceiling(f);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Floor(float f)
		{
			return MathF.Floor(f);
		}

		public static int RoundToInt(float f)
		{
			return (int)MathF.Round(f);
		}
	}
}