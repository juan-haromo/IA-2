namespace BehaviourTrees
{
    public class Leaf : Node
    {
        IStrategy strategy;
        public Leaf(string name, IStrategy strategy) : base(name)
        {
            this.strategy = strategy;
        }

        public override NodeStatus Process()
        {
            return strategy.Process();
        }

        public override void Reset()
        {
            strategy.Reset();
        }
    }
}