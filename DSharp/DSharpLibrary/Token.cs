using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSharpLibrary
{
    public enum TokenType
    {
        Number = 1,
        RealNumber,
        Assign,
        Range,
        ID
    }

    public class Token
    {
        private TokenType _type;
        private string _value;

        public TokenType Type { get { return _type; } }
        public string Value { get { return _value; } }

        public Token(string value, int type)
        {
            _value = value;
            _type = (TokenType)type;
        }

        public override string ToString()
        {
            return string.Format("{0} -> {1}", _value, _type.ToString());
        }
    }
}
