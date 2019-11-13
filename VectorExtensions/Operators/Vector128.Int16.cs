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
        public static Vector128<short> op_UnaryPlus(Vector128<short> v) => v;

        [MethodImpl(AggressiveInlining)]
        public static Vector128<short> op_UnaryNegation(Vector128<short> v)
            => Sse2.Subtract(new Vector128<short>(), v);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_add_epi16")]
        public static Vector128<short> op_Addition(Vector128<short> left, Vector128<short> right)
            => Sse2.Add(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_sub_epi16")]
        public static Vector128<short> op_Subtraction(Vector128<short> left, Vector128<short> right)
            => Sse2.Subtract(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_mullo_epi16")]
        public static Vector128<short> op_Multiply(Vector128<short> left, Vector128<short> right)
            => Sse2.MultiplyLow(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<short> op_Multiply(Vector128<short> vector, short scalar)
            => op_Multiply(vector, Vector128.Create(scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector128<short> op_Multiply(short scalar, Vector128<short> vector)
            => op_Multiply(vector, Vector128.Create(scalar));

        public static Vector128<short> op_Division(Vector128<short> left, Vector128<short> right)
            => Vector128.Create(
                (short)((short)Sse2.Extract(left.As<ushort>(), 0) / (short)Sse2.Extract(right.As<ushort>(), 0)),
                (short)((short)Sse2.Extract(left.As<ushort>(), 1) / (short)Sse2.Extract(right.As<ushort>(), 1)),
                (short)((short)Sse2.Extract(left.As<ushort>(), 2) / (short)Sse2.Extract(right.As<ushort>(), 2)),
                (short)((short)Sse2.Extract(left.As<ushort>(), 3) / (short)Sse2.Extract(right.As<ushort>(), 3)),
                (short)((short)Sse2.Extract(left.As<ushort>(), 4) / (short)Sse2.Extract(right.As<ushort>(), 4)),
                (short)((short)Sse2.Extract(left.As<ushort>(), 5) / (short)Sse2.Extract(right.As<ushort>(), 5)),
                (short)((short)Sse2.Extract(left.As<ushort>(), 6) / (short)Sse2.Extract(right.As<ushort>(), 6)),
                (short)((short)Sse2.Extract(left.As<ushort>(), 7) / (short)Sse2.Extract(right.As<ushort>(), 7)));

        public static Vector128<short> op_Division(Vector128<short> vector, short scalar)
            => Vector128.Create(
                (short)((short)Sse2.Extract(vector.As<ushort>(), 0) / scalar),
                (short)((short)Sse2.Extract(vector.As<ushort>(), 1) / scalar),
                (short)((short)Sse2.Extract(vector.As<ushort>(), 2) / scalar),
                (short)((short)Sse2.Extract(vector.As<ushort>(), 3) / scalar),
                (short)((short)Sse2.Extract(vector.As<ushort>(), 4) / scalar),
                (short)((short)Sse2.Extract(vector.As<ushort>(), 5) / scalar),
                (short)((short)Sse2.Extract(vector.As<ushort>(), 6) / scalar),
                (short)((short)Sse2.Extract(vector.As<ushort>(), 7) / scalar));

        public static Vector128<short> op_Modulus(Vector128<short> left, Vector128<short> right)
            => Vector128.Create(
                (short)((short)Sse2.Extract(left.As<ushort>(), 0) % (short)Sse2.Extract(right.As<ushort>(), 0)),
                (short)((short)Sse2.Extract(left.As<ushort>(), 1) % (short)Sse2.Extract(right.As<ushort>(), 1)),
                (short)((short)Sse2.Extract(left.As<ushort>(), 2) % (short)Sse2.Extract(right.As<ushort>(), 2)),
                (short)((short)Sse2.Extract(left.As<ushort>(), 3) % (short)Sse2.Extract(right.As<ushort>(), 3)),
                (short)((short)Sse2.Extract(left.As<ushort>(), 4) % (short)Sse2.Extract(right.As<ushort>(), 4)),
                (short)((short)Sse2.Extract(left.As<ushort>(), 5) % (short)Sse2.Extract(right.As<ushort>(), 5)),
                (short)((short)Sse2.Extract(left.As<ushort>(), 6) % (short)Sse2.Extract(right.As<ushort>(), 6)),
                (short)((short)Sse2.Extract(left.As<ushort>(), 7) % (short)Sse2.Extract(right.As<ushort>(), 7)));

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_cmpeq_epi16")]
        public static Vector128<short> op_Equality(Vector128<short> left, Vector128<short> right)
            => Sse2.CompareEqual(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<short> op_Inequality(Vector128<short> left, Vector128<short> right)
            => ~op_Equality(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_cmpgt_epi16")]
        public static Vector128<short> op_GreaterThan(Vector128<short> left, Vector128<short> right)
            => Sse2.CompareGreaterThan(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_cmplt_epi16")]
        public static Vector128<short> op_LessThan(Vector128<short> left, Vector128<short> right)
            => Sse2.CompareLessThan(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<short> op_GreaterThanOrEqual(Vector128<short> left, Vector128<short> right)
            => op_GreaterThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<short> op_LessThanOrEqual(Vector128<short> left, Vector128<short> right)
            => op_LessThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_srli_epi16")]
        public static Vector128<short> op_RightShift(Vector128<short> vector, byte count)
            => Sse2.ShiftRightLogical(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_slli_epi16")]
        public static Vector128<short> op_LeftShift(Vector128<short> vector, byte count)
            => Sse2.ShiftLeftLogical(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_srli_epi16")]
        public static Vector128<short> op_RightShift(Vector128<short> vector, sbyte count)
            => op_RightShift(vector, (byte)count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_slli_epi16")]
        public static Vector128<short> op_LeftShift(Vector128<short> vector, sbyte count)
            => op_LeftShift(vector, (byte)count);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<short> op_RightShift(Vector128<short> vector, Vector128<ushort> count)
            => op_RightShift(vector.As<ushort>(), count).As<short>();

        [MethodImpl(AggressiveInlining)]
        public static Vector128<short> op_LeftShift(Vector128<short> vector, Vector128<ushort> count)
            => op_LeftShift(vector.As<ushort>(), count).As<short>();

        [MethodImpl(AggressiveInlining)]
        public static Vector128<short> op_RightShift(Vector128<short> vector, Vector128<short> count)
            => op_RightShift(vector, count.As<ushort>());

        [MethodImpl(AggressiveInlining)]
        public static Vector128<short> op_LeftShift(Vector128<short> vector, Vector128<short> count)
            => op_LeftShift(vector, count.As<ushort>());
    }
}