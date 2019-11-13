using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace VectorExtensions
{
    public partial struct Vector128<T>
    {
        [MethodImpl(AggressiveInlining)]
        public static Vector128<uint> op_UnaryPlus(Vector128<uint> v) => v;

        [MethodImpl(AggressiveInlining)]
        public static Vector128<uint> op_UnaryNegation(Vector128<uint> v)
            => Sse2.Subtract(new Vector128<uint>(), v);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_add_epi32")]
        public static Vector128<uint> op_Addition(Vector128<uint> left, Vector128<uint> right)
            => Sse2.Add(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_sub_epi32")]
        public static Vector128<uint> op_Subtraction(Vector128<uint> left, Vector128<uint> right)
            => Sse2.Subtract(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_mullo_epi32")]
        public static Vector128<uint> op_Multiply(Vector128<uint> left, Vector128<uint> right)
            => Sse41.MultiplyLow(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<uint> op_Multiply(Vector128<uint> vector, uint scalar)
            => op_Multiply(vector, Vector128.Create(scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector128<uint> op_Multiply(uint scalar, Vector128<uint> vector)
            => op_Multiply(vector, Vector128.Create(scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector128<uint> op_Division(Vector128<uint> left, Vector128<uint> right)
            => Vector128.Create(
                Sse2.ConvertToUInt32(left) / Sse2.ConvertToUInt32(right),
                Sse41.Extract(left, 1) / Sse41.Extract(right, 1),
                Sse41.Extract(left, 2) / Sse41.Extract(right, 2),
                Sse41.Extract(left, 3) / Sse41.Extract(right, 3));

        [MethodImpl(AggressiveInlining)]
        public static Vector128<uint> op_Division(Vector128<uint> vector, uint scalar)
            => Vector128.Create(
                Sse2.ConvertToUInt32(vector) / scalar,
                Sse41.Extract(vector, 1) / scalar,
                Sse41.Extract(vector, 2) / scalar,
                Sse41.Extract(vector, 3) / scalar);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<uint> op_Modulus(Vector128<uint> left, Vector128<uint> right)
            => Vector128.Create(
                Sse2.ConvertToUInt32(left) % Sse2.ConvertToUInt32(right),
                Sse41.Extract(left, 1) % Sse41.Extract(right, 1),
                Sse41.Extract(left, 2) % Sse41.Extract(right, 2),
                Sse41.Extract(left, 3) % Sse41.Extract(right, 3));

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_cmpeq_epi32")]
        public static Vector128<uint> op_Equality(Vector128<uint> left, Vector128<uint> right)
            => Sse2.CompareEqual(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<uint> op_Inequality(Vector128<uint> left, Vector128<uint> right)
            => ~op_Equality(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<uint> op_GreaterThan(Vector128<uint> left, Vector128<uint> right)
        {
            var offset = Vector128.Create(int.MinValue);
            return Sse2.CompareGreaterThan(Sse2.Add(left.As<int>(), offset), Sse2.Add(right.As<int>(), offset)).AsUInt32();
        }
        [MethodImpl(AggressiveInlining)]
        public static Vector128<uint> op_LessThan(Vector128<uint> left, Vector128<uint> right)
        {
            var offset = Vector128.Create(int.MinValue);
            return Sse2.CompareLessThan(Sse2.Add(left.As<int>(), offset), Sse2.Add(right.As<int>(), offset)).AsUInt32();
        }

        [MethodImpl(AggressiveInlining)]
        public static Vector128<uint> op_GreaterThanOrEqual(Vector128<uint> left, Vector128<uint> right)
            => op_GreaterThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<uint> op_LessThanOrEqual(Vector128<uint> left, Vector128<uint> right)
            => op_LessThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_srli_epi32")]
        public static Vector128<uint> op_RightShift(Vector128<uint> vector, byte count)
            => Sse2.ShiftRightLogical(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_slli_epi32")]
        public static Vector128<uint> op_LeftShift(Vector128<uint> vector, byte count)
            => Sse2.ShiftLeftLogical(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_srli_epi32")]
        public static Vector128<uint> op_RightShift(Vector128<uint> vector, sbyte count)
            => op_RightShift(vector, (byte)count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_slli_epi32")]
        public static Vector128<uint> op_LeftShift(Vector128<uint> vector, sbyte count)
            => op_LeftShift(vector, (byte)count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_srlv_epi32")]
        public static Vector128<uint> op_RightShift(Vector128<uint> vector, Vector128<uint> count)
            => Avx2.ShiftRightLogicalVariable(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_sllv_epi32")]
        public static Vector128<uint> op_LeftShift(Vector128<uint> vector, Vector128<uint> count)
            => Avx2.ShiftLeftLogicalVariable(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_srlv_epi32")]
        public static Vector128<uint> op_RightShift(Vector128<uint> vector, Vector128<int> count)
            => Avx2.ShiftRightLogicalVariable(vector, count.As<uint>());

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_sllv_epi32")]
        public static Vector128<uint> op_LeftShift(Vector128<uint> vector, Vector128<int> count)
            => Avx2.ShiftLeftLogicalVariable(vector, count.As<uint>());
    }
}
