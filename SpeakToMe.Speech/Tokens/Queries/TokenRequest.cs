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
    public class TokenRequest : Token, IParseToken
    {
        public TokenRequest()
        {
            Words = new List<string> { "show", "show me", "show me a", "show me the", "perform a", "give a", "give the", "give me", 
                                       "give me a", "give me the", "what is the",  "what's my", "what does my", "what is my",
                                       "how's my", "how is my"};
        }

        public override IEnumerable<TokenResult> Parse(string input, string UserId)
        {
            var tokenResults = new List<TokenResult>();

            var results = base.Parse(input, UserId);

            if (results.Any())
            {
                var tokenRequest = results.OrderByDescending(qty => qty.Length).First();

                if (tokenRequest.Start == 0)
                {
                    tokenResults.Add(tokenRequest);
                }
            }

            return tokenResults;
        }
    }
}
