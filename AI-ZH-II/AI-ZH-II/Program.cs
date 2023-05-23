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
            Game game = new Game(new MiniMaxSolver(3));
            game.ColorfulPlay();
            Console.ReadLine();
        }
    }
}
