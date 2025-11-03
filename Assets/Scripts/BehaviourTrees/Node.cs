using System.Collections.Generic;


namespace BehaviourTrees
{
    public class Node
    {
        public readonly string name;
        public readonly List<Node> children;
        protected int currentChild;
        public string GetCurrentChild => children[currentChild].name;

        public Node(string name)
        {
            this.name = name;
            children = new List<Node>();
        }

        public void AddChildren(Node child)
        {
            children.Add(child);
        }

        public virtual NodeStatus Process() => children[currentChild].Process();

        public virtual void Reset()
        {
            currentChild = 0;
            foreach (Node child in children)
            {
                child.Reset();
            }
        }

    }
}
