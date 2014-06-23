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
    public class TokenQueryQuantity : Token, IParseToken
    {
        public TokenQueryQuantity()
        {
            Words = new List<string> { "sc", "stock check", "on hand", "on hand quantity", "quantity on hand", 
                                       "onhand quantity", "quantity onhand", "quantity available", "available quantity", 
                                       "in stock", "in stock amount", "in stock quantity"};
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
                                results = new TokenItem().Parse(remainder, UserId);

                                if (results.Any())
                                {
                                    var tokenItem = results.OrderBy(token => token.Start).First();

                                    if (tokenItem.Start == 0)
                                    {
                                        tokenItem.Start += startPos;
                                        startPos = tokenItem.Start + tokenItem.Length + 1;

                                        tokenResults.Add(tokenItem);

                                        remainder = (remainder.Length <= tokenItem.Length) ? string.Empty : remainder.Substring(tokenItem.Length + 1);
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
