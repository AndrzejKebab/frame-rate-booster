using System;
using System.Runtime.CompilerServices;

namespace ToolBuddy.FrameRateBooster.Optimizations
{
	internal struct BoundsInt
	{
		public Vector3Int m_Position;
		public Vector3Int m_Size;

		public Vector3 center
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				Vector3 res = default;
				res.x = m_Position.x + m_Size.x * 0.5f;
				res.y = m_Position.y + m_Size.y * 0.5f;
				res.z = m_Position.z + m_Size.z * 0.5f;
				return res;
			}
		}

		public Vector3Int min
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				Vector3Int res = default;
				res.x = Math.Min(m_Position.x, m_Position.x + m_Size.x);
				res.y = Math.Min(m_Position.y, m_Position.y + m_Size.y);
				res.z = Math.Min(m_Position.z, m_Position.z + m_Size.z);
				return res;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				m_Position.x = value.x;
				m_Position.y = value.y;
				m_Position.z = value.z;
			}
		}

		public Vector3Int max
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				Vector3Int res = default;
				res.x = Math.Max(m_Position.x, m_Position.x + m_Size.x);
				res.y = Math.Max(m_Position.y, m_Position.y + m_Size.y);
				res.z = Math.Max(m_Position.z, m_Position.z + m_Size.z);
				return res;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				m_Size.x = value.x - m_Position.x;
				m_Size.y = value.y - m_Position.y;
				m_Size.z = value.z - m_Position.z;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ClampToBounds(BoundsInt bounds)
		{
			var bxMax = Math.Max(bounds.m_Position.x, bounds.m_Position.x + bounds.m_Size.x);
			var byMax = Math.Max(bounds.m_Position.y, bounds.m_Position.y + bounds.m_Size.y);
			var bzMax = Math.Max(bounds.m_Position.z, bounds.m_Position.z + bounds.m_Size.z);
			var bxMin = Math.Min(bounds.m_Position.x, bounds.m_Position.x + bounds.m_Size.x);
			var byMin = Math.Min(bounds.m_Position.y, bounds.m_Position.y + bounds.m_Size.y);
			var bzMin = Math.Min(bounds.m_Position.z, bounds.m_Position.z + bounds.m_Size.z);

			m_Position.x = Math.Max(Math.Min(bxMax, m_Position.x), bxMin);
			m_Position.y = Math.Max(Math.Min(byMax, m_Position.y), byMin);
			m_Position.z = Math.Max(Math.Min(bzMax, m_Position.z), bzMin);

			m_Size.x = Math.Min(bxMax - m_Position.x, m_Size.x);
			m_Size.y = Math.Min(byMax - m_Position.y, m_Size.y);
			m_Size.z = Math.Min(bzMax - m_Position.z, m_Size.z);
		}
	}
}