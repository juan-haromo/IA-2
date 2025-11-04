using System;

namespace BehaviourTrees
{
    public class Condition : IStrategy
    {
        public void Reset()
        {
          //Nop  
        }

        readonly Func<bool> predicate;

        public Condition(Func<bool> predicate)
        {
            this.predicate = predicate;
        }

        public NodeStatus Process() => predicate() ? NodeStatus.Success : NodeStatus.Failure;
    }
}