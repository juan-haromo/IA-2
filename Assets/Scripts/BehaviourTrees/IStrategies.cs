namespace BehaviourTrees
{
    public interface IStrategy
    {
        NodeStatus Process();
        void Reset();
    }
}
