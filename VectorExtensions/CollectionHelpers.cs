using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace VectorExtensions
{
    internal static class CollectionHelpers
    {
        internal static Exception ReadOnlyException() => new InvalidOperationException("Collection is read-only.");
        internal static void Copy<T>(IEnumerable<T> collection, T[] array, int arrayIndex)
        {
            foreach (T item in collection)
            {
                array[arrayIndex++] = item;
            }
        }
        internal static int IndexOf<T>(IList<T> list, T item)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (Equals(item, list[i]))
                {
                    return i;
                }
            }
            return -1;
        }
        internal static bool Contains<T>(IList<T> list, T value)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (Equals(list[i], value))
                {
                    return true;
                }
            }
            return false;
        }
        internal static string VectorToString<T>(IEnumerable<T> values, string format, IFormatProvider formatProvider)
        {
            string separator;
            if (formatProvider == null)
            {
                separator = GetListSeparator(CultureInfo.InvariantCulture);
            }
            else
            {
                TextInfo textInfo = formatProvider.GetFormat(typeof(TextInfo)) as TextInfo;
                if (textInfo == null)
                {
                    separator = GetListSeparator(formatProvider as CultureInfo ?? CultureInfo.InvariantCulture);                    
                }
                else
                {
                    separator = GetListSeparator(textInfo);
                }
            }
            IEnumerable<string> selector;
            if (typeof(IFormattable).IsAssignableFrom(typeof(T)))
            {
                selector = values.Select(v => ((IFormattable)v).ToString(format, formatProvider));
            }
            else
            {
                selector = values.Select(v => v.ToString());
            }
            return $"<{string.Join(separator, selector)}>";
        }
        private static string GetListSeparator(CultureInfo cultureInfo)
        {
            if (cultureInfo == null)
            {
                return ",";
            }
            TextInfo textInfo = cultureInfo.TextInfo;
            return textInfo == null ? "," : GetListSeparator(textInfo);
        }
        private static string GetListSeparator(TextInfo textInfo) => textInfo.ListSeparator ?? ",";

        internal static IEnumerator<T> GetFixedEnumerator<T>(IList<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                yield return list[i];
            }
        }
    }
}