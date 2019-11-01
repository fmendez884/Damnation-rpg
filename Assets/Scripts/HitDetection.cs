using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    //public GameObject enemyTarget;
   
    public EnemyState enemyTarget;
    public EnemyState enemyState;

    public PlayerState playerState;
    public PlayerCombat playerCombat;

    
   
    void Start()
    {
        playerCombat = GetComponentInParent<PlayerCombat>();
        playerState = playerCombat.GetComponentInParent<PlayerState>();
        //if (transform.root.gameObject.tag == "Enemy")
        //{
        //    enemyState = GetComponentInParent<EnemyState>();
        //}
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
            

            enemyTarget = other.gameObject.GetComponent<EnemyState>();

            //float attackTime = .23f;
            //float attackTime = 0f;

            playerCombat.DealDamage(enemyTarget);

          


        }
        //else if (other.tag == "Player")
        //{
        //    enemyState.enemyStats.DealDamage(playerState);
        //}
        //else
            //enemyTarget = null;
    }
    
}
