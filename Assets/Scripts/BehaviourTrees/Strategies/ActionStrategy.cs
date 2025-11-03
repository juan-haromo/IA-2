using System;

namespace BehaviourTrees
{
    public class ActionStrategy : IStrategy
    {
        public NodeStatus Procces()
        {
            action?.Invoke();
            return NodeStatus.Success;
        }

        public void Reset()
        {
            //Nop
        }

        readonly Action action;

        public ActionStrategy(Action action)
        {
            this.action = action;
        }
    }
}