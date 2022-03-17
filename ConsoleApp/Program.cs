using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrarySolver;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numb = { 1, 10, 12, 165 };
            List<int> list = Solver.NumbersWithDigitNInBaseP(numb, 16, 10);
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

        }
    }
}
