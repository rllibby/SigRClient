using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Objects.DataClasses;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Practices.Prism.Events;
using SpeakToMe.Core.Models;
using SpeakToMe.Core.Enums;
using SpeakToMe.Core.Interfaces;
using SpeakToMe.Speech.Questions;
using SpeakToMe.Speech.Tokens;
using SpeakToMe.Core.Events;
using SpeakToMe.Core;

namespace SpeakToMe.Speech
{
    // Represent the context in which a command was receieved. Allows access to previous conversation messages and optional state associated with each
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ConversationContext
    {
        public User ConversationUser { get; set; }
        public string ConversationId { get; set; }
        public ConversationType Mode { get; set; }
        public IErpServiceCallback Callback { get; set; }

        [Import]
        private IEventAggregator EventAggregator { get; set; }

        [Import]
        public QuestionManager QuestionManagerReference { get; set; }

        public void Init(string userId, ConversationType mode, string conversationId, IErpServiceCallback callback)
        {
            this.ConversationUser = new User()
            {
                Id = userId
            };

            this.ConversationId = conversationId;
            this.Mode = mode;
            this.Callback = callback;
        }

        public void Say(IErpResult result, object tag)
        {
            string tagString = string.Empty;
            tagString = this.SerializeTag(tag);

            this.EventAggregator.GetEvent<ReplyToChannelEvent>().Publish(new ReplyToChannelEventArgs
            {
                Mode = (ConversationType)this.Mode,
                Reply = result,
                TagString = tagString,
                Tag = tag,
                ConversationId = this.ConversationId,
                Callback = this.Callback,
                UserId = this.ConversationUser.Id,
            });
        }

        private string SerializeTag(object tag)
        {
            string tagString = string.Empty;

            if (tag != null)
            {
                if (tag is IEntityWithChangeTracker)
                {
                    (tag as IEntityWithChangeTracker).SetChangeTracker(null);
                }
                var ser = new DataContractSerializer(tag.GetType());
                var ms = new MemoryStream();
                ser.WriteObject(ms, tag);
                tagString = Encoding.Default.GetString(ms.ToArray());
            }

            return tagString;
        }

        public void AskQuestion(IErpResult state)
        {
            Question question = new Question
            {
                Mode = this.Mode,
                PosedDateTime = DateTime.Now,
                UserId = this.ConversationUser.Id,
                ConversationId = this.ConversationId,
                State = state,
                QuestionText = state.QuestionText
            };

            this.QuestionManagerReference.AddQuestion(question);

            this.Say(state, null);
        }
    }
}