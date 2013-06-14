using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace SpeakToMe.Speech.Tokens.Temporal.TemporalParts
{
    [DataContract]
    public class TokenTime : Token
    {
        public override IEnumerable<TokenResult> Parse(string input, string UserId)
        {
            ////chunk up the input sentence
            var words = SplitToWords(input);
            var results = new List<TokenResult>();
            bool inQuote = false;

            for (int i = 0; i < words.Count; i++)
            {
                Word word = words[i];

                //if we find a quote in the word or the word is a quote, ignore 
                if (word.WordValue.IndexOf("\"") > -1)
                {
                    inQuote = !inQuote;
                }

                if (inQuote)
                {
                    continue;
                }

                bool isPm = false;
                int length = word.EndIndex - word.StartIndex;

                if (word.WordValue.IndexOf("pm") > -1)
                {
                    isPm = true;
                }

                word.WordValue = word.WordValue.Replace("am", "").Replace("pm", "");

                if (i < (words.Count - 1))
                {
                    Word nextWord = words[i + 1];

                    if (nextWord.WordValue.IndexOf("pm") > -1 && nextWord.WordValue.Length == 2)
                    {
                        isPm = true;
                        i = nextWord.EndIndex;
                        length = nextWord.EndIndex - word.StartIndex;
                    }
                }

                //number hunting
                if (word.WordValue.IndexOf(":") > -1)
                {
                    string[] parts = word.WordValue.Split(':');

                    int hours;
                    int minutes;
                    int seconds = 0;

                    if (!int.TryParse(parts[0], out hours))
                    {
                        continue;
                    }

                    if (!int.TryParse(parts[1], out minutes))
                    {
                        continue;
                    }

                    if (parts.Count() > 2)
                    {
                        if (int.TryParse(parts[2], out seconds))
                        {
                            continue;
                        }
                    }

                    if (isPm && hours < 12)
                    {
                        hours += 12;
                    }

                    var ts = new TimeSpan(hours, minutes, seconds);

                    var tr = new TokenResult
                    {
                        Length = length,
                        Start = word.StartIndex,
                        TokenType = this.GetType().ToString(),
                        Value = ts,
                        Token = new TokenTime { Value = ts }
                    };
                    results.Add(tr);
                }
                else
                {
                    int hours;

                    if (int.TryParse(word.WordValue, out hours))
                    {
                        if (isPm && hours < 12)
                        {
                            hours += 12;
                        }

                        var ts = new TimeSpan(hours, 0, 0);

                        var tr = new TokenResult
                        {
                            Length = length,
                            Start = word.StartIndex,
                            TokenType = this.GetType().ToString(),
                            Value = ts,
                            Token = new TokenTime { Value = ts }
                        };
                        results.Add(tr);
                    }
                }
            }

            return results;
        }

        private static List<Word> SplitToWords(string input)
        {
            string sentence = input.Trim();
            var words = new List<Word>();
            string wordAcc = "";
            int lastStart = 0;

            for (int i = 0; i < sentence.Length; i++)
            {
                if (sentence[i] == ' ' || i == sentence.Length - 1)
                {
                    if (wordAcc != "")
                    {
                        if (i == sentence.Length - 1)
                        {
                            wordAcc += sentence[i];
                        }

                        var word = new Word
                        {
                            EndIndex = i - 1,
                            StartIndex = lastStart,
                            WordValue = wordAcc
                        };
                        words.Add(word);
                        wordAcc = "";
                    }
                    lastStart = 0;
                }
                else
                {
                    if (lastStart == 0)
                    {
                        lastStart = i;
                    }

                    wordAcc += sentence[i];
                }
            }

            return words;
        }
    }
}