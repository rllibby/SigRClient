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
    class PhoneNoRules : IRuleClass
    {
        public static string PhoneNoEntity = "Customer";
        public static string PhoneNoTarget = "PhoneNumber";

        public static ErpResultWrapper CreateResponse(string context)
        {
            ErpResultWrapper wrapper = new ErpResultWrapper();

            wrapper.Entity = PhoneNoEntity;
            wrapper.Target = PhoneNoTarget;

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

        /* Eg: phone number */
        public static void PhoneNoResponse(ConversationContext cContext, TokenQueryPhoneNo p1)
        {
            // Ask for which customer
            cContext.AskQuestion(CreateResponse(null));
        }

        /* Eg: phone number for */
        public static void ItemResponse(ConversationContext cContext, TokenQueryPhoneNo p1, TokenFor p2)
        {
            // Ask for which customer
            cContext.AskQuestion(CreateResponse(null));
        }

        /* Eg: give me the phone no. */
        public static void ItemResponse(ConversationContext cContext, TokenRequest p1, TokenQueryPhoneNo p2)
        {
            // Ask for which customer
            cContext.AskQuestion(CreateResponse(null));
        }

        /* Eg: give me the phone no for */
        public static void ItemResponse(ConversationContext cContext, TokenRequest p1, TokenQueryPhoneNo p2, TokenFor p3)
        {
            // Ask for which customer
            cContext.AskQuestion(CreateResponse(null));
        }

        /* Eg: give me phone number for customer */
        public static void ItemResponse(ConversationContext cContext, TokenRequest p1, TokenQueryPhoneNo p2, TokenFor p3, TokenCustomer p4)
        {
            // Ask for which customer
            cContext.AskQuestion(CreateResponse(null));
        }

        /* Eg: phone number XYZ */
        public static void ItemResponse(ConversationContext cContext, TokenQueryPhoneNo p1, TokenQuotedPhrase p2)
        {
            cContext.Say(CreateResponse(p2.Value.ToString()), null);
        }

        /* Eg: phone number for XYZ */
        public static void ItemResponse(ConversationContext cContext, TokenQueryPhoneNo p1, TokenFor p2, TokenQuotedPhrase p3)
        {
            cContext.Say(CreateResponse(p3.Value.ToString()), null);
        }

        /* Eg: give me phone number XYZ */
        public static void ItemResponse(ConversationContext cContext, TokenRequest p1, TokenQueryPhoneNo p2, TokenQuotedPhrase p3)
        {
            cContext.Say(CreateResponse(p3.Value.ToString()), null);
        }

        /* Eg: give me phone number for XYZ */
        public static void ItemResponse(ConversationContext cContext, TokenRequest p1, TokenQueryPhoneNo p2, TokenFor p3, TokenQuotedPhrase p4)
        {
            cContext.Say(CreateResponse(p4.Value.ToString()), null);
        }

        /* Eg: phone number for customer XYZ */
        public static void ItemResponse(ConversationContext cContext, TokenQueryPhoneNo p1, TokenFor p2, TokenCustomer p3, TokenQuotedPhrase p4)
        {
            cContext.Say(CreateResponse(p4.Value.ToString()), null);
        }

        /* Eg: give me phone number for customer XYZ */
        public static void ItemResponse(ConversationContext cContext, TokenRequest p1, TokenQueryPhoneNo p2, TokenFor p3, TokenCustomer p4, TokenQuotedPhrase p5)
        {
            cContext.Say(CreateResponse(p5.Value.ToString()), null);
        }
    }
}
