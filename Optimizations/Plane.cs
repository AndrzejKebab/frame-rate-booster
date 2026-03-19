using System;
using System.Runtime.CompilerServices;

namespace ToolBuddy.FrameRateBooster.Optimizations
{
	internal struct Plane
	{
		public Vector3 m_Normal;
		public float               m_Distance;

		public Plane flipped
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				Plane res = default;
				res.m_Normal.x = -m_Normal.x;
				res.m_Normal.y = -m_Normal.y;
				res.m_Normal.z = -m_Normal.z;
				res.m_Distance = -m_Distance;
				return res;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetNormalAndPosition(Vector3 inNormal, Vector3 inPoint)
		{
			var mag = MathF.Sqrt(inNormal.x * inNormal.x + inNormal.y * inNormal.y + inNormal.z * inNormal.z);
			if (mag > 0.00001f)
			{
				var inv = 1f / mag;
				m_Normal.x = inNormal.x * inv;
				m_Normal.y = inNormal.y * inv;
				m_Normal.z = inNormal.z * inv;
			}
			else
			{
				m_Normal.x = 0f;
				m_Normal.y = 0f;
				m_Normal.z = 0f;
			}

			m_Distance = -(m_Normal.x * inPoint.x + m_Normal.y * inPoint.y + m_Normal.z * inPoint.z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Set3Points(Vector3 a, Vector3 b, Vector3 c)
		{
			var abx = b.x - a.x;
			var aby = b.y - a.y;
			var abz = b.z - a.z;
			var acx = c.x - a.x;
			var acy = c.y - a.y;
			var acz = c.z - a.z;
			var cx  = aby * acz - abz * acy;
			var cy  = abz * acx - abx * acz;
			var cz  = abx * acy - aby * acx;

			var mag = MathF.Sqrt(cx * cx + cy * cy + cz * cz);
			if (mag > 0.00001f)
			{
				var inv = 1f / mag;
				m_Normal.x = cx * inv;
				m_Normal.y = cy * inv;
				m_Normal.z = cz * inv;
			}
			else
			{
				m_Normal.x = 0f;
				m_Normal.y = 0f;
				m_Normal.z = 0f;
			}

			m_Distance = -(m_Normal.x * a.x + m_Normal.y * a.y + m_Normal.z * a.z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Translate(Vector3 translation)
		{
			m_Distance += m_Normal.x * translation.x + m_Normal.y * translation.y + m_Normal.z * translation.z;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Plane Translate(Plane plane, Vector3 translation)
		{
			Plane res = default;
			res.m_Normal = plane.m_Normal;
			res.m_Distance = plane.m_Distance + (plane.m_Normal.x * translation.x + plane.m_Normal.y * translation.y +
			                                     plane.m_Normal.z * translation.z);
			return res;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector3 ClosestPointOnPlane(Vector3 point)
		{
			var     dist = m_Normal.x * point.x + m_Normal.y * point.y + m_Normal.z * point.z + m_Distance;
			Vector3 res  = default;
			res.x = point.x - m_Normal.x * dist;
			res.y = point.y - m_Normal.y * dist;
			res.z = point.z - m_Normal.z * dist;
			return res;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float GetDistanceToPoint(Vector3 point)
		{
			return m_Normal.x * point.x + m_Normal.y * point.y + m_Normal.z * point.z + m_Distance;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool GetSide(Vector3 point)
		{
			return m_Normal.x * point.x + m_Normal.y * point.y + m_Normal.z * point.z + m_Distance > 0.0f;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool SameSide(Vector3 inPt0, Vector3 inPt1)
		{
			var d0 = m_Normal.x * inPt0.x + m_Normal.y * inPt0.y + m_Normal.z * inPt0.z + m_Distance;
			var d1 = m_Normal.x * inPt1.x + m_Normal.y * inPt1.y + m_Normal.z * inPt1.z + m_Distance;
			return (d0 > 0.0f && d1 > 0.0f) || (d0 <= 0.0f && d1 <= 0.0f);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Raycast(Ray ray, out float enter)
		{
			var vdot = ray.direction.x * m_Normal.x + ray.direction.y * m_Normal.y + ray.direction.z * m_Normal.z;
			var ndot = -(ray.origin.x * m_Normal.x + ray.origin.y * m_Normal.y + ray.origin.z * m_Normal.z) -
			           m_Distance;

			if (MathF.Abs(vdot) < 0.000001f)
			{
				enter = 0.0f;
				return false;
			}

			enter = ndot / vdot;
			return enter > 0.0f;
		}
	}
}