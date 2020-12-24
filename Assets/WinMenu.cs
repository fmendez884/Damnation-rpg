using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    [SerializeField] private GameObject winMenuUI = null;
    public bool Win = false;
    public AudioSource menuConfirm;
    public AudioSource menuCancel;
    public AudioSource menuError;
    public AudioSource bellTome;
    public AudioSource fanfare;
    public GameObject controlsPanel;
    public GameObject leaderPanel;
    public GameObject warningPanel;
    public CanvasGroup canvasGroup;
    public float Duration = 0.4f;
    public GameObject gameManager;
    public PauseMenu pauseMenu;
    public TimeDisplay timeDisplay;
    public Score score;
    public int finalScore;
    public TextMeshProUGUI finalScoreTmp;
    public enum State
    {
        Game,
        Win,
        ControlsPanel,
        LeaderPanel
    }
    public State menuState;

    //public bool IsPaused { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        pauseMenu = gameManager.GetComponentInChildren<PauseMenu>();
        timeDisplay = gameManager.GetComponentInChildren<TimeDisplay>();
        score = gameManager.GetComponentInChildren<Score>();
        //finalScore = GameObject.FindGameObjectWithTag("FinalScore").GetComponent<TextMeshProUGUI>();


        Win = false;
        menuState = State.Game;
        winMenuUI.SetActive(false);
        canvasGroup.alpha = 0f;
        menuConfirm.ignoreListenerPause = true;
        menuCancel.ignoreListenerPause = true;
        menuError.ignoreListenerPause = true;
        bellTome.ignoreListenerPause = true;
        fanfare.ignoreListenerPause = true;

    }

    // Update is called once per frame
    void Update()
    {
        
            //Paused = !Paused;
            //SwitchMenuState();

            switch (menuState)
            {
                case State.Game:

                    //menuConfirm.Play();
                    //ActivateMenu();
                    break;
                case State.Win:
                    //DeactivateMenu();
                    break;
                case State.ControlsPanel:
                    //CloseControlsPanel();
                    break;
                case State.LeaderPanel:
                    //CloseControlsPanel();
                    break;

            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (menuState == State.ControlsPanel)
                {

                    CloseControlsPanel();
                }

                if (menuState == State.LeaderPanel)
                {

                    CloseLeaderPanel();
                }
            }

        

        //switch (menuState)
        //{
        //    case State.Game:
        //        break;
        //    case State.Win:
        //        //ActivateMenu();
        //        break;
        //    case State.ControlsPanel:

        //        break;

        //}


    }

    public void FadeUI()
    {

        //Color newColor;

        //float transition = 0f;
        //transition += Time.deltaTime;
        //newColor = Color.Lerp(new Color(0, 0, 0, 0), Color.black, transition);
        //canvasGroup.alpha = newColor.a;

        //newColor = child.color;
        //newColor.a = alpha;
        //child.color = newColor;

        StartCoroutine(SetAlpha(canvasGroup, canvasGroup.alpha, 1));


    }

    public IEnumerator SetAlpha(CanvasGroup canvasGroup, float start, float end)
    {
        float counter = 0f;

        while (counter < Duration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / Duration);

            timeDisplay.stop = true;
            yield return null;
        }
    }

    public void ActivateMenu()
    {
        //AudioListener.pause = true;
        pauseMenu.gameObject.SetActive(false);
        winMenuUI.SetActive(true);
        FadeUI();
        //Time.timeScale = 0;
        CalculateFinalScore();
        menuState = State.Win;
    }

    public void DeactivateMenu()
    {
        menuCancel.Play();
        Time.timeScale = 1;
        AudioListener.pause = false;
        winMenuUI.SetActive(false);
        menuState = State.Game;
    }

    public void SwitchMenuState()
    {
        Win = !Win;
    }

    public void OpenControlsPanel()
    {
        menuConfirm.Play();
        controlsPanel.SetActive(true);
        //menuState = State.ControlsPanel;
    }

    public void CloseControlsPanel()
    {
        menuCancel.Play();
        controlsPanel.SetActive(false);
        //menuState = State.Win;
    }

    public void OpenLeaderPanel()
    {
        menuConfirm.Play();
        leaderPanel.SetActive(true);
        //menuState = State.LeaderPanel;
    }

    public void CloseLeaderPanel()
    {
        menuCancel.Play();
        leaderPanel.SetActive(false);
        //menuState = State.Win;
    }

    public void OnWin()
    {
        ActivateMenu();
        fanfare.Play();
    }

    public void ReturnToMainMenu()
    {
        menuConfirm.Play();
        SceneManager.LoadScene("MainMenu");
    }

    public void Retry()
    {
        menuConfirm.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CalculateFinalScore()
    {
        finalScore = score.finalScore;
        finalScore = score.calculateFinalScore();
        finalScoreTmp.text = finalScore.ToString();
    }

    public void leaderButtonPressed()
    {
        if (PlayerPrefs.GetString("userSignedIn?") == "true")
        {
            OpenLeaderPanel();
        }

        else
        {
            OpenWarningPanel();
        }
    }

    public void OpenWarningPanel()
    {
        menuError.Play();
        warningPanel.SetActive(true);
    }

    public void CloseWarningPanel()
    {
        menuCancel.Play();
        warningPanel.SetActive(false);
    }

    public void PostLeaderBoardButton()
    {
        menuConfirm.Play();
        leaderPanel.GetComponent<LeaderBoard>().postToLeaderBoard();
        leaderPanel.SetActive(false);
    }
}
