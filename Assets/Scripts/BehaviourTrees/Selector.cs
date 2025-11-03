using BehaviourTrees;

namespace BehaviourTrees
{


    public class Selector : Node
    {
        public Selector(string name) : base(name)
        {
        }

        public override Status Process()
        {
            if (currentChild < children.Count)
            {
                switch (children[currentChild].Process())
                {
                    case Status.Running:
                        return Status.Succes;
                    case Status.Succes:
                        Reset();
                        return Status.Succes;
                    case Status.Failure:
                        currentChild++;
                        return currentChild == children.Count? Status.Failure : Status.Running;
                }
            }
            Reset();
            return Status.Failure;
        }
    }
}