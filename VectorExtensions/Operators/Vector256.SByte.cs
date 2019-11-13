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
        public static Vector256<sbyte> op_UnaryPlus(Vector256<sbyte> v) => v;

        [MethodImpl(AggressiveInlining)]
        public static Vector256<sbyte> op_UnaryNegation(Vector256<sbyte> v)
            => Avx2.Subtract(new Vector256<sbyte>(), v);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_add_epi8")]
        public static Vector256<sbyte> op_Addition(Vector256<sbyte> left, Vector256<sbyte> right)
            => Avx2.Add(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_sub_epi8")]
        public static Vector256<sbyte> op_Subtraction(Vector256<sbyte> left, Vector256<sbyte> right)
            => Avx2.Subtract(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<sbyte> op_Multiply(Vector256<sbyte> left, Vector256<sbyte> right)
            => op_Multiply(left.As<byte>(), right.As<byte>()).As<sbyte>();

        [MethodImpl(AggressiveInlining)]
        public static Vector256<sbyte> op_Multiply(Vector256<sbyte> vector, sbyte scalar)
            => op_Multiply(vector, Vector256.Create(scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<sbyte> op_Multiply(sbyte scalar, Vector256<sbyte> vector)
            => op_Multiply(vector, Vector256.Create(scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<sbyte> op_Division(Vector256<sbyte> vector, sbyte scalar)
            => Vector256.Create(Vector128<sbyte>.op_Division(vector.Lower, scalar), Vector128<sbyte>.op_Division(vector.Upper, scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<sbyte> op_Division(Vector256<sbyte> left, Vector256<sbyte> right)
            => Vector256.Create(Vector128<sbyte>.op_Division(left.Lower, right.Lower), Vector128<sbyte>.op_Division(left.Upper, right.Upper));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<sbyte> op_Modulus(Vector256<sbyte> left, Vector256<sbyte> right)
            => Vector256.Create(Vector128<sbyte>.op_Modulus(left.Lower, right.Lower), Vector128<sbyte>.op_Modulus(left.Upper, right.Upper));

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_cmpeq_epi8")]
        public static Vector256<sbyte> op_Equality(Vector256<sbyte> left, Vector256<sbyte> right)
            => Avx2.CompareEqual(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<sbyte> op_Inequality(Vector256<sbyte> left, Vector256<sbyte> right)
            => ~op_Equality(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_cmpgt_epi8")]
        public static Vector256<sbyte> op_GreaterThan(Vector256<sbyte> left, Vector256<sbyte> right)
            => Avx2.CompareGreaterThan(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<sbyte> op_LessThan(Vector256<sbyte> left, Vector256<sbyte> right)
            => Avx2.CompareGreaterThan(right, left);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<sbyte> op_GreaterThanOrEqual(Vector256<sbyte> left, Vector256<sbyte> right)
            => op_GreaterThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<sbyte> op_LessThanOrEqual(Vector256<sbyte> left, Vector256<sbyte> right)
            => op_LessThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<sbyte> op_RightShift(Vector256<sbyte> vector, byte count)
            => op_RightShift(vector.As<byte>(), count).As<sbyte>();

        [MethodImpl(AggressiveInlining)]
        public static Vector256<sbyte> op_LeftShift(Vector256<sbyte> vector, byte count)
            => op_LeftShift(vector.As<byte>(), count).As<sbyte>();

        [MethodImpl(AggressiveInlining)]
        public static Vector256<sbyte> op_RightShift(Vector256<sbyte> vector, sbyte count)
            => op_RightShift(vector, (byte)count);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<sbyte> op_LeftShift(Vector256<sbyte> vector, sbyte count)
            => op_LeftShift(vector, (byte)count);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<sbyte> op_RightShift(Vector256<sbyte> vector, Vector256<byte> count)
            => op_RightShift(vector.As<byte>(), count).As<sbyte>();

        [MethodImpl(AggressiveInlining)]
        public static Vector256<sbyte> op_LeftShift(Vector256<sbyte> vector, Vector256<byte> count)
            => op_LeftShift(vector.As<byte>(), count).As<sbyte>();

        [MethodImpl(AggressiveInlining)]
        public static Vector256<sbyte> op_RightShift(Vector256<sbyte> vector, Vector256<sbyte> count)
            => op_RightShift(vector, count.As<byte>());

        [MethodImpl(AggressiveInlining)]
        public static Vector256<sbyte> op_LeftShift(Vector256<sbyte> vector, Vector256<sbyte> count)
            => op_LeftShift(vector, count.As<byte>());
    }
}
