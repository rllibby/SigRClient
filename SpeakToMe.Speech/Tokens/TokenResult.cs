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
                                typeof(Tokens.Queries.TokenRequest),
                                typeof(Tokens.Queries.TokenQueryPhoneNo),
                                typeof(Tokens.Queries.TokenQueryQuantity),
                                typeof(Tokens.Queries.TokenQueryBalance),
                                typeof(Tokens.Queries.TokenDoes),
                                typeof(Tokens.Queries.TokenOwe)
                            };
        }
    }
}
