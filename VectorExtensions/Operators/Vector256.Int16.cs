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
        public static Vector256<short> op_UnaryPlus(Vector256<short> v) => v;

        [MethodImpl(AggressiveInlining)]
        public static Vector256<short> op_UnaryNegation(Vector256<short> v)
            => Avx2.Subtract(new Vector256<short>(), v);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_add_epi16")]
        public static Vector256<short> op_Addition(Vector256<short> left, Vector256<short> right)
            => Avx2.Add(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_sub_epi16")]
        public static Vector256<short> op_Subtraction(Vector256<short> left, Vector256<short> right)
            => Avx2.Subtract(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_mullo_epi16")]
        public static Vector256<short> op_Multiply(Vector256<short> left, Vector256<short> right)
            => Avx2.MultiplyLow(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<short> op_Multiply(Vector256<short> vector, short scalar)
            => op_Multiply(vector, Vector256.Create(scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<short> op_Multiply(short scalar, Vector256<short> vector)
            => op_Multiply(vector, Vector256.Create(scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<short> op_Division(Vector256<short> left, Vector256<short> right)
            => Vector256.Create(Vector128<short>.op_Division(left.Lower, right.Lower), Vector128<short>.op_Division(left.Upper, right.Upper));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<short> op_Division(Vector256<short> vector, short scalar)
            => Vector256.Create(Vector128<short>.op_Division(vector.Lower, scalar), Vector128<short>.op_Division(vector.Upper, scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<short> op_Modulus(Vector256<short> left, Vector256<short> right)
            => Vector256.Create(Vector128<short>.op_Modulus(left.Lower, right.Lower), Vector128<short>.op_Modulus(left.Upper, right.Upper));

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_cmpeq_epi16")]
        public static Vector256<short> op_Equality(Vector256<short> left, Vector256<short> right)
            => Avx2.CompareEqual(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<short> op_Inequality(Vector256<short> left, Vector256<short> right)
            => ~op_Equality(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_cmpgt_epi16")]
        public static Vector256<short> op_GreaterThan(Vector256<short> left, Vector256<short> right)
            => Avx2.CompareGreaterThan(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_cmplt_epi16")]
        public static Vector256<short> op_LessThan(Vector256<short> left, Vector256<short> right)
            => op_GreaterThan(right, left);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<short> op_GreaterThanOrEqual(Vector256<short> left, Vector256<short> right)
            => op_GreaterThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<short> op_LessThanOrEqual(Vector256<short> left, Vector256<short> right)
            => op_LessThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_srli_epi16")]
        public static Vector256<short> op_RightShift(Vector256<short> vector, byte count)
            => Avx2.ShiftRightLogical(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_slli_epi16")]
        public static Vector256<short> op_LeftShift(Vector256<short> vector, byte count)
            => Avx2.ShiftLeftLogical(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_srli_epi16")]
        public static Vector256<short> op_RightShift(Vector256<short> vector, sbyte count)
            => op_RightShift(vector, (byte)count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_slli_epi16")]
        public static Vector256<short> op_LeftShift(Vector256<short> vector, sbyte count)
            => op_LeftShift(vector, (byte)count);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<short> op_RightShift(Vector256<short> vector, Vector256<ushort> count)
            => op_RightShift(vector.As<ushort>(), count).As<short>();

        [MethodImpl(AggressiveInlining)]
        public static Vector256<short> op_LeftShift(Vector256<short> vector, Vector256<ushort> count)
            => op_LeftShift(vector.As<ushort>(), count).As<short>();

        [MethodImpl(AggressiveInlining)]
        public static Vector256<short> op_RightShift(Vector256<short> vector, Vector256<short> count)
            => op_RightShift(vector, count.As<ushort>());

        [MethodImpl(AggressiveInlining)]
        public static Vector256<short> op_LeftShift(Vector256<short> vector, Vector256<short> count)
            => op_LeftShift(vector, count.As<ushort>());
    }
}
