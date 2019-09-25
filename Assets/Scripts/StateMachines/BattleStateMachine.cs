using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleStateMachine : MonoBehaviour
{
    public enum PerformAction
    {
        WAIT,
        TAKEACTION,
        PERFORMACTION,
    }

    public PerformAction battleStates;

    public List<HandleTurn> PerformList = new List<HandleTurn> ();

    public List<GameObject> HerosInBattle = new List<GameObject> ();
    public List<GameObject> EnemysInBattle = new List<GameObject> ();

    public enum HeroGUI
    {
        ACTIVATE,
        WAITING,
        INPUT1,
        INPUT2,
        DONE
    }

    public HeroGUI HeroInput;

    public List<GameObject> HerosToManage = new List<GameObject> ();
    private HandleTurn HeroChoice;
    
    public GameObject enemyButton;
    public Transform Spacer;

    public GameObject ActionPanel;
    public GameObject EnemySelectPanel;
    public GameObject MagicPanel;

    //magic attack
    public Transform actionSpacer;
    public Transform magicSpacer;
    public GameObject actionButton;
    public GameObject magicButton;
    private List<GameObject> atkBtns = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        battleStates = PerformAction.WAIT;
        EnemysInBattle.AddRange(GameObject.FindGameObjectsWithTag ("Enemy"));
        HerosInBattle.AddRange(GameObject.FindGameObjectsWithTag ("Hero"));
        HeroInput = HeroGUI.ACTIVATE;

        ActionPanel.SetActive (false);
        EnemySelectPanel.SetActive (false);
        MagicPanel.SetActive(false);

        EnemyButtons();
    }

    // Update is called once per frame
    void Update()
    {
        switch (battleStates) 
        {
            case (PerformAction.WAIT):
                if (PerformList.Count > 0)
                {
                    battleStates = PerformAction.TAKEACTION;
                }
            break;
            case (PerformAction.TAKEACTION):
                GameObject performer = GameObject.Find(PerformList[0].Attacker);
                if (PerformList[0].Type == "Enemy") 
                {
                    EnemyStateMachine ESM = performer.GetComponent<EnemyStateMachine> ();
                        for (int i = 0; i<HerosInBattle.Count; i++)
                        {
                            if (PerformList[0].AttackersTarget == HerosInBattle[i])
                            {
                                ESM.HeroToAttack = PerformList[0].AttackersTarget;
                                ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                                break;
                            }
                            else 
                            {
                                PerformList[0].AttackersTarget = HerosInBattle[Random.Range(0, HerosInBattle.Count)];
                                ESM.HeroToAttack = PerformList[0].AttackersTarget;
                                ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                            }
                        }
                    ESM.HeroToAttack = PerformList[0].AttackersTarget;
                    ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                }

                if (PerformList[0].Type == "Hero") 
                {
                    // Debug.Log ("Hero is here to perform");
                    HeroStateMachine HSM = performer.GetComponent<HeroStateMachine> ();
                    HSM.EnemyToAttack = PerformList[0].AttackersTarget;
                    HSM.currentState = HeroStateMachine.TurnState.ACTION;
                }

                battleStates = PerformAction.PERFORMACTION;

            break;
            case (PerformAction.PERFORMACTION):
            
            break;  
        }

        switch (HeroInput)
        {
            case (HeroGUI.ACTIVATE):
                if (HerosToManage.Count > 0)
                {
                    HerosToManage[0].transform.Find("Selector").gameObject.SetActive(true);
                    HeroChoice = new HandleTurn ();
                    
                    ActionPanel.SetActive(true);
                    //populate action buttons 
                    CreateAttackButtons();

                    HeroInput = HeroGUI.WAITING;
                }
            break;
            case (HeroGUI.WAITING):
                //idle
            break;
            case (HeroGUI.DONE):
                HeroInputDone();
            break;
        }
    }

    public void CollectActions(HandleTurn input) 
    {
        PerformList.Add(input);
    }

    void EnemyButtons()  //populate enemy select panel
    {
        foreach (GameObject enemy in EnemysInBattle)
        {
            GameObject newButton = Instantiate (enemyButton) as GameObject;
            EnemySelectButton button = newButton.GetComponent<EnemySelectButton> ();

            EnemyStateMachine cur_enemy = enemy.GetComponent<EnemyStateMachine> ();

            Text buttonText = newButton.transform.Find("Text").gameObject.GetComponent<Text> ();
            // Debug.Log("cur_enemy-", cur_enemy);
            // Debug.Log("cur_enemy.enemy-", cur_enemy.enemy);
            buttonText.text = cur_enemy.enemy.className;

            button.EnemyPrefab = enemy;
            newButton.transform.SetParent(Spacer, false);
        }
    }

    public void Input1() // attack buttton

    {
        HeroChoice.Attacker = HerosToManage[0].name;
        HeroChoice.AttackersGameObject = HerosToManage[0];
        HeroChoice.Type = "Hero";
        //once clicked, attack button, we will choose corresponding attack
            //if goingto add more attack to attack, you con choose a random one
                //or createpublic variable and put one specified attac into that spot
        HeroChoice.ChosenAttack = HerosToManage[0].GetComponent<HeroStateMachine>().hero.Attacks[0]; // or loop through the possible attacks ; grab one at random

        ActionPanel.SetActive(false);
        EnemySelectPanel.SetActive(true);
    }

    public void Input2(GameObject chosenEnemy) // enemy selection 
    {
        HeroChoice.AttackersTarget = chosenEnemy;
        HeroInput = HeroGUI.DONE;
    }

    void HeroInputDone()
    {
        PerformList.Add(HeroChoice);
        EnemySelectPanel.SetActive(false);

        //clean the AttackPanel
        foreach(GameObject atkBtn in atkBtns)
        {
            Destroy(atkBtn);
        }
        atkBtns.Clear();

        HerosToManage[0].transform.Find("Selector").gameObject.SetActive(false);
        HerosToManage.RemoveAt(0);
        HeroInput = HeroGUI.ACTIVATE;
    }

    //create actionButtons
    void CreateAttackButtons()
    {
        GameObject AttackButton = Instantiate(actionButton) as GameObject;
        Text AttackButtonText = AttackButton.transform.Find("Text").gameObject.GetComponent<Text>();
        AttackButtonText.text = "Attack";
        AttackButton.GetComponent<Button>().onClick.AddListener(() => Input1());
        AttackButton.transform.SetParent(actionSpacer, false);
        atkBtns.Add(AttackButton);

        GameObject MagicButton = Instantiate(actionButton) as GameObject;
        Text MagicButtonText = MagicButton.transform.Find("Text").gameObject.GetComponent<Text>();
        MagicButtonText.text = "Magic";
        MagicButton.GetComponent<Button>().onClick.AddListener(() => Input3());
        MagicButton.transform.SetParent(actionSpacer, false);
        atkBtns.Add(MagicButton);

        if(HerosToManage[0].GetComponent<HeroStateMachine>().hero.MagicAttacks.Count > 0)
        {
            foreach(BaseAttack magicAtk in HerosToManage[0].GetComponent<HeroStateMachine>().hero.MagicAttacks)
            {
                Debug.Log(magicAtk);
                GameObject SpellButton =  Instantiate(magicButton) as GameObject;
                Text SpellButtonText = SpellButton.transform.Find("Text").gameObject.GetComponent<Text>();
                SpellButtonText.text = magicAtk.attackName;

                AttackButton ATB = SpellButton.GetComponent<AttackButton>();
                ATB.magicAttackToPerform = magicAtk;
                SpellButton.transform.SetParent(magicSpacer, false);
                atkBtns.Add(SpellButton);
            }
        }
        else
        {
            MagicButton.GetComponent<Button>().interactable = false;
        }
    }

    public void Input3() // switching to magic attacks
    {
        ActionPanel.SetActive(false);
        MagicPanel.SetActive(true);
    }
    public void Input4(BaseAttack chosenMagic)
    {
        HeroChoice.Attacker = HerosToManage[0].name;
        HeroChoice.AttackersGameObject = HerosToManage[0];
        HeroChoice.Type = "Hero";
        Debug.Log("Magic attack");
        HeroChoice.ChosenAttack = chosenMagic;

        MagicPanel.SetActive(false);
        EnemySelectPanel.SetActive(true);
    }


}
