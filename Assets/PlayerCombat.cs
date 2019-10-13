using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    public float attackDelay = .6f;

    public event System.Action onAttack;

    public Animator animator;
    public CharacterAnimator characterAnimator;
    public HitDetection hitDetection;
    public Collider hitCollider;

    public GameObject enemyTarget;
    public EnemyState enemyState;
    public bool hitDetected;

    CharacterStats myStats;

    public enum Action
    {
        IDLE,
        ATTACK
    }

    public Action actionState;

    void Start()
    {
        myStats = GetComponent<CharacterStats>();
        animator = GetComponentInChildren<Animator>();
        characterAnimator = GetComponent<CharacterAnimator>();

        hitDetection = GetComponentInChildren<HitDetection>();
        hitCollider = hitDetection.GetComponent<Collider>();

        actionState = Action.IDLE;
        enemyTarget = null;
    }

    void Update()
    {
        attackCooldown -= Time.deltaTime;

        switch (actionState)
        {
            case (Action.IDLE):
                hitDetection.enabled = false;
                hitCollider.enabled = false;
                //Debug.Log("Hit Detection:  " + hitDetection.enabled);
                //Debug.Log("Mesh Collider:  " + hitDetection.GetComponent<Collider>().enabled);
                break;
            case (Action.ATTACK):
                //Debug.Log("Attack State");
                PlayerAttack();
                actionState = Action.IDLE;
            break;

        }
    }

    public void Attack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0f)
        {
            StartCoroutine(DoDamage(targetStats, attackDelay));

            if (onAttack != null)
                // OnAttack(); 
                attackCooldown = 1f / attackSpeed;
        }
    }

    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);

        stats.TakeDamage(myStats.damage.GetValue());

    }

    public void PlayerAttack()
    {
        //Debug.Log("Enable Hit, Attack Animation, Do Damage");
        hitDetection.enabled = true;
        hitCollider.enabled = true;
        //Debug.Log("Hit Detection:  " + hitDetection.enabled);
        //Debug.Log("Mesh Collider:  " + hitDetection.GetComponent<Collider>().enabled);
        //transform.GetComponentInChildren<HitDetection>().enabled = true;
        //animator.SetTrigger("attack");
        characterAnimator.Attack();
        //DoDamage(enemy, attackDelay);
        DealDamage();

    }

    public void DealDamage()
    {
        if (hitDetection.hitDetected)
        {
            enemyTarget = hitDetection.enemyTarget;
            Debug.Log("Player Damages " + hitDetection.enemyTarget.name);
            // Debug.Log(enemyTarget.name);
            // enemy take damage
            hitDetection.hitDetected = false;

        }
        else
        {
            Debug.Log("no target");
        }
        // enemyTarget = null;
    }
}
