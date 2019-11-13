using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Intrinsics.X86;
using System.Runtime.Intrinsics;
using System.Linq;
using System.Reflection;
using SystemVector = System.Runtime.Intrinsics.Vector128<ulong>;
using System.Numerics;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace VectorExtensions
{
    public readonly struct Word128 : IConvertible, IEquatable<Word128>, IFormattable
    {
        internal readonly SystemVector m128;

        [MethodImpl(AggressiveInlining)]
        internal Word128(SystemVector vector) => m128 = vector;

        [MethodImpl(AggressiveInlining)]
        public Word128(ulong word) => m128 = Vector128.CreateScalar(word);

        [MethodImpl(AggressiveInlining)]
        public Word128(long word) => m128 = Vector128.CreateScalar(word).AsUInt64();

        [MethodImpl(AggressiveInlining)]
        private Word128(decimal value)
        {
            unsafe
            {
                m128 = Sse2.LoadVector128((ulong*)&value);
            }
        }

        [MethodImpl(AggressiveInlining)]
        public bool Equals(Word128 other) => this == other;

        [MethodImpl(AggressiveInlining)]
        public static bool operator ==(Word128 left, Word128 right) => left.m128.Equals(right.m128);

        [MethodImpl(AggressiveInlining)]
        public static bool operator !=(Word128 left, Word128 right) => !(left == right);
        public override bool Equals(object obj) => this == obj as Word128?;

        [MethodImpl(AggressiveInlining)]
        public override int GetHashCode() => m128.GetHashCode();

        public override string ToString() => ToString(null, null);

        public ulong Lower64
        {
            [MethodImpl(AggressiveInlining), Intrinsic("_mm_cvtsi128_si64")]
            get => Sse2.X64.ConvertToUInt64(m128);
        }
        public ulong Upper64
        {
            [MethodImpl(AggressiveInlining)]
            get => Sse41.X64.Extract(m128, 1);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case null:
                case "R":
                case "r":
                case "G":
                case "g":
                    format = "X";
                    break;
            }
            return new BigInteger(((Vector128<byte>)this).ToArray()).ToString(format, formatProvider);
        }

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_and_si128")]
        public static Word128 operator &(Word128 left, Word128 right) => new Word128(Sse2.And(left.m128, right.m128));

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_or_si128")]
        public static Word128 operator |(Word128 left, Word128 right) => new Word128(Sse2.Or(left.m128, right.m128));

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_xor_si128")]
        public static Word128 operator ^(Word128 left, Word128 right) => new Word128(Sse2.Xor(left.m128, right.m128));

        [MethodImpl(AggressiveInlining)]
        public static Word128 operator ~(Word128 word) // ~v = v ^ (vector of all 1's)
            => new Word128(Sse2.Xor(word.m128, Sse41.CompareEqual(word.m128, word.m128)));

        [MethodImpl(AggressiveInlining)]
        public static implicit operator Word128(ulong value) => new Word128(value);

        [MethodImpl(AggressiveInlining)]
        public static implicit operator Word128(long value) => new Word128(value);

        [MethodImpl(AggressiveInlining)]
        public static implicit operator Word128(uint value) => new Word128(value);

        [MethodImpl(AggressiveInlining)]
        public static implicit operator Word128(int value) => new Word128(value);

        [MethodImpl(AggressiveInlining)]
        public static implicit operator Word128(ushort value) => new Word128(value);

        [MethodImpl(AggressiveInlining)]
        public static implicit operator Word128(short value) => new Word128(value);

        [MethodImpl(AggressiveInlining)]
        public static implicit operator Word128(byte value) => new Word128(value);

        [MethodImpl(AggressiveInlining)]
        public static implicit operator Word128(sbyte value) => new Word128(value);

        [MethodImpl(AggressiveInlining)]
        public static explicit operator Word128(decimal value) => new Word128(value);

        TypeCode IConvertible.GetTypeCode() => TypeCode.Object;

        bool IConvertible.ToBoolean(IFormatProvider provider)
            => Convert.ToBoolean(Sse2.ConvertToInt32(m128.AsInt32()));
        byte IConvertible.ToByte(IFormatProvider provider)
            => (byte)Sse2.ConvertToInt32(m128.AsInt32());

        char IConvertible.ToChar(IFormatProvider provider)
            => (char)Sse2.ConvertToInt32(m128.AsInt32());

        DateTime IConvertible.ToDateTime(IFormatProvider provider) => throw new InvalidCastException();

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            decimal value;
            unsafe
            {
                Sse2.Store((ulong*)&value, m128);
            }
            return value;
        }
        double IConvertible.ToDouble(IFormatProvider provider) => throw new InvalidCastException();

        short IConvertible.ToInt16(IFormatProvider provider) => (short)Sse2.ConvertToInt32(m128.AsInt32());

        int IConvertible.ToInt32(IFormatProvider provider) => Sse2.ConvertToInt32(m128.AsInt32());

        long IConvertible.ToInt64(IFormatProvider provider) => (long)Lower64;

        sbyte IConvertible.ToSByte(IFormatProvider provider) => (sbyte)Sse2.ConvertToInt32(m128.AsInt32());

        float IConvertible.ToSingle(IFormatProvider provider) => throw new InvalidCastException();

        string IConvertible.ToString(IFormatProvider provider) => ToString(null, provider);

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition() == typeof(Vector128<>))
            {
                ConstructorInfo ctor = conversionType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(SystemVector) }, null);
                return ctor.Invoke(new object[] { m128 });
            }
            if (conversionType == typeof(BigInteger))
            {
                return new BigInteger(((Vector128<byte>)this).ToArray());
            }
            return Convert.ChangeType(this, Type.GetTypeCode(conversionType), provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider) => (ushort)Sse2.ConvertToInt32(m128.AsInt32());

        uint IConvertible.ToUInt32(IFormatProvider provider) => Sse2.ConvertToUInt32(m128.AsUInt32());

        ulong IConvertible.ToUInt64(IFormatProvider provider) => Lower64;
    }
}