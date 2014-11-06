using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSharpLibrary
{
    public class Syntax
    {
        private List<int> _tokenNumber = new List<int>();
        public Syntax()
        {

        }
        public void TokenStrip(List<Token> tokenList)
        {
            
            foreach (Token t in tokenList)
            {
                var num = (int)((TokenType)Enum.Parse(typeof(TokenType), t.Type.ToString()));
                _tokenNumber.Add(num);
            }
        }
    }
}
