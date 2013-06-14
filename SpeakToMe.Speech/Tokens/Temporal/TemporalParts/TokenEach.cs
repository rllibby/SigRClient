using System;
using System.Collections.Generic;
using System.Linq;

namespace SpeakToMe.Speech.Tokens.Temporal.TemporalParts
{
    public class TokenEach : Token
    {
        public TokenEach()
        {
            this.Words = new List<string> { "each", "every" };
        }
    }
}