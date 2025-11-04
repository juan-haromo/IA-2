using UnityEngine;
using BehaviourTrees;
using UnityEngine.AI;

public class PetBehaviourTree : MonoBehaviour
{
    public BehaviourTree tree;
    public NavMeshAgent agent;
    public Transform player;
    public Transform houseCenter;
    public float houseRadius;
    public float followRadius;

    bool isTimeRegistered;
    float nextWaitTime;
    public float waitTime = 2.0f;

    void OnDrawGizmos()
    {
        if(houseCenter == null){ return; }
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(houseCenter.position, houseRadius);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(houseCenter.position, followRadius);
    }

    void Start()
    {
        tree = new BehaviourTree("Pet tree");
        
        Sequencer followPlayerSequencer = new Sequencer("Follow player sequencer");
        BehaviourTrees.Condition ownerNearHouse = new BehaviourTrees.Condition(() =>
        {
            bool result = Vector3.Distance(player.position, houseCenter.position) < followRadius;
            if(result){ nextWaitTime = Time.time + waitTime; }
            return result;
        });
        FollowTransform followPlayer = new FollowTransform(player, agent);

        followPlayerSequencer.AddChildren(new Leaf("Owner near house", ownerNearHouse));
        followPlayerSequencer.AddChildren(new Leaf("Follow player", followPlayer));

       
        
        Selector playerAwaySelector = new Selector("Player away selector");
        
        Sequencer waitingAwaySequencer = new Sequencer("Waiting away sequencer");
        BehaviourTrees.Condition wasOwnerNearHouse = new BehaviourTrees.Condition(() =>
        {
            if (nextWaitTime < Time.time)
            {
            return false;
            }
                return true;
        });
        waitingAwaySequencer.AddChildren(new Leaf("Was owner near house", wasOwnerNearHouse));
        waitingAwaySequencer.AddChildren(new Leaf("Waiting", new WaitStrategy(agent, nextWaitTime)));

        StrategyWanderAround wanderAround = new StrategyWanderAround(houseCenter, houseRadius, agent);

        playerAwaySelector.AddChildren(waitingAwaySequencer);
        playerAwaySelector.AddChildren(new Leaf("Wander around", wanderAround));

        Selector baseSelector = new Selector("Base selector");

        baseSelector.AddChildren(followPlayerSequencer);
        baseSelector.AddChildren(playerAwaySelector);

        tree.AddChildren(baseSelector);
    }


    void Update()
    {
        tree.Process();
        Debug.Log(tree.GetCurrentChild);
    }
}
