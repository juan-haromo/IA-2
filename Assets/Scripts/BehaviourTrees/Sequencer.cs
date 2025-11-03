using BehaviourTrees;

namespace BehaviourTrees
{
    public class Sequencer : Node
    {
        public Sequencer(string name) : base(name)
        {
        }

        public override Status Process()
        {
            if (currentChild < children.Count)
            {
                switch (children[currentChild].Process())
                {
                    case Status.Running:
                        return Status.Running;
                    case Status.Failure:
                        Reset();
                        return Status.Failure;
                    case Status.Succes:
                        currentChild++;
                        if (currentChild == children.Count) { return Status.Succes; }
                        return Status.Running;
                }
            }
            Reset();
            return Status.Succes;
        }
    }
}