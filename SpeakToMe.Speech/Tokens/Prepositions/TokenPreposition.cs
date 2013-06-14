using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;

namespace SpeakToMe.Speech.Tokens.Prepositions
{
    [DataContract]
    [Export(typeof(IParseToken))]
    public class TokenFor : Token, IParseToken
    {
        public TokenFor()
        {
            this.Words = new List<string> { "for" };
        }
    }

    [DataContract]
    [Export(typeof(IParseToken))]
    public class TokenTo : Token, IParseToken
    {
        public TokenTo()
        {
            this.Words = new List<string> { "to" };
        }
    }
}