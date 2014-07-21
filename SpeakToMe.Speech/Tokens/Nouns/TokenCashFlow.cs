using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;

namespace SpeakToMe.Speech.Tokens.Nouns
{
    [DataContract]
    [Export(typeof(IParseToken))]
    class TokenCashFlow : Token, IParseToken
    {
        public TokenCashFlow()
        {
            Words = new List<string> { "cash flow", "cashflow" };
        }
    }
}
