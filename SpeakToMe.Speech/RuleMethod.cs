using System;
using System.Linq;
using System.Reflection;

namespace SpeakToMe.Speech
{
    //Class that wraps a single rule and the parameters that will be passed in if it is called
    public class RuleMethod
    {
        public MethodInfo Rule { get; set; }
        public object[] PassIns { get; set; }
    }
}