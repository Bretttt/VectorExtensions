using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using static VectorExtensions.CollectionHelpers;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace VectorExtensions
{
    public readonly partial struct Vector64<T> : IList<T>, IReadOnlyList<T>, IEquatable<Vector64<T>>, IFormattable
        where T : unmanaged
    {
        static Vector64()
        {
            int size;
            unsafe
            {
                size = sizeof(T);
            }
            if (8 % size != 0)
            {
                throw new ArgumentException("sizeof(T) must be a factor of 8 (64 bits).", nameof(T));
            }
            Count = 8 / size;
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
                ulong word = Word;
                unsafe
                {
                    return ((T*)&word)[index];
                }
            }
        }

        [MethodImpl(AggressiveInlining)]
        public unsafe void Store(T* address) => *(ulong*)address = Word;

        /// <summary>
        /// Creates a new instance of Vector64 with first element set to <paramref name="value"/>.
        /// </summary>
        /// <param name="value">Value to set the first element to</param>
        [MethodImpl(AggressiveInlining)]
        public Vector64(T value)
        {
            unsafe
            {
                ulong word;
                *(T*)&word = value;
                Word = word;
            }
        }

        [MethodImpl(AggressiveInlining)]
        public unsafe Vector64(T* address) => Word = *(ulong*)address;
        public Vector64(ICollection<T> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }
            if ((uint)values.Count > (uint)Count)
            {
                throw new ArgumentException($"Must have Count less than or equal to {Count}.", nameof(values));
            }
            T[] array = new T[Count];
            values.CopyTo(array, 0);
            unsafe
            {
                fixed (T* p = array)
                {
                    Word = *(ulong*)p;
                }
            }
        }
        int ICollection<T>.Count => Count;
        int IReadOnlyCollection<T>.Count => Count;
        bool ICollection<T>.IsReadOnly => true;

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        internal readonly ulong Word;

        [MethodImpl(AggressiveInlining)]
        internal Vector64(ulong word) => Word = word;
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

        void ICollection<T>.CopyTo(T[] array, int arrayIndex) => Copy(this, array, arrayIndex);

        bool ICollection<T>.Remove(T item) => throw ReadOnlyException();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetFixedEnumerator(this);
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)this).GetEnumerator();


        [MethodImpl(AggressiveInlining)]
        public bool Equals(Vector64<T> other) => Word == other.Word;
        public override string ToString() => ToString(null, null);
        public string ToString(string format, IFormatProvider formatProvider) => CollectionHelpers.VectorToString(this, format, formatProvider);

        [MethodImpl(AggressiveInlining)]
        public static Vector64<T> operator ~(Vector64<T> value)
            => new Vector64<T>(~value.Word);

        [MethodImpl(AggressiveInlining)]
        public static Vector64<T> operator |(Vector64<T> left, Vector64<T> right) 
            => new Vector64<T>(left.Word | right.Word);

        [MethodImpl(AggressiveInlining)]
        public static Vector64<T> operator &(Vector64<T> left, Vector64<T> right)
            => new Vector64<T>(left.Word & right.Word);

        [MethodImpl(AggressiveInlining)]
        public static Vector64<T> operator ^(Vector64<T> left, Vector64<T> right)
            => new Vector64<T>(left.Word ^ right.Word);

        [MethodImpl(AggressiveInlining)]
        public static explicit operator ulong(Vector64<T> vector) => vector.Word;

        [MethodImpl(AggressiveInlining)]
        public static explicit operator long(Vector64<T> vector) => (long)vector.Word;
        public override bool Equals(object obj) => obj is Vector64<T> && Equals((Vector64<T>)obj);
        public override int GetHashCode() => Word.GetHashCode();
    }
}