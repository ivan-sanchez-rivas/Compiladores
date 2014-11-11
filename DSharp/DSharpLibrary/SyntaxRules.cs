using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSharpLibrary
{
    public class SyntaxRules
    {
        public enum GrammarRules
        {
            SentenceCond = 0,
            SentenceCycle,
            SentenceComment,
            SentenceType,
            SentenceEOF,
            TypeInt, //int ID = [0-9]+ Terminador
            TypeString, // string ID = "[.*]" Terminador
            TypeDouble, // double ID = [0-9]*(\.[0.9]*)?Terminador
            OperatorSum, // +
            OperatorRest, // -
            OperatorMult, // *
            OperatorDiv, // (/)
            CompareBiggerThan, // >
            CompareLessThan, // <
            CompareAnd, // &
            CompareNot, // !
            CompareOr, // |
            CompareEqual, // ==
            CompareLessOrEqualThan, // <=
            CompareBiggerOrEqualThan, // >=
            ConditionIf, // if(expresion comparador expresion){ sent } subcond
            SubCondE, // Cualquiera
            SubCondElse, // else{sent}
            SubCondElseIf, //elseif(expresion comparador expresion) { sent } subcond
            CycleWhile, // while(expresion comparador expresion) {sent}
            CycleFor, //for(int asignacion ; ID comparador expresion ; ID++){sent}
            AsignationInt, // ID=[0-9]+
            AsignationString, //ID = ".*"
            AsignationDouble, //ID = [0-9]* (\.[0.9]*)?
            Comment, // # sentence NewLine
            CommentLong, // StartComment Sentence EndComment
            StartComment, // $@
            EndComment, //@$
            Terminator, // ;
            Space, // [/s]
            NewLine, // [/n]
            Tab, // [/t]
            EOF, //EOF
            ID, // [a-z A-Z] [a-z A-Z 0-9]*
        }
            private GrammarRules _type;
            private string _value;

            public GrammarRules Type { get { return _type; } }
            public string Value { get { return _value; } }

            public SyntaxRules(string value, int type)
            {
                _value = value;
                _type = (GrammarRules)type;
            }

            public override string ToString()
            {
                var _typeNumber = (int)((GrammarRules)Enum.Parse(typeof(GrammarRules), _type.ToString()));
                return string.Format("{0} -> {1} {2}", _value, _typeNumber, _type.ToString());
            }
    }
}
