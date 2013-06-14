using SpeakToMe.Speech.Tokens.Nouns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


namespace SpeakToMe.Speech.Tokens
{
    [DataContract]
    [KnownType("GetKnownTypes")]
    public class TokenResult
    {
        [DataMember]
        public object Value { get; set; }

        [DataMember]
        public string TokenType { get; set; }

        [DataMember]
        public int Start { get; set; }

        [DataMember]
        public int Length { get; set; }

        [DataMember]
        public Token Token { get; set; }

        private static IEnumerable<Type> GetKnownTypes()
        {
            return new List<Type>
                            {
                                typeof(Token),
                                typeof(TokenInt),
                                typeof(TokenLong),
                                typeof(TokenNumeric),
                                typeof(TokenPercentage),
                                typeof(TokenQuotedPhrase),
                                typeof(TokenResult),
                                typeof(Tokens.Nouns.TokenNoun),
                                typeof(Tokens.Nouns.TokenToDo),
                                typeof(Tokens.Nouns.TokenEmail),
                                typeof(Tokens.Nouns.TokenSms),
                                typeof(Tokens.Nouns.TokenWeather),
                                typeof(Tokens.Nouns.TokenNews),
                                typeof(Tokens.Nouns.TokenItem),
                                typeof(Tokens.Nouns.TokenNeither),
                                typeof(Tokens.Nouns.TokenYesNo),
                                typeof(TokenReminder),
                                typeof(Tokens.Nouns.TokenState),
                                typeof(Tokens.Prepositions.TokenFor),
                                typeof(Tokens.Queries.TokenQueryQuantity),
                                typeof(Tokens.Queries.TokenRequest),
                                typeof(Tokens.Temporal.TokenDeterminateSeries),
                                typeof(Tokens.Temporal.TokenExactTime),
                                typeof(Tokens.Temporal.TokenIndeterminateSeries),
                                typeof(Tokens.Temporal.TokenTemporal),
                                typeof(Tokens.Temporal.TemporalParts.TokenDayOfWeek),
                                typeof(Tokens.Temporal.TemporalParts.TokenApril),
                                typeof(Tokens.Temporal.TemporalParts.TokenAugust),
                                typeof(Tokens.Temporal.TemporalParts.TokenDayAfterTomorrow),
                                typeof(Tokens.Temporal.TemporalParts.TokenDayBeforeYesterday),
                                typeof(Tokens.Temporal.TemporalParts.TokenDecember),
                                typeof(Tokens.Temporal.TemporalParts.TokenEach),
                                typeof(Tokens.Temporal.TemporalParts.TokenEighteenth),
                                typeof(Tokens.Temporal.TemporalParts.TokenEighth),
                                typeof(Tokens.Temporal.TemporalParts.TokenEleventh),
                                typeof(Tokens.Temporal.TemporalParts.TokenFebruary),
                                typeof(Tokens.Temporal.TemporalParts.TokenFifteenth),
                                typeof(Tokens.Temporal.TemporalParts.TokenFifth),
                                typeof(Tokens.Temporal.TemporalParts.TokenFirst),
                                typeof(Tokens.Temporal.TemporalParts.TokenForteenth),
                                typeof(Tokens.Temporal.TemporalParts.TokenForth),
                                typeof(Tokens.Temporal.TemporalParts.TokenFriday),
                                typeof(Tokens.Temporal.TemporalParts.TokenInt),
                                typeof(Tokens.Temporal.TemporalParts.TokenJanuary),
                                typeof(Tokens.Temporal.TemporalParts.TokenJuly),
                                typeof(Tokens.Temporal.TemporalParts.TokenJune),
                                typeof(Tokens.Temporal.TemporalParts.TokenLong),
                                typeof(Tokens.Temporal.TemporalParts.TokenMarch),
                                typeof(Tokens.Temporal.TemporalParts.TokenMay),
                                typeof(Tokens.Temporal.TemporalParts.TokenMonday),
                                typeof(Tokens.Temporal.TemporalParts.TokenMonth),
                                typeof(Tokens.Temporal.TemporalParts.TokenNinteenth),
                                typeof(Tokens.Temporal.TemporalParts.TokenNinth),
                                typeof(Tokens.Temporal.TemporalParts.TokenNovember),
                                typeof(Tokens.Temporal.TemporalParts.TokenNumeric),
                                typeof(Tokens.Temporal.TemporalParts.TokenOctober),
                                typeof(Tokens.Temporal.TemporalParts.TokenOrdinal),
                                typeof(Tokens.Temporal.TemporalParts.TokenOther),
                                typeof(Tokens.Temporal.TemporalParts.TokenPercentage),
                                typeof(Tokens.Temporal.TemporalParts.TokenRelativeTemporalOrdinal),
                                typeof(Tokens.Temporal.TemporalParts.TokenSaturday),
                                typeof(Tokens.Temporal.TemporalParts.TokenSecond),
                                typeof(Tokens.Temporal.TemporalParts.TokenSeptember),
                                typeof(Tokens.Temporal.TemporalParts.TokenSeventeenth),
                                typeof(Tokens.Temporal.TemporalParts.TokenSeventh),
                                typeof(Tokens.Temporal.TemporalParts.TokenSixteenth),
                                typeof(Tokens.Temporal.TemporalParts.TokenSixth),
                                typeof(Tokens.Temporal.TemporalParts.TokenSpecifiedDate),
                                typeof(Tokens.Temporal.TemporalParts.TokenSunday),
                                typeof(Tokens.Temporal.TemporalParts.TokenTenth),
                                typeof(Tokens.Temporal.TemporalParts.TokenThird),
                                typeof(Tokens.Temporal.TemporalParts.TokenThirteenth),
                                typeof(Tokens.Temporal.TemporalParts.TokenThirtieth),
                                typeof(Tokens.Temporal.TemporalParts.TokenThirtyFirst),
                                typeof(Tokens.Temporal.TemporalParts.TokenThursday),
                                typeof(Tokens.Temporal.TemporalParts.TokenTime),
                                typeof(Tokens.Temporal.TemporalParts.TokenToday),
                                typeof(Tokens.Temporal.TemporalParts.TokenTomorrow),
                                typeof(Tokens.Temporal.TemporalParts.TokenTuesday),
                                typeof(Tokens.Temporal.TemporalParts.TokenTwelth),
                                typeof(Tokens.Temporal.TemporalParts.TokenTwentieth),
                                typeof(Tokens.Temporal.TemporalParts.TokenTwentyEighth),
                                typeof(Tokens.Temporal.TemporalParts.TokenTwentyFifth),
                                typeof(Tokens.Temporal.TemporalParts.TokenTwentyFirst),
                                typeof(Tokens.Temporal.TemporalParts.TokenTwentyFourth),
                                typeof(Tokens.Temporal.TemporalParts.TokenTwentyNinth),
                                typeof(Tokens.Temporal.TemporalParts.TokenTwentySecond),
                                typeof(Tokens.Temporal.TemporalParts.TokenTwentySeventh),
                                typeof(Tokens.Temporal.TemporalParts.TokenTwentySixth),
                                typeof(Tokens.Temporal.TemporalParts.TokenTwentyThird),
                                typeof(Tokens.Temporal.TemporalParts.TokenWednesday),
                                typeof(Tokens.Temporal.TemporalParts.TokenYesterday),
                                typeof(Tokens.Verbs.TokenCreate),
                                typeof(Tokens.Verbs.TokenDelete),
                                typeof(Tokens.Verbs.TokenRemind),
                                typeof(Tokens.Verbs.TokenReset),
                                typeof(Tokens.Verbs.TokenWhatIs),
                                typeof(Tokens.Verbs.TokenWhereIs),
                                typeof(Tokens.Verbs.TokenWhoIs),
                                typeof(Tokens.Verbs.TokenWhoWasIn),
                                typeof(Tokens.Verbs.TokenRemindMeTo),
                                typeof(Tokens.Verbs.TokenRemindMeAt),
                                typeof(System.Type),
                                typeof(Questions.Question),
                                typeof(Tokens.Verbs.TokenTurn)
                            };
        }
    }
}
