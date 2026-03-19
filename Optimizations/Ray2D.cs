using System;
using System.Runtime.CompilerServices;

namespace ToolBuddy.FrameRateBooster.Optimizations
{
	internal struct Ray2D
	{
		private Vector2 m_Origin;
		private Vector2 m_Direction;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Ray2D(Vector2 origin, Vector2 direction)
		{
			m_Origin = origin;

			var mag = MathF.Sqrt(direction.x * direction.x + direction.y * direction.y);
			if (mag > 0.00001f)
			{
				var inv = 1f / mag;
				m_Direction.x = direction.x * inv;
				m_Direction.y = direction.y * inv;
			}
			else
			{
				m_Direction = default;
			}
		}

		public Vector2 origin
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => m_Origin;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => m_Origin = value;
		}

		public Vector2 direction
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => m_Direction;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				var mag = MathF.Sqrt(value.x * value.x + value.y * value.y);
				if (mag > 0.00001f)
				{
					var inv = 1f / mag;
					m_Direction.x = value.x * inv;
					m_Direction.y = value.y * inv;
				}
				else
				{
					m_Direction = default;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector2 GetPoint(float distance)
		{
			Vector2 res = default;
			res.x = m_Origin.x + m_Direction.x * distance;
			res.y = m_Origin.y + m_Direction.y * distance;
			return res;
		}
	}
}