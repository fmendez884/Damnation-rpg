using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public GameObject enemyTarget;
    public EnemyState enemyState;

    public PlayerCombat playerCombat;

    public bool hitDetected;
    //public CharacterStats enemyStats;
    // Start is called before the first frame update
    void Start()
    {
        playerCombat = GameObject.Find("Player").GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnTriggerEnter(Collider other) 
    //{
    //    // Debug.Log(other);
    //    // Debug.Log("tag  " + other.tag);
    //    if (other.tag == "Enemy")
    //    {
    //        hitDetected = true;
    //        Debug.Log("Hit Detected on  " + other);
    //        enemyTarget = other.gameObject;
    //        //enemyTarget.GetComponent<EnemyStats>().TakeDamage(10);
    //        enemyState = enemyTarget.GetComponent<EnemyState>();
    //        playerCombat.DealDamage(enemyState.enemyStats);

    //    }
    //    else
    //        enemyTarget = null;
    //    if (other.tag == "Enemy" && enemyTarget!= null)
    //    {
    //        hitDetected = true;
    //        Debug.Log("Hit Detected on  " + other);
    //        enemyTarget = other.gameObject;
    //        //enemyTarget.GetComponent<EnemyStats>().TakeDamage(10);
    //        playerCombat.DealDamage(enemyTarget.GetComponent<EnemyStats>());

    //    }
    //    else
    //        enemyTarget = null;
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    // Debug.Log(other);
    //    // Debug.Log("tag  " + other.tag);
    //    if (other.tag == "Enemy")
    //    {
    //        hitDetected = true;
    //        Debug.Log("Hit Detected on  " + other);
    //        enemyTarget = other.gameObject;
    //        //enemyTarget.GetComponent<EnemyStats>().TakeDamage(10);
    //        enemyState = enemyTarget.GetComponent<EnemyState>();
    //        playerCombat.DealDamage(enemyState);

    //    }
    //    else
    //        enemyTarget = null;
    //}



    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other);
        // Debug.Log("tag  " + other.tag);
        if (other.tag == "Enemy")
        {
            //hitDetected = true;
            //Debug.Log("Hit Detected on  " + other);
            //enemyTarget = other.gameObject;
            //enemyTarget.GetComponent<EnemyStats>().TakeDamage(10);
            //enemyState = enemyTarget.GetComponent<EnemyState>();

            enemyState = other.gameObject.GetComponent<EnemyState>();

            //float attackTime = .23f;
            //float attackTime = 0f;

            playerCombat.DealDamage(enemyState);

            // Add ragdoll effect / death animation
            //characterAnimator.Death();
            //targetController.removeTarget();;
            //targetController.target = null;
            //targetController.nearByEnemies.Remove(gameObject);

            //StartCoroutine(DamageSequence());

            //IEnumerator DamageSequence()
            //{
            //    // - After 0 seconds, prints "Starting 0.0"
            //    // - After 2 seconds, prints "WaitAndPrint 2.0"
            //    // - After 2 seconds, prints "Done 2.0"
            //    //characterAnimator.Death();

            //    // Start function WaitAndPrint as a coroutine. And wait until it is completed.
            //    // the same as yield WaitAndPrint(2.0);
            //    yield return new WaitForSeconds(attackTime * Time.deltaTime);
            //    //print("Done " + Time.time);
            //    playerCombat.DealDamage(enemyState);

            //}


        }
        else
            enemyTarget = null;
    }
    
}
