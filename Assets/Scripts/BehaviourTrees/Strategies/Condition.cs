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

        public Status Procces() => predicate() ? Status.Succes : Status.Failure;
    }
}