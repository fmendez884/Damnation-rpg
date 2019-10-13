using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCollider : MonoBehaviour
{
    public Transform telepoint;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // OnTriggerEnter();
    }

    public void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Teleporting  " + other);
        Debug.Log(other.transform);
        Debug.Log("position:  " + other.transform.position);
        Debug.Log("telepoint  " + telepoint);
        Debug.Log("telepoint pos   " + telepoint.transform.position);
        Debug.Log(other.transform.parent);
        player = other.transform.parent;
        player.position = telepoint.transform.position;
    }
}
