using System;

namespace BehaviourTrees
{
    public class ActionStrategy : IStrategy
    {
        public Status Procces()
        {
            action?.Invoke();
            return Status.Succes;
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