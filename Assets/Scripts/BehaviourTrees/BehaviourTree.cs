namespace BehaviourTrees
{
    public class BehaviourTree : Node
    {
        public BehaviourTree(string name) : base(name)
        {
        }

        public override Status Process()
        {
            while (currentChild < children.Count)
            {
                Status status = children[currentChild].Process();

                if (status != Status.Succes)
                {
                    return status;
                }
                currentChild++;
            }
            Reset();
            return Status.Succes;
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