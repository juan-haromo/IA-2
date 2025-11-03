namespace BehaviourTrees
{
    public interface IStrategy
    {
        NodeStatus Procces();
        void Reset();
    }
}
