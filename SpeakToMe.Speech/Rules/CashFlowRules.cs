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
    class CashFlowRules : IRuleClass
    {
        public static string CashFlowEntity = "Company";
        public static string CashFlowTarget = "CashFlow";

        public static ErpResultWrapper CreateResponse(string context)
        {
            ErpResultWrapper wrapper = new ErpResultWrapper();

            wrapper.Entity = CashFlowEntity;
            wrapper.Target = CashFlowTarget;
            wrapper.Context = String.Empty;

            return wrapper;
        }

        /* Eg: cash flow */
        public static void CashFlowResponse(ConversationContext cContext, TokenCashFlow p1)
        {
            cContext.Say(CreateResponse(p1.Value.ToString()), null);
        }

        /* Eg: how is my cash flow */
        public static void CashFlowResponse(ConversationContext cContext, TokenRequest p1, TokenCashFlow p2)
        {
            cContext.Say(CreateResponse(p2.Value.ToString()), null);
        }

        /* Eg: what's my cash flow look like */
        public static void CashFlowResponse(ConversationContext cContext, TokenRequest p1, TokenCashFlow p2, TokenLook p3)
        {
            cContext.Say(CreateResponse(p2.Value.ToString()), null);
        }

        /* Eg: what's my cash flow today */
        public static void CashFlowResponse(ConversationContext cContext, TokenRequest p1, TokenCashFlow p2, TokenQuotedPhrase p3)
        {
            cContext.Say(CreateResponse(p2.Value.ToString()), null);
        }

        /* Eg: what's my cash flow look like today */
        public static void CashFlowResponse(ConversationContext cContext, TokenRequest p1, TokenCashFlow p2, TokenLook p3, TokenQuotedPhrase p4)
        {
            cContext.Say(CreateResponse(p2.Value.ToString()), null);
        }
    }
}
