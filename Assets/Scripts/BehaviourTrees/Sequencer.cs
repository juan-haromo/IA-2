using UnityEngine;

namespace BehaviourTrees
{
    public class Sequencer : Node
    {
        public Sequencer(string name) : base(name)
        {
        }

        public override NodeStatus Process()
        {
            Debug.Log(children[currentChild].name);
            if (currentChild < children.Count)
            {
                switch (children[currentChild].Process())
                {
                    case NodeStatus.Running:
                        return NodeStatus.Running;
                    case NodeStatus.Failure:
                        Reset();
                        return NodeStatus.Failure;
                    case NodeStatus.Success:
                        currentChild++;
                        if (currentChild == children.Count) { return NodeStatus.Success; }
                        return NodeStatus.Running;
                }
            }
            Reset();
            return NodeStatus.Success;
        }
    }
}