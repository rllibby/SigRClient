using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;

namespace SpeakToMe.Speech.Tokens.Nouns
{
    [DataContract]
    [Export(typeof(IParseToken))]
    public class TokenReminder : Token, IParseToken
    {
        /// <summary>
        /// Initializes a new instance of the TokenReminder class.
        /// </summary>
        public TokenReminder()
        {
            this.Words = new List<string> { "reminders", "reminder" };
        }
    }
}