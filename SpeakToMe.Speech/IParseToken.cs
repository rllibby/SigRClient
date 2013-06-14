using SpeakToMe.Speech.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpeakToMe.Speech
{
    //Marker interface used to identify token classes that will be loaded at startup
    public interface IParseToken
    {
        IEnumerable<TokenResult> Parse(string input, string userId);
    }
}