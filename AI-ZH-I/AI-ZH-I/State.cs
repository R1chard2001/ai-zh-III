using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_ZH_I
{
    internal class State:ICloneable
    {
        public int[,] Board = new int[7, 10]
        {
            { 1, 5, 3, 4, 3, 6, 7, 1, 1, 6 },
            { 4, 4, 3, 4, 2, 6, 2, 6, 2, 5 },
            { 1, 3, 9, 4, 5, 2, 4, 2, 9, 5 },
            { 5, 2, 3, 5, 5, 6, 4, 6, 2, 4 },
            { 1, 3, 3, 2, 5, 6, 5, 2, 3, 2 },
            { 2, 5, 2, 5, 5, 6, 4, 8, 6, 1 },
            { 9, 2, 3, 6, 5, 6, 2, 2, 2, 0 }
        };
        public int Row = 0;
        public int Col = 0;

        public State()
        {
            
        }

        public object Clone()
        {
            State newState = new State();
            newState.Row = Row;
            newState.Col = Col;
            newState.Board = (int[,])Board.Clone(); // felesleges ezt tbh
            return newState;
        }
        public bool IsTargetState()
        {
            return Board[Row, Col] == 0;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                sb.Append(' ');
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    if (Row == i && Col == j)
                    {
                        sb[sb.Length - 1] = '|';
                    }

                    if (Board[i, j] == 0)
                    {
                        sb.Append('*');
                    }
                    else
                    {
                        sb.Append(Board[i, j]);
                    }
                    

                    if (Row == i && Col == j)
                    {
                        sb.Append('|');
                    }
                    else
                    {
                        sb.Append(' ');
                    }
                    
                }
                if (i < Board.GetLength(0) - 1)
                {
                    sb.AppendLine();
                }
            }
            //sb.Append("Position: (");
            //sb.Append(Row);
            //sb.Append(", ");
            //sb.Append(Col);
            //sb.Append(")");
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is State)) return false;
            State other = (State)obj;
            return other.Row == Row && other.Col == Col;
        }
    }
}
