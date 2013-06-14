using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace SpeakToMe.Speech.Tokens.Temporal.TemporalParts
{
    [DataContract]
    public class TokenSpecifiedDate : Token
    {
        public override IEnumerable<TokenResult> Parse(string input, string UserId)
        {
            int index = -1;
            List<int> quotePoints = this.GetQuotePoints(input);

            while (input.IndexOf("/", index + 1) > -1)
            {
                if (input.IndexOf("/", index + 1) > -1)
                {
                    int localIndex = input.IndexOf("/", index + 1);
                    int start = FindWordStart(input, localIndex);
                    int length = GetLength(input, start);

                    string word = input.Substring(start, length);

                    var parts = word.Split('/');

                    if (!this.IsInQuotes(index + 1, quotePoints))
                    {
                        if (parts.Count() == 3)
                        {
                            int month;
                            int day;
                            int year;

                            if (int.TryParse(parts[0], out month) &&
                                int.TryParse(parts[1], out day) &&
                                int.TryParse(parts[2], out year))
                            {
                                var dt = new DateTime(year, month, day);

                                return new List<TokenResult>
                                {
                                    new TokenResult
                                    {
                                        Length = length,
                                        Start = start - 1,
                                        TokenType = this.GetType().ToString(),
                                        Value = dt,
                                        Token = new TokenSpecifiedDate { Value = dt }
                                    }
                                };
                            }
                        }
                    }

                    index += length;
                }
            }

            return new List<TokenResult>();
        }

        protected bool IsInQuotes(int index, List<int> quotePoints)
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

        protected List<int> GetQuotePoints(string input)
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

        private static int GetLength(string input, int start)
        {
            for (int i = start; i < input.Length; i++)
            {
                if (input[i] == ' ')
                {
                    return (i - 1) - start;
                }
            }

            return input.Length - start;
        }

        private static int FindWordStart(string input, int localIndex)
        {
            for (int i = localIndex; i > 0; i--)
            {
                if (input[i] == ' ')
                {
                    return i + 1;
                }
            }

            return 0;
        }
    }
}