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
    class BalanceRules : IRuleClass
    {
        public static string BalanceEntity = "Customer";
        public static string BalanceTarget = "Balance";

        public static ErpResultWrapper CreateResponse(string context)
        {
            ErpResultWrapper wrapper = new ErpResultWrapper();

            wrapper.Entity = BalanceEntity;
            wrapper.Target = BalanceTarget;

            if (context != null)
            {
                wrapper.Context = context;
            }
            else
            {
                wrapper.IsQuestion = true;
                wrapper.QuestionText = "For which customer?";
            }

            return wrapper;
        }

        /* Eg: balance */
        public static void BalanceResponse(ConversationContext cContext, TokenQueryBalance p1)
        {
            // Ask for which customer
            cContext.AskQuestion(CreateResponse(null));
        }

        /* Eg: balance for */
        public static void BalanceResponse(ConversationContext cContext, TokenQueryBalance p1, TokenFor p2)
        {
            // Ask for which customer
            cContext.AskQuestion(CreateResponse(null));
        }

        /* Eg: give me the balance. */
        public static void BalanceResponse(ConversationContext cContext, TokenRequest p1, TokenQueryBalance p2)
        {
            // Ask for which customer
            cContext.AskQuestion(CreateResponse(null));
        }

        /* Eg: give me the balance for */
        public static void BalanceResponse(ConversationContext cContext, TokenRequest p1, TokenQueryBalance p2, TokenFor p3)
        {
            // Ask for which customer
            cContext.AskQuestion(CreateResponse(null));
        }

        /* Eg: customer balance */
        public static void BalanceResponse(ConversationContext cContext, TokenCustomer p1, TokenQueryBalance p2)
        {
            // Ask for which customer
            cContext.AskQuestion(CreateResponse(null));
        }

        /* Eg: customer balance for */
        public static void BalanceResponse(ConversationContext cContext, TokenCustomer p1, TokenQueryBalance p2, TokenFor p3)
        {
            // Ask for which customer
            cContext.AskQuestion(CreateResponse(null));
        }

        /* Eg: give me customer balance */
        public static void BalanceResponse(ConversationContext cContext, TokenRequest p1, TokenCustomer p2, TokenQueryBalance p3)
        {
            // Ask for which customer
            cContext.AskQuestion(CreateResponse(null));
        }

        /* Eg: give me customer balance for */
        public static void BalanceResponse(ConversationContext cContext, TokenRequest p1, TokenCustomer p2, TokenQueryBalance p3, TokenFor p4)
        {
            // Ask for which customer
            cContext.AskQuestion(CreateResponse(null));
        }

        /* Eg: give me balance for customer */
        public static void BalanceResponse(ConversationContext cContext, TokenRequest p1, TokenQueryBalance p2, TokenFor p3, TokenCustomer p4)
        {
            // Ask for which customer
            cContext.AskQuestion(CreateResponse(null));
        }

        /* Eg: balance XYZ */
        public static void BalanceResponse(ConversationContext cContext, TokenQueryBalance p1, TokenQuotedPhrase p2)
        {
            cContext.Say(CreateResponse(p2.Value.ToString()), null);
        }

        /* Eg: balance for XYZ */
        public static void BalanceResponse(ConversationContext cContext, TokenQueryBalance p1, TokenFor p2, TokenQuotedPhrase p3)
        {
            cContext.Say(CreateResponse(p3.Value.ToString()), null);
        }

        /* Eg: give me balance XYZ */
        public static void BalanceResponse(ConversationContext cContext, TokenRequest p1, TokenQueryBalance p2, TokenQuotedPhrase p3)
        {
            cContext.Say(CreateResponse(p3.Value.ToString()), null);
        }

        /* Eg: give me balance for XYZ */
        public static void BalanceResponse(ConversationContext cContext, TokenRequest p1, TokenQueryBalance p2, TokenFor p3, TokenQuotedPhrase p4)
        {
            cContext.Say(CreateResponse(p4.Value.ToString()), null);
        }

        /* Eg: balance for customer XYZ */
        public static void BalanceResponse(ConversationContext cContext, TokenQueryBalance p1, TokenFor p2, TokenCustomer p3, TokenQuotedPhrase p4)
        {
            cContext.Say(CreateResponse(p4.Value.ToString()), null);
        }

        /* Eg: give me balance for customer XYZ */
        public static void BalanceResponse(ConversationContext cContext, TokenRequest p1, TokenQueryBalance p2, TokenFor p3, TokenCustomer p4, TokenQuotedPhrase p5)
        {
            cContext.Say(CreateResponse(p5.Value.ToString()), null);
        }

        /* Eg: customer balance for xyz*/
        public static void BalanceResponse(ConversationContext cContext, TokenCustomer p1, TokenQueryBalance p2, TokenFor p3, TokenQuotedPhrase p4)
        {
            cContext.Say(CreateResponse(p4.Value.ToString()), null);
        }

        /* Eg: give me customer balance for xyz*/
        public static void BalanceResponse(ConversationContext cContext, TokenRequest p1, TokenCustomer p2, TokenQueryBalance p3, TokenFor p4, TokenQuotedPhrase p5)
        {
            cContext.Say(CreateResponse(p5.Value.ToString()), null);
        }

        /* Eg: How much does customer xyz owe us */
        public static void BalanceResponse(ConversationContext cContext, TokenDoes p1, TokenCustomer p2, TokenQuotedPhrase p3, TokenOwe p4)
        {
            cContext.Say(CreateResponse(p3.Value.ToString()), null);
        }
    }
}
