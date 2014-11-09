using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSharpLibrary
{
    class SyntaxState
    {
            private Func<List<int>> _function;

            public Func<List<int>> Operate
            {
                get { return _function; }
            }

            public SyntaxState(Func<List<int>> function)
            {
                _function = function;
            }
    }
}
