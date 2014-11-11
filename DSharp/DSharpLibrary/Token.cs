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
        Int,//1
        Double,//2
        Elseif,//3
        While,//4
        ID,//5
        If,//6
        For,//7
        Else,//8
        Addition,//9
        Substract,//10
        Multiplication, //11
        Division,//12
        BiggerThan, //13
        LessThan, //14
        And, //15
        Not, //16
        Or, //17
        Equal, //18
        LessOrEqualThan,//19
        BiggerOrEqualThan,//20
        Comment, //21
        MoneyCommentStart,//22
        MoneyCommentEnd,//23
        Terminator,//24
        LeftParenthesis, //25
        RightParenthesis,//26
        LeftBracket,//27
        RightBracket,//28
        Space,//29
        WhiteSpace,//30
        NewLine,//31
        Tab //32
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
