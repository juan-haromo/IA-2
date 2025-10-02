using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;
using TMPro;
using UnityEngine.InputSystem;

public class IA_Lvl3 : MonoBehaviour
{
    public Transform player;

    private float health;
    [SerializeField] private float maxHealth;
    private float criticalHealthLimit;

    private bool lineOfSight;



    Dictionary<string, float> actionScores;

    float distance;

    public List<Transform> patrolPoints;
    public float minPatrolDistance;
    int patrolIndex;

    public float fleeDistance;

    public float viewDistance = 10f;
    public float viewAngle = 60f;

    private const string FLEE = "flee";
    private const string CHASE = "chase";
    private const string PATROL = "patrol";

    [SerializeField] private TextMeshProUGUI lblFlee;
    [SerializeField] private TextMeshProUGUI lblChase;
    [SerializeField] private TextMeshProUGUI lblPatrol;

    NavMeshAgent agent;
    string chossenAction;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = lineOfSight ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, viewDistance);
        Gizmos.color = Color.yellow;
    }

    private void Start()
    {
        if (TryGetComponent<NavMeshAgent>(out NavMeshAgent _agent))
        {
            agent = _agent;
        }
        else
        {
            Debug.LogError(gameObject.name + " has no navmesh agent");
        }

        actionScores = new Dictionary<string, float>()
        {
            {FLEE, 0.0f },
            {CHASE, 0.0f },
            {PATROL, 0.0f }
        };

        health = maxHealth;
    }

    private void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Damage(10);
        }

        //Sense
        distance = Vector3.Distance(transform.position, player.position);
        lineOfSight = PlayerInFOV();

        if (Vector3.Distance(transform.position, patrolPoints[patrolIndex].position) < minPatrolDistance)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Count;
        }

        //Plan 
        float healthRatio = Mathf.Clamp(health / maxHealth,0.01f,1);
        float distanceRatio = Mathf.Clamp(distance / fleeDistance, 0.01f,1);

        if (criticalHealthLimit > healthRatio) { distanceRatio = 0; }

        float riskFactor = (1 - healthRatio) * (1 - distanceRatio);

        float aggroFactor = (healthRatio * distanceRatio);

        float total = riskFactor + aggroFactor;

        riskFactor /= total;
        aggroFactor /= total;
        aggroFactor *= healthRatio > criticalHealthLimit? 1 : 0;

        actionScores[FLEE] = riskFactor * 10 * (lineOfSight ? 1 : 0);
        actionScores[CHASE] = aggroFactor * 10 * (lineOfSight ? 1 : 0);
        actionScores[PATROL] = 3.0f;

        lblFlee.text = "Flee = " + actionScores[FLEE].ToString("n2");
        lblChase.text = "Chase = " + actionScores[CHASE].ToString("n2");
        lblPatrol.text = "Patrol = " + actionScores[PATROL].ToString("n2");

        //Act
        chossenAction = actionScores.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        
        switch (chossenAction)
        {
            case FLEE:
                Flee();
                break;
            case CHASE:
                Chase();
                break;
            case PATROL:
                Patrol();
                break;
            default:
                break;
        }
        
    }

    private void Flee()
    {
        Vector3 fleeDirection = (transform.position - player.position).normalized * 5;
        if (NavMesh.SamplePosition(fleeDirection, out NavMeshHit hit, 1, NavMesh.AllAreas)) 
        {
            agent.SetDestination(transform.position + fleeDirection);    
        }
        else
        {
            agent.SetDestination(FindFleeAlternative(fleeDirection));
        }
    }
    private void Chase()
    {
        UpdatePrediction();
        agent.SetDestination(predictedPlayerPosition);
    }

    private void Patrol()
    {
        agent.SetDestination(patrolPoints[patrolIndex].position);
    }

    public void Damage(float amount)
    {
        health -= Mathf.Abs(amount);
    }

    private bool PlayerInFOV()
    {
        Vector3 dirToPlayer = (player.position - transform.position).normalized;

        if (distance > viewDistance) 
        {
            return false;
        }
        float angleToPlayer = Vector3.Angle(dirToPlayer, transform.forward);
        if(angleToPlayer > viewAngle / 2)
        {
            return false;
        }
        if (Physics.Raycast(transform.position, (player.position - transform.position).normalized, out RaycastHit hitInfo, viewDistance))
        {
            return hitInfo.collider.CompareTag("Player");
        }
        return false;
    }

    #region Flee
    public float maxDistFromDirection = 100;
    public float step = 10;
    public float fleeLength = 3;

    private Vector3 FindFleeAlternative(Vector3 fleeDir)
    {
        float maxDistanceFromPlayer = 0;
        Vector3 bestPosition = transform.position;

        for (float angle = -maxDistFromDirection; angle <= maxDistanceFromPlayer; angle += step) 
        {
            Vector3 dir = Quaternion.Euler(0, angle, 0) * fleeDir;

            Vector3 candite = transform.position + dir * fleeLength;

            if (NavMesh.SamplePosition(candite, out NavMeshHit hit, 1f, NavMesh.AllAreas)) 
            {
                float distanceToPlayer = Vector3.Distance(candite,player.position);
                if(distanceToPlayer > maxDistanceFromPlayer)
                {
                    maxDistanceFromPlayer = distanceToPlayer;       
                    bestPosition = hit.position;
                }
            }
        }
        return bestPosition;
    }
    #endregion


    #region Predict 
    Vector3 lastPlayerPosition;
    Vector3 predictedPlayerPosition;

    private void UpdatePrediction()
    {
        Vector3 currentPlayerPosition = player.position;
        Vector3 moveDirection = (currentPlayerPosition - lastPlayerPosition).normalized;

        float predictionDistance = distance * 0.5f;

        predictedPlayerPosition = currentPlayerPosition + moveDirection * predictionDistance;

        lastPlayerPosition = currentPlayerPosition;
    }
    #endregion
}
