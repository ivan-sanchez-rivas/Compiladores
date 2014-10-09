using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSharp
{
    public class input
    {
        private string textInput;
        private int index;

        public input(string Input)
        {
            this.textInput = Input;
            index = 0;
        }
        private char manual
        {
            get
            {
                char c;
                try
                {
                    c = textInput[index];
                    index++;
                }
                catch (Exception)
                {
                    c = '@';
                    return c;
                }
                return c;
            }
        }
    }
}
