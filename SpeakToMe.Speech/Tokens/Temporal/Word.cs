using System;
using System.Linq;
using System.Runtime.Serialization;

namespace SpeakToMe.Speech.Tokens.Temporal
{
    [DataContract]
    public class Word
    {
        public string WordValue { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
    }
}