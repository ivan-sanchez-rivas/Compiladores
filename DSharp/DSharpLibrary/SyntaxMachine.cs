using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSharpLibrary
{
    public class SyntaxMachine
    {
        private List<State> _states = new List<State>();
        private List<SyntaxRules> _rules = new List<SyntaxRules>();
        private string _tmp = string.Empty;
        private List<int> _tokenNumber = new List<int>();
        List<List<int>> arrayList = new List<List<int>>();
        private int _ruleType = 0;
         public SyntaxMachine()
        {
            SetStates();
        }
        int inputIndex = 0, currentState = 0;
        private bool success = false;
        public void TokenStrip(List<Token> tokenList)
        {

            foreach (Token t in tokenList)
            {
                var num = (int)((TokenType)Enum.Parse(typeof(TokenType), t.Type.ToString()));
                _tokenNumber.Add(num);
            }
            int[] array = _tokenNumber.ToArray();
            arrayList.Add(new List<int>());
            int index = 0;
            foreach (var item in array)
            {
                arrayList[index].Add(item);
                if (item == 24)
                {
                    arrayList.Add(new List<int>());
                    index++;
                }
            }
            //arrayList.Add(array);
            //var result = string.Join(",", _tokenNumber.Select(x => x.ToString()).ToArray());
        }
        public int Operate(string input)
        {
            input.Trim();
            //while ((currentState = _states[currentState].Operate(input, inputIndex)) != -1)
            if ((currentState = _states[currentState].Operate(input, inputIndex)) != -1)
            {
                if (currentState == -2) // whitespace
                {
                    inputIndex++;
                    currentState = 0;
                    //continue;
                }

                if (currentState == 0 || currentState == 5 || currentState == 7 || currentState == 17 || currentState == 19 || currentState == 21)
                {
                    if (currentState == 5 || currentState == 7)
                    {
                        _tmp += input[inputIndex];
                        inputIndex++;
                        currentState = 0;
                    }
                    if (_tmp != string.Empty)
                    {
                        _rules.Add(new Token(_tmp, _ruleType));
                        _tmp = string.Empty;
                    }

                }
                else
                {
                    _tmp += input[inputIndex];
                    inputIndex++;

                }

                if (inputIndex == input.Length) // EOF
                {
                    if (currentState == 5 || currentState == 7)
                        return 0;
                    else
                    {
                        if (_tmp != string.Empty)
                            _rules.Add(new Token(_tmp, _ruleType));
                        success = true;
                        return 1;
                    }
                }
                Operate(input);
            }
            if (success)
                return 1;
            return 0;
        }

        private void SetStates()
        {
            string numbers = "0123456789";   
            string invalid = "\n\t";
            string validIdent = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ" + numbers;

            // 0
            _states.Add(new State((x, y) =>
            {
                char value = x[y];

                if (numbers.Contains(value))
                    return _ruleType = 1;
                else if (value == ':')
                    return 4;
                else if (value == '.')
                    return 6;
                else if (value == '+')
                    return 9;
                else if (value == '-')
                    return 10;
                else if (value == '*')
                    return 11;
                else if (value == '/')
                    return 12;
                else if (value == '&')
                    return 13;
                else if (value == '!')
                    return 14;
                else if (value == '|')
                    return 15;
                else if (value == '=')
                    return 16;
                else if (value == ';')
                    return 17;
                else if (value == '(')
                    return 18;
                else if (value == ')')
                    return 19;
                else if (value == '{')
                    return 20;
                else if (value == '}')
                    return 21;
                else if (value == '>')
                    return 22;
                else if (value == '<')
                    return 23;
                else if (value == '#')
                    return 24;
                else if (value == '$')
                    return 25;
                else if (value == '@')
                    return 26;
                else if (value == 'f')
                {
                    _ruleType = 5;
                    return 27;
                }
                else if (value == 'e')
                {
                    _ruleType = 5;
                    return 28;
                }
                else if (value == 'w')
                {
                    _ruleType = 5;
                    return 29;
                }
                else if (value == 'i')
                {
                    _ruleType = 5;
                    return 30;
                }

                else if (validIdent.Contains(value) && numbers.Contains(value) == false)
                {
                    _ruleType = 5;
                    return 8;
                }
                else if (value == '\t')
                    return -2;
                else if (value == ' ')
                    return -2;
                return -1; // Error
            }));

            // 1
            _states.Add(new State((x, y) =>
            {
                if (numbers.Contains(x[y]))
                    return 1;
                else if (x[y] == '.')
                    return 2;
                else
                    return 0;
            }));

            // 2
            _states.Add(new State((x, y) =>
            {
                if (numbers.Contains(x[y]))
                {
                    _ruleType = 2;
                    return 3;
                }
                else if (x[y] == '.')
                {
                    string number = _tmp.Substring(0, _tmp.Length - 1);
                    _rules.Add(new Token(number, 1));
                    _rules.Add(new Token("..", 4));
                    _tmp = string.Empty;
                    return -2;
                }
                return -1;
            }));

            // 3
            _states.Add(new State((x, y) =>
            {
                if (numbers.Contains(x[y]))
                    return 3;
                else
                    return 0;
            }));

            // 4
            _states.Add(new State((x, y) =>
            {
                if (x[y] == '=')
                {
                    _ruleType = 3;
                    return 5;
                }
                else
                    return -1;
            }));

            // 5
            _states.Add(null);

            // 6
            _states.Add(new State((x, y) =>
            {
                if (x[y] == '.')
                {
                    _ruleType = 4;
                    return 7;
                }
                else
                    return -1;
            }));

            // 7
            _states.Add(null);

            // 8
            _states.Add(new State((x, y) =>
            {
                if (validIdent.Contains(x[y]))
                    return 8;
                else
                    return 0;
            }));

            //9 +
            _states.Add(new State((x, y) =>
            {
                _ruleType = 9;
                return 0;
            }));

            //10 -
            _states.Add(new State((x, y) =>
            {
                _ruleType = 10;
                return 0;
            }));

            //11 *
            _states.Add(new State((x, y) =>
            {
                _ruleType = 11;
                return 0;
            }));

            //12 /
            _states.Add(new State((x, y) =>
            {
                _ruleType = 12;
                return 0;
            }));
            

            //13 &
            _states.Add(new State((x, y) =>
            {
                _ruleType = 15;
                return 0;
            }));

            // 14 !
            _states.Add(new State((x, y) =>
            {
                _ruleType = 16;
                return 0;
            }));
            // 15 |
            _states.Add(new State((x, y) =>
            {
                _ruleType = 17;
                return 0;
            }));

            // 16 =
            _states.Add(new State((x, y) =>
            {
                _ruleType = 18;
                return 0;
            }));

            // 17 ;
            _states.Add(new State((x, y) =>
            {
                _ruleType = 24;
                return 5;
            }));

            // 18 (
            _states.Add(new State((x, y) =>
            {
                _ruleType = 25;
                return 0;
            }));

            // 19 )
            _states.Add(new State((x, y) =>
            {
                _ruleType = 26;
                return 5;
            }));

            // 20 {
            _states.Add(new State((x, y) =>
            {
                _ruleType = 27;
                return 0;
            }));

            // 21 }
            _states.Add(new State((x, y) =>
            {
                _ruleType = 28;
                return 5;
            }));
            // 22 >=, >
            _states.Add(new State((x, y) =>
            {
                if (x[y] == '=')
                {
                    _ruleType = 20;
                    return 5;
                }

                else
                {
                    _ruleType = 13;
                    return 0;
                }

            }));

            // 23 <=, <
            _states.Add(new State((x, y) =>
            {
                if (x[y] == '=')
                {
                    _ruleType = 19;
                    return 5;
                }
                else
                {
                    _ruleType = 14;
                    return 0;
                }
            }));

            // 24 Comentarios #
            _states.Add(new State((x, y) =>
            {
                    _ruleType = 21;
                    return 0;
            }));

            // 25 MoneyCommentStart $@
            _states.Add(new State((x, y) =>
            {
                if (x[y] == '@')
                {
                    _ruleType = 22;
                    return 5;
                }
                else
                {
                    return -1;
                }
            }));

            // 26 MoneyCommentEnd @$
            _states.Add(new State((x, y) =>
            {
                if (x[y] == '$')
                {
                    _ruleType = 23;
                    return 5;
                }
                else
                {
                    return -1;
                }
            }));

            // 27 For
            _states.Add(new State((x, y) =>
            {
                if (x[y] == 'o')
                {
                    _ruleType = 5;
                    return 27;
                }
                if (x[y] == 'r')
                {
                    _ruleType = 7;
                    return 27;

                }
                else if (validIdent.Contains(x[y]))
                {
                    _ruleType = 5;
                    return 27;
                }
                return 0;
                //_tokenType = 7;
                //return 5;


            }));

            // 28 Elseif
            _states.Add(new State((x, y) =>
            {
                if (x[y] == 'l')
                {
                    _ruleType = 5;
                    return 28;
                }
                else if (x[y] == 's')
                {
                    _ruleType = 5;
                    return 28;

                }
                else if (x[y] == 'e')
                {
                    _ruleType = 8;
                    return 28;

                }
                else if (x[y] == 'i')
                {
                    _ruleType = 5;
                    return 28;

                }
                else if (x[y] == 'f')
                {
                    _ruleType = 3;
                    return 28;

                }
                else if (validIdent.Contains(x[y]))
                {
                    _ruleType = 5;
                    return 28;
                }
                return 0;
                //_tokenType = 3;
                //return 5;
            }));

            // 29 While
            _states.Add(new State((x, y) =>
            {
                if (x[y] == 'h')
                {
                    _ruleType = 5;
                    return 29;
                }
                else if (x[y] == 'i')
                {
                    _ruleType = 5;
                    return 29;

                }
                else if (x[y] == 'l')
                {
                    _ruleType = 5;
                    return 29;

                }
                else if (x[y] == 'e')
                {
                    _ruleType = 4;
                    return 29;

                }
                else if (validIdent.Contains(x[y]))
                {
                    _ruleType = 5;
                    return 29;
                }
                return 0;
                //_tokenType = 4;
                //return 5;
            }));

            // 30 If
            _states.Add(new State((x, y) =>
            {
                if (x[y] == 'f')
                {
                    _ruleType = 6;
                    return 30;
                }
                else if (validIdent.Contains(x[y]))
                {
                    _ruleType = 5;
                    return 30;
                }
                return 0;
                //_tokenType = 6;
                //return 5;
            }));
        }
    }
}
