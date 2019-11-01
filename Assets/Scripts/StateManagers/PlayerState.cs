using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public PlayerStats playerStats;
    public PlayerController playerController;
    public CharacterCombat characterCombat;
    public CharacterAnimator characterAnimator;
    public GameObject PlayerPanel;
 
    public enum State
    {
        IDLE,
        RUNNING,
        JUMPING,
        ATTACKING,
        DEFENDING,
        MAGIC,
        DAMAGE,
        DEAD
    }

    public State enemyState;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        playerController = GetComponent<PlayerController>();
        characterCombat = GetComponent<CharacterCombat>();
        characterAnimator = GetComponent<CharacterAnimator>();

    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyState)
        {
            case State.IDLE:
                break;
            case State.RUNNING:

                break;
            case State.JUMPING:

                break;
            case State.ATTACKING:

                break;
            case State.DEFENDING:

                break;
            case State.MAGIC:

                break;
            case State.DAMAGE:
                break;
            case State.DEAD:
                //agent.speed = 0;
                //enemyStats.Die();
                break;
        }
    }

    //void CreatePlayerPanel()
    //{
    //    PlayerPanel = Instantiate(HeroPanel) as GameObject;
    //    stats = PlayerPanel.GetComponent<HeroPanelStats>();
    //    stats.HeroName.text = playerStats.name ;
    //    stats.HeroHP.text = "HP: " + playerStats.currentHealth + "/" + playerStats.maxHealth;
    //    //stats.HeroMP.text = "MP: " + playerStats.currentMana + "/" + playerStats.maxMana;
    //    //ProgressBar = stats.ProgressBar;
    //    //HeroPanel.transform.SetParent(HeroPanelSpacer, false);
    //}

    ////update stats on damage / heal
    //void UpdateHeroPanel()
    //{
    //    stats.HeroHP.text = "HP: " + playerStats.currentHealth + "/" + hero.baseHP;
    //    //stats.HeroMP.text = "MP: " + playerStats.currentMana + "/" + hero.baseMP;
    //}
}
