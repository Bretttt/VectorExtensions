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
        public static Vector256<byte> op_UnaryPlus(Vector256<byte> v) => v;

        [MethodImpl(AggressiveInlining)]
        public static Vector256<byte> op_UnaryNegation(Vector256<byte> v)
            => Avx2.Subtract(new Vector256<byte>(), v);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_add_epi8")]
        public static Vector256<byte> op_Addition(Vector256<byte> left, Vector256<byte> right)
            => Avx2.Add(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_sub_epi8")]
        public static Vector256<byte> op_Subtraction(Vector256<byte> left, Vector256<byte> right)
            => Avx2.Subtract(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<byte> op_Multiply(Vector256<byte> left, Vector256<byte> right)
        {
            Vector256<ushort> lowBits = Vector256.Create((ushort)0x00FF);
            var lowProduct = Avx2.And(lowBits, Avx2.MultiplyLow(left.As<ushort>(), right.As<ushort>())).AsByte();
            var highProduct =
                Avx2.ShiftLeftLogical(
                    Avx2.MultiplyLow(
                        Avx2.ShiftRightLogical(left.As<ushort>(), 8),
                        Avx2.ShiftRightLogical(right.As<ushort>(), 8)
                    ),
                8).AsByte();
            return Avx2.Or(lowProduct, highProduct);
        }
        [MethodImpl(AggressiveInlining)]
        public static Vector256<byte> op_Division(Vector256<byte> left, Vector256<byte> right)
            => Vector256.Create(Vector128<byte>.op_Division(left.Lower, right.Lower), Vector128<byte>.op_Division(left.Upper, right.Upper));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<byte> op_Modulus(Vector256<byte> left, Vector256<byte> right)
            => Vector256.Create(Vector128<byte>.op_Modulus(left.Lower, right.Lower), Vector128<byte>.op_Modulus(left.Upper, right.Upper));

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_cmpeq_epi8")]
        public static Vector256<byte> op_Equality(Vector256<byte> left, Vector256<byte> right)
            => Avx2.CompareEqual(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<byte> op_Inequality(Vector256<byte> left, Vector256<byte> right)
            => ~op_Equality(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<byte> op_GreaterThan(Vector256<byte> left, Vector256<byte> right)
        {
            var offset = Vector256.Create(sbyte.MinValue);
            return Avx2.CompareGreaterThan(Avx2.Add(left.As<sbyte>(), offset), Avx2.Add(right.As<sbyte>(), offset)).AsByte();
        }
        [MethodImpl(AggressiveInlining)]
        public static Vector256<byte> op_LessThan(Vector256<byte> left, Vector256<byte> right)
            => op_GreaterThan(right, left);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<byte> op_GreaterThanOrEqual(Vector256<byte> left, Vector256<byte> right)
            => op_GreaterThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<byte> op_LessThanOrEqual(Vector256<byte> left, Vector256<byte> right)
            => op_LessThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<byte> op_Multiply(Vector256<byte> vector, byte scalar)
            => op_Multiply(vector, Vector256.Create(scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<byte> op_Multiply(byte scalar, Vector256<byte> vector)
            => op_Multiply(vector, Vector256.Create(scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<byte> op_Division(Vector256<byte> vector, byte scalar)
            => Vector256.Create(Vector128<byte>.op_Division(vector.Lower, scalar), Vector128<byte>.op_Division(vector.Upper, scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<byte> op_RightShift(Vector256<byte> vector, byte count)
        {
            var highBits = Vector256.Create((ushort)0xFF00);
            var loBits = Vector256.Create((ushort)0xFF);
            var mask = Avx2.Or(highBits, Avx2.ShiftRightLogical(loBits, count));
            return Avx2.And(Avx2.ShiftRightLogical(vector.As<ushort>(), count), mask).AsByte();
        }

        [MethodImpl(AggressiveInlining)]
        public static Vector256<byte> op_LeftShift(Vector256<byte> vector, byte count)
        {
            var highBits = Vector256.Create((ushort)0xFF00);
            var loBits = Vector256.Create((ushort)0xFF);
            var mask = Avx2.Or(highBits, Avx2.ShiftLeftLogical(loBits, count));
            return Avx2.And(Avx2.ShiftLeftLogical(vector.As<ushort>(), count), mask).AsByte();
        }

        [MethodImpl(AggressiveInlining)]
        public static Vector256<byte> op_RightShift(Vector256<byte> vector, sbyte count)
            => op_RightShift(vector, (byte)count);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<byte> op_LeftShift(Vector256<byte> vector, sbyte count)
            => op_LeftShift(vector, (byte)count);
        public static Vector256<byte> op_RightShift(Vector256<byte> vector, Vector256<byte> count)
        {
            Vector256<uint> loMask = Vector256.Create((uint)0xFF);
            Vector256<uint> vectorChunk = vector.As<uint>() & loMask;
            Vector256<uint> countChunk = count.As<uint>() & loMask;
            Vector256<uint> result = Avx2.ShiftRightLogicalVariable(vectorChunk, countChunk) & loMask;
            vectorChunk = Avx2.ShiftRightLogical(vector.As<uint>(), 8) & loMask;
            countChunk = Avx2.ShiftRightLogical(count.As<uint>(), 8) & loMask;
            result |= Avx2.ShiftLeftLogical(Avx2.ShiftRightLogicalVariable(vectorChunk, countChunk) & loMask, 8);
            vectorChunk = Avx2.ShiftRightLogical(vector.As<uint>(), 16) & loMask;
            countChunk = Avx2.ShiftRightLogical(count.As<uint>(), 16) & loMask;
            result |= Avx2.ShiftLeftLogical(Avx2.ShiftRightLogicalVariable(vectorChunk, countChunk) & loMask, 16);
            vectorChunk = Avx2.ShiftRightLogical(vector.As<uint>(), 24);
            countChunk = Avx2.ShiftRightLogical(count.As<uint>(), 24);
            result |= Avx2.ShiftLeftLogical(Avx2.ShiftRightLogicalVariable(vectorChunk, countChunk) & loMask, 24);
            return result.As<byte>();
        }
        public static Vector256<byte> op_LeftShift(Vector256<byte> vector, Vector256<byte> count)
        {
            Vector256<uint> loMask = Vector256.Create((uint)0xFF);
            Vector256<uint> vectorChunk = vector.As<uint>() & loMask;
            Vector256<uint> countChunk = count.As<uint>() & loMask;
            Vector256<uint> result = Avx2.ShiftLeftLogicalVariable(vectorChunk, countChunk) & loMask;
            vectorChunk = Avx2.ShiftRightLogical(vector.As<uint>(), 8) & loMask;
            countChunk = Avx2.ShiftRightLogical(count.As<uint>(), 8) & loMask;
            result |= Avx2.ShiftLeftLogical(Avx2.ShiftLeftLogicalVariable(vectorChunk, countChunk) & loMask, 8);
            vectorChunk = Avx2.ShiftRightLogical(vector.As<uint>(), 16) & loMask;
            countChunk = Avx2.ShiftRightLogical(count.As<uint>(), 16) & loMask;
            result |= Avx2.ShiftLeftLogical(Avx2.ShiftLeftLogicalVariable(vectorChunk, countChunk) & loMask, 16);
            vectorChunk = Avx2.ShiftRightLogical(vector.As<uint>(), 24);
            countChunk = Avx2.ShiftRightLogical(count.As<uint>(), 24);
            result |= Avx2.ShiftLeftLogical(Avx2.ShiftLeftLogicalVariable(vectorChunk, countChunk) & loMask, 24);
            return result.As<byte>();
        }

        [MethodImpl(AggressiveInlining)]
        public static Vector256<byte> op_RightShift(Vector256<byte> vector, Vector256<sbyte> count)
            => op_RightShift(vector, count.As<byte>());

        [MethodImpl(AggressiveInlining)]
        public static Vector256<byte> op_LeftShift(Vector256<byte> vector, Vector256<sbyte> count)
            => op_LeftShift(vector, count.As<byte>());
    }
}
