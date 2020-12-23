using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] private GameObject deathMenuUI = null;
    public bool Dead = false;
    public AudioSource menuConfirm;
    public AudioSource menuCancel;
    public GameObject controlsPanel;
    public enum State
    {
        Game,
        Dead,
        ControlsPanel
    }
    public State menuState;

    //public bool IsPaused { get; private set; }

    // Start is called before the first frame update
    void Start()
    {

        Dead = false;
        menuState = State.Game;
        deathMenuUI.SetActive(false);
        menuConfirm.ignoreListenerPause = true;
        menuCancel.ignoreListenerPause = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Paused = !Paused;
            //SwitchMenuState();

            switch (menuState)
            {
                case State.Game:

                    menuConfirm.Play();
                    ActivateMenu();
                    break;
                case State.Dead:
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
            case State.Dead:
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
        deathMenuUI.SetActive(true);
        menuState = State.Dead;
    }

    public void DeactivateMenu()
    {
        menuCancel.Play();
        Time.timeScale = 1;
        AudioListener.pause = false;
        deathMenuUI.SetActive(false);
        menuState = State.Game;
    }

    public void SwitchMenuState()
    {
        Dead = !Dead;
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
        menuState = State.Dead;
    }
}
