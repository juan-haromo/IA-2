
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class IASPA_LVL2 : MonoBehaviour
{
    public float minDistance = 0.5f;
    public Transform player;
    public float fleeRange;
    public NavMeshAgent agent;


    private bool _lineOfSight = false;

    public List<Transform> patrolPoints;
    int patrolIndex;
    public float detectionRange = 5;

    private float distance;
    private Vector3 destination;

    private void OnDrawGizmos()
    {
        Gizmos.color = _lineOfSight ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (transform.forward * detectionRange));
    }

    public void Start()
    {
        if (agent == null)
        {
            if (TryGetComponent<NavMeshAgent>(out NavMeshAgent _agent))
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

        _lineOfSight = false;

        

        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, detectionRange))
        {
            _lineOfSight = hit.collider.CompareTag("Player");
        }

        distance = _lineOfSight? Vector3.Distance(transform.position, player.position) : Vector3.Distance(transform.position,patrolPoints[patrolIndex].position);


        //Plan
        if (_lineOfSight)
        {
            if (distance > fleeRange) 
            {
                destination = player.transform.position;
            }
            else
            {
                Vector3 dir = (transform.position - player.position).normalized;
                destination = transform.position + (dir * 5.0f);
            }
        }
        else
        {
            if (distance < minDistance)
            {
                patrolIndex = (patrolIndex + 1) % patrolPoints.Count;
            }
            destination = patrolPoints[patrolIndex].position;
        }
        

        //Action
        agent.SetDestination(destination);
    }
}
