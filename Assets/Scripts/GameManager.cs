using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public RegionData curRegion;

    // SpawnPoints
    public string nextSpawnPoint;
    
    // Hero 
    public GameObject HeroCharacter;

    // Positions
    public Vector3 nextHeroPosition;
    public Vector3 lastHeroPosition;

    // Scenes
    public string sceneToLoad;
    public string lastScene;
    //bools
    public bool isWalking = false;
    public bool canGetEncounter = false;
    public bool gotAttacked = false;

    // Enum
    public enum GameStates
    {
        WORLD_STATE,
        TOWN_STATE,
        BATTLE_STATE,
        IDLE
    }
    public GameStates gameState;

    // BATTLE
    public List<GameObject> enemysToBattle = new List<GameObject>();
    public int enemyAmount;

    // Start is called before the first frame update
    void Awake()
    {
        // check if the instance exsist
        if (instance == null)
        {
            // if not set the instance to this
            Debug.Log("null reference????");
            instance = this;
        }
        // if it exists but is not this instance
        else if (instance != this)
        {
            //destroy it
            Destroy(gameObject);
        }
        // set this to be not destroyable
        DontDestroyOnLoad(gameObject);
        //if (!GameObject.Find("HeroCharacter"));
        //{
        //    GameObject Hero = Instantiate(HeroCharacter, nextHeroPosition, Quaternion.identity) as GameObject;
        //    Hero.name = "HeroCharacter";
        //}
    }

    void Update()
    {
        switch(gameState)
        {
            case (GameStates.WORLD_STATE):
                if(isWalking)
                {
                    RandomEncounter();
                    if (gotAttacked)
                    {
                        gameState = GameStates.BATTLE_STATE;
                    }
                }
            break;
            case (GameStates.TOWN_STATE):

            break;
            case (GameStates.BATTLE_STATE):
                //LOAD BATTLE SCENE
                StartBattle();
                //GO TO IDLE
                gameState = GameStates.IDLE;
            break;
            case (GameStates.IDLE):

            break;  
        }
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadSceneAfterBattle()
    {
        SceneManager.LoadScene(lastScene);
    }

    void RandomEncounter()
    {
        if (isWalking && canGetEncounter)
        {
            if(Random.Range(0,1000) < 10)
            {
                Debug.Log("I got attacked");
                gotAttacked = true;
            }
        }
    }

    void StartBattle()
    {
        // AMOUNT OF ENEMYS
        enemyAmount = Random.Range(1, curRegion.maxAmountEnemys+1);
        // WHICH ENEMYS 
        for (int i = 0; i < enemyAmount; i++)
        {
            enemysToBattle.Add(curRegion.possibleEnemys[Random.Range(0, curRegion.possibleEnemys.Count)]);
        }
        //HERO
        //lastHeroPosition = GameObject.Find("HeroCharacter").gameObject.transform.position;
        nextHeroPosition = lastHeroPosition;
        lastScene = SceneManager.GetActiveScene().name;
        //RESET HERO
        isWalking = false;
        gotAttacked = false;
        canGetEncounter = false;
        //LOAD LEVEL
        SceneManager.LoadScene(curRegion.BattleScene);
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
