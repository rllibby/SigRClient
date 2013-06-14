using SpeakToMe.Core;
using SpeakToMe.Speech.Tokens;
using SpeakToMe.Speech.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;

namespace SpeakToMe.Speech
{
    //Manages the process of allowing each token to process the command and also organizes the results
    [Export]
    public class TokenManager
    {
        [ImportMany(typeof(IParseToken))]
        private List<IParseToken> Tokens { get; set; }

        static TokenManager()
        {
            // Tokens = GetTokens();
        }

        public Dictionary<int, List<TokenResult>> TokenizeInput(string input, string userId)
        {
            var results = new List<TokenResult>();

            try
            {
                foreach (var token in Tokens)
                {
                    results.AddRange(token.Parse(input, userId));
                }
                CreateQuotedPhraseTokens(results, input);
            }
            catch (Exception)
            {
                // Eat it
            }

            // Arrange all token results by their start positions
            var buckets = new Dictionary<int, List<TokenResult>>();

            // Make sure the list is clean
            while (results.Remove(null))
            {
            }

            foreach (var result in results.OrderBy(r => r.Start))
            {
                if (!buckets.ContainsKey(result.Start))
                {
                    buckets[result.Start] = new List<TokenResult>();
                }

                buckets[result.Start].Add(result);
            }

            return buckets;
        }

        private void CreateQuotedPhraseTokens(List<TokenResult> results, string input)
        {
            int index = 0;
            List<WordInfo> words = new List<WordInfo>();
            string accumulator = "";

            for (index = 0; index < input.Length - 1; index++)
            {
                if (input[index] == ' ')
                {
                    words.Add(new WordInfo
                    {
                        Found = false,
                        Length = accumulator.Length,
                        Start = index - accumulator.Length,
                        Value = accumulator
                    });

                    accumulator = "";
                    continue;
                }

                accumulator += input[index];
            }

            accumulator += input[index];

            words.Add(new WordInfo
            {
                Found = false,
                Length = accumulator.Length,
                Start = (index + 1) - accumulator.Length,
                Value = accumulator
            });

            accumulator = "";

            foreach (var word in words)
            {
                TokenResult match = null;

                try
                {
                    match = results.Where(r => word.Start >= r.Start && (word.Start + word.Length) <= (r.Start + r.Length)).FirstOrDefault();
                }
                catch (Exception)
                {
                    match = null;
                }

                if (match != null)
                {
                    if (accumulator.Length > 0)
                    {
                        results.Add(new TokenResult
                        {
                            Length = accumulator.Trim().Length,
                            Start = word.Start - 1 - accumulator.Trim().Length,
                            Token = new TokenQuotedPhrase { Value = accumulator.Trim() },
                            TokenType = typeof(TokenQuotedPhrase).ToString(),
                            Value = accumulator.Trim()
                        });
                        accumulator = "";
                    }
                }
                else
                {
                    accumulator += string.Format("{0} ", word.Value);
                }
            }

            if (accumulator.Length > 0)
            {
                results.Add(new TokenResult
                {
                    Length = accumulator.Trim().Length,
                    Start = input.Length - 1 - accumulator.Trim().Length,
                    Token = new TokenQuotedPhrase { Value = accumulator.Trim() },
                    TokenType = typeof(TokenQuotedPhrase).ToString(),
                    Value = accumulator.Trim()
                });
            }
        }
    }
}