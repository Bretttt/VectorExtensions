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
        public static Vector128<double> op_UnaryPlus(Vector128<double> v) => v;

        [MethodImpl(AggressiveInlining)]
        public static Vector128<double> op_UnaryNegation(Vector128<double> v)
            => Sse2.Subtract(new Vector128<double>(), v);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_add_pd")]
        public static Vector128<double> op_Addition(Vector128<double> left, Vector128<double> right)
            => Sse2.Add(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_sub_pd")]
        public static Vector128<double> op_Subtraction(Vector128<double> left, Vector128<double> right)
            => Sse2.Subtract(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_mul_pd")]
        public static Vector128<double> op_Multiply(Vector128<double> left, Vector128<double> right)
            => Sse2.Multiply(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_mul_sd")]
        public static Vector128<double> op_Multiply(Vector128<double> vector, double scalar)
            => Sse2.MultiplyScalar(vector, Vector128.CreateScalarUnsafe(scalar));

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_mul_sd")]
        public static Vector128<double> op_Multiply(double scalar, Vector128<double> vector)
            => op_Multiply(vector, scalar);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_div_pd")]
        public static Vector128<double> op_Division(Vector128<double> left, Vector128<double> right)
            => Sse2.Divide(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_div_sd")]
        public static Vector128<double> op_Division(Vector128<double> vector, double scalar)
            => Sse2.DivideScalar(vector, Vector128.CreateScalarUnsafe(scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector128<double> op_Modulus(Vector128<double> left, Vector128<double> right)
            => Vector128.Create(
                ((System.Runtime.Intrinsics.Vector128<double>)left).ToScalar() % ((System.Runtime.Intrinsics.Vector128<double>)right).ToScalar(),
                Reinterpret.Cast<ulong, double>(Sse41.X64.Extract(left.As<ulong>(), 1)) % Reinterpret.Cast<ulong, double>(Sse41.X64.Extract(right.As<ulong>(), 1)));

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_cmpeq_pd")]
        public static Vector128<double> op_Equality(Vector128<double> left, Vector128<double> right)
            => Sse2.CompareEqual(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_cmpneq_pd")]
        public static Vector128<double> op_Inequality(Vector128<double> left, Vector128<double> right)
            => Sse2.CompareNotEqual(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_cmpgt_pd")]
        public static Vector128<double> op_GreaterThan(Vector128<double> left, Vector128<double> right)
            => Sse2.CompareGreaterThan(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_cmplt_pd")]
        public static Vector128<double> op_LessThan(Vector128<double> left, Vector128<double> right)
            => Sse2.CompareLessThan(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_cmpge_pd")]
        public static Vector128<double> op_GreaterThanOrEqual(Vector128<double> left, Vector128<double> right)
            => Sse2.CompareGreaterThanOrEqual(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_cmple_pd")]
        public static Vector128<double> op_LessThanOrEqual(Vector128<double> left, Vector128<double> right)
            => Sse2.CompareLessThanOrEqual(left, right);
    }
}