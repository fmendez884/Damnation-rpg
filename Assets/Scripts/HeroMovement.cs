using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{

    float moveSpeed = 300f;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = GameManager.instance.nextHeroPosition;
    }

    // Update is called once per frame
    // void FixedUpdate()
    // {
    //     float moveX = Input.GetAxis("Horizontal");
    //     float moveZ = Input.GetAxis("Vertical");

    //     Vector3 movement = new Vector3(moveX, 0f, moveZ);
    //     GetComponent<Rigidbody>().velocity = movement * moveSpeed * Time.deltaTime;
    //     // transform.Translate(moveX, 0f, moveZ);
    // }

    void FixedUpdate() 
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0f, moveZ);
        GetComponent<Rigidbody>().velocity = movement * moveSpeed * Time.deltaTime;
        // Debug.Log(moveX);
        // Debug.Log(moveZ);
        // transform.Translate(moveX, 0f, moveZ);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnterTown")
        {
            CollisionHandler col =  other.gameObject.GetComponent<CollisionHandler>();
            GameManager.instance.nextHeroPosition = col.spawnPoint.transform.position;
            GameManager.instance.sceneToLoad = col.sceneToLoad;
            GameManager.instance.LoadNextScene();
        }
        if(other.tag == "LeaveTown")
        {
            CollisionHandler col =  other.gameObject.GetComponent<CollisionHandler>();
            GameManager.instance.nextHeroPosition = col.spawnPoint.transform.position;
            GameManager.instance.sceneToLoad = col.sceneToLoad;
            GameManager.instance.LoadNextScene();
        }
    }


}
