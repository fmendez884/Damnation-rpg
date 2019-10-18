using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public GameObject enemyTarget;
    public EnemyState enemyState;

    public PlayerCombat playerCombat;

    
   
    void Start()
    {
        playerCombat = GameObject.Find("Player").GetComponent<PlayerCombat>();
    }

  
    void Update()
    {
        
    }





    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other);
        // Debug.Log("tag  " + other.tag);
        if (other.tag == "Enemy")
        {
            

            enemyState = other.gameObject.GetComponent<EnemyState>();

            //float attackTime = .23f;
            //float attackTime = 0f;

            playerCombat.DealDamage(enemyState);

          


        }
        else
            enemyTarget = null;
    }
    
}
