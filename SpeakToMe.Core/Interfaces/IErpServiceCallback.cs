using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpeakToMe.Core.Enums;

namespace SpeakToMe.Core.Interfaces
{
    public interface IErpResult
    {
        string UserId { get; set; }
        string ConversationId { get; set; }
        bool IsQuestion { get; set; }           /* Asking question in order to get context */
        string QuestionText { get; set; }       /* Question being asked */
        string Entity { get; set; }             /* Entity type: eg customer, item, etc */
        string Target { get; set; }             /* Target of entity: eg balance, price, quantity, etc */
        string Context { get; set; }            /* Entity context: eg customer name, item description */
        DateFilter TemporalType { get; set; }   /* Temporal filter applied to result */
        DateTime StartDate { get; set; }        /* Used for time based application */
        DateTime EndDate { get; set; }          /* Used for range based time application */
    }

    /* 
     * When asking the system to process input, a callback can be specified. This callback must be 
     * wrapped by a class implementing this interface.
     */
    public interface IErpServiceCallback
    {
        void ReturnResult(IErpResult result);
    }
}
