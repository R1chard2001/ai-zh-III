using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_ZH_II
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(new MiniMaxABPruneSolver(4));
            game.ColorfulPlay();
            Console.ReadLine();
        }
    }
}
