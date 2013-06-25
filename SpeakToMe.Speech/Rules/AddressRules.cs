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
    class AddressRules : IRuleClass
    {
        public static string AddressEntity = "Customer";
        public static string AddressTarget = "Address";

        public static ErpResultWrapper CreateResponse(string context)
        {
            ErpResultWrapper wrapper = new ErpResultWrapper();

            wrapper.Entity = AddressEntity;
            wrapper.Target = AddressTarget;

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

        /* Eg: address */
        public static void AddressResponse(ConversationContext cContext, TokenQueryAddress p1)
        {
            // Ask for which customer
            cContext.AskQuestion(CreateResponse(null));
        }

        /* Eg: address for */
        public static void AddressResponse(ConversationContext cContext, TokenQueryAddress p1, TokenFor p2)
        {
            // Ask for which customer
            cContext.AskQuestion(CreateResponse(null));
        }

        /* Eg: give me the address */
        public static void AddressResponse(ConversationContext cContext, TokenRequest p1, TokenQueryAddress p2)
        {
            // Ask for which customer
            cContext.AskQuestion(CreateResponse(null));
        }

        /* Eg: give me the address for */
        public static void AddressResponse(ConversationContext cContext, TokenRequest p1, TokenQueryAddress p2, TokenFor p3)
        {
            // Ask for which customer
            cContext.AskQuestion(CreateResponse(null));
        }

        /* Eg: customer address */
        public static void AddressResponse(ConversationContext cContext, TokenCustomer p1, TokenQueryAddress p2)
        {
            // Ask for which customer
            cContext.AskQuestion(CreateResponse(null));
        }

        /* Eg: customer address for */
        public static void AddressResponse(ConversationContext cContext, TokenCustomer p1, TokenQueryAddress p2, TokenFor p3)
        {
            // Ask for which customer
            cContext.AskQuestion(CreateResponse(null));
        }

        /* Eg: give me customer address */
        public static void AddressResponse(ConversationContext cContext, TokenRequest p1, TokenCustomer p2, TokenQueryAddress p3)
        {
            // Ask for which customer
            cContext.AskQuestion(CreateResponse(null));
        }

        /* Eg: give me customer address for */
        public static void AddressResponse(ConversationContext cContext, TokenRequest p1, TokenCustomer p2, TokenQueryAddress p3, TokenFor p4)
        {
            // Ask for which customer
            cContext.AskQuestion(CreateResponse(null));
        }

        /* Eg: give me address for customer */
        public static void AddressResponse(ConversationContext cContext, TokenRequest p1, TokenQueryAddress p2, TokenFor p3, TokenCustomer p4)
        {
            // Ask for which customer
            cContext.AskQuestion(CreateResponse(null));
        }

        /* Eg: address XYZ */
        public static void AddressResponse(ConversationContext cContext, TokenQueryAddress p1, TokenQuotedPhrase p2)
        {
            cContext.Say(CreateResponse(p2.Value.ToString()), null);
        }

        /* Eg: address for XYZ */
        public static void AddressResponse(ConversationContext cContext, TokenQueryAddress p1, TokenFor p2, TokenQuotedPhrase p3)
        {
            cContext.Say(CreateResponse(p3.Value.ToString()), null);
        }

        /* Eg: give me address XYZ */
        public static void AddressResponse(ConversationContext cContext, TokenRequest p1, TokenQueryAddress p2, TokenQuotedPhrase p3)
        {
            cContext.Say(CreateResponse(p3.Value.ToString()), null);
        }

        /* Eg: give me address for XYZ */
        public static void AddressResponse(ConversationContext cContext, TokenRequest p1, TokenQueryAddress p2, TokenFor p3, TokenQuotedPhrase p4)
        {
            cContext.Say(CreateResponse(p4.Value.ToString()), null);
        }

        /* Eg: address for customer XYZ */
        public static void AddressResponse(ConversationContext cContext, TokenQueryAddress p1, TokenFor p2, TokenCustomer p3, TokenQuotedPhrase p4)
        {
            cContext.Say(CreateResponse(p4.Value.ToString()), null);
        }

        /* Eg: give me address for customer XYZ */
        public static void AddressResponse(ConversationContext cContext, TokenRequest p1, TokenQueryAddress p2, TokenFor p3, TokenCustomer p4, TokenQuotedPhrase p5)
        {
            cContext.Say(CreateResponse(p5.Value.ToString()), null);
        }

        /* Eg: customer address for xyz*/
        public static void AddressResponse(ConversationContext cContext, TokenCustomer p1, TokenQueryAddress p2, TokenFor p3, TokenQuotedPhrase p4)
        {
            cContext.Say(CreateResponse(p4.Value.ToString()), null);
        }

        /* Eg: give me customeraddress for xyz*/
        public static void AddressResponse(ConversationContext cContext, TokenRequest p1, TokenCustomer p2, TokenQueryAddress p3, TokenFor p4, TokenQuotedPhrase p5)
        {
            cContext.Say(CreateResponse(p5.Value.ToString()), null);
        }
    }
}
