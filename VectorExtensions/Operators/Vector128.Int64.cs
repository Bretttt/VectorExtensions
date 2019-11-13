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
        public static Vector128<long> op_UnaryPlus(Vector128<long> v) => v;

        [MethodImpl(AggressiveInlining)]
        public static Vector128<long> op_UnaryNegation(Vector128<long> v)
            => Sse2.Subtract(new Vector128<long>(), v);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_add_epi64")]
        public static Vector128<long> op_Addition(Vector128<long> left, Vector128<long> right)
            => Sse2.Add(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_sub_epi64")]
        public static Vector128<long> op_Subtraction(Vector128<long> left, Vector128<long> right)
            => Sse2.Subtract(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<long> op_Multiply(Vector128<long> left, Vector128<long> right)
            => Vector128.Create(
                Sse2.X64.ConvertToInt64(left) * Sse2.X64.ConvertToInt64(right),
                Sse41.X64.Extract(left, 1) * Sse41.X64.Extract(right, 1));

        [MethodImpl(AggressiveInlining)]
        public static Vector128<long> op_Multiply(Vector128<long> vector, long scalar)
            => Vector128.Create(
                Sse2.X64.ConvertToInt64(vector) * scalar,
                Sse41.X64.Extract(vector, 1) * scalar);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<long> op_Multiply(long scalar, Vector128<long> vector)
            => op_Multiply(vector, scalar);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<long> op_Division(Vector128<long> left, Vector128<long> right)
            => Vector128.Create(
                Sse2.X64.ConvertToInt64(left) / Sse2.X64.ConvertToInt64(right),
                Sse41.X64.Extract(left, 1) / Sse41.X64.Extract(right, 1));

        [MethodImpl(AggressiveInlining)]
        public static Vector128<long> op_Division(Vector128<long> vector, long scalar)
            => Vector128.Create(
                Sse2.X64.ConvertToInt64(vector) / scalar,
                Sse41.X64.Extract(vector, 1) / scalar);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<long> op_Modulus(Vector128<long> left, Vector128<long> right)
            => Vector128.Create(
                Sse2.X64.ConvertToInt64(left) % Sse2.X64.ConvertToInt64(right),
                Sse41.X64.Extract(left, 1) % Sse41.X64.Extract(right, 1));

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_cmpeq_epi64")]
        public static Vector128<long> op_Equality(Vector128<long> left, Vector128<long> right)
            => Sse41.CompareEqual(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<long> op_Inequality(Vector128<long> left, Vector128<long> right)
            => ~op_Equality(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_cmpgt_epi64")]
        public static Vector128<long> op_GreaterThan(Vector128<long> left, Vector128<long> right)
            => Sse42.CompareGreaterThan(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<long> op_LessThan(Vector128<long> left, Vector128<long> right)
            => op_GreaterThan(right, left);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<long> op_GreaterThanOrEqual(Vector128<long> left, Vector128<long> right)
            => op_GreaterThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<long> op_LessThanOrEqual(Vector128<long> left, Vector128<long> right)
            => op_LessThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_srli_epi64")]
        public static Vector128<long> op_RightShift(Vector128<long> vector, byte count)
            => Sse2.ShiftRightLogical(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_slli_epi64")]
        public static Vector128<long> op_LeftShift(Vector128<long> vector, byte count)
            => Sse2.ShiftLeftLogical(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_srli_epi64")]
        public static Vector128<long> op_RightShift(Vector128<long> vector, sbyte count)
            => op_RightShift(vector, (byte)count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_slli_epi64")]
        public static Vector128<long> op_LeftShift(Vector128<long> vector, sbyte count)
            => op_LeftShift(vector, (byte)count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_srlv_epi64")]
        public static Vector128<long> op_RightShift(Vector128<long> vector, Vector128<ulong> count)
            => Avx2.ShiftRightLogicalVariable(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_sllv_epi64")]
        public static Vector128<long> op_LeftShift(Vector128<long> vector, Vector128<ulong> count)
            => Avx2.ShiftLeftLogicalVariable(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_srlv_epi64")]
        public static Vector128<long> op_RightShift(Vector128<long> vector, Vector128<long> count)
            => Avx2.ShiftRightLogicalVariable(vector, count.As<ulong>());

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_sllv_epi64")]
        public static Vector128<long> op_LeftShift(Vector128<long> vector, Vector128<long> count)
            => Avx2.ShiftLeftLogicalVariable(vector, count.As<ulong>());
    }
}