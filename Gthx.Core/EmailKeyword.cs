using System;
using System.ComponentModel.DataAnnotations;

namespace Gthx.Core
{
    /// <summary>
    /// A word that makes the bot email the admin when someone says it, set from the website's admin panel.
    /// </summary>
    public partial class EmailKeyword
    {
        public int Id { get; set; }
        /// <summary>
        /// Unique, ignoring case
        /// </summary>
        [Required]
        [StringLength(60)]
        public string Keyword { get; set; }
        /// <summary>
        /// Who added it, written like FactoidHistory.User
        /// </summary>
        [StringLength(30)]
        public string User { get; set; }
        /// <summary>
        /// UTC time it was added
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
