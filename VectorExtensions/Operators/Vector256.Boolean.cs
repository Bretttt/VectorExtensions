using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace VectorExtensions
{
    public partial struct Vector256<T>
    {
        [MethodImpl(AggressiveInlining)]
        public static Vector256<bool> op_LogicalNot(Vector256<bool> vector)
            => (Vector256.Create((byte)1) ^ vector.As<byte>()).As<bool>();

        [MethodImpl(AggressiveInlining)]
        public static Vector256<bool> op_Equality(Vector256<bool> left, Vector256<bool> right)
            => op_LogicalNot(op_Inequality(left, right));

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_xor_si128")]
        public static Vector256<bool> op_Inequality(Vector256<bool> left, Vector256<bool> right)
            => left ^ right;
    }
}
