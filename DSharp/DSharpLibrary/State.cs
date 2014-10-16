using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSharpLibrary
{
    class State
    {
        private Func<string, int, int> _function;

        public Func<string, int, int> Operate
        {
            get { return _function; }
        }

        public State(Func<string, int, int> function)
        {
            _function = function;
        }
    }
}
