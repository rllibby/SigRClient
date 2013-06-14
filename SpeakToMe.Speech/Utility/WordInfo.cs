using System;
using System.Linq;

namespace SpeakToMe.Speech.Utility
{
    public class WordInfo
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public string Value { get; set; }
        public bool Found { get; set; }
    }
}