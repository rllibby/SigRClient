using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;

namespace SpeakToMe.Speech.Tokens.Verbs
{
    [DataContract]
    [Export(typeof(IParseToken))]
    public class TokenTurn : Token, IParseToken
    {
        public TokenTurn()
        {
            this.Words = new List<string> { "turn", "set" }; 
        }
    }
}