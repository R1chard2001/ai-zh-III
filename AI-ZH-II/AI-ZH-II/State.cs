using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_ZH_II
{
    public class State : ICloneable
    {
        public static char PLAYER1 = 'R';
        public static char PLAYER2 = 'B';
        public static char BLANK = ' ';
        public static char WALL = 'W';

        public char[,] Board = new char[6, 6]
        {
            { PLAYER1, BLANK, BLANK, BLANK, BLANK, PLAYER2 },
            { BLANK,   BLANK, BLANK, BLANK, BLANK, BLANK   },
            { BLANK,   BLANK, BLANK, BLANK, BLANK, BLANK   },
            { BLANK,   BLANK, BLANK, WALL,  BLANK, BLANK   },
            { BLANK,   BLANK, BLANK, BLANK, BLANK, BLANK   },
            { PLAYER2, BLANK, BLANK, BLANK, BLANK, PLAYER1 }
        };
        public char CurrentPlayer = PLAYER1;
        public void ChangePlayer()
        {
            if (CurrentPlayer == PLAYER1)
            {
                CurrentPlayer = PLAYER2;
            }
            else
            {
                CurrentPlayer = PLAYER1;
            }
        }

        public object Clone()
        {
            State newState = new State();
            newState.Board = (char[,])Board.Clone();
            newState.CurrentPlayer = CurrentPlayer;
            return newState;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is State)) return false;
            State other = (State)obj;
            if (!Board.Equals(other.Board))
            {
                return false;
            }
            return CurrentPlayer == other.CurrentPlayer;
        }

        private static int[] ROW_DIFF = new int[16] { -2, -2, -2, -1, -1, -1, 0, 0, 0, 0, 1, 1, 1, 2, 2, 2 };
        private static int[] COL_DIFF = new int[16] { -2, 0, 2, -1, 0, 1, -2, -1, 1, 2, -1, 0, 1, -2, 0, 2 };
        public char GetStatus()
        {
            int p1count = 0;
            int p2count = 0;
            int blankCount = 0;
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    if (Board[i,j] == CurrentPlayer && canCurrentPlayerMove(i, j))
                    {
                        return BLANK;
                    }
                    if (Board[i,j] == PLAYER1)
                    {
                        p1count++;
                    }
                    else if (Board[i,j] == PLAYER2)
                    {
                        p2count++;
                    }
                    else if (Board[i,j] == BLANK)
                    {
                        blankCount++;
                    }
                }
            }

            if (CurrentPlayer == PLAYER1)
            {
                p2count += blankCount;
            }
            else
            {
                p1count += blankCount;
            }

            if (p1count == 0)
            {
                return PLAYER2;
            }
            if (p2count == 0)
            {
                return PLAYER1;
            }

            if (p1count > p2count)
            {
                return PLAYER1;
            }
            return PLAYER2;
        }
        private bool canCurrentPlayerMove(int row, int col)
        {
            for (int i = 0; i < 16; i++)
            {
                int nextRow = row + ROW_DIFF[i];
                int nextCol = col + COL_DIFF[i];
                if (nextRow < 0 || nextRow >= Board.GetLength(0)
                    || nextCol < 0 || nextCol >= Board.GetLength(1))
                    continue;
                if (Board[nextRow, nextCol] == BLANK)
                {
                    return true;
                }

            }
            return false;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("    1   2   3   4   5   6  ");
            sb.AppendLine("  +---+---+---+---+---+---+");
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                sb.Append(i + 1);
                sb.Append(" | ");
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    sb.Append(Board[i,j]);
                    sb.Append(" | ");
                }
                sb.AppendLine();
                sb.AppendLine("  +---+---+---+---+---+---+");
            }
            sb.Append("Current player: ");
            sb.Append(CurrentPlayer);
            return sb.ToString();
        }

        public int GetHeuristics(char player)
        {
            char other = player == PLAYER1 ? PLAYER2 : PLAYER1;
            char status = GetStatus();
            if (status == player)
            {
                return 100;
            }
            if (status == other)
            {
                return -100;
            }
            int pCoutn = 0, oCount = 0;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (Board[i,j] == player)
                    {
                        pCoutn++;
                    }
                    else if (Board[i, j] == other)
                    {
                        oCount++;
                    }
                }
            }

            return pCoutn - oCount;
        }
    }
}
