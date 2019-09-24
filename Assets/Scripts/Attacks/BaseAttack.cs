using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseAttack : MonoBehaviour 
{
    public string attackName; // 
    public string attackDescription; // description of the attack
    public float attackDamage; // Base Damage 15 melee lv 10 stamina 35 = basedmg + (xstamina)
    public float attackCost; // ManaCost


}
