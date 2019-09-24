using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelectButton : MonoBehaviour
{
    public GameObject EnemyPrefab;
    private bool showSelector = false;

    public void SelectEnemy ()
    {
        GameObject.Find("BattleManager").GetComponent<BattleStateMachine> ().Input2(EnemyPrefab); //save input enemy prefab
    }

    public void ToggleSelector()
    {
        // if (showSelector) 
        {
            showSelector =!showSelector;
            EnemyPrefab.transform.FindChild("Selector").gameObject.SetActive(showSelector);
            // showSelector = !showSelector;
        }
    }
    
    // public void ToggleSelector()
    // {
    //     // if (showSelector) 
    //     {
    //         showSelector =!showSelector;
    //         EnemyPrefab.transform.FindChild("Selector").gameObject.SetActive(true);
    //         // showSelector = !showSelector;
    //     }
    // }
    public void HideSelector()
    {
        // if (showSelector) 
        {
            showSelector =!showSelector;
            EnemyPrefab.transform.FindChild("Selector").gameObject.SetActive(showSelector);
            // showSelector = !showSelector;
        }
    }


}
