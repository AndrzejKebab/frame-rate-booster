using System;
using System.Runtime.CompilerServices;

namespace ToolBuddy.FrameRateBooster.Optimizations;

internal struct Color32
{
	public byte r;
	public byte g;
	public byte b;
	public byte a;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator Color32(UnityEngine.Color c)
	{
		Color32 result = default;

		var rClamped = MathF.Max(0f, MathF.Min(1f, c.r));
		var gClamped = MathF.Max(0f, MathF.Min(1f, c.g));
		var bClamped = MathF.Max(0f, MathF.Min(1f, c.b));
		var aClamped = MathF.Max(0f, MathF.Min(1f, c.a));

		result.r = (byte)MathF.Round(rClamped * 255f);
		result.g = (byte)MathF.Round(gClamped * 255f);
		result.b = (byte)MathF.Round(bClamped * 255f);
		result.a = (byte)MathF.Round(aClamped * 255f);

		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator UnityEngine.Color(Color32 c)
	{
		UnityEngine.Color result = default;
		const float inv255 = 1f / 255f;

		result.r = c.r * inv255;
		result.g = c.g * inv255;
		result.b = c.b * inv255;
		result.a = c.a * inv255;

		return result;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Color32 Lerp(Color32 a, Color32 b, float t)
	{
		t = MathF.Max(0f, MathF.Min(1f, t));

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
	
	public byte this[int index]
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
				       _ => throw new IndexOutOfRangeException("Invalid Color32 index(" + index + ")!")
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
					throw new IndexOutOfRangeException("Invalid Color32 index(" + index + ")!");
			}
		}
	}
}