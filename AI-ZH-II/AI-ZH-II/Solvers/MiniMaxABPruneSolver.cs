using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_ZH_II
{
    public class MiniMaxABPruneSolver : ASolver
    {
        Action<Node> BetaPrune;
        public MiniMaxABPruneSolver(int depth, bool useSimpleBetaPrune = true) : base()
        {
            Depth = depth;
            if (useSimpleBetaPrune)
                BetaPrune = BetaPruneSimple;
            else
                BetaPrune = BetaPruneFull;
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
            node.HasBeenExtended = true;
            if (node.GetStatus() != State.BLANK || node.Depth >= Depth) return;

            foreach (Operator op in Operators)
            {
                if (op.IsApplicable(node.State))
                {
                    State newState = op.Apply(node.State);
                    char status = newState.GetStatus();
                    if (status == otherPlayer)
                    {
                        BetaPrune(node);
                        return;
                    }
                    Node newNode = new Node(newState, node);
                    if (status == player)
                    {
                        AlphaPrune(newNode);
                        return;
                    }
                    node.Children.Add(newNode);
                }
            }
            Node childNode = getChildNodeToExtend(node);
            while (childNode != null)
            {
                extendNode(childNode);
                childNode = getChildNodeToExtend(node);
            }
        }
        private Node getChildNodeToExtend(Node parent)
        {
            return parent.Children.Find(x => !x.HasBeenExtended);
        }
        private void BetaPruneFull(Node node)
        {
            while (node != null && node.Parent != null)
            {
                node.Children.Clear();
                Node parent = node.Parent;
                parent.Children.Remove(node);
                if (parent.Children.Count > 0)
                    return;
                node = parent.Parent;
            }
        }
        private void BetaPruneSimple(Node node)
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
