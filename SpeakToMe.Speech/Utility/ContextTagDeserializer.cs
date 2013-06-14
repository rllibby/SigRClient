using SpeakToMe.Core.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace SpeakToMe.Speech.Utility
{
    public class ContextTagDeserializer
    {
        public static object Deserialize(ConversationHistory historyItem)
        {
            if (historyItem.TagType == null)
            {
                return null;
            }

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(historyItem.Tag));

            var ser = new DataContractSerializer(Type.GetType(historyItem.TagType, GetAssembly, GetType, true));
            object returnObj = ser.ReadObject(ms);
            ms.Close();

            return returnObj;
        }

        private static Assembly GetAssembly(AssemblyName assemblyName)
        {
            Assembly ass = Assembly.Load(assemblyName);
            Assembly.Load(new AssemblyName(("SmartHome.SpeechProcessor")));

            return ass;
        }

        private static Type GetType(Assembly ass, string name, bool doit)
        {
            Type theType = Type.GetType(name);

            if (theType == null)
            {
                if (name == "SmartHome.SpeechProcessor.Tokens.TokenResult")
                {
                    return Type.GetType(string.Format("{0},SmartHome.SpeechProcessor", name));
                }
                theType = Type.GetType(string.Format("{0},SmartHome.Data", name));
            }

            return theType;
        }
    }
}