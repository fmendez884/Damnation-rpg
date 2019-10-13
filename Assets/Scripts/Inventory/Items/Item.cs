using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject 
    
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    
    public virtual void Use ()
    {
        // Use the item
        //something might happen 

        Debug.Log("Using" + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
