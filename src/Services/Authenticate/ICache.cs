using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Authenticate
{
    public interface ICache
    {
        Task SetAsync(string key, string value, TimeSpan duration);

        Task SetAsync<T>(string key, T value, TimeSpan duration);

        /// <summary>
        /// Returns serialized string in the cache or null.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <returns>A value or default.</returns>
        Task<string> GetAsync(string key);

        /// <summary>
        /// Returns value stored in the cache or default (null for references).
        /// </summary>
        /// <param name="key">Key.</param>
        /// <typeparam name="T">Type param.</typeparam>
        /// <returns>A value or default.</returns>
        Task<T> GetAsync<T>(string key);

        Task RemoveAsync(string key);
    }
}
