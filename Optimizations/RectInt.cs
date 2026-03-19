using System;
using System.Runtime.CompilerServices;

namespace ToolBuddy.FrameRateBooster.Optimizations
{
	internal struct RectInt
	{
		public int m_XMin;
		public int m_YMin;
		public int m_Width;
		public int m_Height;

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
		}

		public Vector2Int min
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				Vector2Int res = default;
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

		public Vector2Int max
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				Vector2Int res = default;
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

		public Vector2Int position
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				Vector2Int res = default;
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

		public Vector2Int size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				Vector2Int res = default;
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

		public int xMin
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => Math.Min(m_XMin, m_XMin + m_Width);
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				var oldMax = Math.Max(m_XMin, m_XMin + m_Width);
				m_XMin  = value;
				m_Width = oldMax - m_XMin;
			}
		}

		public int yMin
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => Math.Min(m_YMin, m_YMin + m_Height);
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				var oldMax = Math.Max(m_YMin, m_YMin + m_Height);
				m_YMin   = value;
				m_Height = oldMax - m_YMin;
			}
		}

		public int xMax
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => Math.Max(m_XMin, m_XMin + m_Width);
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => m_Width = value - m_XMin;
		}

		public int yMax
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => Math.Max(m_YMin, m_YMin + m_Height);
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => m_Height = value - m_YMin;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ClampToBounds(RectInt bounds)
		{
			var nx = Math.Max(Math.Min(bounds.m_XMin + bounds.m_Width, m_XMin), bounds.m_XMin);
			var ny = Math.Max(Math.Min(bounds.m_YMin + bounds.m_Height, m_YMin), bounds.m_YMin);
			var nw = Math.Min(bounds.m_XMin + bounds.m_Width - nx, m_Width);
			var nh = Math.Min(bounds.m_YMin + bounds.m_Height - ny, m_Height);

			m_XMin   = nx;
			m_YMin   = ny;
			m_Width  = nw;
			m_Height = nh;
		}
	}
}