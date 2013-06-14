using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;

namespace SpeakToMe.Speech.Tokens.Verbs
{
    [DataContract]
    [Export(typeof(IParseToken))]
    public class TokenNamed : Token, IParseToken
    {
        /// <summary>
        /// Initializes a new instance of the TokenNamed class.
        /// </summary>
        public TokenNamed()
        {
            this.Words = new List<string> { "named", "called" };
        }
    }
}