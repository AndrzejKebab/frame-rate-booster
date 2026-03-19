using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ToolBuddy.FrameRateBooster.Optimizations
{
	internal struct Matrix4x4
	{
		public float m00;
		public float m10;
		public float m20;
		public float m30;
		public float m01;
		public float m11;
		public float m21;
		public float m31;
		public float m02;
		public float m12;
		public float m22;
		public float m32;
		public float m03;
		public float m13;
		public float m23;
		public float m33;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector3 MultiplyPoint(Vector3 point)
		{
			Vector3 res = default;
			res.x = m00 * point.x + m01 * point.y + m02 * point.z + m03;
			res.y = m10 * point.x + m11 * point.y + m12 * point.z + m13;
			res.z = m20 * point.x + m21 * point.y + m22 * point.z + m23;
			var invW = 1f / (m30 * point.x + m31 * point.y + m32 * point.z + m33);

			res.x *= invW;
			res.y *= invW;
			res.z *= invW;
			return res;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector3 MultiplyPoint3x4(Vector3 point)
		{
			Vector3 res = default;
			res.x = m00 * point.x + m01 * point.y + m02 * point.z + m03;
			res.y = m10 * point.x + m11 * point.y + m12 * point.z + m13;
			res.z = m20 * point.x + m21 * point.y + m22 * point.z + m23;
			return res;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector3 MultiplyVector(Vector3 vector)
		{
			Vector3 res = default;
			res.x = m00 * vector.x + m01 * vector.y + m02 * vector.z;
			res.y = m10 * vector.x + m11 * vector.y + m12 * vector.z;
			res.z = m20 * vector.x + m21 * vector.y + m22 * vector.z;
			return res;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Matrix4x4 operator*(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			Matrix4x4 res;
			res.m00 = lhs.m00 * rhs.m00 + lhs.m01 * rhs.m10 + lhs.m02 * rhs.m20 + lhs.m03 * rhs.m30;
			res.m01 = lhs.m00 * rhs.m01 + lhs.m01 * rhs.m11 + lhs.m02 * rhs.m21 + lhs.m03 * rhs.m31;
			res.m02 = lhs.m00 * rhs.m02 + lhs.m01 * rhs.m12 + lhs.m02 * rhs.m22 + lhs.m03 * rhs.m32;
			res.m03 = lhs.m00 * rhs.m03 + lhs.m01 * rhs.m13 + lhs.m02 * rhs.m23 + lhs.m03 * rhs.m33;

			res.m10 = lhs.m10 * rhs.m00 + lhs.m11 * rhs.m10 + lhs.m12 * rhs.m20 + lhs.m13 * rhs.m30;
			res.m11 = lhs.m10 * rhs.m01 + lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21 + lhs.m13 * rhs.m31;
			res.m12 = lhs.m10 * rhs.m02 + lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22 + lhs.m13 * rhs.m32;
			res.m13 = lhs.m10 * rhs.m03 + lhs.m11 * rhs.m13 + lhs.m12 * rhs.m23 + lhs.m13 * rhs.m33;

			res.m20 = lhs.m20 * rhs.m00 + lhs.m21 * rhs.m10 + lhs.m22 * rhs.m20 + lhs.m23 * rhs.m30;
			res.m21 = lhs.m20 * rhs.m01 + lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21 + lhs.m23 * rhs.m31;
			res.m22 = lhs.m20 * rhs.m02 + lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22 + lhs.m23 * rhs.m32;
			res.m23 = lhs.m20 * rhs.m03 + lhs.m21 * rhs.m13 + lhs.m22 * rhs.m23 + lhs.m23 * rhs.m33;

			res.m30 = lhs.m30 * rhs.m00 + lhs.m31 * rhs.m10 + lhs.m32 * rhs.m20 + lhs.m33 * rhs.m30;
			res.m31 = lhs.m30 * rhs.m01 + lhs.m31 * rhs.m11 + lhs.m32 * rhs.m21 + lhs.m33 * rhs.m31;
			res.m32 = lhs.m30 * rhs.m02 + lhs.m31 * rhs.m12 + lhs.m32 * rhs.m22 + lhs.m33 * rhs.m32;
			res.m33 = lhs.m30 * rhs.m03 + lhs.m31 * rhs.m13 + lhs.m32 * rhs.m23 + lhs.m33 * rhs.m33;

			return res;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator*(Matrix4x4 lhs, Vector4 vector)
		{
			Vector4 res;
			res.x = lhs.m00 * vector.x + lhs.m01 * vector.y + lhs.m02 * vector.z + lhs.m03 * vector.w;
			res.y = lhs.m10 * vector.x + lhs.m11 * vector.y + lhs.m12 * vector.z + lhs.m13 * vector.w;
			res.z = lhs.m20 * vector.x + lhs.m21 * vector.y + lhs.m22 * vector.z + lhs.m23 * vector.w;
			res.w = lhs.m30 * vector.x + lhs.m31 * vector.y + lhs.m32 * vector.z + lhs.m33 * vector.w;
			return res;
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Matrix4x4 Scale(Vector3 vector)
		{
			Matrix4x4 m;
			m.m00 = vector.x; m.m01 = 0F; m.m02       = 0F; m.m03       = 0F;
			m.m10 = 0F; m.m11       = vector.y; m.m12 = 0F; m.m13       = 0F;
			m.m20 = 0F; m.m21       = 0F; m.m22       = vector.z; m.m23 = 0F;
			m.m30 = 0F; m.m31       = 0F; m.m32       = 0F; m.m33       = 1F;
			return m;
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static  Matrix4x4 Translate(Vector3 vector)
		{
			Matrix4x4 m;
			m.m00 = 1F; m.m01 = 0F; m.m02 = 0F; m.m03 = vector.x;
			m.m10 = 0F; m.m11 = 1F; m.m12 = 0F; m.m13 = vector.y;
			m.m20 = 0F; m.m21 = 0F; m.m22 = 1F; m.m23 = vector.z;
			m.m30 = 0F; m.m31 = 0F; m.m32 = 0F; m.m33 = 1F;
			return m;
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Matrix4x4 Rotate(Quaternion q)
		{
			var x  = q.x * 2.0F;
			var y  = q.y * 2.0F;
			var z  = q.z * 2.0F;
			var xx = q.x * x;
			var yy = q.y * y;
			var zz = q.z * z;
			var xy = q.x * y;
			var xz = q.x * z;
			var yz = q.y * z;
			var wx = q.w * x;
			var wy = q.w * y;
			var wz = q.w * z;

			Matrix4x4 m;
			m.m00 = 1.0f - (yy + zz); m.m10 = xy + wz; m.m20          = xz - wy; m.m30          = 0.0F;
			m.m01 = xy - wz; m.m11          = 1.0f - (xx + zz); m.m21 = yz + wx; m.m31          = 0.0F;
			m.m02 = xz + wy; m.m12          = yz - wx; m.m22          = 1.0f - (xx + yy); m.m32 = 0.0F;
			m.m03 = 0.0F; m.m13             = 0.0F; m.m23             = 0.0F; m.m33             = 1.0F;
			return m;
		}
		
		public float this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return index switch
				       {
					       0  => m00,
					       1  => m10,
					       2  => m20,
					       3  => m30,
					       4  => m01,
					       5  => m11,
					       6  => m21,
					       7  => m31,
					       8  => m02,
					       9  => m12,
					       10 => m22,
					       11 => m32,
					       12 => m03,
					       13 => m13,
					       14 => m23,
					       15 => m33,
					       _  => throw new IndexOutOfRangeException("Invalid matrix index!")
				       };
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				switch (index)
				{
					case 0:  m00 = value; break;
					case 1:  m10 = value; break;
					case 2:  m20 = value; break;
					case 3:  m30 = value; break;
					case 4:  m01 = value; break;
					case 5:  m11 = value; break;
					case 6:  m21 = value; break;
					case 7:  m31 = value; break;
					case 8:  m02 = value; break;
					case 9:  m12 = value; break;
					case 10: m22 = value; break;
					case 11: m32 = value; break;
					case 12: m03 = value; break;
					case 13: m13 = value; break;
					case 14: m23 = value; break;
					case 15: m33 = value; break;

					default:
						throw new IndexOutOfRangeException("Invalid matrix index!");
				}
			}
		}
		
		public float this[int row, int column]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => this[row + column * 4];

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => this[row + column * 4] = value;
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public UnityEngine.Vector4 GetColumn(int index)
		{
			return index switch
			       {
				       0 => new UnityEngine.Vector4(m00, m10, m20, m30),
				       1 => new UnityEngine.Vector4(m01, m11, m21, m31),
				       2 => new UnityEngine.Vector4(m02, m12, m22, m32),
				       3 => new UnityEngine.Vector4(m03, m13, m23, m33),
				       _ => throw new IndexOutOfRangeException("Invalid column index!")
			       };
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public UnityEngine.Vector4 GetRow(int index)
		{
			return index switch
			       {
				       0 => new UnityEngine.Vector4(m00, m01, m02, m03),
				       1 => new UnityEngine.Vector4(m10, m11, m12, m13),
				       2 => new UnityEngine.Vector4(m20, m21, m22, m23),
				       3 => new UnityEngine.Vector4(m30, m31, m32, m33),
				       _ => throw new IndexOutOfRangeException("Invalid row index!")
			       };
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetColumn(int index, Vector4 column)
		{
			this[0, index] = column.x;
			this[1, index] = column.y;
			this[2, index] = column.z;
			this[3, index] = column.w;
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetRow(int index, Vector4 row)
		{
			this[index, 0] = row.x;
			this[index, 1] = row.y;
			this[index, 2] = row.z;
			this[index, 3] = row.w;
		}
	}
}