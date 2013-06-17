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
    public class TokenDoes : Token, IParseToken
    {
        public TokenDoes()
        {
            this.Words = new List<string> { "how much does", "what does" };
        }
    }

    [DataContract]
    [Export(typeof(IParseToken))]
    public class TokenOwe : Token, IParseToken
    {
        public TokenOwe()
        {
            this.Words = new List<string> { "owe", "owe me", "owe us" };
        }
    }

    [DataContract]
    [Export(typeof(IParseToken))]
    class TokenQueryBalance : Token, IParseToken
    {
        public TokenQueryBalance()
        {
            Words = new List<string> { "cb", "balance"}; 
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
                        results = new TokenFor().Parse(remainder, UserId);
                        if (results.Any())
                        {
                            var tokenFor = results.OrderByDescending(token => token.Start).First();

                            tokenFor.Start += startPos;
                            startPos = tokenFor.Start + tokenFor.Length + 1;

                            tokenResults.Add(tokenFor);

                            remainder = (remainder.Length <= tokenFor.Length) ? string.Empty : remainder.Substring(tokenFor.Length + 1);

                            if (remainder.Length > 0)
                            {
                                results = new TokenCustomer().Parse(remainder, UserId);
                                if (results.Any())
                                {
                                    var tokenCust = results.OrderByDescending(token => token.Start).First();

                                    tokenCust.Start += startPos;
                                    startPos = tokenCust.Start + tokenCust.Length + 1;

                                    tokenResults.Add(tokenCust);

                                    remainder = (remainder.Length <= tokenCust.Length) ? string.Empty : remainder.Substring(tokenCust.Length + 1);
                                }
                            }
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
