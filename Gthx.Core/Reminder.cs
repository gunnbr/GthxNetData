using System;
using System.ComponentModel.DataAnnotations;

namespace Gthx.Core
{
    public partial class Reminder
    {
        public int Id { get; set; }
        [StringLength(60)]
        public string Author { get; set; }
        /// <summary>
        /// Channel the reminder was asked for in, or the author's nick if it was asked for in a private message
        /// </summary>
        [StringLength(60)]
        public string Channel { get; set; }
        [StringLength(400)]
        public string Message { get; set; }
        /// <summary>
        /// UTC time the reminder was asked for
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// UTC time the reminder is due
        /// </summary>
        public DateTime DueTimestamp { get; set; }
    }
}
