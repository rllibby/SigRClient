using SpeakToMe.Core;
using SpeakToMe.Core.Interfaces;
using SpeakToMe.Speech.Tokens;
using SpeakToMe.Speech.Tokens.Prepositions;
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
    public class ItemRules : IRuleClass
    {
        public static string ItemEntity = "Item";
        public static string ItemTarget = "Quantity";

        public static ErpResultWrapper CreateResponse(string context)
        {
            ErpResultWrapper wrapper = new ErpResultWrapper();

            wrapper.Entity = ItemEntity;
            wrapper.Target = ItemTarget;

            if (context != null)
            {
                wrapper.Context = context;
            }
            else
            {
                wrapper.IsQuestion = true;
                wrapper.QuestionText = "For which item?";
            }

            return wrapper;
        }

        /* Eg: stock check */
        public static void ItemResponse(ConversationContext cContext, TokenQueryQuantity p1)
        {
            // Ask for which item
            cContext.AskQuestion(CreateResponse(null));
        }

        /* Eg: stock check for */
        public static void ItemResponse(ConversationContext cContext, TokenQueryQuantity p1, TokenFor p2)
        {
            // Ask for which item
            cContext.AskQuestion(CreateResponse(null));
        }

        /* Eg: give me a stock check */
        public static void ItemResponse(ConversationContext cContext, TokenRequest p1, TokenQueryQuantity p2)
        {
            // Ask for which item
            cContext.AskQuestion(CreateResponse(null));
        }

        /* Eg: give me a stock check for */
        public static void ItemResponse(ConversationContext cContext, TokenRequest p1, TokenQueryQuantity p2, TokenFor p3)
        {
            // Ask for which item
            cContext.AskQuestion(CreateResponse(null));
        }

        /* Eg: stock check XYZ */
        public static void ItemResponse(ConversationContext cContext, TokenQueryQuantity p1, TokenQuotedPhrase p2)
        {
            cContext.Say(CreateResponse(p2.Value.ToString()), null);
        }

        /* Eg: stock check for XYZ */
        public static void ItemResponse(ConversationContext cContext, TokenQueryQuantity p1, TokenFor p2, TokenQuotedPhrase p3)
        {
            cContext.Say(CreateResponse(p3.Value.ToString()), null);
        }

        /* Eg: give me a stock check XYZ */
        public static void ItemResponse(ConversationContext cContext, TokenRequest p1, TokenQueryQuantity p2, TokenQuotedPhrase p3)
        {
            cContext.Say(CreateResponse(p3.Value.ToString()), null);
        }

        /* Eg: give me a stock check for XYZ */
        public static void ItemResponse(ConversationContext cContext, TokenRequest p1, TokenQueryQuantity p2, TokenFor p3, TokenQuotedPhrase p4)
        {
            cContext.Say(CreateResponse(p4.Value.ToString()), null);
        }

    }
}
