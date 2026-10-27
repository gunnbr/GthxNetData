using Gthx.Core;
using GthxData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gthx.Data
{
    public class GthxWebData : IGthxWebData
    {
        private const string LikeEscape = "\\";

        private readonly GthxDataContext _Db;

        public GthxWebData(GthxDataContext db)
        {
            this._Db = db;
        }

        public async Task<List<Factoid>> SearchFactoidsAsync(string search, int maxResults, CancellationToken cancellationToken = default)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxResults);

            search = search?.Trim();
            if (string.IsNullOrEmpty(search))
            {
                return new List<Factoid>();
            }

            var pattern = $"%{EscapeLike(search)}%";

            return await _Db.Factoid
                .AsNoTracking()
                .Where(f => EF.Functions.Like(f.Item, pattern, LikeEscape) || EF.Functions.Like(f.Value, pattern, LikeEscape))
                .OrderBy(f => f.Item == search ? 0 : 1)
                .ThenBy(f => f.Item)
                .ThenBy(f => f.Id)
                .Take(maxResults)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<FactoidRefCount>> GetTopFactoidsAsync(int count, CancellationToken cancellationToken = default)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

            // Ref also counts lookups of things that were later forgotten, so only keep refs that still have a factoid.
            var topRefs = await _Db.Ref
                .AsNoTracking()
                .Where(r => _Db.Factoid.Any(f => f.Item == r.Item))
                .OrderByDescending(r => r.Count)
                .ThenBy(r => r.Item)
                .Take(count)
                .ToListAsync(cancellationToken);

            var items = topRefs.Select(r => r.Item).ToList();
            var factoids = await _Db.Factoid
                .AsNoTracking()
                .Where(f => items.Contains(f.Item))
                .OrderBy(f => f.Id)
                .ToListAsync(cancellationToken);

            // SQL Server compares items case-insensitively, so group the same way here.
            var byItem = factoids.ToLookup(f => f.Item, StringComparer.OrdinalIgnoreCase);

            return topRefs
                .Select(r => new FactoidRefCount(r.Item, r.Count, byItem[r.Item].ToList()))
                .ToList();
        }

        public Task<int> GetFactoidCountAsync(CancellationToken cancellationToken = default)
        {
            return _Db.Factoid
                .AsNoTracking()
                .Select(f => f.Item)
                .Distinct()
                .CountAsync(cancellationToken);
        }

        private static string EscapeLike(string text)
        {
            return text
                .Replace(LikeEscape, LikeEscape + LikeEscape)
                .Replace("%", LikeEscape + "%")
                .Replace("_", LikeEscape + "_")
                .Replace("[", LikeEscape + "[");
        }
    }
}
