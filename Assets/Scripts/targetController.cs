using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Be Sure To Include This

public class targetController : MonoBehaviour {

    Camera cam; //Main Camera
    CameraController camController;
    enemyInView target; //Current Focused Enemy In List
    Image image;//Image Of Crosshair
    public AnimationCurve myCurve;
    public Vector3 objOffset = new Vector3(0,1,0);
    public float objOffsetY = 1.2f;
    public float reticleHeight;
    CapsuleCollider targetCollider;
    public PlayerController playerController;
    KeyCode targetInputKey = KeyCode.C;
    KeyCode switchTargetInputKey = KeyCode.Tab;



    bool lockedOn;//Keeps Track Of Lock On Status    

    //Tracks Which Enemy In List Is Current Target
    int lockedEnemy;

    //List of nearby enemies
    public static List<enemyInView> nearByEnemies = new List<enemyInView>();

    void Start () {
        cam = Camera.main;
        camController = cam.GetComponent<CameraController>();

        image = gameObject.GetComponent<Image>();

        lockedOn = false;
        lockedEnemy = 0;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }  

    void Update () {       

        //Press Space Key To Lock On
        if (Input.GetKeyDown(targetInputKey) && !lockedOn)
        {
            if (nearByEnemies.Count >= 1)
            {
                lockedOn = true;
                image.enabled = true;

                //Lock On To First Enemy In List By Default
                lockedEnemy = 0;
                target = nearByEnemies[lockedEnemy];
            }
        }
        //Turn Off Lock On When Space Is Pressed Or No More Enemies Are In The List
        else if ((Input.GetKeyDown(targetInputKey) && lockedOn) || nearByEnemies.Count == 0)
        {
            lockedOn = false;
            image.enabled = false;
            lockedEnemy = 0;
            target = null;
            playerController.target = null;
            camController.enemyTarget = null;
        }

        //Press X To Switch Targets
        if (Input.GetKeyDown(switchTargetInputKey))
        {
            if (lockedEnemy == nearByEnemies.Count - 1)
            {
                //If End Of List Has Been Reached, Start Over
                lockedEnemy = 0;
                target = nearByEnemies[lockedEnemy];
            }
            else
            {
                //Move To Next Enemy In List
                lockedEnemy++;
                target = nearByEnemies[lockedEnemy];
            }           
        }      

        if (lockedOn)
        {
            target = nearByEnemies[lockedEnemy];
            targetCollider = target.GetComponent<CapsuleCollider>();

            CalculateReticleHeight();
            //Debug.Log(reticleHeight);

            //Determine Crosshair Location Based On The Current Target
            // gameObject.transform.position = cam.WorldToScreenPoint(target.transform.position); 
            gameObject.transform.position = cam.WorldToScreenPoint(new Vector3(targetCollider.transform.position.x, reticleHeight, targetCollider.transform.position.z));

            camController.enemyTarget = target.transform;

            //playerController.target = target.transform;
            FaceTarget(target.transform);

            // transform.TransformPoint(new Vector3(objOffset.x, objSize.y / 2, objOffset.y))
            // gameObject.transform.TransformPoint( cam.WorldToScreenPoint(new Vector3(target.transform.position.x, objOffset.y, target.transform.position.z)));

            //Rotate Crosshair
            // gameObject.transform.Rotate(new Vector3(0, 0, -1));
            // gameObject.transform.position = new Vector3(transform.position.x, myCurve.Evaluate((Time.time % myCurve.length)), transform.position.z);
        }             
    }

    void CalculateReticleHeight()
    {
        reticleHeight = target.transform.position.y + targetCollider.center.y + targetCollider.height + target.transform.localScale.y + 1.2f;
    }

    public void FaceTarget(Transform target)
    {
        //Debug.Log(target);
        //Debug.Log(target.transform);
        Vector3 direction = (target.transform.position - playerController.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        playerController.transform.rotation = Quaternion.Slerp(playerController.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


}