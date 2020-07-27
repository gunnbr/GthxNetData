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
                var existingFactoids = _Db.Factoids.Where(f => f.Item == item);

                foreach (var factoid in existingFactoids)
                {
                    _Db.Factoids.Remove(factoid);
                }
            }

            var newFactoid = new Factoid()
            {
                Nick = user,
                Item = item,
                Are = isAre,
                Value = value,
                Dateset = DateTime.UtcNow,
            };

            _Db.Factoids.Add(newFactoid);

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

        public bool ForgetFactoid(string user, string factoid)
        {
            throw new NotImplementedException();
        }

        public List<Factoid> GetFactoid(string factoid)
        {
            throw new NotImplementedException();
        }

        public FactoidInfoReply GetFactoidInfo(string factoid)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public bool IsFactoidLocked(string factoid)
        {
            throw new NotImplementedException();
        }

        public void UpdateLastSeen(string channel, string user, string message)
        {
            throw new NotImplementedException();
        }
    }
}
