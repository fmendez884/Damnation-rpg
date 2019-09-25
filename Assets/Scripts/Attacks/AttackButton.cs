using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
    public BaseAttack magicAttackToPerform;
    
    public void CastMagicAttack()
    {
        Debug.Log("magic attack");
        Debug.Log(magicAttackToPerform);
        GameObject.Find("BattleManager").GetComponent<BattleStateMachine>().Input4(magicAttackToPerform);
    }
}
