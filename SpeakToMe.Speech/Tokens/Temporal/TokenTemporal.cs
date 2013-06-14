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
    public class TokenTemporal : Token, IParseToken
    {
        private static readonly List<Token> Tokens;
        private static readonly List<MethodInfo> TemporalRules;

        static TokenTemporal()
        {
            Tokens = LoadTokens();
            TemporalRules = LoadRules();
        }

        public override IEnumerable<TokenResult> Parse(string input, string UserId) //expecting a sentence
        {
            var results = new List<TokenResult>();
            var returnResults = new List<TokenResult>();
            DateTime? theDate;
            var parmCounts = new List<int>();

            //loop through all the tokens and give each a chance to parse

            foreach (var token in Tokens)
            {
                try
                {
                    results.AddRange(token.Parse(input, UserId));
                }
                catch (Exception)
                {
                    throw;
                }
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
            foreach (var temporalRule in TemporalRules)
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

                    theDate = (DateTime)temporalRule.Invoke(null, passins);

                    if (theDate.HasValue)
                    {
                        int start = (from t in results
                                     select t.Start).Min();

                        int end = (from t in results
                                   select t.Start + t.Length).Max() + 1;

                        int length = end - start;

                        returnResults.Add(new TokenResult
                        {
                            Start = start,
                            Length = length,
                            TokenType = typeof(TokenTemporal).ToString(),
                            Token = new TokenTemporal { Value = theDate },
                            Value = theDate
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
                if (type.Namespace == "SpeakToMe.Speech.Tokens.Temporal.TemporalParts" &&
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
                          where m.Name == "GetDateFromTokens"
                          select m;

            return methods.Cast<MethodInfo>().ToList();
        }
    }
}