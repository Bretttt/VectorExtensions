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
        public static Vector256<int> op_UnaryPlus(Vector256<int> v) => v;

        [MethodImpl(AggressiveInlining)]
        public static Vector256<int> op_UnaryNegation(Vector256<int> v)
            => Avx2.Subtract(new Vector256<int>(), v);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_add_epi32")]
        public static Vector256<int> op_Addition(Vector256<int> left, Vector256<int> right)
            => Avx2.Add(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_sub_epi32")]
        public static Vector256<int> op_Subtraction(Vector256<int> left, Vector256<int> right)
            => Avx2.Subtract(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_mullo_epi32")]
        public static Vector256<int> op_Multiply(Vector256<int> left, Vector256<int> right)
            => Avx2.MultiplyLow(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<int> op_Multiply(Vector256<int> vector, int scalar)
            => op_Multiply(vector, Vector256.Create(scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<int> op_Multiply(int scalar, Vector256<int> vector)
            => op_Multiply(vector, Vector256.Create(scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<int> op_Division(Vector256<int> left, Vector256<int> right)
            => Vector256.Create(Vector128<int>.op_Division(left.Lower, right.Lower), Vector128<int>.op_Division(left.Upper, right.Upper));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<int> op_Division(Vector256<int> vector, int scalar)
            => Vector256.Create(Vector128<int>.op_Division(vector.Lower, scalar), Vector128<int>.op_Division(vector.Upper, scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<int> op_Modulus(Vector256<int> left, Vector256<int> right)
            => Vector256.Create(Vector128<int>.op_Modulus(left.Lower, right.Lower), Vector128<int>.op_Modulus(left.Upper, right.Upper));

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_cmpeq_epi32")]
        public static Vector256<int> op_Equality(Vector256<int> left, Vector256<int> right)
            => Avx2.CompareEqual(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<int> op_Inequality(Vector256<int> left, Vector256<int> right)
            => ~op_Equality(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_cmpgt_epi32")]
        public static Vector256<int> op_GreaterThan(Vector256<int> left, Vector256<int> right)
            => Avx2.CompareGreaterThan(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<int> op_LessThan(Vector256<int> left, Vector256<int> right)
            => op_GreaterThan(right, left);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<int> op_GreaterThanOrEqual(Vector256<int> left, Vector256<int> right)
            => op_GreaterThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<int> op_LessThanOrEqual(Vector256<int> left, Vector256<int> right)
            => op_LessThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_srli_epi32")]
        public static Vector256<int> op_RightShift(Vector256<int> vector, byte count)
            => Avx2.ShiftRightLogical(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_slli_epi32")]
        public static Vector256<int> op_LeftShift(Vector256<int> vector, byte count)
            => Avx2.ShiftLeftLogical(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_srli_epi32")]
        public static Vector256<int> op_RightShift(Vector256<int> vector, sbyte count)
            => op_RightShift(vector, (byte)count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_slli_epi32")]
        public static Vector256<int> op_LeftShift(Vector256<int> vector, sbyte count)
            => op_LeftShift(vector, (byte)count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_srlv_epi32")]
        public static Vector256<int> op_RightShift(Vector256<int> vector, Vector256<uint> count)
            => Avx2.ShiftRightLogicalVariable(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_sllv_epi32")]
        public static Vector256<int> op_LeftShift(Vector256<int> vector, Vector256<uint> count)
            => Avx2.ShiftLeftLogicalVariable(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_srlv_epi32")]
        public static Vector256<int> op_RightShift(Vector256<int> vector, Vector256<int> count)
            => Avx2.ShiftRightLogicalVariable(vector, count.As<uint>());

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_sllv_epi32")]
        public static Vector256<int> op_LeftShift(Vector256<int> vector, Vector256<int> count)
            => Avx2.ShiftLeftLogicalVariable(vector, count.As<uint>());
    }
}
