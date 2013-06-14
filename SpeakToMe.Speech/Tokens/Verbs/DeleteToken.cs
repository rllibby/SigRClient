using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;

namespace SpeakToMe.Speech.Tokens.Verbs
{
    [DataContract]
    [Export(typeof(IParseToken))]
    public class TokenDelete : Token, IParseToken
    {
        public TokenDelete()
        {
            this.Words = new List<string> { "delete" };
        }
    }
}