using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    public float attackDelay = .6f;

    //public event System.Action onAttack;

    public CharacterController characterController;

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

        characterController = GetComponent<CharacterController>();

        actionState = Action.IDLE;
        enemyTarget = null;
    }

    void Update()
    {
        attackCooldown -= Time.deltaTime;

        switch (actionState)
        {
            case (Action.IDLE):
                
                break;
            case (Action.ATTACK):
                //Debug.Log("Attack State");

                PlayerAttack();
                actionState = Action.IDLE;
                //characterController.enabled = true;
                break;
            case (Action.MAGIC):
                PlayerCastMagic();
                actionState = Action.IDLE;
                break;

        }
    }

 

    public void PlayerAttack()
    {
        characterController.enabled = false;
        characterAnimator.Attack();
        
      
        enemyTarget = null;

        characterController.enabled = true;
    }

    public void PlayerCastMagic()
    {
        
        characterAnimator.Magic();
     
    }

   

    public void DealDamage(EnemyState enemyState)
    {

        float damage = (float)myStats.damage.GetValue() * (100f / (100f + (float)enemyState.enemyStats.armor.GetValue()));
        enemyState.enemyStats.TakeDamage((int)damage);
 

    }

  
    
}
