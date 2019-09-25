using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // Hero 
    public GameObject HeroCharacter;

    // Positions
    public Vector3 nextHeroPosition;
    public Vector3 lastHeroPosition;

    // Scenes
    public string sceneToLoad;
    public string lastScene;

    // Start is called before the first frame update
    void Awake()
    {
        // check if the instance exsist
        if (instance == null)
        {
            // if not set the instance to this
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
        if (!GameObject.Find("HeroCharacter"));
        {
            GameObject Hero = Instantiate(HeroCharacter, nextHeroPosition, Quaternion.identity) as GameObject;
            Hero.name = "HeroCharacter";
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
