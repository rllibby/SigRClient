using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SpeakToMe.Speech.Tokens
{
    [DataContract]
    public class Token
    {
        protected List<string> Words;

        [DataMember]
        public object Value { get; set; }

        public virtual IEnumerable<TokenResult> Parse(string input, string userId)
        {
            var results = new List<TokenResult>();
            var quotePoints = GetQuotePoints(input);

            foreach (var word in this.Words)
            {
                string test = input;
                int index, pos;

                index = test.IndexOf(word);

                // While matched
                while (index >= 0)
                {
                    pos = index + word.Length;

                    if (pos <= test.Length) 
                    {
                        bool add = true;

                        if (pos < test.Length)
                        {
                            if (test[pos] != ' ')
                            {
                                add = false;
                            }
                        }
                        if (add)
                        {
                            var result = new TokenResult { Length = word.Length, Start = index, TokenType = this.GetType().ToString(), Value = word, Token = ((Token)Activator.CreateInstance(this.GetType())) };
                            results.Add(result);
                            result.Token.Value = word;
                            index += word.Length;
                        }
                    }

                    // Find next match
                    index = test.IndexOf(" " + word, pos);

                    // If index is >= 0 
                    if (index >= 0)
                    {
                        // Skip the space
                        index++;
                    }
                }
            }

            return results;
        }

        private static bool IsInQuotes(int index, List<int> quotePoints)
        {
            for (int i = 0; i < quotePoints.Count; i++)
            {
                if (i < quotePoints.Count - 1)
                {
                    if (index > quotePoints[i] && index < quotePoints[i + 1] && i % 2 == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static List<int> GetQuotePoints(string input)
        {
            var quotePoints = new List<int>();
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '"')
                {
                    quotePoints.Add(i);
                }
            }

            return quotePoints;
        }
    }
}