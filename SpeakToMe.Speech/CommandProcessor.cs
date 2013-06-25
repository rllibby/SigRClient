using SpeakToMe.Core;
using SpeakToMe.Core.Enums;
using SpeakToMe.Core.Interfaces;
using SpeakToMe.Speech.Questions;
using SpeakToMe.Speech.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace SpeakToMe.Speech
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CommandProcessor
    {
        [Import]
        private QuestionManager QuestionManager { get; set; }

        public CommandProcessor()
        {
            QuestionManager = new QuestionManager();
        }

        public void ProcessCommand(string command, string userId, ConversationType mode, string  conversationId, IErpServiceCallback callback)
        {
            ConversationContext context = ServiceLocator.GetInstance<ConversationContext>();
            Question question = null;
            List<Token> tokens = new List<Token>();

            command = command.TrimEnd(new char[] {'.', '!', '?'});
            string localCommand = command.ToLower().Trim();

            context.Init(userId, mode, conversationId, callback);
            var tokenManager = ServiceLocator.GetInstance<TokenManager>();
            var buckets = tokenManager.TokenizeInput(localCommand, userId);

            question = this.QuestionManager.CheckForActiveQuestion(mode, userId, conversationId);

            if (question != null)
            {
                question.State.Context = command;
                context.Say(question.State, null);
                return;
            }

            RuleMethod ruleMethod = RuleManager.LocateMatchingRule(buckets, context);

            if (ruleMethod != null)
            {
                ruleMethod.Rule.Invoke(null, ruleMethod.PassIns);
            }
            else
            {
                ErpResultWrapper wrapper = new ErpResultWrapper();

                wrapper.IsQuestion = true;
                wrapper.QuestionText = Constants.UnderstandFailure;

                context.Say(wrapper, null);
            }
        }
    }
}