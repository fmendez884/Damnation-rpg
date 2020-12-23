using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyStatsHUD : MonoBehaviour
{
    //public GameObject Enemy;
    public EnemyStats enemyStats;
    public TextMeshProUGUI HUDName;
    public TextMeshProUGUI HUDHealth;

    public Camera cam;
    public Image panel;
    public Vector3 panelPosition;

    public Transform statsPanel;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        statsPanel = gameObject.transform;

        panel = gameObject.GetComponent<Image>();
        //Enemy = GameObject.Fin   ;
        //enemyStats = GetComponentInParent<EnemyStats>();
        enemyStats = gameObject.GetComponentInParent<EnemyStats>();
        //HUDName = GameObject.Find("EnemyNameText").GetComponent<TextMeshProUGUI>();
        //HUDHealth = GameObject.Find("EnemyHPText").GetComponent<TextMeshProUGUI>();
        //HUDName = enemyStats.GetComponentInChildren<EnemyName>().gameObject.GetComponent<TextMeshProUGUI>();
        //HUDHealth = enemyStats.GetComponentInChildren<EnemyHP>().gameObject.GetComponent<TextMeshProUGUI>();

        HUDName.text = enemyStats.name;
    }

    // Update is called once per frame
    void Update()
    {
        //statsPanel.LookAt(new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z)) ;
        statsPanel.LookAt(transform.position + cam.transform.forward);
        //statsPanel.Rotate(0, 180f, 0);

        HUDName.text = enemyStats.characterName;


        HUDHealth.text = "HP: " + enemyStats.currentHealth + "/" + enemyStats.maxHealth;
        //HUDMana.text = "MP: " + currentMana + "/" + maxMana;

        if ((float)enemyStats.currentHealth / (float)enemyStats.maxHealth <= .3f)
        {
            HUDName.color = Color.red;
            HUDHealth.color = Color.red;
        }

        //GetPanelPosition();

        //Determine Crosshair Location Based On The Current Target
        // gameObject.transform.position = cam.WorldToScreenPoint(target.transform.position); 
        //gameObject.transform.position = cam.WorldToScreenPoint(new Vector3(targetCollider.transform.position.x, reticleHeight, targetCollider.transform.position.z));

        //image.transform.position = cam.WorldToScreenPoint(new Vector3(targetCollider.transform.position.x, reticleHeight, targetCollider.transform.position.z));

        //panel.transform.position = cam.WorldToScreenPoint(panelPosition);
    }

    public void GetPanelPosition()
    {
        panelPosition = enemyStats.gameObject.GetComponentInChildren<TargetPoint>().transform.position;
        panelPosition.y -= 2f;
    }


}
