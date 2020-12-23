using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> enemies = new List<GameObject>();
    public bool cleared;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

        //enemies.ForEach(delegate (GameObject enemy)
        //{
        //    enemy.GetComponent<EnemyStats>().currentHealth = 1;
        //});
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void isCleared()
    {
        cleared = true;
        Debug.Log("All enemies are dead.");
        if (player.GetComponent<PlayerStats>().currentHealth > 0 )
        {
            Debug.Log("You fahken win mayt. Good fahken johb");
        }
    }
}
