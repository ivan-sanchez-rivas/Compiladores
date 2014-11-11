using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSharpLibrary
{
    class SyntaxState
    {
        private Func<List<List<int>>, int, int> _function;

        public Func<List<List<int>>, int, int> Operate
        {
            get { return _function; }
        }

        public SyntaxState(Func<List<List<int>>, int, int> function)
        {
            _function = function;
        }
    }
}
