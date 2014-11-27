using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSharpLibrary
{
    public class Variables
    {
        string nombreVariable { get; set; }
        string tipoVariable { get; set; }
        string valorVariable { get; set; }

        public Variables(string _nombreVariable, string _tipoVariable, string _valorVariable)
        {
            nombreVariable = _nombreVariable;
            tipoVariable = _tipoVariable;
            valorVariable = _valorVariable;
        }
    }
}
