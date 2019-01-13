using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asocjacje
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = new AprioriAlgorithm();
            Console.WriteLine(string.Join(Environment.NewLine, x.Execute(0.01)));
            Console.ReadLine();
        }
    }
}
