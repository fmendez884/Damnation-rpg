using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public PlayerCombat playerCombat;
    public HitDetection hitDetection;
    public Collider hitCollider;
    //public EnemyStats enemyStats;
    // Start is called before the first frame update
    void Start()
    {
        //playerCombat = GetComponentInParent<PlayerCombat>();

        //hitDetection = GetComponentInChildren<HitDetection>();
        //hitCollider = hitDetection.GetComponent<Collider>();
        //enemyStats = playerCombat.enemyStats;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void playerCombatDealDamage()
    //{
    //    playerCombat.DealDamage(playerCombat.enemyState);
    //}

    public void enableHitDetection()
    {
        //hitDetection.enabled = true;
        //hitCollider.enabled = true;

        //Debug.Log("Detection Enabled: " + hitDetection.enabled + " Collider enabled: " + hitCollider.enabled);
    }

    public void disableHitDetction()
    {
        //hitDetection.enabled = false;
        //hitCollider.enabled = false;

        //Debug.Log("Detection Disabled: " + hitDetection.enabled + " Collider disabled: " + hitCollider.enabled);
    }


}
