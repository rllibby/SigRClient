﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;

namespace SpeakToMe.Speech.Tokens.Verbs
{
    [DataContract]
    [Export(typeof(IParseToken))]
    public class TokenWhatIs : Token, IParseToken
    {
        public TokenWhatIs()
        {
            this.Words = new List<string> { "what is", "what are", "what's a", "what're" };
        }

        public override IEnumerable<TokenResult> Parse(string input, string UserId)
        {
            var results = new List<TokenResult>();
            results.AddRange(base.Parse(input, UserId));

            if (results.Count > 0)
            {
                var searchTermResult = new TokenResult
                {
                    Length = input.Length - results[0].Length - 1,
                    Start = results[0].Start + results[0].Length + 1,
                    Token =
                        new TokenQuotedPhrase
                        {
                            Value =
                                input.Substring(results[0].Start + results[0].Length + 1)
                        },
                    TokenType = typeof (TokenQuotedPhrase).ToString(),
                    Value = input.Substring(results[0].Start + results[0].Length + 1)
                };
                results.Add(searchTermResult);
            }

            return results;
        }
    }
}