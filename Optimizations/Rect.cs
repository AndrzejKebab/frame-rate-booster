using System;
using System.Runtime.CompilerServices;

namespace ToolBuddy.FrameRateBooster.Optimizations
{
	internal struct Rect
	{
		public float m_XMin;
		public float m_YMin;
		public float m_Width;
		public float m_Height;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Rect MinMaxRect(float xmin, float ymin, float xmax, float ymax)
		{
			Rect r = default;
			r.m_XMin   = xmin;
			r.m_YMin   = ymin;
			r.m_Width  = xmax - xmin;
			r.m_Height = ymax - ymin;
			return r;
		}

		public Vector2 center
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				Vector2 res = default;
				res.x = m_XMin + m_Width * 0.5f;
				res.y = m_YMin + m_Height * 0.5f;
				return res;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				m_XMin = value.x - m_Width * 0.5f;
				m_YMin = value.y - m_Height * 0.5f;
			}
		}

		public Vector2 min
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				Vector2 res = default;
				res.x = m_XMin;
				res.y = m_YMin;
				return res;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				m_XMin = value.x;
				m_YMin = value.y;
			}
		}

		public Vector2 max
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				Vector2 res = default;
				res.x = m_XMin + m_Width;
				res.y = m_YMin + m_Height;
				return res;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				m_Width  = value.x - m_XMin;
				m_Height = value.y - m_YMin;
			}
		}

		public Vector2 position
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				Vector2 res = default;
				res.x = m_XMin;
				res.y = m_YMin;
				return res;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				m_XMin = value.x;
				m_YMin = value.y;
			}
		}

		public Vector2 size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				Vector2 res = default;
				res.x = m_Width;
				res.y = m_Height;
				return res;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				m_Width  = value.x;
				m_Height = value.y;
			}
		}

		public float xMin
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => m_XMin;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				var oldXMax = m_XMin + m_Width;
				m_XMin  = value;
				m_Width = oldXMax - m_XMin;
			}
		}

		public float yMin
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => m_YMin;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				var oldYMax = m_YMin + m_Height;
				m_YMin   = value;
				m_Height = oldYMax - m_YMin;
			}
		}

		public float xMax
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => m_Width + m_XMin;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => m_Width = value - m_XMin;
		}

		public float yMax
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => m_Height + m_YMin;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => m_Height = value - m_YMin;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Contains(Vector2 point)
		{
			return (point.x >= m_XMin) && (point.x < m_XMin + m_Width) &&
			       (point.y >= m_YMin) && (point.y < m_YMin + m_Height);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Contains(Vector3 point)
		{
			return (point.x >= m_XMin) && (point.x < m_XMin + m_Width) &&
			       (point.y >= m_YMin) && (point.y < m_YMin + m_Height);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Rect OrderMinMax(Rect rect)
		{
			if (rect.m_XMin > rect.m_XMin + rect.m_Width)
			{
				var temp = rect.m_XMin;
				rect.m_XMin  = rect.m_XMin + rect.m_Width;
				rect.m_Width = temp - rect.m_XMin;
			}

			if (!(rect.m_YMin > rect.m_YMin + rect.m_Height)) return rect;
			{
				var temp = rect.m_YMin;
				rect.m_YMin   = rect.m_YMin + rect.m_Height;
				rect.m_Height = temp - rect.m_YMin;
			}

			return rect;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Overlaps(Rect other)
		{
			return (other.m_XMin + other.m_Width > m_XMin &&
			        other.m_XMin < m_XMin + m_Width &&
			        other.m_YMin + other.m_Height > m_YMin &&
			        other.m_YMin < m_YMin + m_Height);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 NormalizedToPoint(Rect                rectangle,
		                                                    Vector2 normalizedRectCoordinates)
		{
			Vector2 res = default;
			res.x = rectangle.m_XMin + rectangle.m_Width * normalizedRectCoordinates.x;
			res.y = rectangle.m_YMin + rectangle.m_Height * normalizedRectCoordinates.y;
			return res;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 PointToNormalized(Rect rectangle, Vector2 point)
		{
			Vector2 res = default;
			var               nx  = rectangle.m_Width != 0f ? (point.x - rectangle.m_XMin) / rectangle.m_Width : 0f;
			var               ny  = rectangle.m_Height != 0f ? (point.y - rectangle.m_YMin) / rectangle.m_Height : 0f;

			res.x = MathF.Max(0f, MathF.Min(1f, nx));
			res.y = MathF.Max(0f, MathF.Min(1f, ny));
			return res;
		}
	}
}