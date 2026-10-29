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
    public class GthxAdminData : IGthxAdminData
    {
        private readonly GthxDataContext _Db;

        public GthxAdminData(GthxDataContext db)
        {
            this._Db = db;
        }

        public async Task<List<Factoid>> GetLongFactoidsAsync(int maxItemLength, int maxResults, CancellationToken cancellationToken = default)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(maxItemLength);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxResults);

            return await _Db.Factoid
                .AsNoTracking()
                .Where(f => f.Item.Length > maxItemLength)
                .OrderByDescending(f => f.Item.Length)
                .ThenBy(f => f.Item)
                .ThenBy(f => f.Id)
                .Take(maxResults)
                .ToListAsync(cancellationToken);
        }

        public async Task<Factoid> UpdateFactoidValueAsync(int id, string value, string user, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);

            var factoid = await _Db.Factoid.SingleOrDefaultAsync(f => f.Id == id, cancellationToken);
            if (factoid == null)
            {
                return null;
            }

            var now = DateTime.UtcNow;
            factoid.Value = value;
            factoid.User = user;
            factoid.Timestamp = now;

            _Db.FactoidHistory.Add(new FactoidHistory()
            {
                Item = factoid.Item,
                Value = value,
                User = user,
                Timestamp = now
            });

            await _Db.SaveChangesAsync(cancellationToken);

            return factoid;
        }

        public async Task<int> DeleteFactoidsAsync(IReadOnlyCollection<int> ids, string user, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(ids);
            if (ids.Count == 0)
            {
                return 0;
            }

            var factoids = await _Db.Factoid
                .Where(f => ids.Contains(f.Id))
                .ToListAsync(cancellationToken);
            if (factoids.Count == 0)
            {
                return 0;
            }

            // SQL Server compares items case-insensitively, so treat "RepRap" and "reprap" as the same item here too.
            var items = factoids.Select(f => f.Item).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
            var remaining = await _Db.Factoid
                .AsNoTracking()
                .Where(f => items.Contains(f.Item) && !ids.Contains(f.Id))
                .Select(f => f.Item)
                .Distinct()
                .ToListAsync(cancellationToken);
            var stillSet = new HashSet<string>(remaining, StringComparer.OrdinalIgnoreCase);

            _Db.Factoid.RemoveRange(factoids);

            var now = DateTime.UtcNow;
            foreach (var item in items.Where(i => !stillSet.Contains(i)))
            {
                _Db.FactoidHistory.Add(new FactoidHistory()
                {
                    Item = item,
                    Value = null,
                    User = user,
                    Timestamp = now
                });
            }

            await _Db.SaveChangesAsync(cancellationToken);

            return factoids.Count;
        }

        public Task<int> SetItemLockedAsync(string item, bool isLocked, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(item);

            return _Db.Factoid
                .Where(f => f.Item == item && f.IsLocked != isLocked)
                .ExecuteUpdateAsync(s => s.SetProperty(f => f.IsLocked, isLocked), cancellationToken);
        }

        public Task<List<EmailKeyword>> GetEmailKeywordsAsync(CancellationToken cancellationToken = default)
        {
            return _Db.EmailKeyword
                .AsNoTracking()
                .OrderBy(k => k.Keyword)
                .ToListAsync(cancellationToken);
        }

        public async Task<EmailKeyword> AddEmailKeywordAsync(string keyword, string user, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(keyword);
            keyword = keyword.Trim();

            // SQL Server compares case-insensitively, so this also finds "RepRap" when adding "reprap". The unique
            // index catches two admins adding the same keyword at once.
            if (await _Db.EmailKeyword.AnyAsync(k => k.Keyword == keyword, cancellationToken))
            {
                return null;
            }

            var emailKeyword = new EmailKeyword()
            {
                Keyword = keyword,
                User = user,
                Timestamp = DateTime.UtcNow
            };
            _Db.EmailKeyword.Add(emailKeyword);
            await _Db.SaveChangesAsync(cancellationToken);

            return emailKeyword;
        }

        public async Task<bool> DeleteEmailKeywordAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _Db.EmailKeyword
                .Where(k => k.Id == id)
                .ExecuteDeleteAsync(cancellationToken) > 0;
        }
    }
}
