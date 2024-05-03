using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_ZH_II
{
    internal class Game
    {
        private ASolver solver;

        public Game(ASolver solver)
        {
            this.solver = solver;
        }

        public void Play()
        {
            bool playersMove = true;
            State currentState = new State();
            while (currentState.GetStatus() == State.BLANK)
            {
                Console.WriteLine(currentState);
                if (playersMove)
                {
                    currentState = PlayersMove(currentState);
                }
                else
                {
                    currentState = AIsMove(currentState);
                }
                playersMove = !playersMove;
            }
            Console.WriteLine(currentState);
            Console.Write("Winner: ");
            Console.WriteLine(currentState.GetStatus());
        }
        public void ColorfulPlay()
        {
            bool playersMove = true;
            State currentState = new State();
            while (currentState.GetStatus() == State.BLANK)
            {
                if (playersMove)
                {
                    currentState = ColorfulPlayersMove(currentState);
                }
                else
                {
                    currentState = AIsMove(currentState);
                }
                /*
                currentState = AIsMove(currentState);
                ColorfulPrint(currentState.ToString());
                */
                playersMove = !playersMove;
            }
            Console.Clear();
            ColorfulPrint(currentState.ToString());
            Console.Write("Winner: ");
            ColorfulPrint(currentState.GetStatus().ToString());
        }


        private State PlayersMove(State currentState)
        {
            Operator op = null;
            while (op == null || !op.IsApplicable(currentState))
            {
                int row, col, move;
                do
                {
                    Console.Write("Row: ");
                } while (!int.TryParse(Console.ReadLine(), out row) || row < 1 || row > 6);
                do
                {
                    Console.Write("Col: ");
                } while (!int.TryParse(Console.ReadLine(), out col) || col < 1 || col > 6);
                Console.WriteLine("+---+---+---+---+---+");
                Console.WriteLine("| 1 |   | 2 |   | 3 |");
                Console.WriteLine("+---+---+---+---+---+");
                Console.WriteLine("|   | 4 | 5 | 6 |   |");
                Console.WriteLine("+---+---+---+---+---+");
                Console.WriteLine("| 7 | 8 |XXX| 9 | 10|");
                Console.WriteLine("+---+---+---+---+---+");
                Console.WriteLine("|   | 11| 12| 13|   |");
                Console.WriteLine("+---+---+---+---+---+");
                Console.WriteLine("| 14|   | 15|   | 16|");
                Console.WriteLine("+---+---+---+---+---+");
                do
                {
                    Console.Write("Move: ");
                } while (!int.TryParse(Console.ReadLine(), out move) || move < 1 || move > 16);
                row--;
                col--;
                move--;
                op = new Operator(row, col,(Direction)move);
            }
            return op.Apply(currentState);
        }
        private State ColorfulPlayersMove(State currentState)
        {
            Operator op = null;
            while (op == null || !op.IsApplicable(currentState))
            {
                Console.Clear();
                ColorfulPrint(currentState.ToString());
                int row, col, move;
                Console.Write("Select a row: ");
                if (!int.TryParse(Console.ReadLine(), out row) || row < 1 || row > 6)
                {
                    continue;
                }
                Console.Write("Select a column: ");
                if (!int.TryParse(Console.ReadLine(), out col) || col < 1 || col > 6)
                {
                    continue;
                }
                row--;
                col--;
                if (currentState.Board[row, col] != currentState.CurrentPlayer)
                {
                    Console.WriteLine("Incorrect selection!");
                    System.Threading.Thread.Sleep(1500);
                    continue;
                }
                Console.WriteLine("+---+---+---+---+---+");
                Console.WriteLine("| 1 |   | 2 |   | 3 |");
                Console.WriteLine("+---+---+---+---+---+");
                Console.WriteLine("|   | 4 | 5 | 6 |   |");
                Console.WriteLine("+---+---+---+---+---+");
                Console.WriteLine("| 7 | 8 | + | 9 | 10|");
                Console.WriteLine("+---+---+---+---+---+");
                Console.WriteLine("|   | 11| 12| 13|   |");
                Console.WriteLine("+---+---+---+---+---+");
                Console.WriteLine("| 14|   | 15|   | 16|");
                Console.WriteLine("+---+---+---+---+---+");
                Console.WriteLine("Select the next move to the relative position. ('+' in the helper table)");
                Console.Write("Move: ");
                if (!int.TryParse(Console.ReadLine(), out move) || move < 1 || move > 16)
                {
                    continue;
                }
                move--;
                op = new Operator(row, col, (Direction)move);
            }
            return op.Apply(currentState);
        }

        private State AIsMove(State currentState)
        {
            State nextState = solver.NextMove(currentState);
            if (nextState == null)
            {
                throw new Exception("The AI cannot select the next move.");
            }
            return nextState;
        }

        private void ColorfulPrint(string str)
        {
            foreach (char c in str)
            {
                if (c == State.PLAYER1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Red;
                }
                else if (c == State.PLAYER2)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.BackgroundColor = ConsoleColor.Blue;
                }
                else if (c == State.WALL)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.BackgroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.Write(c);
            }
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}
