using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeakToMe.Core.Models
{
    // Represents the series of statements that have been sent and received that are associated with a conversation
    public class ConversationHistory
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public DateTime MessageDateTime { get; set; }
        public bool UserInitiated { get; set; }
        public string Tag { get; set; }
        public string TagType { get; set; }
    }
}
