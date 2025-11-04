using BehaviourTrees;
using UnityEngine;

namespace BehaviourTrees
{


    public class Selector : Node
    {
        public Selector(string name) : base(name)
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
                    case NodeStatus.Success:
                        Reset();
                        return NodeStatus.Success;
                    case NodeStatus.Failure:
                        currentChild++;
                        return NodeStatus.Running;
                }
            }
            Reset();
            return NodeStatus.Failure;
        }
    }
}