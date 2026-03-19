using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.Mathematics;

namespace ToolBuddy.FrameRateBooster.Optimizations
{
	[BurstCompile]
	[StructLayout(LayoutKind.Sequential, Size = 4, Pack = 1)]
	public struct Color32
	{
		public byte r;
		public byte g;
		public byte b;
		public byte a;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color32 op_Implicit(Color c)
		{
			Color32     result;
			const float maxValue = byte.MaxValue;
			
			c.r      = math.max(0.0f, math.min(1f, c.r));
			result.r = (byte)(c.r * maxValue);
			
			c.g = math.max(0.0f, math.min(1f, c.g));
			result.g = (byte)(c.g * maxValue);
			
			c.b = math.max(0.0f, math.min(1f, c.b));
			result.b = (byte)(c.b * maxValue);
			
			c.a = math.max(0.0f, math.min(1f, c.a));
			result.a = (byte)(c.a * maxValue);
			
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color op_Implicit(Color32 c)
		{
			Color       result;
			const float inverseMaxValue = 1f / byte.MaxValue;
			result.r = c.r * inverseMaxValue;
			result.g = c.g * inverseMaxValue;
			result.b = c.b * inverseMaxValue;
			result.a = c.a * inverseMaxValue;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color32 Lerp(Color32 a, Color32 b, float t)
		{
			t   = math.max(0.0f, math.min(1f, t));
			a.r = (byte)(a.r + (b.r - a.r) * t);
			a.g = (byte)(a.g + (b.g - a.g) * t);
			a.b = (byte)(a.b + (b.b - a.b) * t);
			a.a = (byte)(a.a + (b.a - a.a) * t);
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color32 LerpUnclamped(Color32 a, Color32 b, float t)
		{
			a.r = (byte)(a.r + (b.r - a.r) * t);
			a.g = (byte)(a.g + (b.g - a.g) * t);
			a.b = (byte)(a.b + (b.b - a.b) * t);
			a.a = (byte)(a.a + (b.a - a.a) * t);

			return a;
		}
	}
}