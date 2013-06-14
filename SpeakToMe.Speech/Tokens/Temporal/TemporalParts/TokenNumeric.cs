using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Runtime.Serialization;

namespace SpeakToMe.Speech.Tokens.Temporal.TemporalParts
{
    [DataContract]
    [Export(typeof(IParseToken))]
    public class TokenNumeric : Token, IParseToken
    {
        public override IEnumerable<TokenResult> Parse(string input, string UserId)
        {
            return new List<TokenResult>();
        }
    }

    [DataContract]
    [Export(typeof(IParseToken))]
    public class TokenInt : TokenNumeric, IParseToken
    {
        public override IEnumerable<TokenResult> Parse(string input, string UserId)
        {
            var results = new List<TokenResult>();
            string testString = input;
            bool isInQuote = false;

            string[] words = input.Split(' ');

            foreach (var word in words)
            {
                if (word.IndexOf("\"") > -1)
                {
                    isInQuote = !isInQuote;
                }

                if (isInQuote)
                {
                    continue;
                }

                double dblTest;

                if (double.TryParse(word.Replace(",", "").Replace("%", ""), out dblTest))
                {
                    int intTest;
                    if (int.TryParse(word.Replace(",", "").Replace("%", ""), out intTest))
                    {
                        var result = new TokenResult
                        {
                            Length = word.Length,
                            Start = testString.IndexOf(word),
                            TokenType = typeof (TokenInt).ToString(),
                            Value = intTest,
                            Token = new TokenInt { Value = intTest }
                        };
                        results.Add(result);
                    }
                }
            }

            return results;
        }
    }

    [DataContract]
    [Export(typeof(IParseToken))]
    public class TokenLong : TokenNumeric, IParseToken
    {
        public override IEnumerable<TokenResult> Parse(string input, string UserId)
        {
            var results = new List<TokenResult>();
            string testString = input;

            string[] words = input.Split(' ');

            foreach (var word in words)
            {
                double dblTest;

                if (double.TryParse(word.Replace(",", "").Replace("%", ""), out dblTest))
                {
                    long longTest;
                    if (long.TryParse(word.Replace(",", "").Replace("%", ""), out longTest))
                    {
                        var result = new TokenResult
                        {
                            Length = word.Length,
                            Start = testString.IndexOf(word),
                            TokenType = typeof(TokenLong).ToString(),
                            Value = longTest,
                            Token = new TokenLong { Value = longTest }
                        };

                        results.Add(result);
                    }
                }
            }

            return results;
        }
    }

    [DataContract]
    [Export(typeof(IParseToken))]
    public class TokenPercentage : TokenNumeric, IParseToken
    {
        public override IEnumerable<TokenResult> Parse(string input, string UserId)
        {
            var results = new List<TokenResult>();
            string testString = input;

            string[] words = input.Split(' ');

            foreach (var word in words)
            {
                double dblTest;

                if (double.TryParse(word.Replace(",", "").Replace("%", ""), out dblTest))
                {
                    if (dblTest >= -1 && dblTest <= 1)
                    {
                        var result = new TokenResult
                        {
                            Length = word.Length,
                            Start = testString.IndexOf(word),
                            TokenType = typeof(TokenPercentage).ToString(),
                            Value = dblTest,
                            Token = new TokenPercentage { Value = dblTest }
                        };

                        results.Add(result);
                    }
                }
            }

            return results;
        }
    }
}