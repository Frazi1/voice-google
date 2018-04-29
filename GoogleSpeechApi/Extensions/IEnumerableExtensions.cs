using System;
using System.Collections.Generic;

namespace GoogleSpeechApi.Extensions
{
    public static class EnumerableExtensions
    {
        public static string JoinToString(this IEnumerable<string> enumerable, string separator)
        {
            return string.Join(separator, enumerable);
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action(item);
            }
        }
    }
}