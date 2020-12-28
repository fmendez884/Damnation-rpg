using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    public string userData;
    public TextMeshProUGUI finalScoreText;
    public TimeDisplay timeDisplay;
    public Score score;
    public int finalScore;
    //public WinMenu winMenu;
    public GameObject gameManager;
    public PlayerStats playerStats;
    public int remainingHealth;
    public string leaderBoardDataString;


    [DllImport("__Internal")]
    public static extern void ReceiveLeaderBoardData(string leaderBoardData);


    // Start is called before the first frame update
    void Start()
    {
        userData = PlayerPrefs.GetString("userData");
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        playerStats = gameManager.GetComponent<PlayerStats>();
        remainingHealth = playerStats.currentHealth; 
        score = gameManager.GetComponentInChildren<Score>();
        finalScore = score.calculateFinalScore();
        timeDisplay = score.timeDisplay;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void postToLeaderBoard()
    {
        ScoreData scoreData = new ScoreData();
        scoreData.finalScore = finalScore;
        scoreData.timeElapsed = timeDisplay.currentTime;
        scoreData.remainingHealth = remainingHealth;
        //leaderBoardData.scoreData;
        LeaderBoardData dataToSend = new LeaderBoardData();
        dataToSend.scoreData = scoreData;
        dataToSend.userData = userData;

        leaderBoardDataString = JsonConvert.SerializeObject(dataToSend);
        
        sendLeaderBoardData(leaderBoardDataString);
    }

    public void sendLeaderBoardData(string data)
    {

#if !UNITY_EDITOR && UNITY_WEBGL
                            ReceiveLeaderBoardData(data);
#endif

    }

    class LeaderBoardData
    {
        public ScoreData scoreData;
        public string userData;
    }

    class ScoreData
    {
        public string timeElapsed;
        public int finalScore;
        public int remainingHealth;
    }

}
