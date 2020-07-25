using System;
using System.Collections.Generic;

namespace GthxData
{
    public partial class Tell
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Recipient { get; set; }
        public DateTime? Timestamp { get; set; }
        public string Message { get; set; }
        public bool? InTracked { get; set; }
    }
}
