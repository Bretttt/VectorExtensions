using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using static VectorExtensions.CollectionHelpers;
using SystemVector = System.Runtime.Intrinsics.Vector256<ulong>;
using System.Runtime.Intrinsics;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.MethodImplOptions;
using System.Collections;

namespace VectorExtensions
{
    public readonly partial struct Vector256<T> : IList<T>, IReadOnlyList<T>, IEquatable<Vector256<T>>, IFormattable
        where T : unmanaged
    {
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        internal readonly SystemVector Vector;
        static Vector256()
        {
            int size;
            unsafe
            {
                size = sizeof(T);
            }
            if (32 % size != 0)
            {
                throw new ArgumentException("sizeof(T) must be a factor of 32 (256 bits).", nameof(T));
            }
            Count = 32 / size;
        }
        public static int Count { get; }
        public T this[int index]
        {
            get
            {
                unsafe
                {
                    ulong* p = stackalloc ulong[4];
                    Avx.Store(p, Vector);
                    return ((T*)p)[index];
                }
            }
        }
        public Vector128<T> Upper
        {
            [MethodImpl(AggressiveInlining)]
            get => new Vector128<T>(Vector.GetUpper());
        }
        public Vector128<T> Lower
        {
            [MethodImpl(AggressiveInlining)]
            get => new Vector128<T>(Vector.GetLower());
        }

        [MethodImpl(AggressiveInlining)]
        public unsafe void Store(T* address) => Avx.Store((ulong*)address, Vector);

        [MethodImpl(AggressiveInlining)]
        internal Vector256(SystemVector vector) => Vector = vector;

        [MethodImpl(AggressiveInlining)]
        public Vector256(Vector128<T> lower, Vector128<T> upper)
            => Vector = Vector256.Create(lower.Vector, upper.Vector);

        [MethodImpl(AggressiveInlining)]
        public Vector256(T value)
        {
            unsafe
            {
                SystemVector vector;
                *(T*)&vector = value;
                Vector = vector;
            }
        }
        public Vector256(params T[] values) => Vector = CreateVector(values);
        public Vector256(ICollection<T> values) => Vector = CreateVector(values);
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
                    return Avx.LoadVector256((ulong*)p);
                }
            }
        }
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
        public Vector256<U> As<U>()
            where U : unmanaged => new Vector256<U>(Vector);

        bool ICollection<T>.Remove(T item) => throw ReadOnlyException();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetFixedEnumerator(this);
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)this).GetEnumerator();

        [MethodImpl(AggressiveInlining)]
        public static Vector256<T> operator ~(Vector256<T> value) // ~v = v ^ (vector of all 1's)
            => new Vector256<T>(Avx2.Xor(value.Vector, Avx2.CompareEqual(value.Vector, value.Vector)));

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_or_si256")]
        public static Vector256<T> operator |(Vector256<T> left, Vector256<T> right)
            => new Vector256<T>(Avx2.Or(left.Vector, right.Vector));

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_and_si256")]
        public static Vector256<T> operator &(Vector256<T> left, Vector256<T> right)
            => new Vector256<T>(Avx2.And(left.Vector, right.Vector));

        [MethodImpl(AggressiveInlining), Intrinsic("_mm256_xor_si256")]
        public static Vector256<T> operator ^(Vector256<T> left, Vector256<T> right)
            => new Vector256<T>(Avx2.Xor(left.Vector, right.Vector));

        [MethodImpl(AggressiveInlining)]
        public bool Equals(Vector256<T> other) => Vector.Equals(other.Vector);
        public override bool Equals(object obj) => obj is Vector256<T> && Equals((Vector256<T>)obj);

        [MethodImpl(AggressiveInlining)]
        public static implicit operator System.Runtime.Intrinsics.Vector256<T>(Vector256<T> vector) => vector.Vector.As<ulong, T>();

        [MethodImpl(AggressiveInlining)]
        public static implicit operator Vector256<T>(System.Runtime.Intrinsics.Vector256<T> vector) => new Vector256<T>(vector.AsUInt64());

        [MethodImpl(AggressiveInlining)]
        public override int GetHashCode() => Vector.GetHashCode();
        public override string ToString() => ToString(null, null);
        public string ToString(string format, IFormatProvider formatProvider) => VectorToString(this, format, formatProvider);
    }
}