using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;

namespace SpeakToMe.Speech.Tokens.Nouns
{
    [DataContract]
    [Export(typeof(IParseToken))]
    public class TokenYesNo : Token, IParseToken
    {
        /// <summary>
        /// Initializes a new instance of the TokenYesNo class.
        /// </summary>
        public TokenYesNo()
        {
            this.Words = new List<string> { "yes", "no" };
        }
    }
}