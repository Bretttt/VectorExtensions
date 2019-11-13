using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace VectorExtensions
{
    public partial struct Vector64<T>
    {
        private const ulong TrueVector = 0x0101010101010101ul;
        [MethodImpl(AggressiveInlining)]
        public static Vector64<bool> op_LogicalNot(Vector64<bool> vector)
            => new Vector64<bool>(TrueVector) ^ vector;

        [MethodImpl(AggressiveInlining)]
        public static Vector64<bool> op_Equality(Vector64<bool> left, Vector64<bool> right)
            => op_LogicalNot(op_Inequality(left, right));

        [MethodImpl(AggressiveInlining)]
        public static Vector64<bool> op_Inequality(Vector64<bool> left, Vector64<bool> right)
            => left ^ right;
    }
}