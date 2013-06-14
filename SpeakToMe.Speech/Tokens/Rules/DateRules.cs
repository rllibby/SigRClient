using SpeakToMe.Speech.Tokens.Temporal;
using SpeakToMe.Speech.Tokens.Temporal.TemporalParts;
using System;

namespace SpeakToMe.Speech.Tokens.Rules
{
    public class DateRules
    {
        public static DateTime GetDateFromTokens(TokenDayOfWeek dow, TokenMonth month, TokenOrdinal day)
        {
            return new DateTime(DateTime.Now.Year, (int)month.Value, (int)day.Value);
        }

        public static DateTime GetDateFromTokens(TokenMonth month, TokenOrdinal day)
        {
            return new DateTime(DateTime.Now.Year, (int)month.Value, (int)day.Value);
        }

        public static DateTime GetDateFromTokens(TokenMonth month, TokenOrdinal day, TokenNumeric year)
        {
            return new DateTime((int) year.Value, (int)month.Value, (int)day.Value);
        }

        public static DateTime GetDateFromTokens(TokenSpecifiedDate date)
        {
            return (DateTime) date.Value;
        }

        public static DateTime GetDateFromTokens(TokenDayOfWeek day)
        {
            DateTime theDate = (DateTime)day.Value;
            return new DateTime(theDate.Year, theDate.Month, theDate.Day, 0, 0, 0);
        }

        public static DateTime GetDateFromTokens(TokenDayOfWeek day, TokenTime time)
        {
            DateTime theDate = (DateTime) day.Value;
            return new DateTime(theDate.Year, theDate.Month, theDate.Day, 0, 0, 0) + (TimeSpan)time.Value;
        }

        public static DateTime GetDateFromTokens(TokenRelativeTemporalOrdinal day, TokenTime time)
        {
            DateTime theDate = (DateTime)day.Value;
            return new DateTime(theDate.Year, theDate.Month, theDate.Day, 0, 0, 0) + (TimeSpan)time.Value;
        }

        public static DateTime GetDateFromTokens(TokenRelativeTemporalOrdinal day)
        {
            DateTime theDate = (DateTime)day.Value;
            return new DateTime(theDate.Year, theDate.Month, theDate.Day, 0, 0, 0);
        }

        public static DateTime GetDateFromTokens(TokenOrdinal ordinal, TokenDayOfWeek day)
        {
            int number = (int) ordinal.Value;
            DayOfWeek dayOfWeek = ((DateTime) day.Value).DayOfWeek;
            int foundCounter = 0;

            DateTime startDate = new DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0);

            while (true)
            {
                if (startDate.DayOfWeek == dayOfWeek)
                {
                    foundCounter++;
                    if (foundCounter == number)
                    {
                        return startDate;
                    }
                }

                startDate = startDate.AddDays(1);
            }
        }

        public static DateTime GetDateFromTokens(TokenOrdinal ordinal, TokenDayOfWeek day, TokenTime time)
        {
            DateTime theDate = GetDateFromTokens(ordinal, day);
            return theDate + (TimeSpan) time.Value;
        }

        public static DateTime GetDateFromTokens(TokenOrdinal ordinal, TokenDayOfWeek day, TokenMonth month)
        {
            int number = (int)ordinal.Value;
            DayOfWeek dayOfWeek = ((DateTime)day.Value).DayOfWeek;
            int foundCounter = 0;

            DateTime startDate = new DateTime(DateTime.Now.Year, (int)month.Value, 1, 0, 0, 0);

            while (true)
            {
                if (startDate.DayOfWeek == dayOfWeek)
                {
                    foundCounter++;
                    if (foundCounter == number)
                    {
                        return startDate;
                    }
                }

                startDate = startDate.AddDays(1);
            }
        }

        public static DateTime GetDateFromTokens(TokenOrdinal ordinal, TokenDayOfWeek day, TokenMonth month, TokenTime time)
        {
            return GetDateFromTokens(ordinal, day, month) + (TimeSpan) time.Value;
        }

        public static DateTime GetDateFromTokens(TokenMonth month, TokenOrdinal day, TokenTime time)
        {
            return new DateTime(DateTime.Now.Year, (int)month.Value, (int)day.Value) + (TimeSpan)time.Value;
        }

        public static DateTime GetDateFromTokens(TokenTime time)
        {
            return DateTime.Today + (TimeSpan)time.Value;
        }

        //public static DateTime GetDateFromTokens(TokenSpecifiedDate date)
        //{
        //    return (DateTime) date.Value;
        //}

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public static TokenDeterminateSeries GetSeriesFromTokens(TokenEach each, TokenDayOfWeek dow, TokenMonth month)
        {
            DateTime endDate;
            DateTime startDate;

            if (DateTime.Now.Month <= (int)month.Value)
            {
                endDate = new DateTime(DateTime.Now.Year, (int) month.Value,
                    new DateTime(DateTime.Now.Year, (int)month.Value + 1, 1).AddDays(-1).Day);
                startDate = GetFirstDowOfMonth((int)month.Value, DateTime.Now.Year, ((DateTime)dow.Value).DayOfWeek);
            }
            else
            {
                endDate = new DateTime(DateTime.Now.Year + 1, (int) month.Value,
                    new DateTime(DateTime.Now.Year, (int)month.Value + 1, 1).AddDays(-1).Day);
                startDate = GetFirstDowOfMonth((int)month.Value, DateTime.Now.Year + 1, (DayOfWeek)dow.Value);
            }

            var series = new TokenDeterminateSeries
            {
                EndDate = endDate,
                Interval = 1,
                IntervalType = TokenDeterminateSeries.SeriesIntervalType.DayOfWeek,
                StartDate = startDate,
                Value = dow.Value
            };
            return series;
        }

        public static TokenDeterminateSeries GetSeriesFromTokens(TokenEach each, TokenDayOfWeek dow, TokenInt year)
        {
            var endDate = new DateTime((int)year.Value + 1, 1, 1).AddDays(-1);
            var startDate = GetFirstDowOfYear((int) year.Value, (DayOfWeek) dow.Value);

            var series = new TokenDeterminateSeries
            {
                EndDate = endDate,
                Interval = 1,
                IntervalType = TokenDeterminateSeries.SeriesIntervalType.DayOfWeek,
                StartDate = startDate,
                Value = dow.Value
            };
            return series;
        }

        public static TokenDeterminateSeries GetSeriesFromTokens(TokenEach each, TokenOther other, TokenDayOfWeek dow, TokenMonth month)
        {
            DateTime endDate;
            DateTime startDate;

            if (DateTime.Now.Month <= (int)month.Value)
            {
                endDate = new DateTime(DateTime.Now.Year, (int) month.Value,
                    new DateTime(DateTime.Now.Year, (int)month.Value + 1, 1).AddDays(-1).Day);
                startDate = GetFirstDowOfMonth((int)month.Value, DateTime.Now.Year, (DayOfWeek)dow.Value);
            }
            else
            {
                endDate = new DateTime(DateTime.Now.Year + 1, (int) month.Value,
                    new DateTime(DateTime.Now.Year, (int)month.Value + 1, 1).AddDays(-1).Day);
                startDate = GetFirstDowOfMonth((int)month.Value, DateTime.Now.Year + 1, (DayOfWeek)dow.Value);
            }

            var series = new TokenDeterminateSeries
            {
                EndDate = endDate,
                Interval = 2,
                IntervalType = TokenDeterminateSeries.SeriesIntervalType.DayOfWeek,
                StartDate = startDate,
                Value = dow.Value
            };
            return series;
        }

        public static TokenDeterminateSeries GetSeriesFromTokens(TokenEach each, TokenOrdinal ordinal, TokenDayOfWeek dow, TokenInt year)
        {
            var endDate = new DateTime((int)year.Value + 1, 1, 1).AddDays(-1);
            var startDate = GetFirstDowOfYear((int)year.Value, (DayOfWeek)dow.Value);

            var series = new TokenDeterminateSeries
            {
                EndDate = endDate,
                Interval = (int)ordinal.Value,
                IntervalType = TokenDeterminateSeries.SeriesIntervalType.DayOfWeek,
                StartDate = startDate,
                Value = dow.Value
            };
            return series;
        }

        public static TokenDeterminateSeries GetSeriesFromTokens(TokenEach each, TokenOther other, TokenDayOfWeek dow, TokenInt year)
        {
            var endDate = new DateTime((int)year.Value + 1, 1, 1).AddDays(-1);
            var startDate = GetFirstDowOfYear((int)year.Value, ((DateTime)dow.Value).DayOfWeek);

            var series = new TokenDeterminateSeries
            {
                EndDate = endDate,
                Interval = 2,
                IntervalType = TokenDeterminateSeries.SeriesIntervalType.DayOfWeek,
                StartDate = startDate,
                Value = dow.Value
            };
            return series;
        }

        public static TokenDeterminateSeries GetSeriesFromTokens(TokenEach each, TokenOrdinal ordinal, TokenInt year)
        {
            var endDate = new DateTime((int)year.Value + 1, 1, 1).AddDays(-1);
            var startDate = new DateTime((int)year.Value, 1, (int)ordinal.Value);

            var series = new TokenDeterminateSeries
            {
                EndDate = endDate,
                Interval = (int)ordinal.Value,
                IntervalType = TokenDeterminateSeries.SeriesIntervalType.DayOfMonth,
                StartDate = startDate,
                Value = ordinal.Value
            };
            return series;
        }

        public static TokenIndeterminateSeries GetIndeterminateSeriesFromTokens(TokenEach each, TokenDayOfWeek dow)
        {
            var startDate = DateTime.Now;

            var series = new TokenIndeterminateSeries
            {
                Interval = 1,
                IntervalType = TokenIndeterminateSeries.SeriesIntervalType.DayOfWeek,
                StartDate = startDate,
                Value = dow
            };
            return series;
        }

        public static TokenIndeterminateSeries GetIndeterminateSeriesFromTokens(TokenEach each, TokenDayOfWeek dow, TokenTime time)
        {
            var startDate = DateTime.Today;
            var theDateTime = (TimeSpan)time.Value;

            var series = new TokenIndeterminateSeries
            {
                Interval = 1,
                IntervalType = TokenIndeterminateSeries.SeriesIntervalType.DayOfWeek,
                StartDate = startDate + theDateTime,
                Value = dow
            };
            return series;
        }

        public static TokenIndeterminateSeries GetIndeterminateSeriesFromTokens(TokenEach each, TokenOrdinal ordinal)
        {
            var startDate = DateTime.Now;

            var series = new TokenIndeterminateSeries
            {
                Interval = 1,
                IntervalType = TokenIndeterminateSeries.SeriesIntervalType.DayOfMonth,
                StartDate = startDate,
                Value = ordinal.Value
            };
            return series;
        }

        public static TokenIndeterminateSeries GetIndeterminateSeriesFromTokens(TokenEach each, TokenOrdinal ordinal, TokenTime time)
        {
            var startDate = DateTime.Today;
            var theDateTime = (TimeSpan)time.Value;

            var series = new TokenIndeterminateSeries
            {
                Interval = 1,
                IntervalType = TokenIndeterminateSeries.SeriesIntervalType.DayOfMonth,
                StartDate = startDate + theDateTime,
                Value = ordinal.Value
            };
            return series;
        }

        public static TokenIndeterminateSeries GetIndeterminateSeriesFromTokens(TokenEach each, TokenOrdinal ordinal, TokenDayOfWeek dow)
        {
            var startDate = DateTime.Now;

            var series = new TokenIndeterminateSeries
            {
                Interval = (int)ordinal.Value,
                IntervalType = TokenIndeterminateSeries.SeriesIntervalType.DayOfWeek,
                StartDate = startDate,
                Value = dow
            };
            return series;
        }

        public static TokenIndeterminateSeries GetIndeterminateSeriesFromTokens(TokenEach each, TokenOrdinal ordinal, TokenDayOfWeek dow, TokenTime time)
        {
            var startDate = DateTime.Today;
            var theDateTime = (TimeSpan)time.Value;

            var series = new TokenIndeterminateSeries
            {
                Interval = (int)ordinal.Value,
                IntervalType = TokenIndeterminateSeries.SeriesIntervalType.DayOfWeek,
                StartDate = startDate + theDateTime,
                Value = dow
            };
            return series;
        }

        public static TokenIndeterminateSeries GetIndeterminateSeriesFromTokens(TokenEach each, TokenOther other, TokenDayOfWeek dow)
        {
            var startDate = DateTime.Now;

            var series = new TokenIndeterminateSeries
            {
                Interval = 2,
                IntervalType = TokenIndeterminateSeries.SeriesIntervalType.DayOfWeek,
                StartDate = startDate,
                Value = dow
            };
            return series;
        }

        public static TokenIndeterminateSeries GetIndeterminateSeriesFromTokens(TokenEach each, TokenOther other, TokenDayOfWeek dow, TokenTime time)
        {
            var startDate = DateTime.Today;
            var theDateTime = (TimeSpan)time.Value;

            var series = new TokenIndeterminateSeries
            {
                Interval = 2,
                IntervalType = TokenIndeterminateSeries.SeriesIntervalType.DayOfWeek,
                StartDate = startDate + theDateTime,
                Value = dow
            };
            return series;
        }

        private static DateTime GetFirstDowOfMonth(int month, int year, DayOfWeek dow)
        {
            DateTime theDate = new DateTime(year, month, 1);

            while (theDate.DayOfWeek != dow)
            {
                theDate = theDate.AddDays(1);
            }

            return theDate;
        }

        private static DateTime GetFirstDowOfYear(int year, DayOfWeek dow)
        {
            DateTime theDate = new DateTime(year, 1, 1);

            while (theDate.DayOfWeek != dow)
            {
                theDate = theDate.AddDays(1);
            }

            return theDate;
        }
    }
}