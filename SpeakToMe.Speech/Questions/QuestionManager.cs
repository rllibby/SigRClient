using SpeakToMe.Core.Enums;
using SpeakToMe.Speech.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace SpeakToMe.Speech.Questions
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class QuestionManager
    {
        private List<Question> ActiveQuestions { get; set; }

        /// <summary>
        /// Initializes a new instance of the QuestionManager class.
        /// </summary>
        public QuestionManager()
        {
            this.ActiveQuestions = new List<Question>();
        }

        public Question CheckForActiveQuestion(ConversationType mode, string userId, string conversationId)
        {
            foreach (Question question in ActiveQuestions)
            {
                if ((mode == question.Mode) && string.Equals(userId, question.UserId) && string.Equals(conversationId, question.ConversationId))
                {
                    this.ActiveQuestions.Remove(question);

                    question.State.IsQuestion = false;
                    question.State.QuestionText = string.Empty;

                    return question;
                }
            }

            return null;
        }

        public void AddQuestion(Question question)
        {
            this.ActiveQuestions.Add(question);
        }

        public void RemoveQuestion(Question question)
        {
            this.ActiveQuestions.Remove(question);
        }
    }
}