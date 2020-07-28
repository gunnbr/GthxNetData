using Gthx.Core;
using GthxData;
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
                // TODO: Is there a better way of doing this?
                var existingFactoids = _Db.Factoid.Where(f => f.Item == item);

                foreach (var factoid in existingFactoids)
                {
                    _Db.Factoid.Remove(factoid);
                }
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

            _Db.SaveChanges();

            return true;
        }

        public bool AddTell(string fromUser, string toUser, string message)
        {
            throw new NotImplementedException();
        }

        public ThingiverseRef AddThingiverseReference(string item)
        {
            throw new NotImplementedException();
        }

        public void AddThingiverseTitle(string item, string title)
        {
            throw new NotImplementedException();
        }

        public YoutubeRef AddYoutubeReference(string item)
        {
            throw new NotImplementedException();
        }

        public void AddYoutubeTitle(string item, string title)
        {
            throw new NotImplementedException();
        }

        public bool ForgetFactoid(string user, string item)
        {
            // TODO: Add a trigger on deleting a row to add to the factoid history
            //       OR add to the factoid history here

            // TODO: Is there a better way of doing this?
            var existingFactoids = _Db.Factoid.Where(f => f.Item == item);

            var isLocked = existingFactoids.Any(f => f.IsLocked);
            if (isLocked)
            {
                return false;
            }
            
            foreach (var factoid in existingFactoids)
            {
                _Db.Factoid.Remove(factoid);
            }

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
            return factoids;
        }

        public FactoidInfoReply GetFactoidInfo(string item)
        {
            var history = _Db.FactoidHistory.Where(f => f.Item == item).OrderByDescending(f => f.Timestamp).Take(3).ToList();
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
            throw new NotImplementedException();
        }

        public int GetMood()
        {
            throw new NotImplementedException();
        }

        public List<Tell> GetTell(string forUser)
        {
            return _Db.Tell.Where(t => t.Recipient == forUser).OrderBy(t => t.Timestamp).ToList();
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
