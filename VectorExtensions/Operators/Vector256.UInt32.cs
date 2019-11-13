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
        public static Vector256<uint> op_UnaryPlus(Vector256<uint> v) => v;

        [MethodImpl(AggressiveInlining)]
        public static Vector256<uint> op_UnaryNegation(Vector256<uint> v)
            => Avx2.Subtract(new Vector256<uint>(), v);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_add_epi32")]
        public static Vector256<uint> op_Addition(Vector256<uint> left, Vector256<uint> right)
            => Avx2.Add(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_sub_epi32")]
        public static Vector256<uint> op_Subtraction(Vector256<uint> left, Vector256<uint> right)
            => Avx2.Subtract(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_mullo_epi32")]
        public static Vector256<uint> op_Multiply(Vector256<uint> left, Vector256<uint> right)
            => Avx2.MultiplyLow(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<uint> op_Multiply(Vector256<uint> vector, uint scalar)
            => op_Multiply(vector, Vector256.Create(scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<uint> op_Multiply(uint scalar, Vector256<uint> vector)
            => op_Multiply(vector, Vector256.Create(scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<uint> op_Division(Vector256<uint> left, Vector256<uint> right)
            => Vector256.Create(Vector128<uint>.op_Division(left.Lower, right.Lower), Vector128<uint>.op_Division(left.Upper, right.Upper));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<uint> op_Division(Vector256<uint> vector, uint scalar)
            => Vector256.Create(Vector128<uint>.op_Division(vector.Lower, scalar), Vector128<uint>.op_Division(vector.Upper, scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<uint> op_Modulus(Vector256<uint> left, Vector256<uint> right)
            => Vector256.Create(Vector128<uint>.op_Modulus(left.Lower, right.Lower), Vector128<uint>.op_Modulus(left.Upper, right.Upper));

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_cmpeq_epi32")]
        public static Vector256<uint> op_Equality(Vector256<uint> left, Vector256<uint> right)
            => Avx2.CompareEqual(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<uint> op_Inequality(Vector256<uint> left, Vector256<uint> right)
            => ~op_Equality(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<uint> op_GreaterThan(Vector256<uint> left, Vector256<uint> right)
        {
            var offset = Vector256.Create(int.MinValue);
            return Avx2.CompareGreaterThan(Avx2.Add(left.As<int>(), offset), Avx2.Add(right.As<int>(), offset)).AsUInt32();
        }
        [MethodImpl(AggressiveInlining)]
        public static Vector256<uint> op_LessThan(Vector256<uint> left, Vector256<uint> right)
            => op_GreaterThan(right, left);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<uint> op_GreaterThanOrEqual(Vector256<uint> left, Vector256<uint> right)
            => op_GreaterThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<uint> op_LessThanOrEqual(Vector256<uint> left, Vector256<uint> right)
            => op_LessThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_srli_epi32")]
        public static Vector256<uint> op_RightShift(Vector256<uint> vector, byte count)
            => Avx2.ShiftRightLogical(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_slli_epi32")]
        public static Vector256<uint> op_LeftShift(Vector256<uint> vector, byte count)
            => Avx2.ShiftLeftLogical(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_srli_epi32")]
        public static Vector256<uint> op_RightShift(Vector256<uint> vector, sbyte count)
            => op_RightShift(vector, (byte)count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_slli_epi32")]
        public static Vector256<uint> op_LeftShift(Vector256<uint> vector, sbyte count)
            => op_LeftShift(vector, (byte)count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_srlv_epi32")]
        public static Vector256<uint> op_RightShift(Vector256<uint> vector, Vector256<uint> count)
            => Avx2.ShiftRightLogicalVariable(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_sllv_epi32")]
        public static Vector256<uint> op_LeftShift(Vector256<uint> vector, Vector256<uint> count)
            => Avx2.ShiftLeftLogicalVariable(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_srlv_epi32")]
        public static Vector256<uint> op_RightShift(Vector256<uint> vector, Vector256<int> count)
            => Avx2.ShiftRightLogicalVariable(vector, count.As<uint>());

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_sllv_epi32")]
        public static Vector256<uint> op_LeftShift(Vector256<uint> vector, Vector256<int> count)
            => Avx2.ShiftLeftLogicalVariable(vector, count.As<uint>());
    }
}
