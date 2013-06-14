using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SpeakToMe.Speech.Tokens.Temporal.TemporalParts
{
    [DataContract]
    public class TokenOrdinal : Token
    {
        public override IEnumerable<TokenResult> Parse(string input, string UserId)
        {
            var results = new List<TokenResult>();
            var quotePoints = GetQuotePoints(input);

            foreach (string word in this.Words)
            {
                if (input.IndexOf(word) > -1)
                {
                    int index = input.IndexOf(word);

                    if (index > 0 && !IsInQuotes(index, quotePoints))
                    {
                        if ((index + word.Length == input.Length && input[index - 1] == ' ') ||
                            (input[index - 1] == ' ' && input[index + word.Length] == ' '))
                        {
                            results.Add(
                                new TokenResult
                                {
                                    Start = index,
                                    Length = word.Length,
                                    TokenType = this.GetType().ToString(),
                                    Value = int.Parse(this.Words[0].Trim()),
                                    Token = new TokenOrdinal { Value = int.Parse(this.Words[0].Trim()) }
                                });
                        }
                    }
                    else
                    {
                        if (index > -1 && input[index + word.Length] == ' ' && !IsInQuotes(index, quotePoints))
                        {
                            results.Add(
                                new TokenResult
                                {
                                    Start = index,
                                    Length = word.Length,
                                    TokenType = this.GetType().ToString(),
                                    Value = int.Parse(this.Words[0].Trim()),
                                    Token = new TokenOrdinal { Value = int.Parse(this.Words[0].Trim()) }
                                });
                        }
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

    [DataContract]
    public class TokenFirst : TokenOrdinal
    {
        public TokenFirst()
        {
            this.Words = new List<string> { "1", "first", "1st", "the first", "the 1st" };
        }
    }

    [DataContract]
    public class TokenSecond : TokenOrdinal
    {
        public TokenSecond()
        {
            this.Words = new List<string> { "2", "second", "2nd", "the second", "the 2nd" };
        }
    }

    [DataContract]
    public class TokenThird : TokenOrdinal
    {
        public TokenThird()
        {
            this.Words = new List<string> { "3", "third", "3rd", "the third", "the 3rd" };
        }
    }

    [DataContract]
    public class TokenForth : TokenOrdinal
    {
        public TokenForth()
        {
            this.Words = new List<string> { "4", "forth", "4th", "the fourth", "the 4th" };
        }
    }

    [DataContract]
    public class TokenFifth : TokenOrdinal
    {
        public TokenFifth()
        {
            this.Words = new List<string> { "5", "fifth", "5th", "the fifth", "the 5th" };
        }
    }

    [DataContract]
    public class TokenSixth : TokenOrdinal
    {
        public TokenSixth()
        {
            this.Words = new List<string> { "6", "sixth", "6th", "the sixth", "the 6th" };
        }
    }

    [DataContract]
    public class TokenSeventh : TokenOrdinal
    {
        public TokenSeventh()
        {
            this.Words = new List<string> { "7", "seventh", "7th", "the seventh", "the 7th" };
        }
    }

    [DataContract]
    public class TokenEighth : TokenOrdinal
    {
        public TokenEighth()
        {
            this.Words = new List<string> { "8", "eighth", "8th", "the eighth", "the 8th" };
        }
    }

    [DataContract]
    public class TokenNinth : TokenOrdinal
    {
        public TokenNinth()
        {
            this.Words = new List<string> { "9", "ninth", "9th", "the ninth", "the 9th" };
        }
    }

    [DataContract]
    public class TokenTenth : TokenOrdinal
    {
        public TokenTenth()
        {
            this.Words = new List<string> { "10", "tenth", "10th", "the tenth", "the 10th" };
        }
    }

    [DataContract]
    public class TokenEleventh : TokenOrdinal
    {
        public TokenEleventh()
        {
            this.Words = new List<string> { "11", "eleventh", "11th", "the eleventh", "the 11th" };
        }
    }

    [DataContract]
    public class TokenTwelth : TokenOrdinal
    {
        public TokenTwelth()
        {
            this.Words = new List<string> { "12", "twelth", "12th", "the twelth", "the 12th" };
        }
    }

    [DataContract]
    public class TokenThirteenth : TokenOrdinal
    {
        public TokenThirteenth()
        {
            this.Words = new List<string> { "13", "thirteenth", "13th", "the thirteenth", "the 13th" };
        }
    }

    [DataContract]
    public class TokenForteenth : TokenOrdinal
    {
        public TokenForteenth()
        {
            this.Words = new List<string> { "14", "forteenth", "14th", "the forteenth", "the 14th" };
        }
    }

    [DataContract]
    public class TokenFifteenth : TokenOrdinal
    {
        public TokenFifteenth()
        {
            this.Words = new List<string> { "15", "fifteenth", "15th", "the fifteenth", "the 15th" };
        }
    }

    [DataContract]
    public class TokenSixteenth : TokenOrdinal
    {
        public TokenSixteenth()
        {
            this.Words = new List<string> { "16", "sixteenth", "16th", "the sixteenth", "the 16th" };
        }
    }

    [DataContract]
    public class TokenSeventeenth : TokenOrdinal
    {
        public TokenSeventeenth()
        {
            this.Words = new List<string> { "17", "seventeenth", "17th", "the seventeenth", "the 17th" };
        }
    }

    [DataContract]
    public class TokenEighteenth : TokenOrdinal
    {
        public TokenEighteenth()
        {
            this.Words = new List<string> { "18", "eighteenth", "18th", "the eighteenth", "the 18th" };
        }
    }

    [DataContract]
    public class TokenNinteenth : TokenOrdinal
    {
        public TokenNinteenth()
        {
            this.Words = new List<string> { "19", "nineenth", "19th", "the ninteenth", "the 19th" };
        }
    }

    [DataContract]
    public class TokenTwentieth : TokenOrdinal
    {
        public TokenTwentieth()
        {
            this.Words = new List<string> { "20", "twentieth", "20th", "the twentieth", "the 20th" };
        }
    }

    [DataContract]
    public class TokenTwentyFirst : TokenOrdinal
    {
        public TokenTwentyFirst()
        {
            this.Words = new List<string> { "21", "twentyfirst", "21st", "the twentyfirst", "the 21st" };
        }
    }

    [DataContract]
    public class TokenTwentySecond : TokenOrdinal
    {
        public TokenTwentySecond()
        {
            this.Words = new List<string> { "22", "twentysecond", "22nd", "the twentysecond", "the 22nd" };
        }
    }

    [DataContract]
    public class TokenTwentyThird : TokenOrdinal
    {
        public TokenTwentyThird()
        {
            this.Words = new List<string> { "23", "twentythird", "23rd", "the twentythird", "the 23rd" };
        }
    }

    [DataContract]
    public class TokenTwentyFourth : TokenOrdinal
    {
        public TokenTwentyFourth()
        {
            this.Words = new List<string> { "24", "twentyfourth", "24th", "the twentyfourth", "the 24th" };
        }
    }

    [DataContract]
    public class TokenTwentyFifth : TokenOrdinal
    {
        public TokenTwentyFifth()
        {
            this.Words = new List<string> { "25", "twentyfifth", "25th", "the twentyfifth", "the 25th" };
        }
    }

    [DataContract]
    public class TokenTwentySixth : TokenOrdinal
    {
        public TokenTwentySixth()
        {
            this.Words = new List<string> { "26", "twentysixth", "26th", "the twentysixth", "the 26th" };
        }
    }

    [DataContract]
    public class TokenTwentySeventh : TokenOrdinal
    {
        public TokenTwentySeventh()
        {
            this.Words = new List<string> { "27", "twentyseventh", "27th", "the twentyseventh", "the 27th" };
        }
    }

    [DataContract]
    public class TokenTwentyEighth : TokenOrdinal
    {
        public TokenTwentyEighth()
        {
            this.Words = new List<string> { "28", "twentyeighth", "28th", "the twentyeighth", "the 28th" };
        }
    }

    [DataContract]
    public class TokenTwentyNinth : TokenOrdinal
    {
        public TokenTwentyNinth()
        {
            this.Words = new List<string> { "29", "twentyninth", "29th", "the twentyninth", "the 29th" };
        }
    }

    [DataContract]
    public class TokenThirtieth : TokenOrdinal
    {
        public TokenThirtieth()
        {
            this.Words = new List<string> { "30", "thirtieth", "30th", "the thirtieth", "the 30th" };
        }
    }

    [DataContract]
    public class TokenThirtyFirst : TokenOrdinal
    {
        public TokenThirtyFirst()
        {
            this.Words = new List<string> { "31", "thirtyfirst", "31st", "the thirtyfirst", "the 31st" };
        }
    }
}