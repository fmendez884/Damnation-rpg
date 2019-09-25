using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    private BattleStateMachine BSM;
    public BaseEnemy enemy;

    public enum TurnState
    {
        PROCESSING,   // THE PROGRESS OF WHEN THE HERO CAN MAKE AN ACTION
        CHOOSEACTION,   // ADD THIS HERO TO A LIST
        WAITING,    // IDLE STATE 
        ACTION,   // USED TO DO ACTION 
        DEAD
    }

    public TurnState currentState;
    //for the ProgressBar
    private float cur_cooldown = 0f;
    private float max_cooldown = 5f;
    // public Image ProgressBar;
    //this gameObject
    private Vector3 startposition;
    //timeforaction stuff 
    private bool actionStarted = false;
    public GameObject HeroToAttack ;
    private float animSpeed = 10f;
    public GameObject Selector;
    //alive 
    private bool alive = true;


    // Start is called before the first frame update
    void Start()
    {
        Selector.SetActive(false);
        currentState = TurnState.PROCESSING;
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine> ();
        startposition = transform.position;
        // Debug.Log(startposition, transform);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(currentState);
        switch(currentState)
        {
            case (TurnState.PROCESSING):
                UpgradeProgressBar();
            break;
            case (TurnState.CHOOSEACTION):
                ChooseAction();
                currentState = TurnState.WAITING;
            break;
            case (TurnState.WAITING):
                //idle state
            break;
            case (TurnState.ACTION):
                StartCoroutine (TimeForAction ());
            break;
            case (TurnState.DEAD):
                if (!alive)
                {
                    return;
                }
                else 
                {
                    //change tag of enemy
                    this.gameObject.tag = "DeadEnemy";
                    //not attackable by heros; remove from battle state machine
                    BSM.EnemysInBattle.Remove(this.gameObject);
                    //disable the selector
                    // Selector.SetActive = false;
                    //remove all inputs for enemy attacks 
                    if (BSM.EnemysInBattle.Count > 0)
                    {
                        for (int i = 0; i < BSM.PerformList.Count; i++)
                        {
                            if(BSM.PerformList[i].AttackersGameObject == this.gameObject)
                            {
                                BSM.PerformList.Remove(BSM.PerformList[i]);
                            }
                            if(BSM.PerformList[i].AttackersTarget == this.gameObject)
                            {
                                BSM.PerformList[i].AttackersTarget = BSM.EnemysInBattle[Random.Range(0, BSM.EnemysInBattle.Count)];
                            }
                        }
                    }
                    //change color to gray / play dead animation
                    this.gameObject.GetComponent<MeshRenderer>().material.color = new Color32(105,105,105,250);
                    alive = false;
                    //reset enemy buttons
                    BSM.EnemyButtons();
                    //check alive
                    BSM.battleStates = BattleStateMachine.PerformAction.CHECKALIVE;
                }
            break;
        }
    }

    void UpgradeProgressBar()
    {
        cur_cooldown = cur_cooldown + Time.deltaTime;
        // float calc_cooldown = cur_cooldown / max_cooldown;
        // ProgressBar.transform.localScale = new Vector3(Mathf.Clamp(calc_cooldown, 0, 1), ProgressBar.transform.localScale.y, ProgressBar.transform.localScale.z);
        if (cur_cooldown >= max_cooldown) 
        {
            currentState = TurnState.CHOOSEACTION;
            // Debug.Log(currentState);
        }
    }

    void ChooseAction()
    {
        HandleTurn myAttack = new HandleTurn();
        myAttack.Attacker = enemy.className;
        myAttack.Type = "Enemy";
        myAttack.AttackersGameObject = this.gameObject;
        myAttack.AttackersTarget = BSM.HerosInBattle[Random.Range (0, BSM.HerosInBattle.Count)];
        
        int num = Random.Range(0, enemy.Attacks.Count);
        myAttack.ChosenAttack = enemy.Attacks[Random.Range(0, num)];

        Debug.Log(this.gameObject.name + " has chosen " + myAttack.ChosenAttack.attackName + " and does " + myAttack.ChosenAttack.attackDamage + " damage!");
        BSM.CollectActions (myAttack);
    }

    private IEnumerator TimeForAction() 
    {
        if (actionStarted) 
        {
            yield break;
        }

        actionStarted = true; 

        //animate the enemy near the hero to attack
        Vector3 heroPosition = new Vector3(HeroToAttack.transform.position.x + 1.5f, HeroToAttack.transform.position.y, HeroToAttack.transform.position.z);
        while (MoveTowardsEnemy(heroPosition)) { yield return null; }

        //wait a bit 
        yield return new WaitForSeconds(0.5f);
        //do damage 
        DoDamage();
        //animate back to startposition 
        Vector3 firstPosition = startposition;
        while (MoveTowardsStart(firstPosition)) { yield return null; }

        //remove this performer from the list in BSM
        BSM.PerformList.RemoveAt(0); // the item at the 0 position will be removed
        //reset BSM -> wait 
        BSM.battleStates = BattleStateMachine.PerformAction.WAIT;
        //end coroutine
        actionStarted = false;
        // reset this enemy state 
        cur_cooldown = 0f;
        currentState = TurnState.PROCESSING;
    }

    private bool MoveTowardsEnemy(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    private bool MoveTowardsStart(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    public void TakeDamage(float GetDamageAmount)
    {
        enemy.curHP -= GetDamageAmount;
        if(enemy.curHP <= 0)
        {
            enemy.curHP = 0;
            currentState = TurnState.DEAD;
        }

    }

    void DoDamage()
    {   
        float calc_damage = enemy.curATK + BSM.PerformList[0].ChosenAttack.attackDamage;
        HeroToAttack.GetComponent<HeroStateMachine>().TakeDamge(calc_damage);
    }

}