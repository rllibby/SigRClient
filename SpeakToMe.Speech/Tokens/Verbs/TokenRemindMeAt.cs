﻿using SpeakToMe.Speech.Tokens.Prepositions;
using SpeakToMe.Speech.Tokens.Temporal;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;

namespace SpeakToMe.Speech.Tokens.Verbs
{
    [DataContract]
    [Export(typeof(IParseToken))]
    public class TokenRemindMeAt : Token, IParseToken
    {
        /// <summary>
        /// Initializes a new instance of the TokenRemindMeAt class.
        /// </summary>
        public TokenRemindMeAt()
        {
            this.Words = new List<string> { "remind me", "remind me at", "remind me on" };
        }

        public override IEnumerable<TokenResult> Parse(string input, string UserId)
        {
            var tokenResults = new List<TokenResult>();

            var results = base.Parse(input, UserId); //Let base class find our search string

            if (results.Any()) //if found
            {
                tokenResults.AddRange(results);

                int startPos = results.First().Start + results.First().Length + 1; //Find first character position after string

                //Lets see is there is a preposition between this position and the end that is followed by a temporal phrase
                string remainder = input.Substring(startPos);

                if (remainder.Length > 0)
                {
                    List<TokenResult> temporalResults = new List<TokenResult>();
                    temporalResults.AddRange(new TokenTemporal().Parse(remainder, UserId));
                    temporalResults.AddRange(new TokenDeterminateSeries().Parse(remainder, UserId));
                    temporalResults.AddRange(new TokenIndeterminateSeries().Parse(remainder, UserId));

                    //we found a temporal token
                    if (temporalResults.Any())
                    {
                        //get the last-most result
                        var temporalToken = temporalResults.OrderByDescending(token => token.Start).First();

                        string leftString = (remainder.Length <= temporalToken.Length) ? "" : remainder.Substring(temporalToken.Length + 1).Replace("am", "").Replace("pm", "").Replace("to", "").Trim();

                        if (temporalToken != null)
                        {
                            tokenResults.Add(temporalToken);
                        }

                        if (!string.IsNullOrEmpty(leftString))
                        {
                            tokenResults.Add(new TokenResult
                            {
                                Length = leftString.Length,
                                Start = input.IndexOf(leftString),
                                Token = new TokenQuotedPhrase { Value = leftString },
                                TokenType = typeof(TokenQuotedPhrase).ToString(),
                                Value = leftString
                            });

                            // now get the propositions in that same string
                            /*
                            var prepositionResults = new TokenPreposition().Parse(remainder, UserId);

                            // get the last-most one
                            if (prepositionResults.Any())
                            {
                                var prepResult = prepositionResults.OrderByDescending(prep => prep.Start).First();
                            }
                             */
                        }
                    }
                    else
                    {
                        tokenResults.Add(new TokenResult
                        {
                            Length = remainder.Length,
                            Start = startPos,
                            Token = new TokenQuotedPhrase { Value = remainder },
                            TokenType = typeof(TokenQuotedPhrase).ToString(),
                            Value = remainder
                        });
                    }
                }
            }

            return tokenResults;
        }
    }
}