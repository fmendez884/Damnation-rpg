using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : CharacterStats
{

    
    public TargetController targetController;

    //public targetController1 targetController;
    public CharacterAnimator characterAnimator;

    public float deathTime = 5f;

    NavMeshAgent agent;

    EnemyStats enemyStats;

    //PlayerState playerState;
    //PlayerStats playerStats;

    private void Start()
    {
        
        targetController = GameObject.Find("TargetSystem").GetComponentInChildren<TargetController>();

        

        //targetController = GameObject.Find("GameManager").GetComponentInChildren<targetController1>();
        //targetController = GameObject.Find("GameManager").GetComponentInChildren<TargetController>();

        characterAnimator = GetComponent<CharacterAnimator>();
        //enemyGameObject = this.gameObject;
        enemyStats = GetComponent<EnemyStats>();

        agent = GetComponent<NavMeshAgent>();
        //playerState = GameObject.Find("Player").GetComponent<PlayerState>();
        //playerStats = playerState.playerStats;

    }

    public override void TakeDamage(int damage)
    {
        //base.TakeDamage(damage);

        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, int.MaxValue);

        //Debug.Log("Player Damages " + name + " for " + damage + " damage!");
        //Debug.Log(name + " HP: " + currentHealth);

        if (currentHealth <= 0 || currentHealth == 0)
        {
            Death();
        }

        else
        {
            characterAnimator.Damage();

            //damage = Mathf.Clamp(damage, 0, int.MaxValue);
            //currentHealth -= damage;
            //Debug.Log("Player Damages " + name + " for " + damage + " damage!");
            //Debug.Log(name + " HP: " + currentHealth);

        }

    }

    public void DealDamage(PlayerState playerState)
    {
       

            float calcDamage = (float)enemyStats.damage.GetValue() * (100f / (100f + (float)playerState.playerStats.armor.GetValue()));
            playerState.playerStats.TakeDamage((int)calcDamage);


        //Debug.Log("Enemy Attacks");        
    }


    

    public override void Death()
    {
        base.Death();
        //float deathTime = 5f;

        targetController.target = null;
        TargetController.nearByEnemies.Clear();
        //targetController1.nearByEnemies.Remove(gameObject);


        StartCoroutine(DeathSequence());

        

        IEnumerator DeathSequence()
        {
            
            characterAnimator.Death();
            //gameObject.Disable();
            agent.speed = 0;

            // Start function WaitAndPrint as a coroutine. And wait until it is completed.
            // the same as yield WaitAndPrint(2.0);
            yield return new WaitForSeconds(deathTime);
            //print("Done " + Time.time);

            // drop loot
            gameObject.SetActive(false);
            Debug.Log($"{characterName} has died.");
            EnemyList enemyList = GameObject.FindGameObjectWithTag("GameController").GetComponent<EnemyList>();
            List<GameObject> enemies = enemyList.enemies;
            enemies.Remove(gameObject);
            Debug.Log($"{enemies.Count} enemies remaining");

            if (enemies.Count <= 0 )
            {
                enemyList.isCleared();

            }

        }


    }




}
