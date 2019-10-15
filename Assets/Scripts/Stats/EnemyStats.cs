using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{

    string enemyName;
    public targetController targetController;
    
    

    private void Start()
    {
        enemyName = gameObject.name;
        targetController = GameObject.Find("GameManager").GetComponentInChildren<targetController>();

    }

    public override void TakeDamage(int damage)
    {
      
        base.TakeDamage(damage);
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        currentHealth -= damage;
        Debug.Log("Player Damages " + name + " for " + damage + " damage!");
        Debug.Log(name + " HP: " + currentHealth);
    }

    public override void Die()  
    {
        base.Die();

        // Add ragdoll effect / death animation

        //targetController.removeTarget();;
        targetController.target = null;
        targetController.nearByEnemies.Remove(gameObject);

        gameObject.SetActive(false);
        
        //loot

    }

}
