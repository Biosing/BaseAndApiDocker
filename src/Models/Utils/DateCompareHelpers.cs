using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Utils
{
    public static class DateCompareHelpers
    {
        public static bool Later(this DateTimeOffset first, DateTimeOffset second)
        {
            return Compare(first, second) > 0;
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/api/system.datetimeoffset.compare?view=netcore-3.1.
        /// &#60; (less than) 0 - the first is earlier than the second.
        /// = (equal to) 0 - the first is equal the second.
        /// &#62; (greater than) 0 - the first is later than the second.
        /// </summary>
        /// <param name="first">First.</param>
        /// <param name="second">Second.</param>
        /// <returns>Compare result.</returns>
        private static int Compare(DateTimeOffset first, DateTimeOffset second)
        {
            return first.CompareTo(second);
        }
    }
}
