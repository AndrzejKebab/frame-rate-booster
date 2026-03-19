using System.Runtime.CompilerServices;

namespace ToolBuddy.FrameRateBooster.Optimizations
{
	internal struct Ray
	{
		private Vector3 m_Direction;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Ray(Vector3 origin, Vector3 direction)
		{
			this.origin = origin;
			m_Direction = direction.normalized;
		}

		public Vector3 origin
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set;
		}

		public Vector3 direction
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => m_Direction;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => m_Direction = value.normalized;
		}

		public Vector3 GetPoint(float distance)
		{
			return origin + m_Direction * distance;
		}
	}
}