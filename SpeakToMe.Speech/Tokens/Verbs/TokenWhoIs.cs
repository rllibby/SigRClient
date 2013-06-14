using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;

namespace SpeakToMe.Speech.Tokens.Verbs
{
    [DataContract]
    [Export(typeof(IParseToken))]
    public class TokenWhoIs : Token, IParseToken
    {
        public TokenWhoIs()
        {
            this.Words = new List<string> { "who is", "who are", "who's", "who was", "who were" };
        }

        public override IEnumerable<TokenResult> Parse(string input, string UserId)
        {
            var results = new List<TokenResult>();

            //if "in" is included, we'll defer this to TokenWhoWasIn
            if (input.IndexOf("who is in") > -1 || input.IndexOf("who was in") > -1)
            {
                return results;
            }

            results.AddRange(base.Parse(input, UserId));

            if (results.Count > 0)
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