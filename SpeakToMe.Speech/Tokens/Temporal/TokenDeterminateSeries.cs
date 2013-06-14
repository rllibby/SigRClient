using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace SpeakToMe.Speech.Tokens.Temporal
{
    [DataContract]
    [Export(typeof(IParseToken))]
    public class TokenDeterminateSeries : Token, IParseToken
    {
        public enum SeriesIntervalType
        {
            DayOfWeek = 1,
            DayOfMonth = 2
        }

        private static List<Token> _tokens;
        private static List<MethodInfo> _temporalRules;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public SeriesIntervalType IntervalType { get; set; }
        public int Interval { get; set; }

        public TokenDeterminateSeries()
        {
            _tokens = LoadTokens();
            _temporalRules = LoadRules();
        }

        public override IEnumerable<TokenResult> Parse(string input, string UserId)
        {
            var results = new List<TokenResult>();
            var returnResults = new List<TokenResult>();
            var parmCounts = new List<int>();

            //loop through all the tokens and give each a chance to parse

            foreach (var token in _tokens)
            {
                results.AddRange(token.Parse(input, UserId));
            }

            if (results.Count == 0)
            {
                return new List<TokenResult>();
            }

            //arrange all token results by their start positions
            var buckets = new Dictionary<int, List<TokenResult>>();

            foreach (var result in results.OrderBy(r => r.Start))
            {
                if (!buckets.ContainsKey(result.Start))
                {
                    buckets[result.Start] = new List<TokenResult>();
                }

                buckets[result.Start].Add(result);
            }

            //check our tokens against the rules
            foreach (var temporalRule in _temporalRules)
            {
                //get the params of the method
                var parameters = temporalRule.GetParameters();

                int index = 0;
                var parms = new List<TokenResult>();

                //for each parm, see if the coresponding bucket has a token of that type
                foreach (var parameterInfo in parameters)
                {
                    Type searchType = parameterInfo.ParameterType;

                    TokenResult token = (from t in buckets.ElementAt(index).Value
                                         where t.Token.GetType() == searchType || t.Token.GetType().IsSubclassOf(searchType)
                                         select t).FirstOrDefault();

                    if (null != token)
                    {
                        parms.Add(token);

                        if (index < buckets.Count - 1)
                        {
                            index++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                if (parms.Count == parameters.Count())
                {
                    var passins = new object[parms.Count];

                    for (int i = 0; i < parms.Count; i++)
                    {
                        passins[i] = parms[i].Token;
                    }

                    var theSeries = (TokenDeterminateSeries)temporalRule.Invoke(null, passins);

                    if (theSeries != null)
                    {
                        int start = (from t in results
                                     select t.Start).Min();

                        int end = (from t in results
                                   select t.Start + t.Length).Max();

                        int length = end - start;

                        returnResults.Add(new TokenResult
                        {
                            Start = start,
                            Length = length,
                            TokenType = typeof(TokenDeterminateSeries).ToString(),
                            Token = theSeries,
                            Value = theSeries
                        });

                        parmCounts.Add(parms.Count);
                    }
                }
            }

            int maxParmCount = 0;
            TokenResult returnResult = null;
            int pointer = 0;

            foreach (int parmCount in parmCounts)
            {
                if (parmCount > maxParmCount)
                {
                    maxParmCount = parmCount;
                    returnResult = returnResults[pointer];
                    pointer++;
                }
            }

            if (returnResult != null)
            {
                return new List<TokenResult> { returnResult };
            }

            return new List<TokenResult>();
        }

        private static List<Token> LoadTokens()
        {
            var tokens = new List<Token>();
            var assembly = Assembly.GetExecutingAssembly();

            foreach (Type type in assembly.GetTypes())
            {
                if (type.Namespace == "SmartHome.SpeechProcessor.Tokens.Temporal.TemporalParts" &&
                    type.Name.StartsWith("Token") &&
                    type.Name != "TokenDayOfWeek" &&
                    type.Name != "TokenMonth" &&
                    type.Name != "TokenOrdinal" &&
                    type.Name != "TokenRelativeTemporalOrdinal")
                {
                    ConstructorInfo ci = type.GetConstructor(Type.EmptyTypes);
                    tokens.Add((Token)ci.Invoke(null));
                }
            }

            return tokens;
        }

        private static List<MethodInfo> LoadRules()
        {
            var assembly = Assembly.GetExecutingAssembly();

            Type ruleClass = (from t in assembly.GetTypes()
                              where t.Namespace == "SpeakToMe.Speech.Tokens.Rules"
                              where t.Name == "DateRules"
                              select t).First();

            var methods = from m in ruleClass.GetMembers()
                          where m.Name == "GetSeriesFromTokens"
                          select m;

            return methods.Cast<MethodInfo>().ToList();
        }

        public override string ToString()
        {
            string response;

            if (this.IntervalType == SeriesIntervalType.DayOfWeek)
            {
                response = string.Format("every {0}", this.StartDate.DayOfWeek);
            }
            else
            {
                response = string.Format("every {0}", IntToOrdinal(this.StartDate.Day));
            }

            if ((this.EndDate - this.StartDate).Days > 31)
            {
                response += string.Format(" in {0}", this.StartDate.Year);
            }
            else
            {
                response += string.Format(" in {0}", IntToMonthName(this.StartDate.Month));
            }

            return response;
        }

        private static string IntToMonthName(int value)
        {
            switch (value)
            {
                case 1:
                    return "January";
                case 2:
                    return "February";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";
                default:
                    return value.ToString();
            }
        }

        private static string IntToOrdinal(int value)
        {
            switch (value)
            {
                case 1:
                    return "1st";
                case 2:
                    return "2nd";
                case 3:
                    return "3rd";
                case 4:
                    return "4th";
                case 5:
                    return "5th";
                case 6:
                    return "6th";
                case 7:
                    return "7th";
                case 8:
                    return "8th";
                case 9:
                    return "9th";
                case 10:
                    return "10th";
                case 11:
                    return "11th";
                case 12:
                    return "12th";
                case 13:
                    return "13th";
                case 14:
                    return "14th";
                case 15:
                    return "15th";
                case 16:
                    return "16th";
                case 17:
                    return "17th";
                case 18:
                    return "18th";
                case 19:
                    return "19th";
                case 20:
                    return "20th";
                case 21:
                    return "21st";
                case 22:
                    return "22nd";
                case 23:
                    return "23rd";
                case 24:
                    return "24th";
                case 25:
                    return "25th";
                case 26:
                    return "26th";
                case 27:
                    return "27th";
                case 28:
                    return "28th";
                case 29:
                    return "29th";
                case 30:
                    return "30th";
                case 31:
                    return "31st";
                default:
                    return value.ToString();
            }
        }
    }
}