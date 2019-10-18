using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    EnemyStats enemyStats;
    PlayerState playerState;
    //GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        enemyStats = GetComponentInParent<EnemyStats>();
        //player = GameObject.Find("Player");
        playerState = GameObject.Find("Player").GetComponent<PlayerState>();
  

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TriggerDamage()
    {
        //Debug.Log("Triggering damage from enemy");
        enemyStats.DealDamage(playerState);
    }

}
