using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class IASpa : MonoBehaviour
{
    public Transform player;
    public float fleeRange;
    public NavMeshAgent agent;

    private float distance;
    private Vector3 destination;

    public void Start()
    {
        if(agent == null)
        {
            if(TryGetComponent<NavMeshAgent>(out NavMeshAgent _agent))
            {
                agent = _agent;
            }
            else
            {
                Debug.LogError(gameObject.name + " has no nav mesh agent");
            }
        }
    }

    private void Update()
    {
        //Senese
        distance = Vector3.Distance(transform.position, player.position);

        //Plan
        if(distance < fleeRange)
        {
            Vector3 dir = (transform.position - player.position).normalized;
            destination = transform.position + (dir * 5.0f);
        }
        else
        {
            destination = player.position;
        }

        //Action
        agent.SetDestination(destination);
    }
}
