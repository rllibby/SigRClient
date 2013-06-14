using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;

namespace SpeakToMe.Speech.Tokens
{
    [DataContract]
    [Export(typeof(IParseToken))]
    public class TokenQuotedPhrase : Token, IParseToken
    {
        public override IEnumerable<TokenResult> Parse(string input, string UserId)
        {
            int bias = 0;
            var results = new List<TokenResult>();
            string test = input;
            while (test.IndexOf('"') > -1)
            {
                int index = test.IndexOf('"');

                int endIndex = test.IndexOf('"', index + 1);

                if (endIndex == (-1))
                {
                    break;
                }

                var result = new TokenResult
                {
                    Length = (endIndex - 1) - (index + 1),
                    Start = index + bias,
                    TokenType = this.GetType().ToString(),
                    Value = test.Substring(index + 1, endIndex - (index + 1)),
                    Token = new TokenQuotedPhrase { Value = test.Substring(index + 1, endIndex - (index + 1)) }
                };
                
                results.Add(result);
                
                int origLength = test.Length;
                test = test.Substring(endIndex + 1);
                bias += (origLength - test.Length);
            }

            return results;
        }
    }
}