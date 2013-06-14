using SpeakToMe.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeakToMe.Core.Models
{
    // Represents a single conversation associated with a single user
    public class Conversation
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Address { get; set; }
        public bool Active { get; set; }
        public DateTime Initiated { get; set; }
        public ConversationType Mode { get; set; }
        public List<ConversationHistory> ConversationHistory { get; set; }
    }
}
