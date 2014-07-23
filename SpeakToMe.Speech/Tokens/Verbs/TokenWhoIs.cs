using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;
using SpeakToMe.Speech.Tokens.Queries;

namespace SpeakToMe.Speech.Tokens.Verbs
{
    [DataContract]
    [Export(typeof(IParseToken))]
    public class TokenWhoIs : Token, IParseToken
    {
        public TokenWhoIs()
        {
            this.Words = new List<string> { "who is", "who are", "who are my", "who's", "who was", "who were" };
        }

        public override IEnumerable<TokenResult> Parse(string input, string UserId)
        {
            var tokenResults = new List<TokenResult>();

            //if "in" is included, we'll defer this to TokenWhoWasIn
            if (input.IndexOf("who is in") > -1 || input.IndexOf("who was in") > -1)
            {
                return tokenResults;
            }

            var results = base.Parse(input, UserId);

            if (results.Any())
            {
                var tokenRequest = results.OrderByDescending(qty => qty.Length).First();

                if (tokenRequest.Start == 0)
                {
                    tokenResults.Add(tokenRequest);

                    var remainder = input.Substring(tokenRequest.Start + tokenRequest.Length + 1).Trim();

                    var topResults = new TokenQueryTop().Parse(remainder, UserId);

                    if (topResults.Any())
                    {
                        //tokenResults.AddRange(topResults);
                        return tokenResults;
                    }

                    var searchTermResult = new TokenResult
                    {
                        Length = input.Length - tokenRequest.Length - 1,
                        Start = tokenRequest.Start + tokenRequest.Length + 1,
                        Token = new TokenQuotedPhrase
                        {
                            Value = input.Substring(tokenRequest.Start + tokenRequest.Length + 1)
                        },
                        TokenType = typeof(TokenQuotedPhrase).ToString(),
                        Value = input.Substring(tokenRequest.Start + tokenRequest.Length + 1)
                    };

                    tokenResults.Add(searchTermResult);
                }
            }

            return tokenResults;
        }
    }
}