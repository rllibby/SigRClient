using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using SpeakToMe.Core.Enums;
using SpeakToMe.Core.Interfaces;
using SpeakToMe.Speech.Tokens;

namespace SpeakToMe.Speech.Questions
{
    [DataContract]
    public class Question
    {
        [DataMember]
        public ConversationType Mode { get; set; }
        
        [DataMember]
        public string ConversationId { get; set; }
        
        [DataMember]
        public string UserId { get; set; }
        
        [DataMember]
        public DateTime PosedDateTime { get; set; }

        [DataMember]
        public string QuestionText { get; set; }
        
        [DataMember]
        public IErpResult State { get; set; }
    }
}