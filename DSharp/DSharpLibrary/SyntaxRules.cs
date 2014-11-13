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
            SentenceCycle, //1
            SentenceComment, //2
            SentenceType, //3
            SentenceEOF, //4
            TypeInt, //5. int ID = [0-9]+ Terminador
            TypeString, //6. string ID = "[.*]" Terminador
            TypeDouble, //7. double ID = [0-9]*(\.[0.9]*)?Terminador
            OperatorSum, //8. +
            OperatorRest, //9. -
            OperatorMult, //10. *
            OperatorDiv, //11. (/)
            CompareBiggerThan, //12. >
            CompareLessThan, //13. <
            CompareAnd, //14. &
            CompareNot, //15. !
            CompareOr, //16. |
            CompareEqual, //17. ==
            CompareLessOrEqualThan, //18. <=
            CompareBiggerOrEqualThan, //19. >=
            ConditionIf, //20. if(expresion comparador expresion){ sent } subcond
            //                         if LeftParen ID [Comparador] INT/numero RightParen LeftBracket int ID = INT/numero Terminador RightBracket
            //       Caso <            (6 25 5 14 1 26 27 5 5 18 1 24 28)
            //       Caso >            (6 25 5 13 1 26 27 5 5 18 1 24 28)
            //       Caso ==
            //       Caso <=           (6 25 5 19 1 26 27 5 5 18 1 24 28)
            //       Caso >=           (6 25 5 20 1 26 27 5 5 18 1 24 28)
            SubCondE, //21. Cualquiera
            SubCondElse, //22. else{sent}
            SubCondElseIf, //23. elseif(expresion comparador expresion) { sent } subcond
            CycleWhile, //24. while(expresion comparador expresion) {sent}
            CycleFor, //25. for(int asignacion ; ID comparador expresion ; ID++){sent}
            AsignationInt, //26. ID=[0-9]+
            AsignationString, //27. ID = ".*"
            AsignationDouble, //28. ID = [0-9]* (\.[0.9]*)?
            Comment, //29. # sentence NewLine
            CommentLong, //30. StartComment Sentence EndComment
            StartComment, //31. $@
            EndComment, //32. @$
            Terminator, //33. ;
            Space, //34. [/s]
            NewLine, //35. [/n]
            Tab, //36. [/t]
            EOF, //37. EOF
            ID, //38. [a-z A-Z] [a-z A-Z 0-9]*
            Error, //39
            TypeIntError, //40
            ErrorIf, //41
            SentenceError, //42
            ErrorElse, //43
            ErrorElseIf, //44
            ErrorWhile, //45
            ErrorCommentLong //46
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
                return string.Format("{0} {1}", _typeNumber, _type.ToString());
            }
    }
}
