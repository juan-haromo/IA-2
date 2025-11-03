namespace BehaviourTrees
{
    public class BehaviourTree : Node
    {
        public BehaviourTree(string name) : base(name)
        {
        }

        public override NodeStatus Process()
        {
            while (currentChild < children.Count)
            {
                NodeStatus status = children[currentChild].Process();

                if (status != NodeStatus.Success)
                {
                    return status;
                }
                currentChild++;
            }
            Reset();
            return NodeStatus.Success;
        }

        public override void Reset()
        {
            base.Reset();
            currentChild = 0;
            foreach(Node n in children)
            {
                n.Reset();
            }
        }
    }
}