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
        public static Vector256<ushort> op_UnaryPlus(Vector256<ushort> v) => v;

        [MethodImpl(AggressiveInlining)]
        public static Vector256<ushort> op_UnaryNegation(Vector256<ushort> v)
            => Avx2.Subtract(new Vector256<ushort>(), v);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_add_epi16")]
        public static Vector256<ushort> op_Addition(Vector256<ushort> left, Vector256<ushort> right)
            => Avx2.Add(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_sub_epi16")]
        public static Vector256<ushort> op_Subtraction(Vector256<ushort> left, Vector256<ushort> right)
            => Avx2.Subtract(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_mullo_epi16")]
        public static Vector256<ushort> op_Multiply(Vector256<ushort> left, Vector256<ushort> right)
            => Avx2.MultiplyLow(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<ushort> op_Multiply(Vector256<ushort> vector, ushort scalar)
            => op_Multiply(vector, Vector256.Create(scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<ushort> op_Multiply(ushort scalar, Vector256<ushort> vector)
            => op_Multiply(vector, Vector256.Create(scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<ushort> op_Division(Vector256<ushort> left, Vector256<ushort> right)
            => Vector256.Create(Vector128<ushort>.op_Division(left.Lower, right.Lower), Vector128<ushort>.op_Division(left.Upper, right.Upper));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<ushort> op_Division(Vector256<ushort> vector, ushort scalar)
            => Vector256.Create(Vector128<ushort>.op_Division(vector.Lower, scalar), Vector128<ushort>.op_Division(vector.Upper, scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<ushort> op_Modulus(Vector256<ushort> left, Vector256<ushort> right)
            => Vector256.Create(Vector128<ushort>.op_Modulus(left.Lower, right.Lower), Vector128<ushort>.op_Modulus(left.Upper, right.Upper));

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_cmpeq_epi16")]
        public static Vector256<ushort> op_Equality(Vector256<ushort> left, Vector256<ushort> right)
            => Avx2.CompareEqual(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<ushort> op_Inequality(Vector256<ushort> left, Vector256<ushort> right)
            => ~op_Equality(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<ushort> op_GreaterThan(Vector256<ushort> left, Vector256<ushort> right)
        {
            var offset = Vector256.Create(short.MinValue);
            return Avx2.CompareGreaterThan(Avx2.Add(left.As<short>(), offset), Avx2.Add(right.As<short>(), offset)).AsUInt16();
        }
        [MethodImpl(AggressiveInlining)]
        public static Vector256<ushort> op_LessThan(Vector256<ushort> left, Vector256<ushort> right)
            => op_GreaterThan(right, left);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<ushort> op_GreaterThanOrEqual(Vector256<ushort> left, Vector256<ushort> right)
            => op_GreaterThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<ushort> op_LessThanOrEqual(Vector256<ushort> left, Vector256<ushort> right)
            => op_LessThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_srli_epi16")]
        public static Vector256<ushort> op_RightShift(Vector256<ushort> vector, byte count)
            => Avx2.ShiftRightLogical(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_slli_epi16")]
        public static Vector256<ushort> op_LeftShift(Vector256<ushort> vector, byte count)
            => Avx2.ShiftLeftLogical(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_srli_epi16")]
        public static Vector256<ushort> op_RightShift(Vector256<ushort> vector, sbyte count)
            => op_RightShift(vector, (byte)count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_slli_epi16")]
        public static Vector256<ushort> op_LeftShift(Vector256<ushort> vector, sbyte count)
            => op_LeftShift(vector, (byte)count);
        public static Vector256<ushort> op_RightShift(Vector256<ushort> vector, Vector256<ushort> count)
        {
            Vector256<uint> loMask = Vector256.Create((uint)0xFFFF);
            Vector256<uint> vectorChunk = vector.As<uint>() & loMask;
            Vector256<uint> countChunk = count.As<uint>() & loMask;
            Vector256<uint> result = Avx2.ShiftRightLogicalVariable(vectorChunk, countChunk) & loMask;
            vectorChunk = Avx2.ShiftRightLogical(vector.As<uint>(), 16);
            countChunk = Avx2.ShiftRightLogical(count.As<uint>(), 16);
            result |= Avx2.ShiftLeftLogical(Avx2.ShiftRightLogicalVariable(vectorChunk, countChunk) & loMask, 16);
            return result.As<ushort>();
        }
        public static Vector256<ushort> op_LeftShift(Vector256<ushort> vector, Vector256<ushort> count)
        {
            Vector256<uint> loMask = Vector256.Create((uint)0xFFFF);
            Vector256<uint> vectorChunk = vector.As<uint>() & loMask;
            Vector256<uint> countChunk = count.As<uint>() & loMask;
            Vector256<uint> result = Avx2.ShiftLeftLogicalVariable(vectorChunk, countChunk) & loMask;
            vectorChunk = Avx2.ShiftRightLogical(vector.As<uint>(), 16);
            countChunk = Avx2.ShiftRightLogical(count.As<uint>(), 16);
            result |= Avx2.ShiftLeftLogical(Avx2.ShiftLeftLogicalVariable(vectorChunk, countChunk) & loMask, 16);
            return result.As<ushort>();
        }
        [MethodImpl(AggressiveInlining)]
        public static Vector256<ushort> op_RightShift(Vector256<ushort> vector, Vector256<short> count)
            => op_RightShift(vector, count.As<ushort>());

        [MethodImpl(AggressiveInlining)]
        public static Vector256<ushort> op_LeftShift(Vector256<ushort> vector, Vector256<short> count)
            => op_LeftShift(vector, count.As<ushort>());
    }
}