using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SpeakToMe.Speech.Tokens.Temporal.TemporalParts
{
    [DataContract]
    public class TokenDayOfWeek : Token
    {
        public override IEnumerable<TokenResult> Parse(string input, string UserId)
        {
            var results = new List<TokenResult>();

            bool isPast = input.IndexOf("past") > -1 || input.IndexOf("last") > -1;
            List<int> quotePoints = GetQuotePoints(input);

            foreach (var word in this.Words)
            {
                int index = string.Format("{0} ", input).IndexOf(word.ToLower());

                if (index > -1 && !IsInQuotes(index, quotePoints))
                {
                    results.Add(new TokenResult
                    {
                        Length = word.Length,
                        Start = index,
                        TokenType = this.GetType().ToString(),
                        Value = isPast ? GetPastDateForDay((DayOfWeek)Enum.Parse(typeof(DayOfWeek), this.Words[0])) : GetFutureDateForDay((DayOfWeek)Enum.Parse(typeof(DayOfWeek), this.Words[0])),
                        Token = new TokenDayOfWeek { Value = isPast ? GetPastDateForDay((DayOfWeek)Enum.Parse(typeof(DayOfWeek), this.Words[0])) : GetFutureDateForDay((DayOfWeek)Enum.Parse(typeof(DayOfWeek), this.Words[0])) }
                    });
                }
            }

            return results;
        }

        private static bool IsInQuotes(int index, List<int> quotePoints)
        {
            for (int i = 0; i < quotePoints.Count ; i++)
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

        private static DateTime GetFutureDateForDay(DayOfWeek day)
        {
            int days = (7 - (int) DateTime.Now.DayOfWeek + (int) day);
            if (days > 7)
            {
                days = days - 7;
            }

            DateTime date = DateTime.Now.AddDays(days);

            return date;
        }

        private static DateTime GetPastDateForDay(DayOfWeek day)
        {
            return DateTime.Now.AddDays(-7 + ((int)day - (int)DateTime.Now.DayOfWeek));
        }
    }

    [DataContract]
    public class TokenSunday : TokenDayOfWeek
    {
        public TokenSunday()
        {
            this.Words = new List<string> { "Sunday", " sun ", " sun. " };
        }
    }

    [DataContract]
    public class TokenMonday : TokenDayOfWeek
    {
        public TokenMonday()
        {
            this.Words = new List<string> { "Monday", " mon ", " mon. " };
        }
    }

    [DataContract]
    public class TokenTuesday : TokenDayOfWeek
    {
        public TokenTuesday()
        {
            this.Words = new List<string> { "Tuesday", " tue ", " tues. " };
        }
    }

    [DataContract]
    public class TokenWednesday : TokenDayOfWeek
    {
        public TokenWednesday()
        {
            this.Words = new List<string> { "Wednesday", " wed ", " wed. " };
        }
    }

    [DataContract]
    public class TokenThursday : TokenDayOfWeek
    {
        public TokenThursday()
        {
            this.Words = new List<string> { "Thursday", " thu ", " thu. " };
        }
    }

    [DataContract]
    public class TokenFriday : TokenDayOfWeek
    {
        public TokenFriday()
        {
            this.Words = new List<string> { "Friday", " fri ", " fri. " };
        }
    }

    [DataContract]
    public class TokenSaturday : TokenDayOfWeek
    {
        public TokenSaturday()
        {
            this.Words = new List<string> { "Saturday", " sat ", " sat." };
        }
    }
}