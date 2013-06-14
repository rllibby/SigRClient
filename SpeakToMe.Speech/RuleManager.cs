using SpeakToMe.Speech.Tokens;
using SpeakToMe.Speech.Tokens.Verbs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SpeakToMe.Speech
{
    //Class rewsponsible for locating rules that match the pattern of tokens deternined to be in the command
    public static class RuleManager
    {
        private static readonly List<MethodInfo> CommandRules;

        static RuleManager()
        {
            CommandRules = LoadRules();
        }

        public static RuleMethod LocateMatchingRule(Dictionary<int, List<TokenResult>> buckets, ConversationContext context)
        {
            var Rules = new List<RuleMethod>();

            //check our tokens against the rules
            foreach (var rule in CommandRules)
            {
                //Console.WriteLine(rule.Name + "  params:" + rule.GetParameters().Count().ToString());
                //get the params of the method
                var parameters = (from p in rule.GetParameters()
                                  where p.ParameterType != typeof(ConversationContext)
                                  select p).ToList();

                int index = 0;
                var parms = new List<TokenResult>();

                //for each parm, see if the corresponding bucket has a token of that type
                foreach (var parameterInfo in parameters)
                {
                    Type searchType = parameterInfo.ParameterType;

                    TokenResult token = null;

                    if (buckets.Count > 0)
                    {
                        token = (from t in buckets.ElementAt(index).Value
                                 where
                                      t.Token.GetType() == searchType ||
                                      t.Token.GetType().IsSubclassOf(searchType)
                                 select t).FirstOrDefault();

                        if (searchType == typeof(TokenWhoWasIn) && token != null)
                        {
                            for (int i = index + 1; i < buckets.Count; i++)
                            {
                                foreach (var result in buckets.ElementAt(i).Value)
                                {
                                    if (result.Token is TokenQuotedPhrase)
                                    {
                                        parms.Add(token);
                                        parms.Add(result);
                                        break;
                                    }
                                }
                            }

                            break;
                        }
                    }

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

                if (parms.Count == parameters.Count() && parms.Count > 0)
                {
                    var passins = new object[parms.Count + 1];
                    passins[0] = context;

                    for (int i = 1; i < parms.Count + 1; i++)
                    {
                        passins[i] = parms[i - 1].Token;
                    }

                    Rules.Add(new RuleMethod
                    {
                        PassIns = passins,
                        Rule = rule
                    });
                }
            }

            RuleMethod ruleMethod = Rules.Where(r => r.PassIns.Count() == Rules.Max(s => s.PassIns.Count())).FirstOrDefault();

            return ruleMethod;
        }

        private static List<MethodInfo> LoadRules()
        {
            var rules = new List<MethodInfo>();
            var assemblies = new List<Assembly>();
            var currentAssembly = Assembly.GetExecutingAssembly();
            string path = currentAssembly.Location;
            IEnumerable methods = null;

            foreach (string dll in Directory.GetFiles(Path.GetDirectoryName(path), "*.dll"))
            {
                try
                {
                    assemblies.Add(Assembly.LoadFile(dll));
                }
                catch (Exception)
                {
                }
            }

            foreach (var assembly in assemblies)
            {
                List<Type> ruleClasses = (from t in assembly.GetTypes()
                                          where !t.IsInterface
                                          where !t.IsAbstract
                                          where t.GetInterface("SpeakToMe.Core.Interfaces.IRuleClass") != null
                                          select t).ToList();

                foreach (var ruleClass in ruleClasses)
                {
                    if (ruleClass == null)
                    {
                        continue;
                    }

                    methods = from m in ruleClass.GetMethods()
                              where m.IsStatic
                              select m;

                    rules.AddRange(methods.Cast<object>().Cast<MethodInfo>());
                }
            }

            return rules.ToList();
        }
    }
}