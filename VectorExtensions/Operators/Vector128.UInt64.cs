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
        public static Vector128<ulong> op_UnaryPlus(Vector128<ulong> v) => v;

        [MethodImpl(AggressiveInlining)]
        public static Vector128<ulong> op_UnaryNegation(Vector128<ulong> v)
            => Sse2.Subtract(new Vector128<ulong>(), v);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_add_epi64")]
        public static Vector128<ulong> op_Addition(Vector128<ulong> left, Vector128<ulong> right)
            => Sse2.Add(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_sub_epi64")]
        public static Vector128<ulong> op_Subtraction(Vector128<ulong> left, Vector128<ulong> right)
            => Sse2.Subtract(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<ulong> op_Multiply(Vector128<ulong> left, Vector128<ulong> right)
            => Vector128.Create(
                Sse2.X64.ConvertToUInt64(left) * Sse2.X64.ConvertToUInt64(right),
                Sse41.X64.Extract(left, 1) * Sse41.X64.Extract(right, 1));

        [MethodImpl(AggressiveInlining)]
        public static Vector128<ulong> op_Multiply(Vector128<ulong> vector, ulong scalar)
            => Vector128.Create(
                Sse2.X64.ConvertToUInt64(vector) * scalar,
                Sse41.X64.Extract(vector, 1) * scalar);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<ulong> op_Multiply(ulong scalar, Vector128<ulong> vector)
            => op_Multiply(vector, scalar);

        public static Vector128<ulong> op_Division(Vector128<ulong> left, Vector128<ulong> right)
            => Vector128.Create(
                Sse2.X64.ConvertToUInt64(left) / Sse2.X64.ConvertToUInt64(right),
                Sse41.X64.Extract(left, 1) / Sse41.X64.Extract(right, 1));

        [MethodImpl(AggressiveInlining)]
        public static Vector128<ulong> op_Division(Vector128<ulong> vector, ulong scalar)
            => Vector128.Create(
                Sse2.X64.ConvertToUInt64(vector) / scalar,
                Sse41.X64.Extract(vector, 1) / scalar);

        public static Vector128<ulong> op_Modulus(Vector128<ulong> left, Vector128<ulong> right)
            => Vector128.Create(
                Sse2.X64.ConvertToUInt64(left) % Sse2.X64.ConvertToUInt64(right),
                Sse41.X64.Extract(left, 1) % Sse41.X64.Extract(right, 1));

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_cmpeq_epi64")]
        public static Vector128<ulong> op_Equality(Vector128<ulong> left, Vector128<ulong> right)
            => Sse41.CompareEqual(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<ulong> op_Inequality(Vector128<ulong> left, Vector128<ulong> right)
            => ~op_Equality(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<ulong> op_GreaterThan(Vector128<ulong> left, Vector128<ulong> right)
        {
            var offset = Vector128.Create(long.MinValue);
            return Sse42.CompareGreaterThan(Sse2.Add(left.As<long>(), offset), Sse2.Add(right.As<long>(), offset)).AsUInt64();
        }
        [MethodImpl(AggressiveInlining)]
        public static Vector128<ulong> op_LessThan(Vector128<ulong> left, Vector128<ulong> right)
            => op_GreaterThan(right, left);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<ulong> op_GreaterThanOrEqual(Vector128<ulong> left, Vector128<ulong> right)
            => op_GreaterThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<ulong> op_LessThanOrEqual(Vector128<ulong> left, Vector128<ulong> right)
            => op_LessThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_srli_epi64")]
        public static Vector128<ulong> op_RightShift(Vector128<ulong> vector, byte count)
            => Sse2.ShiftRightLogical(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_slli_epi64")]
        public static Vector128<ulong> op_LeftShift(Vector128<ulong> vector, byte count)
            => Sse2.ShiftLeftLogical(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_srli_epi64")]
        public static Vector128<ulong> op_RightShift(Vector128<ulong> vector, sbyte count)
            => op_RightShift(vector, (byte)count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_slli_epi64")]
        public static Vector128<ulong> op_LeftShift(Vector128<ulong> vector, sbyte count)
            => op_LeftShift(vector, (byte)count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_srlv_epi64")]
        public static Vector128<ulong> op_RightShift(Vector128<ulong> vector, Vector128<ulong> count)
            => Avx2.ShiftRightLogicalVariable(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_sllv_epi64")]
        public static Vector128<ulong> op_LeftShift(Vector128<ulong> vector, Vector128<ulong> count)
            => Avx2.ShiftLeftLogicalVariable(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_srlv_epi64")]
        public static Vector128<ulong> op_RightShift(Vector128<ulong> vector, Vector128<long> count)
            => Avx2.ShiftRightLogicalVariable(vector, count.As<ulong>());

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_sllv_epi64")]
        public static Vector128<ulong> op_LeftShift(Vector128<ulong> vector, Vector128<long> count)
            => Avx2.ShiftLeftLogicalVariable(vector, count.As<ulong>());
    }
}