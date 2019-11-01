using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    public string name;
    public int maxHealth = 100;
    public int currentHealth;
    public Stat damage;
    public Stat armor;



    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            //TakeDamage(10);
        }
    }

    public virtual void TakeDamage(int damage)
    {
        ////calculate damge 
        //damage -= armor.GetValue();
        //damage = Mathf.Clamp(damage, 0, int.MaxValue);

        //currentHealth -= damage;
        //Debug.Log(transform.name + "takes" + damage + " damage.");
        //Debug.Log(currentHealth);



        //if (currentHealth >= 0 || currentHealth == 0)
        //{
        //    Die();
        //}
    }

    public virtual void Death() 
    {
        // Die in some way 
        //This method is meant to be overriddeen 
        Debug.Log(name + " has died.");
    }
}
