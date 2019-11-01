using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public GameObject Player;
    public PlayerStats playerStats;
    public Text HUDName;
    public Text HUDHealth;
    public Text HUDMana;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        playerStats = Player.GetComponent<PlayerStats>();


        HUDName = GameObject.Find("PlayerName").GetComponent<Text>();
        HUDHealth = GameObject.Find("PlayerHP").GetComponent<Text>();
        HUDMana = GameObject.Find("PlayerMP").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerStats == null)
        {
            Player = GameObject.FindWithTag("Player");
            playerStats = Player.GetComponent<PlayerStats>();

            HUDName.color = Color.white;
            HUDHealth.color = Color.white;
        }
            HUDName.text = playerStats.name;


            HUDHealth.text = "HP: " + playerStats.currentHealth + "/" + playerStats.maxHealth;
            //HUDMana.text = "MP: " + currentMana + "/" + maxMana;
        if ((float)playerStats.currentHealth / (float)playerStats.maxHealth <= .3f)
        {
            HUDName.color = Color.red;
            HUDHealth.color = Color.red;
        }
    }
}
