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
        public static Vector128<byte> op_UnaryPlus(Vector128<byte> v) => v;

        [MethodImpl(AggressiveInlining)]
        public static Vector128<byte> op_UnaryNegation(Vector128<byte> v)
            => Sse2.Subtract(new Vector128<byte>(), v);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_add_epi8")]
        public static Vector128<byte> op_Addition(Vector128<byte> left, Vector128<byte> right)
            => Sse2.Add(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_sub_epi8")]
        public static Vector128<byte> op_Subtraction(Vector128<byte> left, Vector128<byte> right)
            => Sse2.Subtract(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<byte> op_Multiply(Vector128<byte> left, Vector128<byte> right)
        {
            Vector128<ushort> lowBits = Vector128.Create((ushort)0x00FF);
            var lowProduct = Sse2.And(lowBits, Sse2.MultiplyLow(left.As<ushort>(), right.As<ushort>())).AsByte();
            var highProduct =
                Sse2.ShiftLeftLogical(
                    Sse2.MultiplyLow(
                        Sse2.ShiftRightLogical(left.As<ushort>(), 8),
                        Sse2.ShiftRightLogical(right.As<ushort>(), 8)
                    ),
                8).AsByte();
            return Sse2.Or(lowProduct, highProduct);
        }
        public static Vector128<byte> op_Division(Vector128<byte> left, Vector128<byte> right)
            => Vector128.Create(
                (byte)(Sse41.Extract(left, 0) / Sse41.Extract(right, 0)),
                (byte)(Sse41.Extract(left, 1) / Sse41.Extract(right, 1)),
                (byte)(Sse41.Extract(left, 2) / Sse41.Extract(right, 2)),
                (byte)(Sse41.Extract(left, 3) / Sse41.Extract(right, 3)),
                (byte)(Sse41.Extract(left, 4) / Sse41.Extract(right, 4)),
                (byte)(Sse41.Extract(left, 5) / Sse41.Extract(right, 5)),
                (byte)(Sse41.Extract(left, 6) / Sse41.Extract(right, 6)),
                (byte)(Sse41.Extract(left, 7) / Sse41.Extract(right, 7)),
                (byte)(Sse41.Extract(left, 8) / Sse41.Extract(right, 8)),
                (byte)(Sse41.Extract(left, 9) / Sse41.Extract(right, 9)),
                (byte)(Sse41.Extract(left, 10) / Sse41.Extract(right, 10)),
                (byte)(Sse41.Extract(left, 11) / Sse41.Extract(right, 11)),
                (byte)(Sse41.Extract(left, 12) / Sse41.Extract(right, 12)),
                (byte)(Sse41.Extract(left, 13) / Sse41.Extract(right, 13)),
                (byte)(Sse41.Extract(left, 14) / Sse41.Extract(right, 14)),
                (byte)(Sse41.Extract(left, 15) / Sse41.Extract(right, 15)));

        public static Vector128<byte> op_Modulus(Vector128<byte> left, Vector128<byte> right)
        => Vector128.Create(
                (byte)(Sse41.Extract(left, 0) % Sse41.Extract(right, 0)),
                (byte)(Sse41.Extract(left, 1) % Sse41.Extract(right, 1)),
                (byte)(Sse41.Extract(left, 2) % Sse41.Extract(right, 2)),
                (byte)(Sse41.Extract(left, 3) % Sse41.Extract(right, 3)),
                (byte)(Sse41.Extract(left, 4) % Sse41.Extract(right, 4)),
                (byte)(Sse41.Extract(left, 5) % Sse41.Extract(right, 5)),
                (byte)(Sse41.Extract(left, 6) % Sse41.Extract(right, 6)),
                (byte)(Sse41.Extract(left, 7) % Sse41.Extract(right, 7)),
                (byte)(Sse41.Extract(left, 8) % Sse41.Extract(right, 8)),
                (byte)(Sse41.Extract(left, 9) % Sse41.Extract(right, 9)),
                (byte)(Sse41.Extract(left, 10) % Sse41.Extract(right, 10)),
                (byte)(Sse41.Extract(left, 11) % Sse41.Extract(right, 11)),
                (byte)(Sse41.Extract(left, 12) % Sse41.Extract(right, 12)),
                (byte)(Sse41.Extract(left, 13) % Sse41.Extract(right, 13)),
                (byte)(Sse41.Extract(left, 14) % Sse41.Extract(right, 14)),
                (byte)(Sse41.Extract(left, 15) % Sse41.Extract(right, 15)));

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_cmpeq_epi8")]
        public static Vector128<byte> op_Equality(Vector128<byte> left, Vector128<byte> right)
            => Sse2.CompareEqual(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<byte> op_Inequality(Vector128<byte> left, Vector128<byte> right)
            => ~op_Equality(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<byte> op_GreaterThan(Vector128<byte> left, Vector128<byte> right)
        {
            var offset = Vector128.Create(sbyte.MinValue);
            return Sse2.CompareGreaterThan(Sse2.Add(left.As<sbyte>(), offset), Sse2.Add(right.As<sbyte>(), offset)).AsByte();
        }
        [MethodImpl(AggressiveInlining)]
        public static Vector128<byte> op_LessThan(Vector128<byte> left, Vector128<byte> right)
        {
            var offset = Vector128.Create(sbyte.MinValue);
            return Sse2.CompareLessThan(Sse2.Add(left.As<sbyte>(), offset), Sse2.Add(right.As<sbyte>(), offset)).AsByte();
        }
        [MethodImpl(AggressiveInlining)]
        public static Vector128<byte> op_GreaterThanOrEqual(Vector128<byte> left, Vector128<byte> right)
            => op_GreaterThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<byte> op_LessThanOrEqual(Vector128<byte> left, Vector128<byte> right)
            => op_LessThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<byte> op_Multiply(Vector128<byte> vector, byte scalar)
            => op_Multiply(vector, Vector128.Create(scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector128<byte> op_Multiply(byte scalar, Vector128<byte> vector)
            => op_Multiply(vector, Vector128.Create(scalar));

        public static Vector128<byte> op_Division(Vector128<byte> vector, byte scalar)
            => Vector128.Create(
                (byte)(Sse41.Extract(vector, 0) / scalar),
                (byte)(Sse41.Extract(vector, 1) / scalar),
                (byte)(Sse41.Extract(vector, 2) / scalar),
                (byte)(Sse41.Extract(vector, 3) / scalar),
                (byte)(Sse41.Extract(vector, 4) / scalar),
                (byte)(Sse41.Extract(vector, 5) / scalar),
                (byte)(Sse41.Extract(vector, 6) / scalar),
                (byte)(Sse41.Extract(vector, 7) / scalar),
                (byte)(Sse41.Extract(vector, 8) / scalar),
                (byte)(Sse41.Extract(vector, 9) / scalar),
                (byte)(Sse41.Extract(vector, 10) / scalar),
                (byte)(Sse41.Extract(vector, 11) / scalar),
                (byte)(Sse41.Extract(vector, 12) / scalar),
                (byte)(Sse41.Extract(vector, 13) / scalar),
                (byte)(Sse41.Extract(vector, 14) / scalar),
                (byte)(Sse41.Extract(vector, 15) / scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector128<byte> op_RightShift(Vector128<byte> vector, byte count)
        {
            var highBits = Vector128.Create((ushort)0xFF00);
            var loBits = Vector128.Create((ushort)0xFF);
            var mask = Sse2.Or(highBits, Sse2.ShiftRightLogical(loBits, count));
            return Sse2.And(Sse2.ShiftRightLogical(vector.As<ushort>(), count), mask).AsByte();
        }
        [MethodImpl(AggressiveInlining)]
        public static Vector128<byte> op_LeftShift(Vector128<byte> vector, byte count)
        {
            var highBits = Vector128.Create((ushort)0xFF00);
            var loBits = Vector128.Create((ushort)0xFF);
            var mask = Sse2.Or(highBits, Sse2.ShiftLeftLogical(loBits, count));
            return Sse2.And(Sse2.ShiftLeftLogical(vector.As<ushort>(), count), mask).AsByte();
        }
        [MethodImpl(AggressiveInlining)]
        public static Vector128<byte> op_RightShift(Vector128<byte> vector, sbyte count)
            => op_RightShift(vector, (byte)count);

        [MethodImpl(AggressiveInlining)]
        public static Vector128<byte> op_LeftShift(Vector128<byte> vector, sbyte count)
            => op_LeftShift(vector, (byte)count);
        public static Vector128<byte> op_RightShift(Vector128<byte> vector, Vector128<byte> count)
        {
            Vector128<uint> loMask = Vector128.Create((uint)0xFF);
            Vector128<uint> vectorChunk = vector.As<uint>() & loMask;
            Vector128<uint> countChunk = count.As<uint>() & loMask;
            Vector128<uint> result = Avx2.ShiftRightLogicalVariable(vectorChunk, countChunk) & loMask;
            vectorChunk = Sse2.ShiftRightLogical(vector.As<uint>(), 8) & loMask;
            countChunk = Sse2.ShiftRightLogical(count.As<uint>(), 8) & loMask;
            result |= Sse2.ShiftLeftLogical(Avx2.ShiftRightLogicalVariable(vectorChunk, countChunk) & loMask, 8);
            vectorChunk = Sse2.ShiftRightLogical(vector.As<uint>(), 16) & loMask;
            countChunk = Sse2.ShiftRightLogical(count.As<uint>(), 16) & loMask;
            result |= Sse2.ShiftLeftLogical(Avx2.ShiftRightLogicalVariable(vectorChunk, countChunk) & loMask, 16);
            vectorChunk = Sse2.ShiftRightLogical(vector.As<uint>(), 24);
            countChunk = Sse2.ShiftRightLogical(count.As<uint>(), 24);
            result |= Sse2.ShiftLeftLogical(Avx2.ShiftRightLogicalVariable(vectorChunk, countChunk) & loMask, 24);
            return result.As<byte>();
        }
        public static Vector128<byte> op_LeftShift(Vector128<byte> vector, Vector128<byte> count)
        {
            Vector128<uint> loMask = Vector128.Create((uint)0xFF);
            Vector128<uint> vectorChunk = vector.As<uint>() & loMask;
            Vector128<uint> countChunk = count.As<uint>() & loMask;
            Vector128<uint> result = Avx2.ShiftLeftLogicalVariable(vectorChunk, countChunk) & loMask;
            vectorChunk = Sse2.ShiftRightLogical(vector.As<uint>(), 8) & loMask;
            countChunk = Sse2.ShiftRightLogical(count.As<uint>(), 8) & loMask;
            result |= Sse2.ShiftLeftLogical(Avx2.ShiftLeftLogicalVariable(vectorChunk, countChunk) & loMask, 8);
            vectorChunk = Sse2.ShiftRightLogical(vector.As<uint>(), 16) & loMask;
            countChunk = Sse2.ShiftRightLogical(count.As<uint>(), 16) & loMask;
            result |= Sse2.ShiftLeftLogical(Avx2.ShiftLeftLogicalVariable(vectorChunk, countChunk) & loMask, 16);
            vectorChunk = Sse2.ShiftRightLogical(vector.As<uint>(), 24);
            countChunk = Sse2.ShiftRightLogical(count.As<uint>(), 24);
            result |= Sse2.ShiftLeftLogical(Avx2.ShiftLeftLogicalVariable(vectorChunk, countChunk) & loMask, 24);
            return result.As<byte>();
        }
        [MethodImpl(AggressiveInlining)]
        public static Vector128<byte> op_RightShift(Vector128<byte> vector, Vector128<sbyte> count)
            => op_RightShift(vector, count.As<byte>());

        [MethodImpl(AggressiveInlining)]
        public static Vector128<byte> op_LeftShift(Vector128<byte> vector, Vector128<sbyte> count)
            => op_LeftShift(vector, count.As<byte>());
    }
}