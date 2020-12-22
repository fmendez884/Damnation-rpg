using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    public GameObject Player;
    public PlayerStats playerStats;
    public Text HUDName;
    public Text HUDHealth;
    public Text HUDMana;

    public GameObject ActionPanel;
    public Transform actionSpacer;
    public GameObject actionButton;

    public HealthBar healthBar;
    

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        playerStats = Player.GetComponent<PlayerStats>();

        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.setMaxHealth(playerStats.maxHealth);

        ActionPanel = GameObject.Find("ActionPanel");
        //ActionPanel.SetActive(true);

        //CreateAttackButtons();

        HUDName = GameObject.Find("PlayerName").GetComponent<Text>();
        HUDHealth = GameObject.Find("PlayerHP").GetComponent<Text>();
        //HUDMana = GameObject.Find("PlayerMP").GetComponent<Text>();
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
            healthBar.setMaxHealth(playerStats.maxHealth);
        }
            HUDName.text = playerStats.playerName;


            HUDHealth.text = "HP: " + playerStats.currentHealth + "/" + playerStats.maxHealth;
            healthBar.setHealth(playerStats.currentHealth);
            //HUDMana.text = "";
        //HUDMana.text = "MP: " + currentMana + "/" + maxMana;

        if ((float)playerStats.currentHealth / (float)playerStats.maxHealth <= .3f)
        {
            HUDName.color = Color.red;
            HUDHealth.color = Color.red;
        }
    }

    void CreateAttackButtons()
    {
        GameObject AttackButton = Instantiate(actionButton) as GameObject;
        TextMeshPro AttackButtonText = AttackButton.transform.Find("Text").gameObject.GetComponent<TextMeshPro>();
        AttackButtonText.text = "Attack";
        AttackButton.GetComponent<Button>().onClick.AddListener(Input1);
        AttackButton.transform.SetParent(actionSpacer, false);

    }

    void Input1()
    {

    }
}
