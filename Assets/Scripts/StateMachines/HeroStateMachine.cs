using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroStateMachine : MonoBehaviour
{
    private BattleStateMachine BSM;
    public BaseHero hero;

    public enum TurnState
    {
        PROCESSING,   // THE PROGRESS OF WHEN THE HERO CAN MAKE AN ACTION
        ADDTOLIST,   // ADD THIS HERO TO A LIST
        WAITING,    // IDLE STATE
        SELECTING, // WHEN THE PLAYER IS GOING TO SELECT THE ACTION
        ACTION,   // USED TO DO ACTION 
        DEAD
    }

    public TurnState currentState;
    //for the ProgressBar
    private float cur_cooldown = 0f;
    private float max_cooldown = 5f;
    private Image ProgressBar;
    public GameObject Selector;
    //Ienumerator
    public GameObject EnemyToAttack;
    private bool actionStarted = false;
    private Vector3 startposition;
    private float animSpeed = 10f;
    private bool alive = true;
    //heroPanel
    private HeroPanelStats stats;
    public GameObject HeroPanel;
    private Transform HeroPanelSpacer;



    // Start is called before the first frame update
    void Start()
    {
        //find spacer object in gamescene, make connection
        HeroPanelSpacer = GameObject.Find("BattleCanvas").transform.FindChild("HeroPanel").transform.FindChild("HeroPanelSpacer");
        //create panel, fill in info of corresponding hero
        CreateHeroPanel();

        startposition = transform.position;
        cur_cooldown = Random.Range(0, 2.5f);
        Selector.SetActive(false);
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        currentState = TurnState.PROCESSING;
        ProgressBar.GetComponent<Image>().color = new Color32(88,123,225,150);
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
            case (TurnState.ADDTOLIST):
                BSM.HerosToManage.Add(this.gameObject);
                currentState = TurnState.WAITING;
            break;
            case (TurnState.WAITING):
                //idle
            break;
            // case (TurnState.SELECTING):

            // break;
            case (TurnState.ACTION):
                StartCoroutine(TimeForAction ());
            break;
            case (TurnState.DEAD):
                if (!alive)
                {
                    return;
                }
                else 
                {
                    // change tag
                    this.gameObject.tag = "DeadHero";
                    // not attackable by enemy
                    BSM.HerosInBattle.Remove(this.gameObject);
                    // not managable
                    BSM.HerosToManage.Remove(this.gameObject);
                    // deactivate selector
                    Selector.SetActive(false);
                    //reset gui
                    BSM.ActionPanel.SetActive(false);
                    BSM.EnemySelectPanel.SetActive(false);
                    //remove item from performList 
                    if (BSM.HerosInBattle.Count > 0);
                    {
                        for(int i = 0; i < BSM.PerformList.Count; i++)
                        {
                            if (i != 0)
                            {
                                if (BSM.PerformList[i].AttackersGameObject == this.gameObject)
                                {
                                    BSM.PerformList.Remove(BSM.PerformList[i]);
                                }
                                
                                if (BSM.PerformList[i].AttackersTarget == this.gameObject)
                                {
                                    BSM.PerformList[i].AttackersTarget = BSM.HerosInBattle[Random.Range(0, BSM.HerosInBattle.Count)];
                                }
                            }
                        }
                        //change color / dead animation
                        this.gameObject.GetComponent<MeshRenderer>().material.color = new Color32(105,105,105,105);
                        //reset heroInput  //// BSM.HeroInput = BattleStateMachine.HeroGUI.ACTIVATE;
                        BSM.battleStates = BattleStateMachine.PerformAction.CHECKALIVE;
                        alive = false;
                    }
                }
            break;
        }
    }

    void UpgradeProgressBar()
    {
        ProgressBar.GetComponent<Image>().color = new Color32(80,80,150,150);
        cur_cooldown = cur_cooldown + Time.deltaTime;
        float calc_cooldown = cur_cooldown / max_cooldown;
        ProgressBar.transform.localScale = new Vector3(Mathf.Clamp(calc_cooldown, 0, 1), ProgressBar.transform.localScale.y, ProgressBar.transform.localScale.z);
        if (cur_cooldown >= max_cooldown) 
        {
            ProgressBar.GetComponent<Image>().color = new Color32(250,255,180,240);  //before: 250,255,131,240)
            currentState = TurnState.ADDTOLIST;
            // Debug.Log(currentState);
        }
    }

    private IEnumerator TimeForAction() 
    {
        if (actionStarted) 
        {
            yield break;
        }

        actionStarted = true; 

        //animate the enemy near the hero to attack
        Vector3 enemyPosition = new Vector3(EnemyToAttack.transform.position.x - 1.5f, EnemyToAttack.transform.position.y, EnemyToAttack.transform.position.z);
        while (MoveTowardsEnemy(enemyPosition)) { yield return null; }

        //wait a bit 
        yield return new WaitForSeconds(0.5f);
        //do damage 
        DealDamage();
        //animate back to startposition 
        Vector3 firstPosition = startposition;
        while (MoveTowardsStart(firstPosition)) { yield return null; }

        //remove this performer from the list in BSM
        BSM.PerformList.RemoveAt(0); // the item at the 0 position will be removed
            //reset BSM -> wait 
        if(BSM.battleStates != BattleStateMachine.PerformAction.WIN && BSM.battleStates != BattleStateMachine.PerformAction.LOSE)
        {
            BSM.battleStates = BattleStateMachine.PerformAction.WAIT;
            //end coroutine
            // reset this enemy state 
            cur_cooldown = 0f;
            currentState = TurnState.PROCESSING;
        }
        else 
        {
            currentState = TurnState.WAITING;
        }
            actionStarted = false;
    }

    private bool MoveTowardsEnemy(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    private bool MoveTowardsStart(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    public void TakeDamge(float getDamageAmount) 
    {
        hero.curHP -= getDamageAmount;
        if(hero.curHP <= 0)
        {
            hero.curHP = 0;
            currentState = TurnState.DEAD;
        }
        UpdateHeroPanel();
    }

    //deal damage 
    void DealDamage()
    {
        float calc_damage = hero.curATK + BSM.PerformList[0].ChosenAttack.attackDamage;
        EnemyToAttack.GetComponent<EnemyStateMachine>().TakeDamage(calc_damage);
        Debug.Log(this.gameObject.name + " has chosen " + BSM.PerformList[0].ChosenAttack.attackName + " and does " + calc_damage + " damage!");
    }
    
    //create hero panel
    void CreateHeroPanel()
    {
        HeroPanel = Instantiate(HeroPanel) as GameObject;
        stats = HeroPanel.GetComponent<HeroPanelStats>();
        stats.HeroName.text = hero.className;
        stats.HeroHP.text = "HP: " + hero.curHP + "/" + hero.baseHP;
        stats.HeroMP.text = "MP: " + hero.curMP + "/" + hero.baseMP;
        ProgressBar = stats.ProgressBar;
        HeroPanel.transform.SetParent(HeroPanelSpacer, false);
    }

    //update stats on damage / heal
    void UpdateHeroPanel()
    {
        stats.HeroHP.text = "HP: " + hero.curHP + "/" + hero.baseHP;
        stats.HeroMP.text = "MP: " + hero.curMP + "/" + hero.baseMP;
    }
}