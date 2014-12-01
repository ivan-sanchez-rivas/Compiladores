using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DSharpLibrary
{
    public class SyntaxMachine
    {
        private List<SyntaxState> _states = new List<SyntaxState>();
        private List<SyntaxRules> _rules = new List<SyntaxRules>();
        private List<Variables> _variables = new List<Variables>(); 
        public List<string> _tokenValue = new List<string>();
        public List<string> tokenValues = new List<string>();
        private List<int> _tokenNumber = new List<int>();
        public List<List<int>> arrayList = new List<List<int>>();
        public List<SyntaxRules> Rules { get { return _rules; } }
        private string _tmp = string.Empty;
        private int _ruleType = 0;
        int operateCounter = 0;
        
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
                var val = t.Value;
                _tokenValue.Add(val);
            }

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
                //if (item == 5) // INT
                //{
                //    arrayList.Add(new List<int>());
                //    index++;
                //}
                if (item == 1) // ;
                {
                    arrayList.Add(new List<int>());
                    index++;
                }
                if (item == 24) // ;
                {
                    arrayList.Add(new List<int>());
                    index++;
                }
                if (item == 34) // decimal
                {
                    arrayList.Add(new List<int>());
                    index++;
                }
                else if (item == 9) //+
                {
                    arrayList.Add(new List<int>());
                    index++;
                }
                else if (item == 10) //-
                {
                    arrayList.Add(new List<int>());
                    index++;
                }
                else if (item == 11) //*
                {
                    arrayList.Add(new List<int>());
                    index++;
                }
                else if (item == 12) // (/)
                {
                    arrayList.Add(new List<int>());
                    index++;
                }
                else if (item == 15) // AND
                {
                    arrayList.Add(new List<int>());
                    index++;
                }
                else if (item == 16) // NOT
                {
                    arrayList.Add(new List<int>());
                    index++;
                }
                else if (item == 17) // OR
                {
                    arrayList.Add(new List<int>());
                    index++;
                }
                else if (item == 26) // )
                {
                    arrayList.Add(new List<int>());
                    index++;
                }
                else if (item == 27) // {
                {
                    arrayList.Add(new List<int>());
                    index++;
                }
                else if (item == 28) // }
                {
                    arrayList.Add(new List<int>());
                    index++;
                }
                else if(item == 6) //if(
                {
                    arrayList.Add(new List<int>());
                    index++;
                }
                else if (item == 3) //elseif(
                {
                    arrayList.Add(new List<int>());
                    index++;
                }
                else if (item == 4) //while(
                {
                    arrayList.Add(new List<int>());
                    index++;
                }
                else if (item == 22) //$@
                {
                    arrayList.Add(new List<int>());
                    index++;
                }
                if (item == 23) //$@
                {
                    arrayList.Add(new List<int>());
                    index++;
                }
            }
            syntax.Operate(arrayList,_tokenValue);

            return syntax.Rules;
            //arrayList.Add(array);
            //var result = string.Join(",", _tokenNumber.Select(x => x.ToString()).ToArray());
        }
        public int Operate(List<List<int>> input, List<string> _value)
        {
            operateCounter++;
            if (operateCounter == 1)
            {
                foreach (string token in _value)
                {
                    tokenValues.Add(token);
                }
            }

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
                if (currentState == -3)
                {
                    currentState = 0;
                    inputIndex--;
                }
                if (currentState == -4)
                {
                    inputIndex++;
                    _rules.Add(new SyntaxRules(_tmp, _ruleType));
                    currentState = 0;
                    
                }
                if (currentState == 0||currentState == 17 || currentState == 19 || currentState == 21)
                {
                    //if (currentState == 7)
                    //{
                    //    _tmp += input[inputIndex];
                    //    inputIndex++;
                    //    currentState = 0;
                    //}
                    if (_tmp != string.Empty)
                    {
                        _rules.Add(new SyntaxRules(_tmp, _ruleType));
                        _tmp = string.Empty;
                        inputIndex++;

                    }


                }
                else
                {
                    inputIndex++;
                    _tmp += input[inputIndex];
                }

                if (inputIndex == input.Count) // EOF
                {
                    if (/*currentState == 2 || */currentState == 7)
                        return 0;
                    else
                    {
                        if (_tmp != string.Empty)
                            _rules.Add(new SyntaxRules(_tmp, _ruleType));
                        success = true;
                        return 1;
                    }
                }
                Operate(input,_value);
            }
            else
            {
                _rules.Add(new SyntaxRules(_tmp, _ruleType));
            }
            if (success)
                return 1;
            return 0;
        }

        private void SetStates()
        {
            // 0 
           
            _states.Add(new SyntaxState((x,y) =>
            {
                if (x[y].Count == 0)
                {
                    return -2;
                }
                else
                {
                    var value = x[y];
                    var result = string.Join(" ", value.Select(z => z.ToString()).ToArray());
                    if (result == "5 5 18 1") //int ID = [0-9]+ Terminador

                        return 1;
                    else if (result == "5 5 18 5" || result == "5 5 18 5 24")
                    {
                        _ruleType = 40;
                        return -4;
                    }
                    if (result == "2 5 18 1" || result == "2 5 18 34") // double ID = [0-9].[0-9] TERMINADOR
                        return 8;
                    //
                    if (result == "5 9") //ID +
                        return 2;
                    else if (result == "5 5")
                    {
                        _ruleType = 42;
                        return -4;
                    }
                    if (result == "5 10") //ID -
                        return 2;
                    else if (result == "5 5")
                    {
                        _ruleType = 42;
                        return -4;
                    }
                    if (result == "5 11") //ID *
                        return 2;
                    else if (result == "5 5")
                    {
                        _ruleType = 42;
                        return -4;
                    }
                    if (result == "5 12") //ID /
                        return 2;
                    else if (result == "5 5")
                    {
                        _ruleType = 42;
                        return -4;
                    }
                    if (result == "1" || result == "34") //number + OR number decimal
                        return 2;
                    else if (result == "1 5")
                    {
                        _ruleType = 42;
                        return -4;
                    }
                    if (result == "6") //if
                        return 3;
                    if (result == "8 27") //else
                        return 4;
                    else if (result == "8 27") //else{ error
                    {
                        _ruleType = 43;
                        return -4;
                    }
                    else if (result == "8")
                    {
                        _ruleType = 43;
                        return -4;
                    }
                    else if (result == "8 28") //else} error
                    {
                        _ruleType = 43;
                        return -4;
                    }
                    if (result == "3") //elseif
                        return 5;
                    if (result == "4") //while
                        return 6;
                    if (result == "22") //CommentStart $@
                        return 7;
                    else
                    {
                        _ruleType = 39;
                        return -4;
                    }
                }
            }));
            // 1 TypeInt
            _states.Add(new SyntaxState((x, y) =>
            {
                var value = x[y];
                var result = string.Join(" ", value.Select(z => z.ToString()).ToArray());
                if (result == "24")
                {
                    int indexVariable = 0;
                    for (int i = 0; i < y; i++)
                    {
                        indexVariable += x[i].Count;
                    }
                    _ruleType = 26;
                    var variable = new Variables(tokenValues[indexVariable-3], "int", tokenValues[indexVariable-1]);
                    _variables.Add(variable);
                    return 0;
                }
                else
                {
                    _ruleType = 40;
                    return 0;
                }

            }));
            // 2 ID
            _states.Add(new SyntaxState((x, y) =>
            {
                var value = x[y];
                
                var result = string.Join(" ", value.Select(z => z.ToString()).ToArray());
                
                if (result == "5 9") // ID +
                    return 2;
                else if (result == "5 10") // ID -
                    return 2;
                else if (result == "5 11") // ID *
                    return 2;
                else if (result == "5 12") // ID (/)
                    return 2;
                else if (result == "1") // number /
                    return 2;
                else if (result == "34") // decimal
                    return 2;
                else if (result == "9") // number +
                    return 2;
                else if (result == "10") // number -
                    return 2;
                else if (result == "11") // number *
                    return 2;
                else if (result == "12") // number /
                    return 2;

                else if (result == "5 24") //ID ;
                {
                    _ruleType = 3;
                    return 0;
                }
                else if (result == "24") //number;
                {
                    _ruleType = 3;
                    return 0;
                }
                else
                {
                    _ruleType = 42;
                    return 0;
                }


            }));
            //

            // 3 IF CONDITION
            _states.Add(new SyntaxState((x, y) =>
            {
                var value = x[y];
                var result = string.Join(" ", value.Select(z => z.ToString()).ToArray());
                if (result == "25 5 13 1")  // > expresion
                    return 3;
                if (result == "25 5 14 1")  // < expresion
                    return 3;
                if (result == "25 5 19 1") // <= expresion
                    return 3;
                if (result == "25 5 20 1") // >= expresion
                    return 3;
                if (result == "25 5 13 5")  // > expresion
                    return 3;
                if (result == "25 5 14 5")  // < expresion
                    return 3;
                if (result == "25 5 19 5") // <= expresion
                    return 3;
                if (result == "25 5 20 5") // >= expresion
                    return 3;
                if (result == "25 5 13 1 26")  // > expresion
                    return 3;
                if (result == "25 5 14 1 26")  // < expresion
                    return 3;
                if (result == "25 5 19 1 26") // <= expresion
                    return 3;
                if (result == "25 5 20 1 26") // >= expresion
                    return 3;
                if (result == "25 5 13 5 26")  // > expresion
                    return 3;
                if (result == "25 5 14 5 26")  // < expresion
                    return 3;
                if (result == "25 5 19 5 26") // <= expresion
                    return 3;
                if (result == "25 5 20 5 26") // >= expresion
                    return 3;
                //ID y luego DOUBLE
                if (result == "25 5 13 34")  // > expresion
                    return 3;
                if (result == "25 5 14 34")  // < expresion
                    return 3;
                if (result == "25 5 19 34") // <= expresion
                    return 3;
                if (result == "25 5 20 34") // >= expresion
                    return 3;
                if (result == "25 5 13 34")  // > expresion
                    return 3;
                if (result == "25 5 14 34")  // < expresion
                    return 3;
                if (result == "25 5 19 34") // <= expresion
                    return 3;
                if (result == "25 5 20 34") // >= expresion
                    return 3;
                if (result == "25 34")
                    return 3;
                //MULTIOPERADORES
                if (result == "15") // AND
                    return 3;
                if (result == "16") //NOT
                    return 3;
                if (result == "17") //OR
                    return 3;
                if (result == "5 13 1")  // > expresion
                    return 3;
                if (result == "5 14 1")  // < expresion
                    return 3;
                if (result == "5 19 1") // <= expresion
                    return 3;
                if (result == "5 20 1") // >= expresion
                    return 3;
                if (result == "5 13 5")  // > expresion
                    return 3;
                if (result == "5 14 5")  // < expresion
                    return 3;
                if (result == "5 19 5") // <= expresion
                    return 3;
                if (result == "5 20 5") // >= expresion
                    return 3;
                if (result == "1")
                    return 3;
                //numero primero y despues variable
                if (result == "25 1")  // > expresion
                    return 3;
                if (result == "13 5 26")
                    return 3;
                if (result == "14 5 26")
                    return 3;
                if (result == "19 5 26")
                    return 3;
                if (result == "20 5 26")
                    return 3;

                // numero y numero 
                if (result == "13 1")
                    return 3;
                if (result == "14 1")
                    return 3;
                if (result == "19 1")
                    return 3;
                if (result == "20 1")
                    return 3;
                if (result == "25 1")
                    return 3;
                //numero y double
                if (result == "13 34")
                    return 3;
                if (result == "14 34")
                    return 3;
                if (result == "19 34")
                    return 3;
                if (result == "20 34")
                    return 3;
                if (result == "25 34")
                    return 3;
                //
                if (result == "26")
                    return 3;
                if (result == "27") // {
                    return 3;
                
                if (result == "28") // }
                {
                    _ruleType = 20;
                    return 0;
                }
                else
                {
                    _ruleType = 41;
                    return -3;
                }
            }));

            // 4 ELSE CONDITION
            _states.Add(new SyntaxState((x, y) =>
            {
                var value = x[y];
                var result = string.Join(" ", value.Select(z => z.ToString()).ToArray());
                //if (result == "27") // {
                //    return 4;
                if (result == "28") // }
                {
                    _ruleType = 22;
                    return 0;
                }
                else
                {
                    _ruleType = 43;
                    return -2;
                }
            }));

            // 5 ELSEIF CONDITION
            _states.Add(new SyntaxState((x, y) =>
            {
                var value = x[y];
                var result = string.Join(" ", value.Select(z => z.ToString()).ToArray());
                //if (result == "25 5")     // (expresion
                //    return 2;
                if (result == "25 5 13 1")  // > expresion
                    return 5;
                if (result == "25 5 14 1")  // < expresion
                    return 5;
                if (result == "25 5 19 1") // <= expresion
                    return 5;
                if (result == "25 5 20 1") // >= expresion
                    return 5;

                if (result == "25 5 13 5")  // > expresion
                    return 5;
                if (result == "25 5 14 5")  // < expresion
                    return 5;
                if (result == "25 5 19 5") // <= expresion
                    return 5;
                if (result == "25 5 20 5") // >= expresion
                    return 5;

                if (result == "25 5 13 1 26")  // > expresion
                    return 5;
                if (result == "25 5 14 1 26")  // < expresion
                    return 5;
                if (result == "25 5 19 1 26") // <= expresion
                    return 5;
                if (result == "25 5 20 1 26") // >= expresion
                    return 5;
                if (result == "25 5 13 5 26")  // > expresion
                    return 5;
                if (result == "25 5 14 5 26")  // < expresion
                    return 5;
                if (result == "25 5 19 5 26") // <= expresion
                    return 5;
                if (result == "25 5 20 5 26") // >= expresion
                    return 5;
                //ID y luego DOUBLE
                if (result == "25 5 13 34")  // > expresion
                    return 5;
                if (result == "25 5 14 34")  // < expresion
                    return 5;
                if (result == "25 5 19 34") // <= expresion
                    return 5;
                if (result == "25 5 20 34") // >= expresion
                    return 5;
                if (result == "25 5 13 34")  // > expresion
                    return 5;
                if (result == "25 5 14 34")  // < expresion
                    return 5;
                if (result == "25 5 19 34") // <= expresion
                    return 5;
                if (result == "25 5 20 34") // >= expresion
                    return 5;
                if (result == "25 34")
                    return 5;

                //MULTIOPERADORES
                if (result == "15") // AND
                    return 5;
                if (result == "16") //NOT
                    return 5;
                if (result == "17") //OR
                    return 5;
                if (result == "5 13 1")  // > expresion
                    return 5;
                if (result == "5 14 1")  // < expresion
                    return 5;
                if (result == "5 19 1") // <= expresion
                    return 5;
                if (result == "5 20 1") // >= expresion
                    return 5;
                if (result == "5 13 5")  // > expresion
                    return 5;
                if (result == "5 14 5")  // < expresion
                    return 5;
                if (result == "5 19 5") // <= expresion
                    return 5;
                if (result == "5 20 5") // >= expresion
                    return 5;
                if (result == "1")
                    return 5;

                //numero primero y despues variable
                if (result == "25 1")  // > expresion
                    return 5;
                if (result == "13 5 26")
                    return 5;
                if (result == "14 5 26")
                    return 5;
                if (result == "19 5 26")
                    return 5;
                if (result == "20 5 26")
                    return 5;
                // numero y numero 
                if (result == "13 1")
                    return 5;
                if (result == "14 1")
                    return 5;
                if (result == "19 1")
                    return 5;
                if (result == "20 1")
                    return 5;
                //numero y double
                if (result == "13 34")
                    return 5;
                if (result == "14 34")
                    return 5;
                if (result == "19 34")
                    return 5;
                if (result == "20 34")
                    return 5;
                if (result == "25 34")
                    return 5;
                //
                if (result == "26")
                    return 5;
                if (result == "27") // {
                    return 5;
                if (result == "28") // }
                {
                    _ruleType = 23;
                    return 0;
                }
                else
                {
                    _ruleType = 44;
                    return -3;
                }
            }));

            // 6 While CONDITION
            _states.Add(new SyntaxState((x, y) =>
            {
                var value = x[y];
                var result = string.Join(" ", value.Select(z => z.ToString()).ToArray());
                //if (result == "25 5")     // (expresion
                //    return 2;
                if (result == "25 5 13 1")  // > expresion
                    return 6;
                if (result == "25 5 14 1")  // < expresion
                    return 6;
                if (result == "25 5 19 1") // <= expresion
                    return 6;
                if (result == "25 5 20 1") // >= expresion
                    return 6;

                if (result == "25 5 13 5")  // > expresion
                    return 6;
                if (result == "25 5 14 5")  // < expresion
                    return 6;
                if (result == "25 5 19 5") // <= expresion
                    return 6;
                if (result == "25 5 20 5") // >= expresion
                    return 6;

                if (result == "25 5 13 1 26")  // > expresion
                    return 6;
                if (result == "25 5 14 1 26")  // < expresion
                    return 6;
                if (result == "25 5 19 1 26") // <= expresion
                    return 6;
                if (result == "25 5 20 1 26") // >= expresion
                    return 6;
                if (result == "25 5 13 5 26")  // > expresion
                    return 6;
                if (result == "25 5 14 5 26")  // < expresion
                    return 6;
                if (result == "25 5 19 5 26") // <= expresion
                    return 6;
                if (result == "25 5 20 5 26") // >= expresion
                    return 6;
                //ID y luego DOUBLE
                if (result == "25 5 13 34")  // > expresion
                    return 6;
                if (result == "25 5 14 34")  // < expresion
                    return 6;
                if (result == "25 5 19 34") // <= expresion
                    return 6;
                if (result == "25 5 20 34") // >= expresion
                    return 6;
                if (result == "25 5 13 34")  // > expresion
                    return 6;
                if (result == "25 5 14 34")  // < expresion
                    return 6;
                if (result == "25 5 19 34") // <= expresion
                    return 6;
                if (result == "25 5 20 34") // >= expresion
                    return 6;
                if (result == "25 34")
                    return 6;

                //MULTIOPERADORES
                if (result == "15") // AND
                    return 6;
                if (result == "16") //NOT
                    return 6;
                if (result == "17") //OR
                    return 6;
                if (result == "5 13 1")  // > expresion
                    return 6;
                if (result == "5 14 1")  // < expresion
                    return 6;
                if (result == "5 19 1") // <= expresion
                    return 6;
                if (result == "5 20 1") // >= expresion
                    return 6;
                if (result == "5 13 5")  // > expresion
                    return 6;
                if (result == "5 14 5")  // < expresion
                    return 6;
                if (result == "5 19 5") // <= expresion
                    return 6;
                if (result == "5 20 5") // >= expresion
                    return 6;
                if (result == "1")
                    return 6;

                //numero primero y despues variable
                if (result == "25 1")  // > expresion
                    return 6;
                if (result == "13 5 26")
                    return 6;
                if (result == "14 5 26")
                    return 6;
                if (result == "19 5 26")
                    return 6;
                if (result == "20 5 26")
                    return 6;
                // numero y numero 
                if (result == "13 1")
                    return 6;
                if (result == "14 1")
                    return 6;
                if (result == "19 1")
                    return 6;
                if (result == "20 1")
                    return 6;
                //numero y double
                if (result == "13 34")
                    return 5;
                if (result == "14 34")
                    return 5;
                if (result == "19 34")
                    return 5;
                if (result == "20 34")
                    return 5;
                if (result == "25 34")
                    return 5;
                //
                if (result == "26")
                    return 6;
                if (result == "27") // {
                    return 6;
                //else if (result == "5 5 18 1 24") //int ID = [0-9]+ Terminador
                //    return 2;
                if (result == "28") // }
                {
                    _ruleType = 24;
                    return 0;
                }
                else
                {
                    _ruleType = 45;
                    return -3;
                }
            }));
            //7 CommentStart and End
            _states.Add(new SyntaxState((x, y) =>
            {
                var value = x[y];
                var result = string.Join(" ", value.Select(z => z.ToString()).ToArray());
                if (x[y].Count == 0)
                {
                    _ruleType = 46;
                    return -3;
                }
                if (result == "23")
                {
                    _ruleType = 30;
                    return 0;
                }
                if (Regex.IsMatch(result, ".*23$"))
                {
                    _ruleType = 30;
                    return 0;
                }
                else
                {
                    return 7;
                }


                //else
                //{
                //    _ruleType = 46;
                //    return -3;
                //}

            }));
            // 8 TypeDouble
            _states.Add(new SyntaxState((x, y) =>
            {
                var value = x[y];
                var result = string.Join(" ", value.Select(z => z.ToString()).ToArray());
                if (result == "24")
                {
                    int indexVariable = 0;
                    for (int i = 0; i < y; i++)
                    {
                        indexVariable += x[i].Count;
                    }
                    _ruleType = 28;
                    var variable = new Variables(tokenValues[indexVariable - 3], "double", tokenValues[indexVariable - 1]);
                    _variables.Add(variable);
                    return 0;
                }
                else
                {
                    _ruleType = 47;
                    return 0;
                }

            }));
        }
    }
}
