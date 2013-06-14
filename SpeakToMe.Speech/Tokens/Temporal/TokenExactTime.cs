using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;

namespace SpeakToMe.Speech.Tokens.Temporal
{
    [DataContract]
    [Export(typeof(IParseToken))]
    public class TokenExactTime : TokenTemporal, IParseToken
    {
    }
}