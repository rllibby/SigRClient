using SpeakToMe.Core;
using SpeakToMe.Core.Interfaces;
using SpeakToMe.Speech.Tokens;
using SpeakToMe.Speech.Tokens.Prepositions;
using SpeakToMe.Speech.Tokens.Misc;
using SpeakToMe.Speech.Tokens.Verbs;
using SpeakToMe.Speech.Tokens.Nouns;
using SpeakToMe.Speech.Tokens.Queries;
using SpeakToMe.Speech.Tokens.Temporal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeakToMe.Speech.Rules
{
    class EasterEggRule : IRuleClass
    {
        /* Eg: balance */
        public static void EasterEggResponse(ConversationContext cContext, TokenEaserEgg p1)
        {
            ErpResultWrapper wrapper = new ErpResultWrapper();

            wrapper.IsQuestion = true;
            wrapper.QuestionText = "I'm sorry, Dave. I'm afraid I can't do that.";

            cContext.Say(wrapper, null);
        }
    
    }
}
