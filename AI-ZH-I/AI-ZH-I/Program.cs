using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_ZH_I
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ASolver solver = new DepthFirst();
            solver.Solve();
            Console.ReadLine();
        }
    }
}
