using Microsoft.Practices.Prism.Events;
using SpeakToMe.Core.Enums;
using SpeakToMe.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeakToMe.Core.Events
{
    // This event is raised when there is a need to send a reply to a message.  Who actually performs the send is dependent on the message type
    public class ReplyToChannelEvent : CompositePresentationEvent<ReplyToChannelEventArgs>
    {
    }

    public class ReplyToChannelEventArgs
    {
        public string UserId { get; set; }
        public string ConversationId { get; set; }
        public ConversationType Mode { get; set; }
        public IErpResult Reply { get; set; }
        public string TagString { get; set; }
        public object Tag { get; set; }
        public IErpServiceCallback Callback { get; set; }
    }
}
