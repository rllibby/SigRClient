using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;

namespace SpeakToMe.Speech.Tokens.Nouns
{
    [DataContract]
    [Export(typeof(IParseToken))]
    public class TokenState : Token, IParseToken
    {
        [DataMember]
        public bool IsEnabled { get; set; }

        public TokenState()
        {
            this.Words = new List<string> { "enable", "disable" };
        }

        public override IEnumerable<TokenResult> Parse(string input, string UserId)
        {
            var results = base.Parse(input, UserId);

            foreach (var result in results)
            {
                if (result.Value.ToString() == "enable")
                {
                    this.IsEnabled = true;
                    result.Value = true;
                    result.Token.Value = true;
                    (result.Token as TokenState).IsEnabled = true;
                }

                if (result.Value.ToString() == "disable")
                {
                    this.IsEnabled = false;
                    result.Value = false;
                    result.Token.Value = false;
                    (result.Token as TokenState).IsEnabled = false;
                }
            }

            return results;
        }
    }
}