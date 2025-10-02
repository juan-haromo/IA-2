using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class SPA_Sheva : MonoBehaviour
{
    NavMeshAgent agent;
 
    #region Decisions Variables
    Dictionary<int, float> actionWeight;
    const int HEAL_PLAYER = 0;
    const int HEAL_SELF = 1;
    const int AMMO = 2;
    const int ATTACK = 3;
    const int WANDER = 4;
    int chosenAction;

    Vector3 destination;
    [SerializeField] TextMeshProUGUI currentAction;

    #endregion

    #region Senses
    public P1_Player player;
    float playerDanger;
    float selfDanger;

    List<Transform> enemiesInSight;

    #endregion

    #region Unity Calls
    private void Awake()
    {
        actionWeight = new Dictionary<int, float>()
        {
            { HEAL_PLAYER, 0 },
            { HEAL_SELF, 0},
            { AMMO, 0},
            { ATTACK, 0},
            { WANDER, 0}
        };

        currentHealth = maxHealth;
        if (!TryGetComponent<NavMeshAgent>(out agent))
        {
            Debug.LogError("No nav mesh agent is attached to " + name);
        }
        wanderDestination = transform.position;
    }

    public void Update()
    {
        //Sense 
        playerDanger = player.currentHealth / player.maxHealth; // 1 Full HP , 0 dead
        selfDanger = currentHealth / maxHealth;// 1 Full HP , 0 dead
        enemiesInSight = DetectEnemies();



        //0 = 100% HP, 10 = 80%, 20 = 60% , 30 = 40% , 40 = 20%, 50 = 0%HP 
        //Plan
        actionWeight[HEAL_PLAYER] = 50 * (1 - playerDanger) * (remainingHeals > 0 ? 1 : 0);
        actionWeight[HEAL_SELF] = 49 * (1 - selfDanger) * (remainingHeals > 0 ? 1 : 0);
        actionWeight[AMMO] = (50 - player.ammo) * (player.ammo < ammo ? 1 : 0);
        actionWeight[ATTACK] = (15 + (15 * (enemiesInSight.Contains(player.target) ? 1 : 0)) + enemiesInSight.Count) * (enemiesInSight.Count > 0 ? 1 : 0);
        actionWeight[WANDER] = 10;

        //Act
        chosenAction = actionWeight.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;

        switch (chosenAction)
        {
            case HEAL_PLAYER:
                HealPlayer();
                currentAction.text = "Healing Player";
                break;
            case HEAL_SELF:
                HealSelf();
                currentAction.text = "Healing Self";
                break;
            case AMMO:
                GiveAmmo();
                currentAction.text = "Giving Ammo";
                break;
            case ATTACK:
                Attack();
                currentAction.text = "Attacking";
                break;
            case WANDER:
                Wander();
                currentAction.text = "Wandering";
                break;
            default:
                Debug.LogWarning("Something went wrong with " + gameObject.name + " default case");
                break;
        }
    }
    #endregion

    #region Actions
    void HealPlayer()
    {
        destination = player.transform.position;
        agent.SetDestination(destination);

        if (Vector3.Distance(destination, transform.position) < 2.5f)
        {
            remainingHeals--;
            player.Heal(HEAL_AMOUNT);
            Debug.Log("Self heal, remaining: " + remainingHeals);
        }
    }

    public void HealSelf()
    {
        destination = transform.position;
        agent.SetDestination(destination);


        remainingHeals--;
        currentHealth += HEAL_AMOUNT;
        Debug.Log("Self heal, remaining: " + remainingHeals);
    }

    public void GiveAmmo()
    {
        destination = player.transform.position;
        agent.SetDestination(destination);

        if (Vector3.Distance(destination, transform.position) < 2.5f)
        {
            int givenAmmo = ammo > 10 ? 10 : ammo;
            ammo -= givenAmmo;
            player.ammo += givenAmmo;
            Debug.Log("Ammo given");
        }
    }

    float detectionTime;
    float detectionDelay = 5.0f;
    Transform enemyTarget;
    private void Attack()
    {
        //Find a new target if there is not or search each X seconds
        if (enemyTarget == null || Time.time < detectionTime)
        {
            detectionTime = Time.time + detectionDelay;
            //Give priority to our last target
            if (enemyTarget != null && enemiesInSight.Contains(enemyTarget))
            {

            }
            //Give priority to player target
            else if (enemiesInSight.Contains(player.target))
            {
                enemyTarget = player.target;
            }
            //Find closest target
            else
            {
                enemyTarget = enemiesInSight[0];
                for (int i = 1; i < enemiesInSight.Count; i++)
                {
                    if (Vector3.Distance(transform.position, enemyTarget.position) > Vector3.Distance(transform.position, enemiesInSight[i].position))
                    {
                        enemyTarget = enemiesInSight[i];
                    }
                }
            }
        }

        agent.SetDestination(enemyTarget.position);
        if (Vector3.Distance(enemyTarget.position, transform.position) <= shootRange)
        {
            Shoot(enemyTarget.position);
        }

    }

    Vector3 wanderDestination;
    float wanderTime = 0;
    float wanderDelay = 5.0f;
    public void Wander()
    {
        if (Time.time > wanderTime)
        {
        Debug.Log("Looking for destination " );
            bool found = false;
            while (!found)
            {
                if (NavMesh.SamplePosition(player.transform.position + (Random.insideUnitSphere * 10f), out NavMeshHit hit, 10f, 1))
                {
                    wanderDestination = hit.position;
                    found = true;
                }
            }
            wanderTime = Time.time + wanderDelay;
        }
        agent.SetDestination(wanderDestination);
    }
    #endregion

    #region Enemy Detection
    [SerializeField] float viewDistance = 5;
    [SerializeField] float viewAngle;
    [SerializeField] LayerMask enemyLayer;
    List<Transform> DetectEnemies()
    {
        List<Transform> detectedEnemies = new List<Transform>();
        //Detect all enemies in range
        Collider[] enemiesInRadius = Physics.OverlapSphere(transform.position, viewDistance, enemyLayer);

        foreach (Collider col in enemiesInRadius)
        {
            //Check if enemy is in field of vie
            Vector3 dirToEnemy = (col.gameObject.transform.position - transform.position).normalized;
            float angleToEnemy = Vector3.Angle(dirToEnemy, transform.forward);
            if (angleToEnemy > viewAngle / 2) { continue; }
            //Check if enemy is in line of sight
            RaycastHit hit;
            if (!Physics.Raycast(transform.position, dirToEnemy, out hit, viewDistance)) { continue; }
            if (hit.collider != col) { continue; }
            detectedEnemies.Add(col.gameObject.transform);
        }

        return detectedEnemies;
    }
    #endregion

    #region Shoot
    [Header("Shoot")]
    [SerializeField] ParticleSystem impactParticles;
    [SerializeField] int ammo = 100;
    [SerializeField] float shootRange;
    float nextShootTime;
    [SerializeField] float shootDelay;
    void Shoot(Vector3 target)
    {
        if (Time.time < nextShootTime) { return; }
        nextShootTime = Time.time + shootDelay;
        ammo--;
        if (Physics.Raycast(transform.position, target - transform.position, out RaycastHit hit, shootRange))
        {
            impactParticles.gameObject.transform.SetPositionAndRotation(hit.point, Quaternion.LookRotation(hit.normal));
            impactParticles.Play();
            if (hit.collider.CompareTag("Enemy")) { Debug.Log("Sheva hit enemy"); }
        }
    }

    #endregion

    #region Health
    public float maxHealth;
    float currentHealth;

    public void Damage(float amount)
    {
        currentHealth -= Mathf.Abs(amount);
    }

    const int HEAL_AMOUNT = 20;
    int remainingHeals = 5;
    #endregion
}

