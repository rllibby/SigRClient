using System;
using System.Collections.Generic;
using System.Linq;

namespace SpeakToMe.Speech.Tokens.Temporal.TemporalParts
{
    public class TokenOther : Token
    {
        public TokenOther()
        {
            this.Words = new List<string> { "other" };
        }
    }
}