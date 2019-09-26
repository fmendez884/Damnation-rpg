using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraAreaCheck : MonoBehaviour
{
    public bool isInArea;
    public Camera mainCamera;
    public Camera fieldCamera;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // switch(isInArea)
        // {
        //     case (true):
        //         Debug.Log("move camera north");
        //         //move camera
        //         moveCamera();
        //     break;
        //     case (false):
        //         Debug.Log("move camera south");
        //     break;
        // }
        // if (isInArea)
        // {
        //     Debug.Log(isInArea);
        //     Debug.Log("move camera north");
        //         //move camera
        //     moveCamera();
        // }
        // else
        // {
        //     Debug.Log("move camera south");
        //     Debug.Log(isInArea);
        // }
    }

    // public void switchState()
    // {
    //     Debug.Log("switching camera state");
    //     Debug.Log(isInArea);
    //     isInArea = !isInArea;
    //     Debug.Log(isInArea);
    // }

    // public void moveCamera()
    // {
    //     if (isInArea)
    //     {
    //         Debug.Log("Moving Camera");
    //     }
    // }

    public void OnTriggerEnter(Collider other) {
        //When the player gets close enough (into this trigger's volume)
        //then we turn on the field Camera and temporarily turn off the Main one
        if (other.gameObject.tag == "Player") {
            SwitchToFieldCamera();
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            SwitchToMainCamera();
        }
    }

    private void SwitchToFieldCamera() {
        fieldCamera.enabled = true;
        mainCamera.enabled = false;
        Debug.Log("Entered Camera Field");
        Debug.Log("Switch to Field camera");
    }

    private void SwitchToMainCamera() {
        fieldCamera.enabled = false;
        mainCamera.enabled = true;
        Debug.Log("Exited Camera Field");
        Debug.Log("Switch to Main Camera");
    }


}
