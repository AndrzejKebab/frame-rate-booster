using System.Runtime.CompilerServices;

namespace ToolBuddy.FrameRateBooster.Optimizations
{
    internal struct Quaternion
    {
        public float x;
        public float y;
        public float z;
        public float w;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
        {
            Quaternion res = default;
            res.x = lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y;
            res.y = lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z;
            res.z = lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x;
            res.w = lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z;
            return res;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator *(Quaternion rotation, Vector3 point)
        {
            var x = rotation.x * 2f;
            var y = rotation.y * 2f;
            var z = rotation.z * 2f;
            var xx = rotation.x * x;
            var yy = rotation.y * y;
            var zz = rotation.z * z;
            var xy = rotation.x * y;
            var xz = rotation.x * z;
            var yz = rotation.y * z;
            var wx = rotation.w * x;
            var wy = rotation.w * y;
            var wz = rotation.w * z;

            Vector3 res = default;
            res.x = (1f - (yy + zz)) * point.x + (xy - wz) * point.y + (xz + wy) * point.z;
            res.y = (xy + wz) * point.x + (1f - (xx + zz)) * point.y + (yz - wx) * point.z;
            res.z = (xz - wy) * point.x + (yz + wx) * point.y + (1f - (xx + yy)) * point.z;
            return res;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(Quaternion a, Quaternion b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        }
    }
}