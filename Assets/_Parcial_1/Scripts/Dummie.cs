using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dummie : MonoBehaviour
{
    public List<Transform> Waypoints;
    int currentPoint = 0;
    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(Waypoints[currentPoint].position);
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, Waypoints[currentPoint].position) < 2f)
        {
            currentPoint = (currentPoint + 1) % Waypoints.Count;
            agent.SetDestination(Waypoints[currentPoint].position);
        }
    }
}
