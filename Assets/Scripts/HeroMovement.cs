using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{

    float moveSpeed = 300f;

    private Vector3 curPos, lastPos;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform);
        if(transform == null)
        {
            Debug.Log("transform is null");
        }
        if(GameManager.instance.nextHeroPosition != null)
        {
            GameObject spawnPoint = GameObject.Find(GameManager.instance.nextSpawnPoint);
            transform.position = spawnPoint.transform.position; //Null Reference exceptionobject not set to instance

            GameManager.instance.nextSpawnPoint = "";
        }
        else if (GameManager.instance.lastHeroPosition != Vector3.zero)
        {
            transform.position = GameManager.instance.nextHeroPosition;
            GameManager.instance.lastHeroPosition = Vector3.zero;
        }
        Debug.Log(transform);
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

        curPos = transform.position;
        if (curPos == lastPos)
        {
            GameManager.instance.isWalking = false; /////null reference exception object not set to instance
        }
        else
        {
            GameManager.instance.isWalking = true;   ///////// null reference exceptionobject not set to instance
        }
        lastPos = curPos;
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case ("teleporter"):
            {
                CollisionHandler col =  other.gameObject.GetComponent<CollisionHandler>();
                GameManager.instance.nextSpawnPoint = col.spawnPointName;
                GameManager.instance.sceneToLoad = col.sceneToLoad;
                GameManager.instance.LoadNextScene();
            }
            break;
            break;
            case ("EncounterZone"):
                RegionData region = other.gameObject.GetComponent<RegionData>();
                GameManager.instance.curRegion = region;
            break;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "EncounterZone")
        {
            // RegionData region = other.gameObject.GetComponent<RegionData>();
            GameManager.instance.canGetEncounter = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "EncounterZone")
        {
            // RegionData region = other.gameObject.GetComponent<RegionData>();
            GameManager.instance.canGetEncounter = false;
        }
    }

}
