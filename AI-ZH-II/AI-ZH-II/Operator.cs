using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_ZH_II
{
    public class Operator
    {
        int Row, Col, NextRow, NextCol;
        bool newDisc;
        private static int[] ROW_DIFF = new int[16] { -2, -2, -2, -1, -1, -1, 0, 0, 0, 0, 1, 1, 1, 2, 2, 2 };
        private static int[] COL_DIFF = new int[16] { -2, 0, 2, -1, 0, 1, -2, -1, 1, 2, -1, 0, 1, -2, 0, 2 };
        public Operator(int row, int col, Direction dir)
        {
            Row = row;
            Col = col;
            NextRow = getNextRow(dir);
            NextCol = getNextCol(dir);
            switch (dir)
            {
                case Direction.UP_RIGHT_UP_RIGHT:
                case Direction.UP_UP:
                case Direction.LEFT_LEFT:
                case Direction.RIGHT_RIGHT:
                case Direction.DOWN_LEFT_DOWN_LEFT:
                case Direction.DOWN_DOWN:
                case Direction.DOWN_RIGHT_DOWN_RIGHT:
                    newDisc = false;
                    break;
                default:
                    newDisc = true;
                    break;
            }
        }
        public bool IsApplicable(State state)
        {
            if (NextRow < 0 || NextRow >= 6 || NextCol < 0 || NextCol >= 6)
            {
                return false;
            }
            if (state.Board[Row,Col] != state.CurrentPlayer)
            {
                return false;
            }
            return state.Board[NextRow, NextCol] == State.BLANK;
        }

        private int getNextRow(Direction dir)
        {
            return Row + ROW_DIFF[(int)dir];
        }
        private int getNextCol(Direction dir)
        {
            return Col + COL_DIFF[(int)dir];
        }
        public State Apply(State state)
        {
            State newState = (State)state.Clone();
            char otherPlayer = newState.CurrentPlayer == State.PLAYER1 ? State.PLAYER2 : State.PLAYER1;
            if (!newDisc)
            {
                newState.Board[Row, Col] = State.BLANK;
            }
            newState.Board[NextRow, NextCol] = newState.CurrentPlayer;
            for (int i = NextRow - 1; i < NextRow + 2; i++)
            {
                if (i < 0 || i >= 6)
                {
                    continue;
                }
                for (int j = NextCol - 1; j < NextCol + 2; j++)
                {
                    if (j < 0 || j >= 6)
                    {
                        continue;
                    }
                    if (newState.Board[i,j] == otherPlayer)
                    {
                        newState.Board[i, j] = newState.CurrentPlayer;
                    }
                }
            }
            newState.ChangePlayer();
            return newState;
        }
    }
}
