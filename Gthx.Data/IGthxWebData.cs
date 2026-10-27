using Gthx.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gthx.Data
{
    /// <summary>
    /// A factoid and how many times it has been looked up.
    /// One item can have several values (e.g. "x is a" and "x is also b").
    /// </summary>
    public record FactoidRefCount(string Item, int Count, List<Factoid> Factoids);

    /// <summary>
    /// Read-only queries for the gthx website. Nothing here writes to the database,
    /// so the website can use a database login that only has read access.
    /// </summary>
    public interface IGthxWebData
    {
        /// <summary>
        /// Factoids whose item or value contains <paramref name="search"/> (case-insensitive).
        /// The search text is matched literally: % _ and [ aren't wildcards.
        /// Exact item matches come first. Returns an empty list for blank searches.
        /// </summary>
        public Task<List<Factoid>> SearchFactoidsAsync(string search, int maxResults, CancellationToken cancellationToken = default);

        /// <summary>
        /// The most-referenced factoids that still exist, most references first.
        /// </summary>
        public Task<List<FactoidRefCount>> GetTopFactoidsAsync(int count, CancellationToken cancellationToken = default);

        /// <summary>
        /// The number of distinct factoid items.
        /// </summary>
        public Task<int> GetFactoidCountAsync(CancellationToken cancellationToken = default);
    }
}
