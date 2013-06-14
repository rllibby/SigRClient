using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;

namespace SpeakToMe.Speech.Tokens.Nouns
{
    [DataContract]
    [Export(typeof(IParseToken))]
    public class TokenToDo : TokenNoun, IParseToken
    {
        public TokenToDo()
        {
            this.Words = new List<string> { "todo", "todo item", "a todo", "a todo item", "a new todo item", "a new todo", "todos" };
        }
    }
}