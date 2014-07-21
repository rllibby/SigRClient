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
    public class TokenOverdue : Token, IParseToken
    {
        public TokenOverdue()
        {
            Words = new List<string> { "overdue" };
        }
    }

    [DataContract]
    [Export(typeof(IParseToken))]
    public class TokenSales : Token, IParseToken
    {
        public TokenSales()
        {
            Words = new List<string> { "sales" };
        }
    }

    [DataContract]
    [Export(typeof(IParseToken))]
    class TokenQueryTop : Token, IParseToken
    {
        public TokenQueryTop()
        {
            Words = new List<string> { "top" };
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
                        results = new TokenOverdue().Parse(remainder, UserId);
                        if (results.Any())
                        {
                            var tokenOverdue = results.OrderByDescending(token => token.Start).First();

                            tokenOverdue.Start += startPos;
                            startPos = tokenOverdue.Start + tokenOverdue.Length + 1;

                            tokenResults.Add(tokenOverdue);

                            remainder = (remainder.Length <= tokenOverdue.Length) ? string.Empty : remainder.Substring(tokenOverdue.Length + 1);

                            if (remainder.Length > 0)
                            {
                                results = new TokenCustomers().Parse(remainder, UserId);
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
                        else
                        {
                            results = new TokenSales().Parse(remainder, UserId);

                            if (results.Any())
                            {
                                var tokenSales = results.OrderByDescending(token => token.Start).First();

                                tokenSales.Start += startPos;
                                startPos = tokenSales.Start + tokenSales.Length + 1;

                                tokenResults.Add(tokenSales);

                                remainder = (remainder.Length <= tokenSales.Length) ? string.Empty : remainder.Substring(tokenSales.Length + 1);

                                if (remainder.Length > 0)
                                {
                                    results = new TokenCustomers().Parse(remainder, UserId);
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
