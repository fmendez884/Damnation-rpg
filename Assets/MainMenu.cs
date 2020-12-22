using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //public AudioClip newGameClip;
    public AudioSource newGameSound;
    public AudioSource bellTome;
    public AudioSource menuConfirm;
    public AudioSource menuCancel;
    public GameObject controlsPanel;

    //private void Start()
    //{

    //}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && controlsPanel.activeSelf)
        {
            CloseControlsPanel();
        }
    }

    public void OpenControlsPanel()
    {
        menuConfirm.Play();
        controlsPanel.SetActive(true);
    }

    public void CloseControlsPanel()
    {
        menuCancel.Play();
        controlsPanel.SetActive(false);
    }

    public void PlayGame()
    {
        //float waitTime = .5f;
        //StartCoroutine(SoundSequence());



        //IEnumerator SoundSequence()
        //{

        //    newGameSound.Play();
 
    

        //    // Start function WaitAndPrint as a coroutine. And wait until it is completed.
        //    // the same as yield WaitAndPrint(2.0);
        //    yield return new WaitForSeconds(waitTime);
        //    //print("Done " + Time.time);


     

        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
      
        //}

        newGameSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    

    public void QuitGame()
    {
        //Debug.Log("Exiting Game");
        bellTome.Play();
        Application.Quit();
    }

    //public void PlayNewGameSound()
    //{
    //    float waitTime = 2f;
    //    StartCoroutine(SoundSequence());



    //    IEnumerator SoundSequence()
    //    {
    //        //playerController.controllerState = PlayerController.Controller.DEAD;
    //        newGameSound.Play();
    //        //newGameClip.isReadyToPlay();
    //        //characterAnimator.Death();
    //        //gameObject.Disable();
    //        //agent.speed = 0;

    //        // Start function WaitAndPrint as a coroutine. And wait until it is completed.
    //        // the same as yield WaitAndPrint(2.0);
    //        yield return new WaitForSeconds(waitTime);
    //        //print("Done " + Time.time);

    //        // drop loot
    //        //gameObject.SetActive(false);

    //        //PlayerManager.instance.KillPlayer();
    //    }
    //}
}
