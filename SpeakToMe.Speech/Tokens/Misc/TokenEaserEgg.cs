using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SpeakToMe.Speech.Tokens.Nouns;
using SpeakToMe.Speech.Tokens.Prepositions;

namespace SpeakToMe.Speech.Tokens.Misc
{
    [DataContract]
    [Export(typeof(IParseToken))]
    public class TokenEaserEgg : Token, IParseToken
    {
        public TokenEaserEgg()
        {
            Words = new List<string> { "open the pod bay doors, hal", "open the pod bay door, hal", 
                                        "open the pod bay doors hal", "open the pod bay door hal", 
                                        "open the pod bay door", "open the pod bay doors"
            };
        }
    }
}
