using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;

namespace SpeakToMe.Speech.Tokens.Verbs
{
    [DataContract]
    [Export(typeof(IParseToken))]
    public class TokenWhoWasIn : Token, IParseToken
    {
        public TokenWhoWasIn()
        {
            this.Words = new List<string> { "who is in", "who was in" };
        }

        public override IEnumerable<TokenResult> Parse(string input, string UserId)
        {
            var results = new List<TokenResult>();
            results.AddRange(base.Parse(input, UserId));
            TokenResult foo = null;
            foreach (var result in results)
            {
                if (result.TokenType.ToLower().IndexOf("preposition") > -1)
                {
                    foo = result; 
                }
            }

            if (foo != null)
            {
                results.Remove(foo);
            }

            if (results.Count > 0 && input.Length > results[0].Length)
            {
                var searchTermResult = new TokenResult
                {
                    Length = input.Length - results[0].Length - 1,
                    Start = results[0].Start + results[0].Length + 1,
                    Token =
                        new TokenQuotedPhrase
                        {
                            Value =
                                input.Substring(results[0].Start + results[0].Length + 1)
                        },
                    TokenType = typeof(TokenQuotedPhrase).ToString(),
                    Value = input.Substring(results[0].Start + results[0].Length + 1)
                };
                results.Add(searchTermResult);
            }

            return results;
        }
    }
}