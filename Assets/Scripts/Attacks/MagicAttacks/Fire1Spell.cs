using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire1Spell : BaseAttack
{
    public Fire1Spell()
    {
        attackName = "Fire 1";
        attackDescription = "Basic Fire Spell which hardly burns.";
        attackDamage = 20f;
        attackCost = 5f;
    }
}
