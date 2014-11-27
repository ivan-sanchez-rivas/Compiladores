using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpLibrary;

namespace DSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                List<SyntaxRules> rules = new List<SyntaxRules>();
                var machine = new Machine();
                var syntax = new SyntaxMachine();
                Console.WriteLine("Enter a source code input:");
                string input = Console.ReadLine();

                if (input.Length == 0)
                {
                    Console.WriteLine("There's no code in the input.");
                    return;
                }
                else
                {
                    int result = machine.Operate(input);
                    if (result == 0)
                        Console.WriteLine("There ars errors on the source code.");
                    else
                    {
                        Console.WriteLine("The tokens are:\n");
                        foreach (Token t in machine.Tokens)
                        {
                            Console.WriteLine(t);

                        }

                        Console.WriteLine("\n");
                        Console.WriteLine("Analizador Sintactico: \n");
                        rules = syntax.TokenStrip(machine.Tokens);

                        foreach (SyntaxRules sr in rules)
                        {
                            Console.WriteLine(sr);
                        }


                    }
                    Console.ReadLine();
                }
                Console.Clear();
            }
        }
    }
}
