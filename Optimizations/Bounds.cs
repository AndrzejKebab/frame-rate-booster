using System;
using System.Runtime.CompilerServices;

namespace ToolBuddy.FrameRateBooster.Optimizations
{
	internal struct Bounds
	{
		public Vector3 m_Center;
		public Vector3 m_Extents;

		public Vector3 center
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => m_Center;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => m_Center = value;
		}

		public Vector3 size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				Vector3 res = default;
				res.x = m_Extents.x * 2f;
				res.y = m_Extents.y * 2f;
				res.z = m_Extents.z * 2f;
				return res;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				m_Extents.x = value.x * 0.5f;
				m_Extents.y = value.y * 0.5f;
				m_Extents.z = value.z * 0.5f;
			}
		}

		public Vector3 extents
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => m_Extents;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => m_Extents = value;
		}

		public Vector3 min
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				Vector3 res = default;
				res.x = m_Center.x - m_Extents.x;
				res.y = m_Center.y - m_Extents.y;
				res.z = m_Center.z - m_Extents.z;
				return res;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				var maxX = m_Center.x + m_Extents.x;
				var maxY = m_Center.y + m_Extents.y;
				var maxZ = m_Center.z + m_Extents.z;
				m_Extents.x = (maxX - value.x) * 0.5f;
				m_Extents.y = (maxY - value.y) * 0.5f;
				m_Extents.z = (maxZ - value.z) * 0.5f;
				m_Center.x  = value.x + m_Extents.x;
				m_Center.y  = value.y + m_Extents.y;
				m_Center.z  = value.z + m_Extents.z;
			}
		}

		public Vector3 max
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				Vector3 res = default;
				res.x = m_Center.x + m_Extents.x;
				res.y = m_Center.y + m_Extents.y;
				res.z = m_Center.z + m_Extents.z;
				return res;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				var minX = m_Center.x - m_Extents.x;
				var minY = m_Center.y - m_Extents.y;
				var minZ = m_Center.z - m_Extents.z;
				m_Extents.x = (value.x - minX) * 0.5f;
				m_Extents.y = (value.y - minY) * 0.5f;
				m_Extents.z = (value.z - minZ) * 0.5f;
				m_Center.x  = minX + m_Extents.x;
				m_Center.y  = minY + m_Extents.y;
				m_Center.z  = minZ + m_Extents.z;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetMinMax(Vector3 min, Vector3 max)
		{
			m_Extents.x = (max.x - min.x) * 0.5f;
			m_Extents.y = (max.y - min.y) * 0.5f;
			m_Extents.z = (max.z - min.z) * 0.5f;
			m_Center.x  = min.x + m_Extents.x;
			m_Center.y  = min.y + m_Extents.y;
			m_Center.z  = min.z + m_Extents.z;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Encapsulate(Vector3 point)
		{
			var minX = MathF.Min(m_Center.x - m_Extents.x, point.x);
			var minY = MathF.Min(m_Center.y - m_Extents.y, point.y);
			var minZ = MathF.Min(m_Center.z - m_Extents.z, point.z);
			var maxX = MathF.Max(m_Center.x + m_Extents.x, point.x);
			var maxY = MathF.Max(m_Center.y + m_Extents.y, point.y);
			var maxZ = MathF.Max(m_Center.z + m_Extents.z, point.z);

			m_Extents.x = (maxX - minX) * 0.5f;
			m_Extents.y = (maxY - minY) * 0.5f;
			m_Extents.z = (maxZ - minZ) * 0.5f;
			m_Center.x  = minX + m_Extents.x;
			m_Center.y  = minY + m_Extents.y;
			m_Center.z  = minZ + m_Extents.z;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Encapsulate(Bounds bounds)
		{
			var minX = MathF.Min(m_Center.x - m_Extents.x, bounds.m_Center.x - bounds.m_Extents.x);
			var minY = MathF.Min(m_Center.y - m_Extents.y, bounds.m_Center.y - bounds.m_Extents.y);
			var minZ = MathF.Min(m_Center.z - m_Extents.z, bounds.m_Center.z - bounds.m_Extents.z);
			var maxX = MathF.Max(m_Center.x + m_Extents.x, bounds.m_Center.x + bounds.m_Extents.x);
			var maxY = MathF.Max(m_Center.y + m_Extents.y, bounds.m_Center.y + bounds.m_Extents.y);
			var maxZ = MathF.Max(m_Center.z + m_Extents.z, bounds.m_Center.z + bounds.m_Extents.z);

			m_Extents.x = (maxX - minX) * 0.5f;
			m_Extents.y = (maxY - minY) * 0.5f;
			m_Extents.z = (maxZ - minZ) * 0.5f;
			m_Center.x  = minX + m_Extents.x;
			m_Center.y  = minY + m_Extents.y;
			m_Center.z  = minZ + m_Extents.z;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Expand(float amount)
		{
			amount      *= 0.5f;
			m_Extents.x += amount;
			m_Extents.y += amount;
			m_Extents.z += amount;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Expand(Vector3 amount)
		{
			m_Extents.x += amount.x * 0.5f;
			m_Extents.y += amount.y * 0.5f;
			m_Extents.z += amount.z * 0.5f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Intersects(Bounds bounds)
		{
			return (m_Center.x - m_Extents.x <= bounds.m_Center.x + bounds.m_Extents.x) &&
			       (m_Center.x + m_Extents.x >= bounds.m_Center.x - bounds.m_Extents.x) &&
			       (m_Center.y - m_Extents.y <= bounds.m_Center.y + bounds.m_Extents.y) &&
			       (m_Center.y + m_Extents.y >= bounds.m_Center.y - bounds.m_Extents.y) &&
			       (m_Center.z - m_Extents.z <= bounds.m_Center.z + bounds.m_Extents.z) &&
			       (m_Center.z + m_Extents.z >= bounds.m_Center.z - bounds.m_Extents.z);
		}
	}
}