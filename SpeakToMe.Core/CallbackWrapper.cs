using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpeakToMe.Core.Enums;
using SpeakToMe.Core.Interfaces;

namespace SpeakToMe.Core
{
    public class ErpResultWrapper : IErpResult
    {
        public string UserId { get; set; }
        public string ConversationId { get; set; }
        public bool IsQuestion { get; set; }
        public string QuestionText { get; set; }
        public string Entity { get; set; }
        public string Target { get; set; }
        public string Context { get; set; }
        public DateFilter TemporalType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ErpResultWrapper()
        {
            this.UserId = string.Empty;
            this.ConversationId = string.Empty;
            this.IsQuestion = false;
            this.QuestionText = string.Empty;
            this.Entity = string.Empty;
            this.Target = string.Empty;
            this.Context = string.Empty;
        }
    }

    public class CallbackWrapper : IErpServiceCallback
    {
        public CallbackWrapper(Action<IErpResult> call)
        {
            ProcessAnswer = call;
        }
        public void ReturnResult(IErpResult result)
        {
            ProcessAnswer.Invoke(result);
        }

        public Action<IErpResult> ProcessAnswer
        {
            get;
            set;
        }
    }
}
