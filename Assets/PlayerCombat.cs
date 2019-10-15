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

    public PlayerStats playerStats;

    public GameObject enemyTarget;
    public EnemyState enemyState;
    public EnemyStats enemyStats;

    public bool hitDetected;

    CharacterStats myStats;

    public enum Action
    {
        IDLE,
        ATTACK,
        MAGIC
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
            case (Action.MAGIC):
                PlayerCastMagic();
                actionState = Action.IDLE;
                break;

        }
    }

    //public void Attack(CharacterStats targetStats)
    //{
    //    if (attackCooldown <= 0f)
    //    {
    //        StartCoroutine(DoDamage(targetStats, attackDelay));

    //        if (onAttack != null)
    //            // OnAttack(); 
    //            attackCooldown = 1f / attackSpeed;
    //    }
    //}

    //IEnumerator DoDamage(CharacterStats stats, float delay)
    //{
    //    yield return new WaitForSeconds(delay);

    //    stats.TakeDamage(myStats.damage.GetValue());

    //}

    //public void PlayerAttack()
    //{
    //    //Debug.Log("Enable Hit, Attack Animation, Do Damage");
    //    hitDetection.enabled = true;
    //    hitCollider.enabled = true;
    //    //Debug.Log("Hit Detection:  " + hitDetection.enabled);
    //    //Debug.Log("Mesh Collider:  " + hitDetection.GetComponent<Collider>().enabled);
    //    //transform.GetComponentInChildren<HitDetection>().enabled = true;
    //    //animator.SetTrigger("attack");
    //    characterAnimator.Attack();
    //    //DoDamage(enemy, attackDelay);
    //    //if (hitDetection.hitDetected && hitDetection.enemyTarget)
    //    //{
    //    //enemyTarget = hitDetection.enemyTarget;
    //    ////StartCoroutine("DealDamage");
    //    //DealDamage();
    //    //}
    //    if (hitDetected == true)
    //    {
    //        enemyTarget = hitDetection.enemyTarget;
    //    }
    //    //StartCoroutine("DealDamage");
    //    //if (hitDetection.hitDetected && hitDetection.enemyTarget)
    //    //{
    //    //    enemyTarget = hitDetection.enemyTarget;
    //    //    //StartCoroutine("DealDamage");
    //    //    DealDamage();
    //    //}
    //    //else
    //    //{
    //    //    Debug.Log("no target");
    //    //}
    //    DealDamage();
    //    enemyTarget = null;
    //    //DealDamage();

    //    //hitDetection.enabled = false;
    //    //hitCollider.enabled = false;



    //}

    public void PlayerAttack()
    {
   
        hitDetection.enabled = true;
        hitCollider.enabled = true;
        
        characterAnimator.Attack();
        
        //if (hitDetected == true)
        //{
        //    enemyTarget = hitDetection.enemyTarget;
        //}
        
        //DealDamage();
        enemyTarget = null;


    }

    public void PlayerCastMagic()
    {
        //Debug.Log("Enable Hit, Attack Animation, Do Damage");
        //hitDetection.enabled = true;
        //hitCollider.enabled = true;
        //Debug.Log("Hit Detection:  " + hitDetection.enabled);
        //Debug.Log("Mesh Collider:  " + hitDetection.GetComponent<Collider>().enabled);
        //transform.GetComponentInChildren<HitDetection>().enabled = true;
        //animator.SetTrigger("attack");
        characterAnimator.Magic();
        //DoDamage(enemy, attackDelay);
        //if (hitDetection.hitDetected && hitDetection.enemyTarget)
        //{
        //    enemyTarget = hitDetection.enemyTarget;
        //    //StartCoroutine("DealDamage");
        //    DealDamage();
        //}

    }

    //public void DealDamage()
    //{
    //    if (hitDetection.hitDetected)
    //    {
    //        enemyTarget = hitDetection.enemyTarget;

    //        Debug.Log("Player Damages " + hitDetection.enemyTarget.name);
    //        // Debug.Log(enemyTarget.name);
    //        // enemy take damage
    //        hitDetection.hitDetected = false;

    //    }
    //    else
    //    {
    //        Debug.Log("no target");
    //    }
    //    enemyTarget = null;
    //}

    public void DealDamage(EnemyState enemyState)
    {
        //if (hitDetection.enemyTarget != null)
        //calculateDamage(enemyState);
        enemyState.enemyStats.TakeDamage(calculateDamage(enemyState));
        //Debug.Log("Player Damages " + hitDetection.enemyTarget.name + " for " + calculateDamage(enemyState) + " damage!");
         
    }

    public int calculateDamage(EnemyState enemyState)
    {
        //attackCooldown = 5f;
        //int damage = 1;
        float damage = (float)myStats.damage.GetValue() * (100f / (100f + (float)enemyState.enemyStats.armor.GetValue()));

        return (int)damage;
    }


    
}
