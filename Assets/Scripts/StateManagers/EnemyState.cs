using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public Enemy enemy;
    public EnemyStats enemyStats;
    public EnemyController enemyController;
    public CharacterCombat characterCombat;
    public CharacterAnimator characterAnimator;
    

    public enum State
    {
        IDLE,
        ATTACK,
        DAMAGE,
        DEAD
    }

    public State enemyState;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        enemyStats = GetComponent<EnemyStats>();
        enemyController = GetComponent<EnemyController>();
        characterCombat = GetComponent<CharacterCombat>();
        characterAnimator = GetComponent<CharacterAnimator>();
        

    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyState)
        {
            case State.IDLE:
                break;
            case State.ATTACK:

                break;
            case State.DAMAGE:
                break;
            case State.DEAD:
                //agent.speed = 0;
                //enemyStats.Die();
                break;
        }
    }
}
