using System.Diagnostics.CodeAnalysis;

namespace Valsy.Domain.Common.Extensions
{
    /// <summary>
    /// Helper methods for working with <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Returns true if the numerable is null or empty.
        /// </summary>
        public static bool IsNullOrEmpty<T>([NotNullWhen(false)] this IEnumerable<T> enumerable)
        {
            return enumerable is null || !enumerable.Any();
        }
        /// <summary>
        /// Returns true if the numerable is null or empty.
        /// </summary>
        public static bool IsNullOrEmpty<T>([NotNullWhen(false)] this List<T> enumerable)
        {
            return enumerable is null || enumerable.Count == 0;
        }
        /// <summary>
        /// Converts an enumerable to a SQL IN clause string, or returns empty parentheses if none.
        /// Example: (1,2,3)
        /// </summary>
        public static string ToSqlInClause<T>(this IEnumerable<T> source)
        {
            if (source == null || !source.Any())
                return "()";

            return $"({string.Join(",", source)})";
        }
    }
}
