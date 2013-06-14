using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeakToMe.Core.Interfaces
{
    // Classes that implement this interface represent a form of presence / mode of communication with the system.  
    public interface IPresence : IPartImportsSatisfiedNotification
    {
        bool IsConnected { get; }
        void Initialize();
        void ProcessCommand(string command, string userId, string conversationId, IErpServiceCallback callback);
    }
}
