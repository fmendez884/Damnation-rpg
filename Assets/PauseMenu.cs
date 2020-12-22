using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI = null;
    public bool Paused = false;
    public AudioSource menuConfirm;
    public AudioSource menuCancel;
    public GameObject controlsPanel;
    public enum State
    {
        Game,
        Paused,
        ControlsPanel
    }
    public State menuState;

    //public bool IsPaused { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Paused = false;
        menuState = State.Game;
        pauseMenuUI.SetActive(false);
        menuConfirm.ignoreListenerPause = true;
        menuCancel.ignoreListenerPause = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //Paused = !Paused;
            //SwitchMenuState();

            switch (menuState)
            {
                case State.Game:

                    menuConfirm.Play();
                    ActivateMenu();
                    break;
                case State.Paused:
                    DeactivateMenu();
                    break;
                case State.ControlsPanel:
                    CloseControlsPanel();
                    break;

            }
        }

        switch (menuState)
        {
            case State.Game:
                break;
            case State.Paused:
                ActivateMenu();
                break;
            case State.ControlsPanel:
                
                break;

        }


    }

    public void ActivateMenu()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        pauseMenuUI.SetActive(true);
        menuState = State.Paused;
    }

    public void DeactivateMenu()
    {
        menuCancel.Play();
        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseMenuUI.SetActive(false);
        menuState = State.Game;
    }

    public void SwitchMenuState()
    {
        Paused = !Paused;
    }

    public void OpenControlsPanel()
    {
        menuConfirm.Play();
        controlsPanel.SetActive(true);
        menuState = State.ControlsPanel;
    }

    public void CloseControlsPanel()
    {
        menuCancel.Play();
        controlsPanel.SetActive(false);
        menuState = State.Paused;
    }
}
