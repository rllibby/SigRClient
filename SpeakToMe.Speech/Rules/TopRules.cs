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
    class TopRules : IRuleClass
    {
        public static string TopEntity = "Customer";
        public static string TopSalesTarget = "TopSales";
        public static string TopOverdueTarget = "TopOverdue";

        private static ErpResultWrapper CreateResponse(Token context)
        {
            ErpResultWrapper wrapper = new ErpResultWrapper();

            wrapper.Entity = TopEntity;
            wrapper.Target = ((context != null) && (context is TokenOverdue)) ? TopOverdueTarget : TopSalesTarget;
            wrapper.Context = String.Empty;

            return wrapper;
        }

        /* Eg: top overdue customers */
        public static void TopResponse(ConversationContext cContext, TokenQueryTop p1, TokenOverdue p2, TokenCustomers p3)
        {
            cContext.Say(CreateResponse(p2), null);
        }

        /* Eg: top sales customers */
        public static void TopResponse(ConversationContext cContext, TokenQueryTop p1, TokenSales p2, TokenCustomers p3)
        {
            cContext.Say(CreateResponse(p2), null);
        }

        /* Eg: give me the top sales customers */
        public static void TopResponse(ConversationContext cContext, TokenRequest p1, TokenQueryTop p2, TokenSales p3, TokenCustomers p4)
        {
            cContext.Say(CreateResponse(p3), null);
        }

        /* Eg: give me the top overdue customers */
        public static void TopResponse(ConversationContext cContext, TokenRequest p1, TokenQueryTop p2, TokenOverdue p3, TokenCustomers p4)
        {
            cContext.Say(CreateResponse(p3), null);
        }

        /* Eg: give me the top sales customers */
        public static void TopResponse(ConversationContext cContext, TokenWhoIs p1, TokenQueryTop p2, TokenSales p3, TokenCustomers p4)
        {
            cContext.Say(CreateResponse(p3), null);
        }

        /* Eg: give me the top overdue customers */
        public static void TopResponse(ConversationContext cContext, TokenWhoIs p1, TokenQueryTop p2, TokenOverdue p3, TokenCustomers p4)
        {
            cContext.Say(CreateResponse(p3), null);
        }
    }
}
