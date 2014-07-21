using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SpeakToMe.Speech.Tokens.Nouns;
using SpeakToMe.Speech.Tokens.Prepositions;

namespace SpeakToMe.Speech.Tokens.Queries
{
    [DataContract]
    [Export(typeof(IParseToken))]
    public class TokenLook : Token, IParseToken
    {
        public TokenLook()
        {
            this.Words = new List<string> { "look", "look like" };
        }
    }

    [DataContract]
    [Export(typeof(IParseToken))]
    class TokenQueryCashFlow : Token, IParseToken
    {
        public TokenQueryCashFlow()
        {
            Words = new List<string> { "cash flow", "cashflow" };
        }

        public override IEnumerable<TokenResult> Parse(string input, string UserId)
        {
            var tokenResults = new List<TokenResult>();
            string remainder = string.Empty;

            var results = base.Parse(input, UserId);

            if (results.Any())
            {
                tokenResults.Add(results.OrderByDescending(qty => qty.Length).First());

                int startPos = tokenResults.First().Start + tokenResults.First().Length + 1;

                if (startPos < input.Length)
                {
                    remainder = input.Substring(startPos);

                    if (remainder.Length > 0)
                    {
                        results = new TokenLook().Parse(remainder, UserId);
                        if (results.Any())
                        {
                            var tokenLook = results.OrderByDescending(token => token.Start).First();

                            tokenLook.Start += startPos;
                            startPos = tokenLook.Start + tokenLook.Length + 1;

                            tokenResults.Add(tokenLook);

                            remainder = (remainder.Length <= tokenLook.Length) ? string.Empty : remainder.Substring(tokenLook.Length + 1);
                        }

                        if (!string.IsNullOrEmpty(remainder))
                        {
                            tokenResults.Add(new TokenResult
                            {
                                Length = remainder.Length,
                                Start = startPos,
                                Token = new TokenQuotedPhrase { Value = remainder },
                                TokenType = typeof(TokenQuotedPhrase).ToString(),
                                Value = remainder
                            });
                        }
                    }
                }
            }

            return tokenResults;
        }
    }
}
