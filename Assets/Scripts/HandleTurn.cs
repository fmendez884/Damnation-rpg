using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HandleTurn
{
    public string Attacker; // name of attacker
    public string Type; // type of character (enemy || hero)
    public GameObject AttackersGameObject; // who attacks (state machine)
    public GameObject AttackersTarget; // who is going to be attacked (state machine)

    // which attack is performed
    public BaseAttack ChosenAttack;

}
