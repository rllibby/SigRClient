using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;

namespace SpeakToMe.Speech.Tokens.Nouns
{
    [DataContract]
    [Export(typeof(IParseToken))]
    public class TokenItem : Token, IParseToken
    {
        public TokenItem()
        {
            this.Words = new List<string> { "item", "part" };
        }
    }
}