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
        public static Vector256<double> op_UnaryPlus(Vector256<double> v) => v;

        [MethodImpl(AggressiveInlining)]
        public static Vector256<double> op_UnaryNegation(Vector256<double> v)
            => Avx.Subtract(new Vector256<double>(), v);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_add_ps")]
        public static Vector256<double> op_Addition(Vector256<double> left, Vector256<double> right)
            => Avx.Add(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_sub_ps")]
        public static Vector256<double> op_Subtraction(Vector256<double> left, Vector256<double> right)
            => Avx.Subtract(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_mul_ps")]
        public static Vector256<double> op_Multiply(Vector256<double> left, Vector256<double> right)
            => Avx.Multiply(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<double> op_Multiply(Vector256<double> vector, double scalar)
            => Avx.Multiply(vector, Vector256.Create(scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<double> op_Multiply(double scalar, Vector256<double> vector)
            => op_Multiply(vector, scalar);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_div_ps")]
        public static Vector256<double> op_Division(Vector256<double> left, Vector256<double> right)
            => Avx.Divide(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<double> op_Division(Vector256<double> vector, double scalar)
            => Avx.Divide(vector, Vector256.Create(scalar));
        public static Vector256<double> op_Modulus(Vector256<double> left, Vector256<double> right)
            => Vector256.Create(Vector128<double>.op_Modulus(left.Lower, right.Lower), Vector128<double>.op_Modulus(left.Upper, right.Upper));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<double> op_Equality(Vector256<double> left, Vector256<double> right)
            => Avx.Compare(left, right, FloatComparisonMode.OrderedEqualNonSignaling);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<double> op_Inequality(Vector256<double> left, Vector256<double> right)
            => Avx.Compare(left, right, FloatComparisonMode.OrderedNotEqualNonSignaling);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<double> op_GreaterThan(Vector256<double> left, Vector256<double> right)
            => Avx.Compare(left, right, FloatComparisonMode.OrderedGreaterThanNonSignaling);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<double> op_LessThan(Vector256<double> left, Vector256<double> right)
            => Avx.Compare(left, right, FloatComparisonMode.OrderedLessThanNonSignaling);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<double> op_GreaterThanOrEqual(Vector256<double> left, Vector256<double> right)
            => Avx.Compare(left, right, FloatComparisonMode.OrderedGreaterThanOrEqualNonSignaling);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<double> op_LessThanOrEqual(Vector256<double> left, Vector256<double> right)
            => Avx.Compare(left, right, FloatComparisonMode.OrderedLessThanOrEqualNonSignaling);
    }
}
