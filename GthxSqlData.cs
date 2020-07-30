using Gthx.Core;
using GthxData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gthx.Data
{
    public class GthxSqlData : IGthxData
    {
        private readonly GthxDataContext _Db;

        public GthxSqlData(GthxDataContext db)
        {
            this._Db = db;
        }

        public bool AddFactoid(string user, string item, bool isAre, string value, bool replaceExisting)
        {
            if (replaceExisting)
            {
                ForgetFactoid(user, item);
            }

            var newFactoid = new Factoid()
            {
                User = user,
                Item = item,
                IsAre = isAre,
                Value = value,
                Timestamp = DateTime.UtcNow,
            };

            _Db.Factoid.Add(newFactoid);

            var history = new FactoidHistory()
            {
                Item = item,
                Value = value,
                User = user,
                Timestamp = DateTime.UtcNow
            };

            _Db.FactoidHistory.Add(history);

            _Db.SaveChanges();

            return true;
        }

        public bool AddTell(string fromUser, string toUser, string message)
        {
            var newTell = new Tell()
            {
                Author = fromUser,
                Recipient = toUser,
                Message = message,
                Timestamp = DateTime.UtcNow
            };

            _Db.Tell.Add(newTell);
            var numChanges = _Db.SaveChanges();

            return numChanges > 0;
        }

        public ThingiverseRef AddThingiverseReference(string item)
        {
            var success = int.TryParse(item, out var intItem);
            if (!success)
            {
                return null;
            }

            // TODO: Make a stored procedure for this
            var thingRef = _Db.ThingiverseRef.FirstOrDefault(t => t.Item == intItem);
            if (thingRef == null)
            {
                thingRef = new ThingiverseRef()
                {
                    Item = intItem,
                    Count = 1,
                    Timestamp = DateTime.UtcNow
                };
            }
            else
            {
                thingRef.Count++;
            }

            _Db.SaveChanges();

            return thingRef;
        }

        public void AddThingiverseTitle(string item, string title)
        {
            var success = int.TryParse(item, out var intItem);
            if (!success)
            {
                return;
            }

            // TODO: Make a stored procedure for this
            var thingRef = _Db.ThingiverseRef.FirstOrDefault(t => t.Item == intItem);
            if (thingRef == null)
            {
                thingRef = new ThingiverseRef()
                {
                    Item = intItem,
                    Count = 1,
                    Timestamp = DateTime.UtcNow,
                    Title = title
                };
            }
            else
            {
                thingRef.Title = title;
            }

            _Db.SaveChanges();
        }

        public YoutubeRef AddYoutubeReference(string item)
        {
            // TODO: Make a stored procedure for this
            var youRef = _Db.YoutubeRef.FirstOrDefault(t => t.Item == item);
            if (youRef == null)
            {
                youRef = new YoutubeRef()
                {
                    Item = item,
                    Count = 1,
                    Timestamp = DateTime.UtcNow
                };
            }
            else
            {
                youRef.Count++;
            }

            _Db.SaveChanges();

            return youRef;
        }

        public void AddYoutubeTitle(string item, string title)
        {
            // TODO: Make a stored procedure for this
            var youRef = _Db.YoutubeRef.FirstOrDefault(t => t.Item == item);
            if (youRef == null)
            {
                youRef = new YoutubeRef()
                {
                    Item = item,
                    Count = 1,
                    Timestamp = DateTime.UtcNow,
                    Title = title
                };
            }
            else
            {
                youRef.Title = title;
            }

            _Db.SaveChanges();
        }

        public bool ForgetFactoid(string user, string item)
        {
            var existingFactoids = _Db.Factoid.Where(f => f.Item == item);
            if (!existingFactoids.Any())
            {
                return true;
            }

            var isLocked = existingFactoids.Any(f => f.IsLocked);
            if (isLocked)
            {
                return false;
            }

            _Db.Factoid.RemoveRange(existingFactoids);

            var deleteHistory = new FactoidHistory()
            {
                Item = item,
                Value = null,
                User = user,
                Timestamp = DateTime.UtcNow
            };

            _Db.FactoidHistory.Add(deleteHistory);

            _Db.SaveChanges();

            return true;
        }

        public List<Factoid> GetFactoid(string item)
        {
            var factoids = _Db.Factoid.Where(f => f.Item == item).ToList();
            if (factoids.Count == 0)
            {
                return null;
            }

            // TODO: Make a single query or an SP for this!
            var factoidRef = _Db.Ref.FirstOrDefault(r => r.Item == item);
            if (factoidRef == null)
            {
                factoidRef = new Ref()
                {
                    Item = item,
                    Count = 0
                };
                _Db.Ref.Add(factoidRef);
            }

            factoidRef.Count++;
            factoidRef.Timestamp = DateTime.UtcNow;
            _Db.SaveChanges();

            return factoids;
        }

        public FactoidInfoReply GetFactoidInfo(string item)
        {
            // TODO: Find a way to combine these into a single query,
            //       as is done in the original gthx.
            var history = _Db.FactoidHistory.Where(f => f.Item == item).OrderByDescending(f => f.Timestamp).Take(4).ToList();
            if (history.Count == 0)
            {
                return null;
            }

            var refCount = 0;
            var reference = _Db.Ref.Where(f => f.Item == item).FirstOrDefault();
            if (reference != null)
            {
                refCount = reference.Count;
            }

            return new FactoidInfoReply()
            {
                RefCount = refCount,
                InfoList = history
            };
        }

        public List<Seen> GetLastSeen(string user)
        {
            var seen = _Db.Set<Seen>().Where(s => EF.Functions.Like(s.User, $"%{user}%")).Take(3).ToList();
            if (seen.Count() == 0)
            {
                return null;
            }

            return seen;
        }

        public int GetMood()
        {
            // TODO: Make a single query or SP for this:
            /*
                SELECT botsnack - botsmack as mood
                FROM
                (
                    SELECT IFNULL ((SELECT count FROM refs WHERE item="botsnack"), 0) as botsnack, botsmack
                    FROM
                    (
                        SELECT IFNULL ((SELECT count FROM refs WHERE item="botsmack"), 0) as botsmack
                    ) as T2
                ) as T;
            */

            var snackRef = _Db.Ref.FirstOrDefault(r => r.Item == "botsnack");
            var botsnack = snackRef?.Count ?? 0;

            var smackRef = _Db.Ref.FirstOrDefault(r => r.Item == "botsmack");
            var botsmack = smackRef?.Count ?? 0;

            return botsnack - botsmack;
        }

        public List<Tell> GetTell(string forUser)
        {
            var tells = _Db.Tell.Where(t => t.Recipient == forUser).OrderBy(t => t.Timestamp).ToList();
            _Db.Tell.RemoveRange(tells);
            _Db.SaveChanges();
            return tells;
        }

        public bool IsFactoidLocked(string item)
        {
            var existingFactoids = _Db.Factoid.Where(f => f.Item == item);

            return existingFactoids.Any(f => f.IsLocked);
        }

        public void UpdateLastSeen(string channel, string user, string message)
        {
            var seenData = _Db.Seen.FirstOrDefault(s => s.User == user);
            if (seenData == null)
            {
                seenData = new Seen() { User = user };
                _Db.Seen.Add(seenData);
            }
            seenData.Message = message;
            seenData.Channel = channel;
            seenData.Timestamp = DateTime.UtcNow;
            _Db.SaveChanges();
        }
    }
}
