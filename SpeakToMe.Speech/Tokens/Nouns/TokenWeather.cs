using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;

namespace SpeakToMe.Speech.Tokens.Nouns
{
    [DataContract]
    [Export(typeof(IParseToken))]
    public class TokenWeather : Token, IParseToken
    {
        public TokenWeather()
        {
            this.Words = new List<string> { "weather", "the weather" };
        }
    }
}