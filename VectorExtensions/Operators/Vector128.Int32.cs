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
        public static Vector128<int> op_UnaryPlus(Vector128<int> v) => v;

        [MethodImpl(AggressiveInlining)]
        public static Vector128<int> op_UnaryNegation(Vector128<int> v)
            => Sse2.Subtract(new Vector128<int>(), v);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_add_epi32")]
        public static Vector128<int> op_Addition(Vector128<int> left, Vector128<int> right)
            => Sse2.Add(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_sub_epi32")]
        public static Vector128<int> op_Subtraction(Vector128<int> left, Vector128<int> right)
            => Sse2.Subtract(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_mullo_epi32")]
        public static Vector128<int> op_Multiply(Vector128<int> left, Vector128<int> right)
            => Sse41.MultiplyLow(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<int> op_Multiply(Vector128<int> vector, int scalar)
            => op_Multiply(vector, Vector128.Create(scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector128<int> op_Multiply(int scalar, Vector128<int> vector)
            => op_Multiply(vector, Vector128.Create(scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector128<int> op_Division(Vector128<int> left, Vector128<int> right)
            => Vector128.Create(
                Sse2.ConvertToInt32(left) / Sse2.ConvertToInt32(right),
                Sse41.Extract(left, 1) / Sse41.Extract(right, 1),
                Sse41.Extract(left, 2) / Sse41.Extract(right, 2),
                Sse41.Extract(left, 3) / Sse41.Extract(right, 3));

        [MethodImpl(AggressiveInlining)]
        public static Vector128<int> op_Division(Vector128<int> vector, int scalar)
            => Vector128.Create(
                Sse2.ConvertToInt32(vector) / scalar,
                Sse41.Extract(vector, 1) / scalar,
                Sse41.Extract(vector, 2) / scalar,
                Sse41.Extract(vector, 3) / scalar);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<int> op_Modulus(Vector128<int> left, Vector128<int> right)
            => Vector128.Create(
                Sse2.ConvertToInt32(left) % Sse2.ConvertToInt32(right),
                Sse41.Extract(left, 1) % Sse41.Extract(right, 1),
                Sse41.Extract(left, 2) % Sse41.Extract(right, 2),
                Sse41.Extract(left, 3) % Sse41.Extract(right, 3));

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_cmpeq_epi32")]
        public static Vector128<int> op_Equality(Vector128<int> left, Vector128<int> right)
            => Sse2.CompareEqual(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<int> op_Inequality(Vector128<int> left, Vector128<int> right)
            => ~op_Equality(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_cmpgt_epi32")]
        public static Vector128<int> op_GreaterThan(Vector128<int> left, Vector128<int> right)
            => Sse2.CompareGreaterThan(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_cmplt_epi32")]
        public static Vector128<int> op_LessThan(Vector128<int> left, Vector128<int> right)
            => Sse2.CompareLessThan(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<int> op_GreaterThanOrEqual(Vector128<int> left, Vector128<int> right)
            => op_GreaterThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<int> op_LessThanOrEqual(Vector128<int> left, Vector128<int> right)
            => op_LessThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_srli_epi32")]
        public static Vector128<int> op_RightShift(Vector128<int> vector, byte count)
            => Sse2.ShiftRightLogical(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_slli_epi32")]
        public static Vector128<int> op_LeftShift(Vector128<int> vector, byte count)
            => Sse2.ShiftLeftLogical(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_srli_epi32")]
        public static Vector128<int> op_RightShift(Vector128<int> vector, sbyte count)
            => op_RightShift(vector, (byte)count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_slli_epi32")]
        public static Vector128<int> op_LeftShift(Vector128<int> vector, sbyte count)
            => op_LeftShift(vector, (byte)count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_srlv_epi32")]
        public static Vector128<int> op_RightShift(Vector128<int> vector, Vector128<uint> count)
            => Avx2.ShiftRightLogicalVariable(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_sllv_epi32")]
        public static Vector128<int> op_LeftShift(Vector128<int> vector, Vector128<uint> count)
            => Avx2.ShiftLeftLogicalVariable(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_srlv_epi32")]
        public static Vector128<int> op_RightShift(Vector128<int> vector, Vector128<int> count)
            => Avx2.ShiftRightLogicalVariable(vector, count.As<uint>());

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_sllv_epi32")]
        public static Vector128<int> op_LeftShift(Vector128<int> vector, Vector128<int> count)
            => Avx2.ShiftLeftLogicalVariable(vector, count.As<uint>());
    }
}