using UnityEngine;
using UnityEngine.AI;

public class Chad : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform player;
    float nextTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (nextTime < Time.time) 
        {
            nextTime = Time.time + 0.5f;
            agent.SetDestination(player.position);
        }
    }
}
