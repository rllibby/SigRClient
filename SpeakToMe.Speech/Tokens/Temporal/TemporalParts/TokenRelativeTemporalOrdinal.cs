using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SpeakToMe.Speech.Tokens.Temporal.TemporalParts
{
    [DataContract]
    public class TokenRelativeTemporalOrdinal : Token
    {
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
    }

    [DataContract]
    public class TokenTomorrow : TokenRelativeTemporalOrdinal
    {
        public TokenTomorrow()
        {
            this.Words = new List<string> { "tomorrow" };
        }

        public override IEnumerable<TokenResult> Parse(string input, string UserId)
        {
            var results = new List<TokenResult>();
            List<int> quotePoints = this.GetQuotePoints(input);

            int index = input.IndexOf(this.Words[0]);

            if (index > -1 && input.IndexOf("day after tomorrow") == -1 && !this.IsInQuotes(index, quotePoints))
            {
                results.Add(new TokenResult
                {
                    Length = this.Words[0].Length,
                    Start = index,
                    TokenType = typeof(TokenDayOfWeek).ToString(),
                    Value = DateTime.Now.AddDays(1),
                    Token = new TokenDayOfWeek { Value = DateTime.Now.AddDays(1) }
                });
            }
            return results;
        }
    }

    [DataContract]
    public class TokenYesterday : TokenRelativeTemporalOrdinal
    {
        public TokenYesterday()
        {
            this.Words = new List<string> { "yesterday" };
        }

        public override IEnumerable<TokenResult> Parse(string input, string UserId)
        {
            var results = new List<TokenResult>();
            List<int> quotePoints = this.GetQuotePoints(input);

            int index = input.IndexOf(this.Words[0]);

            if (index > -1 && input.IndexOf("day before yesterday") == -1 && !this.IsInQuotes(index, quotePoints))
            {
                results.Add(new TokenResult
                {
                    Length = this.Words[0].Length,
                    Start = index,
                    TokenType = typeof(TokenDayOfWeek).ToString(),
                    Value = DateTime.Now.AddDays(-1),
                    Token = new TokenDayOfWeek { Value = DateTime.Now.AddDays(-1) }
                });
            }
            return results;
        }
    }

    [DataContract]
    public class TokenDayBeforeYesterday : TokenRelativeTemporalOrdinal
    {
        public TokenDayBeforeYesterday()
        {
            this.Words = new List<string> { "day before yesterday" };
        }

        public override IEnumerable<TokenResult> Parse(string input, string UserId)
        {
            var results = new List<TokenResult>();
            List<int> quotePoints = this.GetQuotePoints(input);

            int index = input.IndexOf(this.Words[0]);

            if (index > -1 && !this.IsInQuotes(index, quotePoints))
            {
                results.Add(new TokenResult
                {
                    Length = this.Words[0].Length,
                    Start = index,
                    TokenType = typeof(TokenDayOfWeek).ToString(),
                    Value = DateTime.Now.AddDays(-2),
                    Token = new TokenDayOfWeek { Value = DateTime.Now.AddDays(-2) }
                });
            }
            return results;
        }
    }

    [DataContract]
    public class TokenDayAfterTomorrow : TokenRelativeTemporalOrdinal
    {
        public TokenDayAfterTomorrow()
        {
            this.Words = new List<string> { "day after tomorrow" };
        }

        public override IEnumerable<TokenResult> Parse(string input, string UserId)
        {
            var results = new List<TokenResult>();
            List<int> quotePoints = this.GetQuotePoints(input);

            int index = input.IndexOf(this.Words[0]);

            if (index > -1 && !this.IsInQuotes(index, quotePoints))
            {
                results.Add(new TokenResult
                {
                    Length = this.Words[0].Length,
                    Start = index,
                    TokenType = typeof(TokenDayOfWeek).ToString(),
                    Value = DateTime.Now.AddDays(2),
                    Token = new TokenDayOfWeek { Value = DateTime.Now.AddDays(2) }
                });
            }
            return results;
        }
    }

    [DataContract]
    public class TokenToday : TokenRelativeTemporalOrdinal
    {
        public TokenToday()
        {
            this.Words = new List<string> { "today" };
        }

        public override IEnumerable<TokenResult> Parse(string input, string UserId)
        {
            var results = new List<TokenResult>();
            List<int> quotePoints = this.GetQuotePoints(input);

            int index = input.IndexOf(this.Words[0]);

            if (index > -1 && !this.IsInQuotes(index, quotePoints))
            {
                results.Add(new TokenResult
                {
                    Length = this.Words[0].Length,
                    Start = index,
                    TokenType = typeof (TokenDayOfWeek).ToString(),
                    Value = DateTime.Now,
                    Token = new TokenDayOfWeek { Value = DateTime.Now }
                });
            }
            return results;
        }
    }
}