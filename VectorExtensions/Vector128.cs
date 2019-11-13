using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using static VectorExtensions.CollectionHelpers;
using SystemVector = System.Runtime.Intrinsics.Vector128<ulong>;
using System.Runtime.Intrinsics;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace VectorExtensions
{
    public readonly partial struct Vector128<T> : IList<T>, IReadOnlyList<T>, IEquatable<Vector128<T>>, IFormattable
        where T : unmanaged
    {
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        internal readonly SystemVector Vector;

        static Vector128()
        {
            int size;
            unsafe
            {
                size = sizeof(T);
            }
            if (16 % size != 0)
            {
                throw new ArgumentException("sizeof(T) must be a factor of 16 (128 bits).", nameof(T));
            }
            Count = 16 / size;
        }        
        public static int Count { get; }
        public T this[int index]
        {
            get
            {
                if ((uint)index >= (uint)Count)
                {
                    throw new IndexOutOfRangeException();
                }
                unsafe
                {
                    ulong* p = stackalloc ulong[2];
                    Sse2.Store(p, Vector);
                    return ((T*)p)[index];
                }
            }
        }
        public Vector64<T> Lower
        {
            [MethodImpl(AggressiveInlining)]
            get => new Vector64<T>(Vector.ToScalar());
        }
        public Vector64<T> Upper
        {
            [MethodImpl(AggressiveInlining)]
            get => new Vector64<T>(Sse41.X64.Extract(Vector, 1));
        }
        [MethodImpl(AggressiveInlining)]
        public unsafe void Store(T* address) => Sse2.Store((ulong*)address, Vector);

        [MethodImpl(AggressiveInlining)]
        internal Vector128(SystemVector vector) => Vector = vector;

        [MethodImpl(AggressiveInlining)]
        public Vector128(T value)
        {
            unsafe
            {
                SystemVector vector;
                *(T*)&vector = value;
                Vector = vector;
            }
        }

        [MethodImpl(AggressiveInlining)]
        public Vector128(Vector64<T> lower, Vector64<T> upper) => Vector = Vector128.Create(lower.Word, upper.Word);
        public Vector128(params T[] values) => Vector = CreateVector(values);
        public Vector128(ICollection<T> values) => Vector = CreateVector(values);
        private static SystemVector CreateVector(ICollection<T> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }
            if ((uint)values.Count > (uint)Count)
            {
                throw new ArgumentException($"Must have Count less than or equal to {Count}.", nameof(values));
            }
            T[] items = new T[Count];
            values.CopyTo(items, 0);
            unsafe
            {
                fixed (T* p = items)
                {
                    return Sse2.LoadVector128((ulong*)p);
                }
            }
        }

        [MethodImpl(AggressiveInlining)]
        public unsafe Vector128(T* address) => Vector = Sse2.LoadVector128((ulong*)address);

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_and_si128")]
        public static Vector128<T> operator &(Vector128<T> left, Vector128<T> right) 
            => new Vector128<T>(Sse2.And(left.Vector, right.Vector));

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_or_si128")]
        public static Vector128<T> operator |(Vector128<T> left, Vector128<T> right)
            => new Vector128<T>(Sse2.Or(left.Vector, right.Vector));

        [MethodImpl(AggressiveInlining), Intrinsic("_mm_xor_si128")]
        public static Vector128<T> operator ^(Vector128<T> left, Vector128<T> right)
            => new Vector128<T>(Sse2.Xor(left.Vector, right.Vector));

        [MethodImpl(AggressiveInlining)]
        public static Vector128<T> operator ~(Vector128<T> vector) // ~v = v ^ (vector of all 1's)
            => new Vector128<T>(Sse2.Xor(vector.Vector, Sse41.CompareEqual(vector.Vector, vector.Vector)));

        [MethodImpl(AggressiveInlining)]
        public static implicit operator System.Runtime.Intrinsics.Vector128<T>(Vector128<T> vector) => vector.Vector.As<ulong, T>();

        [MethodImpl(AggressiveInlining)]
        public static implicit operator Vector128<T>(System.Runtime.Intrinsics.Vector128<T> vector) => new Vector128<T>(vector.AsUInt64());
        public static explicit operator Word128(Vector128<T> vector) => new Word128(vector.Vector);
        public static explicit operator Vector128<T>(Word128 word) => new Vector128<T>(word.m128);
        public Vector128<U> As<U>()
            where U : unmanaged => new Vector128<U>(Vector);

        int ICollection<T>.Count => Count;
        int IReadOnlyCollection<T>.Count => Count;
        bool ICollection<T>.IsReadOnly => true; 
        T IList<T>.this[int index]
        {
            get => this[index];
            set => throw ReadOnlyException();
        }

        int IList<T>.IndexOf(T item) => IndexOf(this, item);

        void IList<T>.Insert(int index, T item) => throw ReadOnlyException();

        void IList<T>.RemoveAt(int index) => throw ReadOnlyException();

        void ICollection<T>.Add(T item) => throw ReadOnlyException();

        void ICollection<T>.Clear() => throw ReadOnlyException();

        bool ICollection<T>.Contains(T item) => Contains(this, item);

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            if ((uint)arrayIndex > (uint)(array.Length - Count))
            {
                throw new IndexOutOfRangeException();
            }
            unsafe
            {
                fixed (T* p = array)
                {
                    Store(p);
                }
            }
        }
        bool ICollection<T>.Remove(T item) => throw ReadOnlyException();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetFixedEnumerator(this);
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)this).GetEnumerator();

        [MethodImpl(AggressiveInlining)]
        public bool Equals(Vector128<T> other) => Vector.Equals(other.Vector);
        public override bool Equals(object obj) => obj is Vector128<T> && Equals((Vector128<T>)obj);

        [MethodImpl(AggressiveInlining)]
        public override int GetHashCode() => Vector.GetHashCode();
        public override string ToString() => ToString(null, null);
        public string ToString(string format, IFormatProvider formatProvider) => VectorToString(this, format, formatProvider);
    }
}