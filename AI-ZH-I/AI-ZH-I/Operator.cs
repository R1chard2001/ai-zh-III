using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_ZH_I
{
    internal class Operator
    {
        private int[] ROW_DIFF = new int[8] { -1, -1, 0, 1, 1, 1, 0, -1 };
        private int[] COL_DIFF = new int[8] { 0, 1, 1, 1, 0, -1, -1, -1 };
        Direction Dir;
        public Operator(Direction dir)
        {
            Dir = dir;
        }


        public bool IsApplicable(State state)
        {
            int nextRow = getNextRow(state);
            int nextCol = getNextCol(state);
            return nextRow >= 0 && nextRow < state.Board.GetLength(0) 
                && nextCol >= 0 && nextCol < state.Board.GetLength(1);
        } 
        private int getNextRow(State state)
        {
            int row = state.Row;
            int col = state.Col;
            return state.Row + state.Board[row, col] * ROW_DIFF[(int)Dir];
        }
        private int getNextCol(State state)
        {
            int row = state.Row;
            int col = state.Col;
            return state.Col + state.Board[row, col] * COL_DIFF[(int)Dir];
        }

        public State Apply(State state)
        {
            State newState = (State)state.Clone();
            newState.Row = getNextRow(state);
            newState.Col = getNextCol(state);
            return newState;
        }
    }
}
