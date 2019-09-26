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
        // if(other.tag == "teleporter")
        // {
        //     CollisionHandler col =  other.gameObject.GetComponent<CollisionHandler>();
        //     GameManager.instance.nextSpawnPoint = col.spawnPointName;
        //     GameManager.instance.sceneToLoad = col.sceneToLoad;
        //     GameManager.instance.LoadNextScene();
        // }
        // // if(other.tag == "EnterTown")
        // // {
        // //     CollisionHandler col =  other.gameObject.GetComponent<CollisionHandler>();
        // //     // GameManager.instance.nextHeroPosition = col.spawnPoint.transform.position;
        // //     GameManager.instance.sceneToLoad = col.sceneToLoad;
        // //     GameManager.instance.LoadNextScene();
        // // }
        // // if(other.tag == "LeaveTown")
        // // {
        // //     CollisionHandler col =  other.gameObject.GetComponent<CollisionHandler>();
        // //     // GameManager.instance.nextHeroPosition = col.spawnPoint.transform.position;
        // //     GameManager.instance.sceneToLoad = col.sceneToLoad;
        // //     GameManager.instance.LoadNextScene();
        // // }

        // if (other.tag == "region1")
        // {
        //     GameManager.instance.curRegion = 0;
        // }
        // if (other.tag == "region2")
        // {
        //     GameManager.instance.curRegion = 1;
        // }

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
            case ("region1"):
            {
                GameManager.instance.curRegion = 0;
            }
            break;
            case ("region2"):
            {
                GameManager.instance.curRegion = 1;
            }
            break;
            case ("cameraField"):
                // Debug.Log("in camera field");
                // cameraAreaCheck camera = new cameraAreaCheck();
                // camera.switchState();
                // camera.moveCamera();
            break;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "region1" || other.tag == "region2")
        {
            GameManager.instance.canGetEncounter = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "region1" || other.tag == "region2")
        {
            GameManager.instance.canGetEncounter = false;
        }
    }

}
