using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_ZH_II
{
    public class MiniMaxABPruneSolver : ASolver
    {
        public MiniMaxABPruneSolver(int depth) : base()
        {
            Depth = depth;
        }
        public int Depth;

        char player;
        char otherPlayer;
        public override State NextMove(State currentState)
        {
            player = currentState.CurrentPlayer;
            otherPlayer = player == State.PLAYER1 ? State.PLAYER2 : State.PLAYER1;
            Node currentNode = new Node(currentState);
            extendNode(currentNode);
            currentNode.SortChildrenMinimax(currentState.CurrentPlayer);
            if (currentNode.Children.Count == 0)
                foreach (Operator op in Operators)
                    if (op.IsApplicable(currentState))
                        return op.Apply(currentState);
            return currentNode.Children[0].State;
        }

        private void extendNode(Node node)
        {
            if (node.GetStatus() != State.BLANK || node.Depth >= Depth) return;

            foreach (Operator op in Operators)
            {
                if (op.IsApplicable(node.State))
                {
                    State newState = op.Apply(node.State);
                    char status = newState.GetStatus();
                    if (status == otherPlayer) // lépésünk miatt nyer az ellenfél
                    {
                        BetaPrune(node); // ne feltételezzük, hogy az ellenfél rossz lépést tesz, nem érdemes a többit nézni
                        return;
                    }
                    Node newNode = new Node(newState, node);
                    if (status == player) // ha mi nyerünk, több opciót nem kell nézni
                    {
                        AlphaPrune(newNode);
                        return;
                    }
                    node.Children.Add(newNode);
                    extendNode(newNode);
                }
            }
        }
        private void BetaPrune(Node node)
        {
            node.Children.Clear();
            Node temp = node.Parent;
            if (temp == null)
                return;
            temp.Children.Remove(node);
        }
        private void AlphaPrune(Node node)
        {
            Node parent = node.Parent;
            if (parent == null) return;
            parent.Children.Clear();
            parent.Children.Add(node);
        }
    }
}
