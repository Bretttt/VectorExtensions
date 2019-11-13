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
        public static Vector256<float> op_UnaryPlus(Vector256<float> v) => v;

        [MethodImpl(AggressiveInlining)]
        public static Vector256<float> op_UnaryNegation(Vector256<float> v)
            => Avx.Subtract(new Vector256<float>(), v);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_add_ps")]
        public static Vector256<float> op_Addition(Vector256<float> left, Vector256<float> right)
            => Avx.Add(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_sub_ps")]
        public static Vector256<float> op_Subtraction(Vector256<float> left, Vector256<float> right)
            => Avx.Subtract(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_mul_ps")]
        public static Vector256<float> op_Multiply(Vector256<float> left, Vector256<float> right)
            => Avx.Multiply(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<float> op_Multiply(Vector256<float> vector, float scalar)
            => Avx.Multiply(vector, Vector256.Create(scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<float> op_Multiply(float scalar, Vector256<float> vector)
            => op_Multiply(vector, scalar);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_div_ps")]
        public static Vector256<float> op_Division(Vector256<float> left, Vector256<float> right)
            => Avx.Divide(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<float> op_Division(Vector256<float> vector, float scalar)
            => Avx.Divide(vector, Vector256.Create(scalar));
        public static Vector256<float> op_Modulus(Vector256<float> left, Vector256<float> right)
            => Vector256.Create(Vector128<float>.op_Modulus(left.Lower, right.Lower), Vector128<float>.op_Modulus(left.Upper, right.Upper));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<float> op_Equality(Vector256<float> left, Vector256<float> right)
            => Avx.Compare(left, right, FloatComparisonMode.OrderedEqualNonSignaling);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<float> op_Inequality(Vector256<float> left, Vector256<float> right)
            => Avx.Compare(left, right, FloatComparisonMode.OrderedNotEqualNonSignaling);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<float> op_GreaterThan(Vector256<float> left, Vector256<float> right)
            => Avx.Compare(left, right, FloatComparisonMode.OrderedGreaterThanNonSignaling);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<float> op_LessThan(Vector256<float> left, Vector256<float> right)
            => Avx.Compare(left, right, FloatComparisonMode.OrderedLessThanNonSignaling);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<float> op_GreaterThanOrEqual(Vector256<float> left, Vector256<float> right)
            => Avx.Compare(left, right, FloatComparisonMode.OrderedGreaterThanOrEqualNonSignaling);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<float> op_LessThanOrEqual(Vector256<float> left, Vector256<float> right)
            => Avx.Compare(left, right, FloatComparisonMode.OrderedLessThanOrEqualNonSignaling);
    }
}
