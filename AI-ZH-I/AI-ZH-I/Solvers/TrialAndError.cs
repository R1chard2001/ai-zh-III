using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_ZH_I
{
    internal class TrialAndError : ASolver
    {
        private Random random = new Random();
        public State CurrentState;
        public TrialAndError() : base()
        {
            CurrentState = new State();
        }
        public override Operator SelectOperator()
        {
            while (true)
            {
                int index = random.Next(Operators.Count);
                if (Operators[index].IsApplicable(CurrentState))
                {
                    return Operators[index];
                }
            }
        }
        public override void Solve()
        {
            int step = 0;
            Console.WriteLine("Step {0}", step++);
            Console.WriteLine(CurrentState);
            while (!CurrentState.IsTargetState())
            {
                Operator o = SelectOperator();
                CurrentState = o.Apply(CurrentState);
                Console.WriteLine("-----------------");
                Console.WriteLine("Step {0}", step++);
                Console.WriteLine(CurrentState);
            }
        }
    }
}
