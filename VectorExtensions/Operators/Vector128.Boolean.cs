using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace VectorExtensions
{
    public partial struct Vector128<T>
    {
        [MethodImpl(AggressiveInlining)]
        public static Vector128<bool> op_LogicalNot(Vector128<bool> vector)
            => (Vector128.Create((byte)1) ^ vector.As<byte>()).As<bool>();

        [MethodImpl(AggressiveInlining)]
        public static Vector128<bool> op_Equality(Vector128<bool> left, Vector128<bool> right)
            => op_LogicalNot(op_Inequality(left, right));

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_xor_si128")]
        public static Vector128<bool> op_Inequality(Vector128<bool> left, Vector128<bool> right)
            => left ^ right;
    }
}