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
        public static Vector128<float> op_UnaryPlus(Vector128<float> v) => v;

        [MethodImpl(AggressiveInlining)]
        public static Vector128<float> op_UnaryNegation(Vector128<float> v)
            => Sse.Subtract(new Vector128<float>(), v);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_add_ps")]
        public static Vector128<float> op_Addition(Vector128<float> left, Vector128<float> right)
            => Sse.Add(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_sub_ps")]
        public static Vector128<float> op_Subtraction(Vector128<float> left, Vector128<float> right)
            => Sse.Subtract(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_mul_ps")]
        public static Vector128<float> op_Multiply(Vector128<float> left, Vector128<float> right)
            => Sse.Multiply(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_mul_ss")]
        public static Vector128<float> op_Multiply(Vector128<float> vector, float scalar) 
            => Sse.MultiplyScalar(vector, Vector128.CreateScalarUnsafe(scalar));

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_mul_ss")]
        public static Vector128<float> op_Multiply(float scalar, Vector128<float> vector)
            => op_Multiply(vector, scalar);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_div_ps")]
        public static Vector128<float> op_Division(Vector128<float> left, Vector128<float> right)
            => Sse.Divide(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_div_ss")]
        public static Vector128<float> op_Division(Vector128<float> vector, float scalar) 
            => Sse.DivideScalar(vector, Vector128.CreateScalarUnsafe(scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector128<float> op_Modulus(Vector128<float> left, Vector128<float> right)
            => Vector128.Create(
                ((System.Runtime.Intrinsics.Vector128<float>)left).ToScalar() % ((System.Runtime.Intrinsics.Vector128<float>)right).ToScalar(),
                Sse41.Extract(left, 1) % Sse41.Extract(right, 1),
                Sse41.Extract(left, 2) % Sse41.Extract(right, 2),
                Sse41.Extract(left, 3) % Sse41.Extract(right, 3));

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_cmpeq_ps")]
        public static Vector128<float> op_Equality(Vector128<float> left, Vector128<float> right)
            => Sse.CompareEqual(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_cmpneq_ps")]
        public static Vector128<float> op_Inequality(Vector128<float> left, Vector128<float> right)
            => Sse.CompareNotEqual(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_cmpgt_ps")]
        public static Vector128<float> op_GreaterThan(Vector128<float> left, Vector128<float> right)
            => Sse.CompareGreaterThan(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_cmplt_ps")]
        public static Vector128<float> op_LessThan(Vector128<float> left, Vector128<float> right)
            => Sse.CompareLessThan(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_cmpge_ps")]
        public static Vector128<float> op_GreaterThanOrEqual(Vector128<float> left, Vector128<float> right)
            => Sse.CompareGreaterThanOrEqual(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_cmple_ps")]
        public static Vector128<float> op_LessThanOrEqual(Vector128<float> left, Vector128<float> right)
            => Sse.CompareLessThanOrEqual(left, right);
    }
}
