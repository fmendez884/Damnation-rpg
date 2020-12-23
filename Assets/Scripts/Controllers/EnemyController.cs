using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;

    Transform target;
    NavMeshAgent agent;
    CharacterCombat combat;
    PlayerState playerState;
    EnemyStats enemyStats;

    CharacterAnimator characterAnimator;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();

        characterAnimator = GetComponent<CharacterAnimator>();

        playerState = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
        enemyStats = GetComponent<EnemyStats>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        
        if (distance <= lookRadius)
        {
            if (agent.isActiveAndEnabled)
            {
                agent.SetDestination(target.position);
            }
        }

        if (distance <= agent.stoppingDistance)
        {
            CharacterStats targetStats = target.GetComponent<CharacterStats>();
            if(targetStats != null)
            {
                //Debug.Log("Enemy is ready to attack");
                // Attack the target
                //combat.Attack(targetStats);
                AttackTarget();
                // Face the target
                FaceTarget();
            }
        }
    }

    void FaceTarget() 
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void AttackTarget()
    {
        characterAnimator.Attack();
    }


}

