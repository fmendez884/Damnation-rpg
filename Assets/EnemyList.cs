using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> enemiesSlain = new List<GameObject>();
    public bool cleared;
    public Score score;
    public WinMenu winMenu;
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        player = GetComponent<PlayerManager>().player;
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        score = GetComponentInChildren<Score>();

        //winMenu = GetComponentInChildren<WinMenu>();

        //enemies.ForEach(delegate (GameObject enemy)
        //{
        //    enemy.GetComponent<EnemyStats>().currentHealth = 1;
        //});
    }

    // Update is called once per frame
    void Update()
    {
        score.tmp.text = ((int)score.score).ToString();

        //if(Input.GetKeyDown(KeyCode.V))
        //{
        //    enemies.ForEach(delegate (GameObject enemy)
        //    {
        //        var enemyStats = enemy.GetComponent<EnemyStats>();
        //        enemyStats.TakeDamage(enemyStats.maxHealth);
        //    });
        //}

        //if(Input.GetKeyDown(KeyCode.B)) 
        //{
        //    player.GetComponent<PlayerStats>().Death();
        //}

    }

    public void isCleared()
    {
        cleared = true;
        if (player.GetComponent<PlayerStats>().currentHealth > 0 )
        {
            winMenu.OnWin();
        }
    }

    public void evaluateSlainEnemy(GameObject enemy)
    {
        var enemyValues = new Dictionary<string, float>(){
            {"Imp", 166.6f},
            {"Mesmerize", 333f},
            {"Marlboro", 499.9f},
            { "Diabolos", 666f }
        };

        enemiesSlain.Add(enemy);
        score.score += enemyValues[enemy.GetComponent<EnemyStats>().characterName];
    }

    public void updateEnemyList(GameObject enemy)
    {
        enemies.Remove(enemy);
        //Debug.Log($"{enemies.Count} enemies remaining");

        evaluateSlainEnemy(enemy);

        if (enemies.Count <= 0)
        {
            isCleared();

        }
    }

}

//public class EnemyValues
//{
//    public static float Imp = 166.6f;
//    public static float Mesmerize = 333f;
//    public static float Marlboro = 499.9f;
//    public static float Diabolos= 666f;


//}