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
        public static Vector256<ulong> op_UnaryPlus(Vector256<ulong> v) => v;

        [MethodImpl(AggressiveInlining)]
        public static Vector256<ulong> op_UnaryNegation(Vector256<ulong> v) 
            => Avx2.Subtract(new Vector256<ulong>(), v);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_add_epi64")]
        public static Vector256<ulong> op_Addition(Vector256<ulong> left, Vector256<ulong> right) 
            => Avx2.Add(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_sub_epi64")]
        public static Vector256<ulong> op_Subtraction(Vector256<ulong> left, Vector256<ulong> right) 
            => Avx2.Subtract(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<ulong> op_Multiply(Vector256<ulong> left, Vector256<ulong> right)
            => Vector256.Create(Vector128<ulong>.op_Multiply(left.Lower, right.Lower), Vector128<ulong>.op_Multiply(left.Upper, right.Upper));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<ulong> op_Multiply(Vector256<ulong> vector, ulong scalar)
            => Vector256.Create(Vector128<ulong>.op_Multiply(vector.Lower, scalar), Vector128<ulong>.op_Multiply(vector.Upper, scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<ulong> op_Multiply(ulong scalar, Vector256<ulong> vector) 
            => op_Multiply(vector, scalar);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<ulong> op_Division(Vector256<ulong> left, Vector256<ulong> right)
            => Vector256.Create(Vector128<ulong>.op_Division(left.Lower, right.Lower), Vector128<ulong>.op_Division(left.Upper, right.Upper));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<ulong> op_Division(Vector256<ulong> vector, ulong scalar)
            => Vector256.Create(Vector128<ulong>.op_Division(vector.Lower, scalar), Vector128<ulong>.op_Division(vector.Upper, scalar));

        [MethodImpl(AggressiveInlining)]
        public static Vector256<ulong> op_Modulus(Vector256<ulong> left, Vector256<ulong> right)
            => Vector256.Create(Vector128<ulong>.op_Modulus(left.Lower, right.Lower), Vector128<ulong>.op_Modulus(left.Upper, right.Upper));

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_cmpeq_epi64")]
        public static Vector256<ulong> op_Equality(Vector256<ulong> left, Vector256<ulong> right) 
            => Avx2.CompareEqual(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<ulong> op_Inequality(Vector256<ulong> left, Vector256<ulong> right) 
            => ~op_Equality(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<ulong> op_GreaterThan(Vector256<ulong> left, Vector256<ulong> right)
        {
            var offset = Vector256.Create(long.MinValue);
            return Avx2.CompareGreaterThan(Avx2.Add(left.As<long>(), offset), Avx2.Add(right.As<long>(), offset)).AsUInt64();
        }
        [MethodImpl(AggressiveInlining)]
        public static Vector256<ulong> op_LessThan(Vector256<ulong> left, Vector256<ulong> right) 
            => op_GreaterThan(right, left);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<ulong> op_GreaterThanOrEqual(Vector256<ulong> left, Vector256<ulong> right)
            => op_GreaterThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining)]
        public static Vector256<ulong> op_LessThanOrEqual(Vector256<ulong> left, Vector256<ulong> right)
            => op_LessThan(left, right) | op_Equality(left, right);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_srli_epi64")]
        public static Vector256<ulong> op_RightShift(Vector256<ulong> vector, byte count) 
            => Avx2.ShiftRightLogical(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_slli_epi64")]
        public static Vector256<ulong> op_LeftShift(Vector256<ulong> vector, byte count) 
            => Avx2.ShiftLeftLogical(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_srli_epi64")]
        public static Vector256<ulong> op_RightShift(Vector256<ulong> vector, sbyte count)
            => op_RightShift(vector, (byte)count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_slli_epi64")]
        public static Vector256<ulong> op_LeftShift(Vector256<ulong> vector, sbyte count)
            => op_LeftShift(vector, (byte)count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_srlv_epi64")]
        public static Vector256<ulong> op_RightShift(Vector256<ulong> vector, Vector256<ulong> count) 
            => Avx2.ShiftRightLogicalVariable(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_sllv_epi64")]
        public static Vector256<ulong> op_LeftShift(Vector256<ulong> vector, Vector256<ulong> count) 
            => Avx2.ShiftLeftLogicalVariable(vector, count);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_srlv_epi64")]
        public static Vector256<ulong> op_RightShift(Vector256<ulong> vector, Vector256<long> count) 
            => Avx2.ShiftRightLogicalVariable(vector, count.As<ulong>());

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_sllv_epi64")]
        public static Vector256<ulong> op_LeftShift(Vector256<ulong> vector, Vector256<long> count) 
            => Avx2.ShiftLeftLogicalVariable(vector, count.As<ulong>());
    }
}
