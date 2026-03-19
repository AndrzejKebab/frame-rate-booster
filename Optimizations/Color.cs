using System;
using System.Runtime.CompilerServices;

namespace ToolBuddy.FrameRateBooster.Optimizations
{
	internal struct Color
	{
		public float r;
		public float g;
		public float b;
		public float a;

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
		public static Color operator -(Color a, Color b)
		{
			a.r -= b.r;
			a.g -= b.g;
			a.b -= b.b;
			a.a -= b.a;
			return a;
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
		public static Color operator /(Color a, float b)
		{
			var inv = 1f / b;
			a.r *= inv;
			a.g *= inv;
			a.b *= inv;
			a.a *= inv;
			return a;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color Lerp(Color a, Color b, float t)
		{
			t   =  MathF.Max(0f, MathF.Min(1f, t));
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

		public float grayscale
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => 0.299f * r + 0.587f * g + 0.114f * b;
		}

		public float maxColorComponent
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => MathF.Max(MathF.Max(r, g), b);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator UnityEngine.Vector4(Color c)
		{
			UnityEngine.Vector4 result = default;
			result.x = c.r;
			result.y = c.g;
			result.z = c.b;
			result.w = c.a;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Color(UnityEngine.Vector4 v)
		{
			Color result = default;
			result.r = v.x;
			result.g = v.y;
			result.b = v.z;
			result.a = v.w;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Color RGBMultiplied(float multiplier)
		{
			Color result = default;
			result.r = r * multiplier;
			result.g = g * multiplier;
			result.b = b * multiplier;
			result.a = a;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Color RGBMultiplied(Color multiplier)
		{
			multiplier.r = r * multiplier.r;
			multiplier.g = g * multiplier.g;
			multiplier.b = b * multiplier.b;
			multiplier.a = a;
			return multiplier;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Color AlphaMultiplied(float multiplier)
		{
			Color result = default;
			result.r = r;
			result.g = g;
			result.b = b;
			result.a = a * multiplier;
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RGBToHSV(Color rgbColor, out float H, out float S, out float V)
		{
			if ((rgbColor.b > rgbColor.g) && (rgbColor.b > rgbColor.r))
				RGBToHSVHelper(4f, rgbColor.b, rgbColor.r, rgbColor.g, out H, out S, out V);
			else if (rgbColor.g > rgbColor.r)
				RGBToHSVHelper(2f, rgbColor.g, rgbColor.b, rgbColor.r, out H, out S, out V);
			else
				RGBToHSVHelper(0f, rgbColor.r, rgbColor.g, rgbColor.b, out H, out S, out V);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void RGBToHSVHelper(float     offset, float     dominantcolor, float colorone, float colortwo,
		                                   out float H,      out float S,             out float V)
		{
			V = dominantcolor;
			if (V != 0f)
			{
				var small = MathF.Min(colorone, colortwo);
				var diff  = V - small;

				if (diff != 0f)
				{
					S = diff / V;
					H = offset + ((colorone - colortwo) / diff);
				}
				else
				{
					S = 0f;
					H = offset + (colorone - colortwo);
				}

				H /= 6f;
				if (H < 0f) H += 1.0f;
			}
			else
			{
				S = 0f;
				H = 0f;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Color HSVToRGB(float H, float S, float V, bool hdr = false)
		{
			Color retval = default;
			if (S == 0f)
			{
				retval.r = V;
				retval.g = V;
				retval.b = V;
				retval.a = 1f;
			}
			else if (V == 0f)
			{
				retval.r = 0f;
				retval.g = 0f;
				retval.b = 0f;
				retval.a = 1f;
			}
			else
			{
				retval.a = 1f;
				var h_to_floor = H * 6.0f;
				var   temp       = (int)MathF.Floor(h_to_floor);
				var t          = h_to_floor - temp;
				var var_1      = V * (1f - S);
				var var_2      = V * (1f - S * t);
				var var_3      = V * (1f - S * (1f - t));

				switch (temp)
				{
					case 0:
						retval.r = V;
						retval.g = var_3;
						retval.b = var_1;
						break;
					case 1:
						retval.r = var_2;
						retval.g = V;
						retval.b = var_1;
						break;
					case 2:
						retval.r = var_1;
						retval.g = V;
						retval.b = var_3;
						break;
					case 3:
						retval.r = var_1;
						retval.g = var_2;
						retval.b = V;
						break;
					case 4:
						retval.r = var_3;
						retval.g = var_1;
						retval.b = V;
						break;
					case 5:
						retval.r = V;
						retval.g = var_1;
						retval.b = var_2;
						break;
					case 6:
						retval.r = V;
						retval.g = var_3;
						retval.b = var_1;
						break;
					case -1:
						retval.r = V;
						retval.g = var_1;
						retval.b = var_2;
						break;
				}

				if (hdr) return retval;
				retval.r = MathF.Max(0f, MathF.Min(1f, retval.r));
				retval.g = MathF.Max(0f, MathF.Min(1f, retval.g));
				retval.b = MathF.Max(0f, MathF.Min(1f, retval.b));
			}

			return retval;
		}
		
		public float this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return index switch
				       {
					       0 => r,
					       1 => g,
					       2 => b,
					       3 => a,
					       _ => throw new IndexOutOfRangeException("Invalid Color index(" + index + ")!")
				       };
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				switch (index)
				{
					case 0: r = value; break;
					case 1: g = value; break;
					case 2: b = value; break;
					case 3: a = value; break;
					default:
						throw new IndexOutOfRangeException("Invalid Color index(" + index + ")!");
				}
			}
		}
	}
}