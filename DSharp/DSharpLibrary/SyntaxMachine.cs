using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSharpLibrary
{
    public class SyntaxMachine
    {
        private List<SyntaxState> _states = new List<SyntaxState>();
        private List<SyntaxRules> _rules = new List<SyntaxRules>();
        private string _tmp = string.Empty;
        private List<int> _tokenNumber = new List<int>();
        List<List<int>> arrayList = new List<List<int>>();
        private int _ruleType = 0;
        public List<SyntaxRules> Rules { get { return _rules; } }
        public SyntaxMachine()
        {
            SetStates();
        }
        int inputIndex = 0, currentState = 0;
        private bool success = false;
        public List<SyntaxRules> TokenStrip(List<Token> tokenList)
        {
            var syntax = new SyntaxMachine();
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

            syntax.Operate(arrayList);
            
            return syntax.Rules;
            //arrayList.Add(array);
            //var result = string.Join(",", _tokenNumber.Select(x => x.ToString()).ToArray());
        }
        public int Operate(List<List<int>> input)
        {
            //input.Trim();
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
                        _rules.Add(new SyntaxRules(_tmp, _ruleType));
                        _tmp = string.Empty;
                    }

                }
                else
                {
                    _tmp += input[inputIndex];
                    inputIndex++;

                }

                if (inputIndex == input.Count) // EOF
                {
                    if (currentState == 5 || currentState == 7)
                        return 0;
                    else
                    {
                        if (_tmp != string.Empty)
                            _rules.Add(new SyntaxRules(_tmp, _ruleType));
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
            // 0
            _states.Add(new SyntaxState((x, y) =>
            {
                var value = x[y];
                var result = string.Join("", value.Select(z => z.ToString()).ToArray());
                ////5 5 18 1 24
                if (result == "5518124")
                    return _ruleType = 5;
                else
                    return -2;
            }));

            // 1
            _states.Add(new SyntaxState((x, y) =>
            {
                return 1;
            }));

        }
    }
}
