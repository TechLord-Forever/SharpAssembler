using System.Collections.Generic;
using System.Linq;

namespace SharpAssembler
{
    /// <summary>
    /// Extensions for the <see cref="IEnumerable{T}"/> type.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Returns only those elements from the sequence that are not <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements.</typeparam>
        /// <param name="enumerable">The enumerable sequence.</param>
        /// <returns>The enumerable sequence without any <see langword="null"/> elements.</returns>
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Where(v => v != null);
        }
    }
}
