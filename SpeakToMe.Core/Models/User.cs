using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeakToMe.Core.Models
{
    public class User
    {
        private List<Conversation> _ActiveConversations;

        public string Id { get; set; }

        public List<Conversation> ActiveConversations
        {
            get
            {
                if (_ActiveConversations == null)
                {
                    _ActiveConversations = new List<Conversation>();
                }
                return _ActiveConversations;
            }
            set
            {
                _ActiveConversations = value;
            }
        }
    }
}
