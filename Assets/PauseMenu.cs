using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    public bool Paused = false;

    //public bool IsPaused { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        //Paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //Paused = !Paused;
            SwitchMenuState();
        }

        if(Paused)
        {
            ActivateMenu();
        }

        else
        {
            DeactivateMenu();
        }
    }

    public void ActivateMenu()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        pauseMenuUI.SetActive(true);
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseMenuUI.SetActive(false);
    }

    public void SwitchMenuState()
    {
        Paused = !Paused;
    }
}
