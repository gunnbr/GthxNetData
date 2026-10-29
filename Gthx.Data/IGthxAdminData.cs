using Gthx.Core;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gthx.Data
{
    /// <summary>
    /// Changes made from the website's admin panel: to factoids, and to the keywords the bot emails the admin about. Kept apart from <see cref="IGthxWebData"/> so the
    /// website's public pages can use a database login that only has read access, and only the admin pages use
    /// one that can write.
    /// </summary>
    /// <remarks>
    /// The database login needs SELECT, UPDATE and DELETE on Factoid, SELECT and INSERT on FactoidHistory (EF
    /// reads back the new row's id), and SELECT, INSERT and DELETE on EmailKeyword, nothing more.
    /// Unlike the bot's <see cref="IGthxData"/>, these ignore IsLocked: changing locked factoids is what an admin
    /// is for.
    /// </remarks>
    public interface IGthxAdminData
    {
        /// <summary>
        /// Factoids whose item is longer than <paramref name="maxItemLength"/> characters, longest first.
        /// The bot no longer sets these, so they were most likely set by mistake.
        /// </summary>
        public Task<List<Factoid>> GetLongFactoidsAsync(int maxItemLength, int maxResults, CancellationToken cancellationToken = default);

        /// <summary>
        /// Replaces the value of one factoid and records the change in FactoidHistory, as if
        /// <paramref name="user"/> had set it. Returns the changed factoid, or null if there isn't one with that id.
        /// </summary>
        public Task<Factoid> UpdateFactoidValueAsync(int id, string value, string user, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes the factoids with these ids. For each item left with no values, records in FactoidHistory that
        /// <paramref name="user"/> forgot it, the same as the bot's forget. Returns the number deleted; ids that
        /// don't exist are skipped.
        /// </summary>
        public Task<int> DeleteFactoidsAsync(IReadOnlyCollection<int> ids, string user, CancellationToken cancellationToken = default);

        /// <summary>
        /// Locks or unlocks every value of <paramref name="item"/> (case-insensitive, as SQL Server compares it).
        /// The bot won't change or forget a locked item. Returns the number of values changed.
        /// </summary>
        public Task<int> SetItemLockedAsync(string item, bool isLocked, CancellationToken cancellationToken = default);

        /// <summary>
        /// The keywords the bot emails the admin about, in alphabetical order.
        /// </summary>
        public Task<List<EmailKeyword>> GetEmailKeywordsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a keyword, as added by <paramref name="user"/>. Returns the new keyword, or null if it's already
        /// there (ignoring case).
        /// </summary>
        public Task<EmailKeyword> AddEmailKeywordAsync(string keyword, string user, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes the keyword with this id. Returns false if there isn't one.
        /// </summary>
        public Task<bool> DeleteEmailKeywordAsync(int id, CancellationToken cancellationToken = default);
    }
}
