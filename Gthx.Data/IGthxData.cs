using Gthx.Core;
using System;
using System.Collections.Generic;

namespace Gthx.Data
{
    public class FactoidInfoReply
    {
        public int RefCount;
        public List<FactoidHistory> InfoList;
    }

    public interface IGthxData
    {
        public List<Seen> GetLastSeen(string user);
        public void UpdateLastSeen(string channel, string user, string message);
        public bool AddFactoid(string user, string factoid, bool isAre, string value, bool replaceExisting);
        public bool IsFactoidLocked(string factoid);
        public bool ForgetFactoid(string user, string factoid);
        public List<Factoid> GetFactoid(string factoid);
        public FactoidInfoReply GetFactoidInfo(string factoid);
        public bool AddTell(string fromUser, string toUser, string message);
        public List<Tell> GetTell(string forUser);
        public ThingiverseRef AddThingiverseReference(string item);
        public void AddThingiverseTitle(string item, string title);
        public YoutubeRef AddYoutubeReference(string item);
        public void AddYoutubeTitle(string item, string title);
        public PrintablesRef AddPrintablesReference(int item);
        public void AddPrintablesTitle(int item, string title);
        public MakerWorldRef AddMakerWorldReference(int item);
        public void AddMakerWorldTitle(int item, string title);
        public Reminder AddReminder(string author, string channel, string message, DateTime due);
        public List<Reminder> GetReminders(string author);
        public bool CancelReminder(string author, int id);
        public List<Reminder> GetDueReminders(DateTime now);
        public List<Reminder> TakeDueReminders(string author, DateTime now);
        public bool DeleteReminder(int id);
        public int GetMood();
    }
}
