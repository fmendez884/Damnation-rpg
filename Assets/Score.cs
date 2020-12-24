using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public float score = 0f;
    public TextMeshProUGUI tmp;
    public GameObject gameManager;
    public EnemyList enemyList;
    public int finalScore;
    public TimeDisplay timeDisplay;
    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        //tmp.text = score.ToString();
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        enemyList = gameManager.GetComponent<EnemyList>();
    }

    // Update is called once per frame
    void Update()
    {
        tmp.text = score.ToString();
    }

    public int calculateFinalScore()
    {
        finalScore = (int)score;
        return finalScore;
    }
}
