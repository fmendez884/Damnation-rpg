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

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other);
        // Debug.Log("tag  " + other.tag);
        if (other.tag == "Enemy")
        {
            hitDetected = true;
            Debug.Log("Hit Detected on  " + other);
            enemyTarget = other.gameObject;
            //enemyTarget.GetComponent<EnemyStats>().TakeDamage(10);
            enemyState = enemyTarget.GetComponent<EnemyState>();
            playerCombat.DealDamage(enemyState);

        }
        else
            enemyTarget = null;
    }
}
