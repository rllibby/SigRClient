using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SpeakToMe.Speech.Tokens.Temporal.TemporalParts
{
    [DataContract]
    public class TokenMonth : Token
    {
        public override IEnumerable<TokenResult> Parse(string input, string UserId)
        {
            var results = new List<TokenResult>();
            List<int> quotePoints = GetQuotePoints(input);

            foreach (var word in this.Words)
            {
                int index = input.IndexOf(word);

                if (index > -1 && !IsInQuotes(index, quotePoints))
                {
                    string refString = this.Words[0];
                    int retValue = 0;

                    switch (refString)
                    {
                        case "january":
                            retValue = 1;
                            break;
                        case "february":
                            retValue = 2;
                            break;
                        case "march":
                            retValue = 3;
                            break;
                        case "april":
                            retValue = 4;
                            break;
                        case "may":
                            retValue = 5;
                            break;
                        case "june":
                            retValue = 6;
                            break;
                        case "july":
                            retValue = 7;
                            break;
                        case "august":
                            retValue = 8;
                            break;
                        case "september":
                            retValue = 9;
                            break;
                        case "october":
                            retValue = 10;
                            break;
                        case "november":
                            retValue = 11;
                            break;
                        case "december":
                            retValue = 12;
                            break;
                    }

                    results.Add(new TokenResult
                    {
                        Length = word.Length,
                        Start = index,
                        TokenType = this.GetType().ToString(),
                        Value = retValue,
                        Token = new TokenMonth { Value = retValue }
                    });
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

    [DataContract]
    public class TokenJanuary : TokenMonth
    {
        public TokenJanuary()
        {
            this.Words = new List<string> { "january", " 1/", "jan ", "jan. " };
        }
    }

    [DataContract]
    public class TokenFebruary : TokenMonth
    {
        public TokenFebruary()
        {
            this.Words = new List<string> { "february", " 2/", "feb ", "feb. " };
        }
    }

    [DataContract]
    public class TokenMarch : TokenMonth
    {
        public TokenMarch()
        {
            this.Words = new List<string> { "march", " 3/", "mar ", "mar. " };
        }
    }

    [DataContract]
    public class TokenApril : TokenMonth
    {
        public TokenApril()
        {
            this.Words = new List<string> { "april", " 4/", "apr ", "apr. " };
        }
    }

    [DataContract]
    public class TokenMay : TokenMonth
    {
        public TokenMay()
        {
            this.Words = new List<string> { "may", " 5/" };
        }
    }

    [DataContract]
    public class TokenJune : TokenMonth
    {
        public TokenJune()
        {
            this.Words = new List<string> { "june", " 6/", "jun ", "jun. " };
        }
    }

    [DataContract]
    public class TokenJuly : TokenMonth
    {
        public TokenJuly()
        {
            this.Words = new List<string> { "july", " 7/", "jul ", "jul. " };
        }
    }

    [DataContract]
    public class TokenAugust : TokenMonth
    {
        public TokenAugust()
        {
            this.Words = new List<string> { "august", " 8/", "aug ", "aug. " };
        }
    }

    [DataContract]
    public class TokenSeptember : TokenMonth
    {
        public TokenSeptember()
        {
            this.Words = new List<string> { "september", " 9/", "sep ", "sep. " };
        }
    }

    [DataContract]
    public class TokenOctober : TokenMonth
    {
        public TokenOctober()
        {
            this.Words = new List<string> { "october", " 10/", "oct ", "oct. " };
        }
    }

    [DataContract]
    public class TokenNovember : TokenMonth
    {
        public TokenNovember()
        {
            this.Words = new List<string> { "november", " 11/", "nov ", "nov. " };
        }
    }

    [DataContract]
    public class TokenDecember : TokenMonth
    {
        public TokenDecember()
        {
            this.Words = new List<string> { "december", " 12/", "dec ", "dec. " };
        }
    }
}