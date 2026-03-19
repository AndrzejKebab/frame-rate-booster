using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.Mathematics;

namespace ToolBuddy.FrameRateBooster.Optimizations
{
	[BurstCompile]
	[StructLayout(LayoutKind.Sequential, Size = 16, Pack = 4)]
	public struct Color
	{
		public float r;
		public float g;
		public float b;
		public float a;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color get_black()
		{
			Color result;
			result.r = 0.0f;
			result.g = 0.0f;
			result.b = 0.0f;
			result.a = 1f;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color get_blue()
		{
			Color result;
			result.r = 0.0f;
			result.g = 0.0f;
			result.b = 1f;
			result.a = 1f;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color get_clear()
		{
			Color result;
			result.r = 0.0f;
			result.g = 0.0f;
			result.b = 0.0f;
			result.a = 0.0f;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color get_cyan()
		{
			Color result;
			result.r = 0.0f;
			result.g = 1f;
			result.b = 1f;
			result.a = 1f;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color get_gray()
		{
			Color result;
			result.r = 0.5f;
			result.g = 0.5f;
			result.b = 0.5f;
			result.a = 1f;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color get_green()
		{
			Color result;
			result.r = 0.0f;
			result.g = 1f;
			result.b = 0.0f;
			result.a = 1f;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color get_grey()
		{
			Color result;
			result.r = 0.5f;
			result.g = 0.5f;
			result.b = 0.5f;
			result.a = 1f;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color get_magenta()
		{
			Color result;
			result.r = 1f;
			result.g = 0.0f;
			result.b = 1f;
			result.a = 1f;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color get_red()
		{
			Color result;
			result.r = 1f;
			result.g = 0.0f;
			result.b = 0.0f;
			result.a = 1f;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color get_white()
		{
			Color result;
			result.r = 1f;
			result.g = 1f;
			result.b = 1f;
			result.a = 1f;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color get_yellow()
		{
			Color result;
			result.r = 1f;
			result.g = 0.9215686f;
			result.b = 0.01568628f;
			result.a = 1f;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color operator +(Color a, Color b)
		{
			a.r += b.r;
			a.g += b.g;
			a.b += b.b;
			a.a += b.a;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color operator /(Color a, float b)
		{
			var inverse = 1 / b;
			a.r *= inverse;
			a.g *= inverse;
			a.b *= inverse;
			a.a *= inverse;
			return a;
		}

		////TODO test
		//[MethodImpl(MethodImplOptions.AggressiveInlining)]
		//public Color get_linear()
		//{
		//    Color result;
		//    result.r = Mathf.GammaToLinearSpace(r);
		//    result.g = Mathf.GammaToLinearSpace(g);
		//    result.b = Mathf.GammaToLinearSpace(b);
		//    result.a = a;
		//    return result;
		//}

		////TODO test
		//[MethodImpl(MethodImplOptions.AggressiveInlining)]
		//public Color get_gamma()
		//{
		//    Color result;
		//    result.r = Mathf.LinearToGammaSpace(r);
		//    result.g = Mathf.LinearToGammaSpace(g);
		//    result.b = Mathf.LinearToGammaSpace(b);
		//    result.a = a;
		//    return result;
		//}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector4(Color c)
		{
			Vector4 result;
			result.x = c.r;
			result.y = c.g;
			result.z = c.b;
			result.w = c.a;
			return result;
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Color(Vector4 v)
		{
			Color result;
			result.r = v.x;
			result.g = v.y;
			result.b = v.z;
			result.a = v.w;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color operator *(Color a, Color b)
		{
			a.r *= b.r;
			a.g *= b.g;
			a.b *= b.b;
			a.a *= b.a;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color operator *(Color a, float b)
		{
			a.r *= b;
			a.g *= b;
			a.b *= b;
			a.a *= b;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color operator *(float b, Color a)
		{
			a.r *= b;
			a.g *= b;
			a.b *= b;
			a.a *= b;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color operator -(Color a, Color b)
		{
			a.r -= b.r;
			a.g -= b.g;
			a.b -= b.b;
			a.a -= b.a;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color Lerp(Color a, Color b, float t)
		{
			t = math.clamp(t, 0.0f, 1f);
			a.r += (b.r - a.r) * t;
			a.g += (b.g - a.g) * t;
			a.b += (b.b - a.b) * t;
			a.a += (b.a - a.a) * t;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color LerpUnclamped(Color a, Color b, float t)
		{
			a.r += (b.r - a.r) * t;
			a.g += (b.g - a.g) * t;
			a.b += (b.b - a.b) * t;
			a.a += (b.a - a.a) * t;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Color RGBMultiplied(float multiplier)
		{
			Color result;
			result.r = r * multiplier;
			result.g = g * multiplier;
			result.b = b * multiplier;
			result.a = a;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Color AlphaMultiplied(float multiplier)
		{
			Color result;
			result.r = r;
			result.g = g;
			result.b = b;
			result.a = a * multiplier;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal Color RGBMultiplied(Color multiplier)
		{
			multiplier.r = r * multiplier.r;
			multiplier.g = g * multiplier.g;
			multiplier.b = b * multiplier.b;
			multiplier.a = a;
			return multiplier;
		}
	}
}