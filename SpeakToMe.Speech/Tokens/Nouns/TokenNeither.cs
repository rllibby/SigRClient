using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;

namespace SpeakToMe.Speech.Tokens.Nouns
{
    [DataContract]
    [Export(typeof(IParseToken))]
    public class TokenNeither : Token, IParseToken
    {
        /// <summary>
        /// Initializes a new instance of the TokenNeither class.
        /// </summary>
        public TokenNeither()
        {
            this.Words = new List<string> { "neither" };
        }
    }
}