using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DSharpLibrary
{
    public class Machine
    {
        private List<State> _states = new List<State>();
        private List<Token> _tokens = new List<Token>();
        private string _tmp = string.Empty;
        private int _tokenType = 0;

        public List<Token> Tokens { get { return _tokens; } }

        public Machine()
        {
            SetStates();
        }
        int inputIndex = 0, currentState = 0;
        private bool success = false;
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
                        _tokens.Add(new Token(_tmp, _tokenType));
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
                            _tokens.Add(new Token(_tmp, _tokenType));
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
                    return _tokenType = 1;
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
                else if (validIdent.Contains(value) && numbers.Contains(value) == false)
                {
                    _tokenType = 5;
                    return 8;
                }
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
                    _tokenType = 2;
                    return 3;
                }
                else if (x[y] == '.')
                {
                    string number = _tmp.Substring(0, _tmp.Length - 1);
                    _tokens.Add(new Token(number, 1));
                    _tokens.Add(new Token("..", 4));
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
                    _tokenType = 3;
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
                    _tokenType = 4;
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
                _tokenType = 9;
                return 0;
            }));

            //10 -
            _states.Add(new State((x, y) =>
            {
                _tokenType = 10;
                return 0;
            }));

            //11 *
            _states.Add(new State((x, y) =>
            {
                _tokenType = 11;
                return 0;
            }));

            //12 /
            _states.Add(new State((x, y) =>
            {
                _tokenType = 12;
                return 0;
            }));
            

            //13 &
            _states.Add(new State((x, y) =>
            {
                _tokenType = 15;
                return 0;
            }));

            // 14 !
            _states.Add(new State((x, y) =>
            {
                _tokenType = 16;
                return 0;
            }));
            // 15 |
            _states.Add(new State((x, y) =>
            {
                _tokenType = 17;
                return 0;
            }));

            // 16 =
            _states.Add(new State((x, y) =>
            {
                _tokenType = 18;
                return 0;
            }));

            // 17 ;
            _states.Add(new State((x, y) =>
            {
                _tokenType = 24;
                return 5;
            }));

            // 18 (
            _states.Add(new State((x, y) =>
            {
                _tokenType = 25;
                return 0;
            }));

            // 19 )
            _states.Add(new State((x, y) =>
            {
                _tokenType = 26;
                return 5;
            }));

            // 20 {
            _states.Add(new State((x, y) =>
            {
                _tokenType = 27;
                return 0;
            }));

            // 21 }
            _states.Add(new State((x, y) =>
            {
                _tokenType = 28;
                return 5;
            }));
            // 22 >=, >
            _states.Add(new State((x, y) =>
            {
                if (x[y] == '=')
                {
                    _tokenType = 20;
                    return 5;
                }

                else
                {
                    _tokenType = 13;
                    return 0;
                }

            }));

            // 23 <=, <
            _states.Add(new State((x, y) =>
            {
                if (x[y] == '=')
                {
                    _tokenType = 19;
                    return 5;
                }
                else
                {
                    _tokenType = 14;
                    return 0;
                }
            }));

            // 24 Comentarios #
            _states.Add(new State((x, y) =>
            {
                    _tokenType = 21;
                    return 0;
            }));

            // 25 MoneyCommentStart $@
            _states.Add(new State((x, y) =>
            {
                if (x[y] == '@')
                {
                    _tokenType = 22;
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
                    _tokenType = 23;
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
                _tokenType = 7;
                return 5;
            }));

            // 28 Elseif
            _states.Add(new State((x, y) =>
            {
                _tokenType = 3;
                return 5;
            }));

            // 29 While
            _states.Add(new State((x, y) =>
            {
                _tokenType = 9;
                return 4;
            }));

            // 30 If
            _states.Add(new State((x, y) =>
            {
                _tokenType = 6;
                return 5;
            }));

            // 31 Else
            _states.Add(new State((x, y) =>
            {
                _tokenType = 8;
                return 5;
            }));
        }
    }
}
