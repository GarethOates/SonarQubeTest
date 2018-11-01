using System;
using Utilities;

namespace SonarQubeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ICalculator calc = new Calculator();

            Console.WriteLine("{0} + {1} = {2}", 2, 2, calc.Addition(2, 2));
            Console.WriteLine("{0} - {1} = {2}", 10, 5, calc.Subtraction(10, 5));
            Console.WriteLine("{0} * {1} = {2}", 8, 2, calc.Multiplication(8, 2));
            Console.WriteLine("{0} / {1} = {2}", 64, 4, calc.Division(64, 4));

            Console.ReadLine();
        }
    }
}
