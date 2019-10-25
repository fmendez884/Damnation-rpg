using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public float deathTime = 5f;
    CharacterAnimator characterAnimator;
    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        characterAnimator = GetComponent<CharacterAnimator>();
    }
    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }

        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
        }
    }

    public override void TakeDamage(int damage)
    {
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, int.MaxValue);

        Debug.Log("Enemy Damages " + name + " for " + damage + " damage!");
        Debug.Log(name + " HP: " + currentHealth);

        if (currentHealth == 0)
        {
            Death();
        }

        else
        {
            characterAnimator.Damage();

            //damage = Mathf.Clamp(damage, 0, int.MaxValue);
            //currentHealth -= damage;

            

            

        }
    }


    public override void Death()
    {

        // Kill the player
        //targetController.target = null;
        TargetController.nearByEnemies.Clear();
        //targetController1.nearByEnemies.Remove(gameObject);


        StartCoroutine(DeathSequence());



        IEnumerator DeathSequence()
        {

            characterAnimator.Death();
            //gameObject.Disable();
            //agent.speed = 0;

            // Start function WaitAndPrint as a coroutine. And wait until it is completed.
            // the same as yield WaitAndPrint(2.0);
            yield return new WaitForSeconds(deathTime);
            //print("Done " + Time.time);

            // drop loot
            //gameObject.SetActive(false);

            PlayerManager.instance.KillPlayer();
        }
    }
}
