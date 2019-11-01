using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    EnemyStats enemyStats;
    PlayerState playerState;
    PlayerController playerController;
    //GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        enemyStats = GetComponentInParent<EnemyStats>();
        //player = GameObject.Find("Player");
        playerState = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
  

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

    void IdleControllerState()
    {
        playerController.controllerState = PlayerController.Controller.IDLE;
    }

}
