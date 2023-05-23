using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_ZH_I
{
    internal abstract class ASolver
    {
        protected List<Operator> Operators = new List<Operator>();
        private void GenerateOperators()
        {
            for (int i = 0; i < 8; i++)
            {
                Operators.Add(new Operator((Direction) i));
            }
        }
        public ASolver()
        {
            GenerateOperators();
        }
        public abstract Operator SelectOperator();
        public abstract void Solve();
    }
}
