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
        public static Vector128<sbyte> op_UnaryPlus(Vector128<sbyte> v) => v;

        [MethodImpl(AggressiveInlining)]
        public static Vector128<sbyte> op_UnaryNegation(Vector128<sbyte> v)
            => Sse2.Subtract(new Vector128<sbyte>(), v);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_add_epi8")]
        public static Vector128<sbyte> op_Addition(Vector128<sbyte> left, Vector128<sbyte> right)
            => Sse2.Add(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_sub_epi8")]
        public static Vector128<sbyte> op_Subtraction(Vector128<sbyte> left, Vector128<sbyte> right)
            => Sse2.Subtract(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<sbyte> op_Multiply(Vector128<sbyte> left, Vector128<sbyte> right) 
            => op_Multiply(left.As<byte>(), right.As<byte>()).As<sbyte>();

        [MethodImpl(AggressiveInlining)]
        public static Vector128<sbyte> op_Multiply(Vector128<sbyte> vector, sbyte scalar)
            => op_Multiply(vector, Vector128.Create(scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector128<sbyte> op_Multiply(sbyte scalar, Vector128<sbyte> vector)
            => op_Multiply(vector, Vector128.Create(scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector128<sbyte> op_Division(Vector128<sbyte> vector, sbyte scalar)
            => Vector128.Create(
                (sbyte)((sbyte)Sse41.Extract(vector.As<byte>(), 0) / scalar),
                (sbyte)((sbyte)Sse41.Extract(vector.As<byte>(), 1) / scalar),
                (sbyte)((sbyte)Sse41.Extract(vector.As<byte>(), 2) / scalar),
                (sbyte)((sbyte)Sse41.Extract(vector.As<byte>(), 3) / scalar),
                (sbyte)((sbyte)Sse41.Extract(vector.As<byte>(), 4) / scalar),
                (sbyte)((sbyte)Sse41.Extract(vector.As<byte>(), 5) / scalar),
                (sbyte)((sbyte)Sse41.Extract(vector.As<byte>(), 6) / scalar),
                (sbyte)((sbyte)Sse41.Extract(vector.As<byte>(), 7) / scalar),
                (sbyte)((sbyte)Sse41.Extract(vector.As<byte>(), 8) / scalar),
                (sbyte)((sbyte)Sse41.Extract(vector.As<byte>(), 9) / scalar),
                (sbyte)((sbyte)Sse41.Extract(vector.As<byte>(), 10) / scalar),
                (sbyte)((sbyte)Sse41.Extract(vector.As<byte>(), 11) / scalar),
                (sbyte)((sbyte)Sse41.Extract(vector.As<byte>(), 12) / scalar),
                (sbyte)((sbyte)Sse41.Extract(vector.As<byte>(), 13) / scalar),
                (sbyte)((sbyte)Sse41.Extract(vector.As<byte>(), 14) / scalar),
                (sbyte)((sbyte)Sse41.Extract(vector.As<byte>(), 15) / scalar));
        public static Vector128<sbyte> op_Division(Vector128<sbyte> left, Vector128<sbyte> right)
            => Vector128.Create(
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 0) / (sbyte)Sse41.Extract(right.As<byte>(), 0)),
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 1) / (sbyte)Sse41.Extract(right.As<byte>(), 1)),
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 2) / (sbyte)Sse41.Extract(right.As<byte>(), 2)),
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 3) / (sbyte)Sse41.Extract(right.As<byte>(), 3)),
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 4) / (sbyte)Sse41.Extract(right.As<byte>(), 4)),
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 5) / (sbyte)Sse41.Extract(right.As<byte>(), 5)),
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 6) / (sbyte)Sse41.Extract(right.As<byte>(), 6)),
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 7) / (sbyte)Sse41.Extract(right.As<byte>(), 7)),
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 8) / (sbyte)Sse41.Extract(right.As<byte>(), 8)),
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 9) / (sbyte)Sse41.Extract(right.As<byte>(), 9)),
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 10) / (sbyte)Sse41.Extract(right.As<byte>(), 10)),
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 11) / (sbyte)Sse41.Extract(right.As<byte>(), 11)),
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 12) / (sbyte)Sse41.Extract(right.As<byte>(), 12)),
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 13) / (sbyte)Sse41.Extract(right.As<byte>(), 13)),
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 14) / (sbyte)Sse41.Extract(right.As<byte>(), 14)),
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 15) / (sbyte)Sse41.Extract(right.As<byte>(), 15)));

        public static Vector128<sbyte> op_Modulus(Vector128<sbyte> left, Vector128<sbyte> right)
            => Vector128.Create(
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 0) % (sbyte)Sse41.Extract(right.As<byte>(), 0)),
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 1) % (sbyte)Sse41.Extract(right.As<byte>(), 1)),
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 2) % (sbyte)Sse41.Extract(right.As<byte>(), 2)),
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 3) % (sbyte)Sse41.Extract(right.As<byte>(), 3)),
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 4) % (sbyte)Sse41.Extract(right.As<byte>(), 4)),
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 5) % (sbyte)Sse41.Extract(right.As<byte>(), 5)),
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 6) % (sbyte)Sse41.Extract(right.As<byte>(), 6)),
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 7) % (sbyte)Sse41.Extract(right.As<byte>(), 7)),
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 8) % (sbyte)Sse41.Extract(right.As<byte>(), 8)),
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 9) % (sbyte)Sse41.Extract(right.As<byte>(), 9)),
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 10) % (sbyte)Sse41.Extract(right.As<byte>(), 10)),
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 11) % (sbyte)Sse41.Extract(right.As<byte>(), 11)),
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 12) % (sbyte)Sse41.Extract(right.As<byte>(), 12)),
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 13) % (sbyte)Sse41.Extract(right.As<byte>(), 13)),
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 14) % (sbyte)Sse41.Extract(right.As<byte>(), 14)),
                (sbyte)((sbyte)Sse41.Extract(left.As<byte>(), 15) % (sbyte)Sse41.Extract(right.As<byte>(), 15)));

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_cmpeq_epi8")]
        public static Vector128<sbyte> op_Equality(Vector128<sbyte> left, Vector128<sbyte> right)
            => Sse2.CompareEqual(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<sbyte> op_Inequality(Vector128<sbyte> left, Vector128<sbyte> right)
            => ~op_Equality(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_cmpgt_epi8")]
        public static Vector128<sbyte> op_GreaterThan(Vector128<sbyte> left, Vector128<sbyte> right)
            => Sse2.CompareGreaterThan(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_cmplt_epi8")]
        public static Vector128<sbyte> op_LessThan(Vector128<sbyte> left, Vector128<sbyte> right)
            => Sse2.CompareLessThan(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<sbyte> op_GreaterThanOrEqual(Vector128<sbyte> left, Vector128<sbyte> right)
            => op_GreaterThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<sbyte> op_LessThanOrEqual(Vector128<sbyte> left, Vector128<sbyte> right)
            => op_LessThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<sbyte> op_RightShift(Vector128<sbyte> vector, byte count)
            => op_RightShift(vector.As<byte>(), count).As<sbyte>();

        [MethodImpl(AggressiveInlining)]
        public static Vector128<sbyte> op_LeftShift(Vector128<sbyte> vector, byte count)
            => op_LeftShift(vector.As<byte>(), count).As<sbyte>();

        [MethodImpl(AggressiveInlining)]
        public static Vector128<sbyte> op_RightShift(Vector128<sbyte> vector, sbyte count)
            => op_RightShift(vector, (byte)count);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<sbyte> op_LeftShift(Vector128<sbyte> vector, sbyte count)
            => op_LeftShift(vector, (byte)count);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<sbyte> op_RightShift(Vector128<sbyte> vector, Vector128<byte> count)
            => op_RightShift(vector.As<byte>(), count).As<sbyte>();

        [MethodImpl(AggressiveInlining)]
        public static Vector128<sbyte> op_LeftShift(Vector128<sbyte> vector, Vector128<byte> count)
            => op_LeftShift(vector.As<byte>(), count).As<sbyte>();

        [MethodImpl(AggressiveInlining)]
        public static Vector128<sbyte> op_RightShift(Vector128<sbyte> vector, Vector128<sbyte> count)
            => op_RightShift(vector, count.As<byte>());

        [MethodImpl(AggressiveInlining)]
        public static Vector128<sbyte> op_LeftShift(Vector128<sbyte> vector, Vector128<sbyte> count)
            => op_LeftShift(vector, count.As<byte>());
    }
}