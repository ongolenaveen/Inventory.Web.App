using System;
using System.Collections.Generic;
using System.Linq;

namespace Inventory.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Throw Argument Exception when the received string value is null
        /// </summary>
        /// <param name="value">Value of the string</param>
        /// <param name="parameterName">Parameter Name</param>
        /// <returns>Raises Exception if the received string value is null or whitespace</returns>
        public static void ThrowIfIsNullOrWhitespace(this string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(parameterName);
        }


        /// <summary>
        /// Throw Argument Exception when the received string value is null
        /// </summary>
        /// <param name="value">Value of the string</param>
        /// <param name="parameterName">Parameter Name</param>
        /// <returns>Raises Exception if the received string value is Null or Empty</returns>
        public static void ThrowIfIsNullOrEmpty(this string value, string parameterName)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(parameterName);
        }

        /// <summary>
        /// Throw Argument Exception when the received enumerable [is null or Empty].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The enumerable.</param>
        /// <returns>
        /// Raises Exception if the received enumerable is empty or null
        /// </returns>
        public static void ThrowIfIsNullOrEmpty<T>(this IEnumerable<T> enumerable, string parameterName)
        {
            if (enumerable == null || !enumerable.Any())
                throw new ArgumentNullException(parameterName);
        }
    }
}
