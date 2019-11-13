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
        public static Vector128<ushort> op_UnaryPlus(Vector128<ushort> v) => v;

        [MethodImpl(AggressiveInlining)]
        public static Vector128<ushort> op_UnaryNegation(Vector128<ushort> v)
            => Sse2.Subtract(new Vector128<ushort>(), v);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_add_epi16")]
        public static Vector128<ushort> op_Addition(Vector128<ushort> left, Vector128<ushort> right)
            => Sse2.Add(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_sub_epi16")]
        public static Vector128<ushort> op_Subtraction(Vector128<ushort> left, Vector128<ushort> right)
            => Sse2.Subtract(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_mullo_epi16")]
        public static Vector128<ushort> op_Multiply(Vector128<ushort> left, Vector128<ushort> right)
            => Sse2.MultiplyLow(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<ushort> op_Multiply(Vector128<ushort> vector, ushort scalar)
            => op_Multiply(vector, Vector128.Create(scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector128<ushort> op_Multiply(ushort scalar, Vector128<ushort> vector)
            => op_Multiply(vector, Vector128.Create(scalar));

        public static Vector128<ushort> op_Division(Vector128<ushort> left, Vector128<ushort> right)
            => Vector128.Create(
                (ushort)(Sse2.Extract(left, 0) / Sse2.Extract(right, 0)),
                (ushort)(Sse2.Extract(left, 1) / Sse2.Extract(right, 1)),
                (ushort)(Sse2.Extract(left, 2) / Sse2.Extract(right, 2)),
                (ushort)(Sse2.Extract(left, 3) / Sse2.Extract(right, 3)),
                (ushort)(Sse2.Extract(left, 4) / Sse2.Extract(right, 4)),
                (ushort)(Sse2.Extract(left, 5) / Sse2.Extract(right, 5)),
                (ushort)(Sse2.Extract(left, 6) / Sse2.Extract(right, 6)),
                (ushort)(Sse2.Extract(left, 7) / Sse2.Extract(right, 7)));

        [MethodImpl(AggressiveInlining)]
        public static Vector128<ushort> op_Division(Vector128<ushort> vector, ushort scalar)
            => Vector128.Create(
                (ushort)(Sse2.Extract(vector, 0) / scalar),
                (ushort)(Sse2.Extract(vector, 1) / scalar),
                (ushort)(Sse2.Extract(vector, 2) / scalar),
                (ushort)(Sse2.Extract(vector, 3) / scalar),
                (ushort)(Sse2.Extract(vector, 4) / scalar),
                (ushort)(Sse2.Extract(vector, 5) / scalar),
                (ushort)(Sse2.Extract(vector, 6) / scalar),
                (ushort)(Sse2.Extract(vector, 7) / scalar));

        public static Vector128<ushort> op_Modulus(Vector128<ushort> left, Vector128<ushort> right)
            => Vector128.Create(
                (ushort)(Sse2.Extract(left, 0) % Sse2.Extract(right, 0)),
                (ushort)(Sse2.Extract(left, 1) % Sse2.Extract(right, 1)),
                (ushort)(Sse2.Extract(left, 2) % Sse2.Extract(right, 2)),
                (ushort)(Sse2.Extract(left, 3) % Sse2.Extract(right, 3)),
                (ushort)(Sse2.Extract(left, 4) % Sse2.Extract(right, 4)),
                (ushort)(Sse2.Extract(left, 5) % Sse2.Extract(right, 5)),
                (ushort)(Sse2.Extract(left, 6) % Sse2.Extract(right, 6)),
                (ushort)(Sse2.Extract(left, 7) % Sse2.Extract(right, 7)));

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_cmpeq_epi16")]
        public static Vector128<ushort> op_Equality(Vector128<ushort> left, Vector128<ushort> right)
            => Sse2.CompareEqual(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<ushort> op_Inequality(Vector128<ushort> left, Vector128<ushort> right)
            => ~op_Equality(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<ushort> op_GreaterThan(Vector128<ushort> left, Vector128<ushort> right)
        {
            var offset = Vector128.Create(short.MinValue);
            return Sse2.CompareGreaterThan(Sse2.Add(left.As<short>(), offset), Sse2.Add(right.As<short>(), offset)).AsUInt16();
        }
        [MethodImpl(AggressiveInlining)]
        public static Vector128<ushort> op_LessThan(Vector128<ushort> left, Vector128<ushort> right)
        {
            var offset = Vector128.Create(short.MinValue);
            return Sse2.CompareLessThan(Sse2.Add(left.As<short>(), offset), Sse2.Add(right.As<short>(), offset)).AsUInt16();
        }
        [MethodImpl(AggressiveInlining)]
        public static Vector128<ushort> op_GreaterThanOrEqual(Vector128<ushort> left, Vector128<ushort> right)
            => op_GreaterThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<ushort> op_LessThanOrEqual(Vector128<ushort> left, Vector128<ushort> right)
            => op_LessThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_srli_epi16")]
        public static Vector128<ushort> op_RightShift(Vector128<ushort> vector, byte count)
            => Sse2.ShiftRightLogical(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_slli_epi16")]
        public static Vector128<ushort> op_LeftShift(Vector128<ushort> vector, byte count)
            => Sse2.ShiftLeftLogical(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_srli_epi16")]
        public static Vector128<ushort> op_RightShift(Vector128<ushort> vector, sbyte count)
            => op_RightShift(vector, (byte)count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_slli_epi16")]
        public static Vector128<ushort> op_LeftShift(Vector128<ushort> vector, sbyte count)
            => op_LeftShift(vector, (byte)count);
        public static Vector128<ushort> op_RightShift(Vector128<ushort> vector, Vector128<ushort> count)
        {
            Vector128<uint> loMask = Vector128.Create((uint)0xFFFF);
            Vector128<uint> vectorChunk = vector.As<uint>() & loMask;
            Vector128<uint> countChunk = count.As<uint>() & loMask;
            Vector128<uint> result = Avx2.ShiftRightLogicalVariable(vectorChunk, countChunk) & loMask;
            vectorChunk = Sse2.ShiftRightLogical(vector.As<uint>(), 16);
            countChunk = Sse2.ShiftRightLogical(count.As<uint>(), 16);
            result |= Sse2.ShiftLeftLogical(Avx2.ShiftRightLogicalVariable(vectorChunk, countChunk) & loMask, 16);
            return result.As<ushort>();
        }
        public static Vector128<ushort> op_LeftShift(Vector128<ushort> vector, Vector128<ushort> count)
        {
            Vector128<uint> loMask = Vector128.Create((uint)0xFFFF);
            Vector128<uint> vectorChunk = vector.As<uint>() & loMask;
            Vector128<uint> countChunk = count.As<uint>() & loMask;
            Vector128<uint> result = Avx2.ShiftLeftLogicalVariable(vectorChunk, countChunk) & loMask;
            vectorChunk = Sse2.ShiftRightLogical(vector.As<uint>(), 16);
            countChunk = Sse2.ShiftRightLogical(count.As<uint>(), 16);
            result |= Sse2.ShiftLeftLogical(Avx2.ShiftLeftLogicalVariable(vectorChunk, countChunk) & loMask, 16);
            return result.As<ushort>();
        }

        [MethodImpl(AggressiveInlining)]
        public static Vector128<ushort> op_RightShift(Vector128<ushort> vector, Vector128<short> count)
            => op_RightShift(vector, count.As<ushort>());

        [MethodImpl(AggressiveInlining)]
        public static Vector128<ushort> op_LeftShift(Vector128<ushort> vector, Vector128<short> count)
            => op_LeftShift(vector, count.As<ushort>());
    }
}