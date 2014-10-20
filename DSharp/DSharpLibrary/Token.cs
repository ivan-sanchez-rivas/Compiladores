using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSharpLibrary
{
    public enum TokenType
    {
        Error = 0,
        Int,//
        Double,//
        Elseif,//
        While,//
        ID,//
        If,//
        For,//
        Else,//
        Addition,//
        Substract,//
        Multiplication, //
        Division,//
        BiggerThan, //
        LessThan, //
        And, //
        Not, //
        Or, //
        Equal, //
        LessOrEqualThan,//
        BiggerOrEqualThan,//
        Comment, //
        MoneyCommentStart,//
        MoneyCommentEnd,//
        Terminator,//
        LeftParenthesis, //
        RightParenthesis,
        LeftBracket,//
        RightBracket,//
        Space,//
        WhiteSpace,//
        NewLine,
        Tab //
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
            var _typeNumber = (int)((TokenType)Enum.Parse(typeof(TokenType), _type.ToString()));
            return string.Format("{0} -> {1} {2}", _value, _typeNumber, _type.ToString());
        }
    }
}
