using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public GameObject enemyTarget;
    public bool hitDetected;
    //public CharacterStats enemyStats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        // Debug.Log(other);
        // Debug.Log("tag  " + other.tag);
        if (other.tag == "Enemy" )
        {
            hitDetected = true;
            Debug.Log("Hit Detected on  " + other);
            enemyTarget = other.gameObject;
            enemyTarget.GetComponent<EnemyStats>().TakeDamage(10);

        }
    }
}
