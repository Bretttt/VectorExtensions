using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace VectorExtensions
{
    public partial struct Vector256<T>
    {
        [MethodImpl(AggressiveInlining)]
        public static Vector256<long> op_UnaryPlus(Vector256<long> v) => v;

        [MethodImpl(AggressiveInlining)]
        public static Vector256<long> op_UnaryNegation(Vector256<long> v)
            => Avx2.Subtract(new Vector256<long>(), v);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_add_epi64")]
        public static Vector256<long> op_Addition(Vector256<long> left, Vector256<long> right)
            => Avx2.Add(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_sub_epi64")]
        public static Vector256<long> op_Subtraction(Vector256<long> left, Vector256<long> right)
            => Avx2.Subtract(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<long> op_Multiply(Vector256<long> left, Vector256<long> right)
            => Vector256.Create(Vector128<long>.op_Multiply(left.Lower, right.Lower), Vector128<long>.op_Multiply(left.Upper, right.Upper));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<long> op_Multiply(Vector256<long> vector, long scalar)
            => Vector256.Create(Vector128<long>.op_Multiply(vector.Lower, scalar), Vector128<long>.op_Multiply(vector.Upper, scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<long> op_Multiply(long scalar, Vector256<long> vector)
            => op_Multiply(vector, scalar);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<long> op_Division(Vector256<long> left, Vector256<long> right)
            => Vector256.Create(Vector128<long>.op_Division(left.Lower, right.Lower), Vector128<long>.op_Division(left.Upper, right.Upper));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<long> op_Division(Vector256<long> vector, long scalar)
            => Vector256.Create(Vector128<long>.op_Division(vector.Lower, scalar), Vector128<long>.op_Division(vector.Upper, scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<long> op_Modulus(Vector256<long> left, Vector256<long> right)
            => Vector256.Create(Vector128<long>.op_Modulus(left.Lower, right.Lower), Vector128<long>.op_Modulus(left.Upper, right.Upper));

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_cmpeq_epi64")]
        public static Vector256<long> op_Equality(Vector256<long> left, Vector256<long> right)
            => Avx2.CompareEqual(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<long> op_Inequality(Vector256<long> left, Vector256<long> right)
            => ~op_Equality(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_cmpgt_epi64")]
        public static Vector256<long> op_GreaterThan(Vector256<long> left, Vector256<long> right)
            => Avx2.CompareGreaterThan(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<long> op_LessThan(Vector256<long> left, Vector256<long> right)
            => op_GreaterThan(right, left);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<long> op_GreaterThanOrEqual(Vector256<long> left, Vector256<long> right)
            => op_GreaterThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<long> op_LessThanOrEqual(Vector256<long> left, Vector256<long> right)
            => op_LessThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_srli_epi64")]
        public static Vector256<long> op_RightShift(Vector256<long> vector, byte count)
            => Avx2.ShiftRightLogical(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_slli_epi64")]
        public static Vector256<long> op_LeftShift(Vector256<long> vector, byte count)
            => Avx2.ShiftLeftLogical(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_srli_epi64")]
        public static Vector256<long> op_RightShift(Vector256<long> vector, sbyte count)
            => op_RightShift(vector, (byte)count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_slli_epi64")]
        public static Vector256<long> op_LeftShift(Vector256<long> vector, sbyte count)
            => op_LeftShift(vector, (byte)count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_srlv_epi64")]
        public static Vector256<long> op_RightShift(Vector256<long> vector, Vector256<ulong> count)
            => Avx2.ShiftRightLogicalVariable(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_sllv_epi64")]
        public static Vector256<long> op_LeftShift(Vector256<long> vector, Vector256<ulong> count)
            => Avx2.ShiftLeftLogicalVariable(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_srlv_epi64")]
        public static Vector256<long> op_RightShift(Vector256<long> vector, Vector256<long> count)
            => Avx2.ShiftRightLogicalVariable(vector, count.As<ulong>());

        [MethodImpl(AggressiveInlining), Intrinsic("_m256m_sllv_epi64")]
        public static Vector256<long> op_LeftShift(Vector256<long> vector, Vector256<long> count)
            => Avx2.ShiftLeftLogicalVariable(vector, count.As<ulong>());
    }
}
