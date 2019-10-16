﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Be Sure To Include This

public class targetController : MonoBehaviour {

    Camera cam; //Main Camera
    CameraController camController;
    public GameObject target; //Current Focused Enemy In List
    public Image image;//Image Of Crosshair
    public AnimationCurve myCurve;
    public Vector3 objOffset = new Vector3(0, 1, 0);
    public float objOffsetY = 1.2f;
    public float reticleHeight;
    CapsuleCollider targetCollider;
    public PlayerController playerController;
    readonly KeyCode targetInputKey = KeyCode.Q;
    readonly KeyCode switchTargetInputKey = KeyCode.Tab;



    bool lockedOn;//Keeps Track Of Lock On Status    

    //Tracks Which Enemy In List Is Current Target
    int lockedEnemy;

    //List of nearby enemies
    public static List<GameObject> nearByEnemies = new List<GameObject>();

    void Start()
    {
        cam = Camera.main;
        camController = cam.GetComponent<CameraController>();

        image = gameObject.GetComponent<Image>();

        lockedOn = false;
        lockedEnemy = 0;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {

        //checkEnemyList();

        //Press Key To Lock On
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

    //public void checkEnemyList()
    //{

    //    for (int i = 0; i < nearByEnemies.Count; ++i)
    //    {
    //        if (nearByEnemies[i].activeSelf == false)
    //        {
    //            nearByEnemies.Remove(nearByEnemies[i]);

    //        }
    //    }
    //}

    public void removeTarget()
    {
        target = null;
    }

    //================================================
    ////Lock On Script
    ////================================================
    //int current;
    //bool locked;

    //GameObject[] enemies;
    //GameObject closestEnemy;
    //Transform reticle; //Current targeted enemy indicator


    //public void Update()
    //{

    //    if (closest != null & amp; &amp; locked)

    //    {
    //        activeIcon.active = true;
    //        activeIcon.transform.position.y = (closest.transform.position.y + 1);
    //        activeIcon.transform.position.x = (closest.transform.position.x);
    //        activeIcon.transform.position.z = (closest.transform.position.z);
    //    }
    //    else
    //    {
    //        activeIcon.active = false;
    //    }


    //    if (Input.GetButtonDown("Lock"))
    //    {
    //        //Looks for the closest enemy
    //        FindClosestEnemy();
    //        locked = !locked;
    //    }
    //    if (locked)
    //    {
    //        //If there aren't any enemies (or the player killed the last one targeted) make sure that the lock is false
    //        if (!closest)
    //        {
    //            activeIcon.active = false;
    //            locked = false;
    //            closest = null;
    //        }
    //        if (playerController.isAttacking)
    //            transform.LookAt(Vector3(closest.transform.position.x, transform.position.y, closest.transform.position.z));
    //    }

    //}


    //public GameObject FindClosestEnemy()
    //{
    //    // Find all game objects with tag Enemy
    //    GameObject[] enemyLocations = GameObject.FindGameObjectsWithTag("Enemy");
    //    //var closest : GameObject; 
    //    var distance = Mathf.Infinity;
    //    var position = transform.position;
    //    // Iterate through them and find the closest one
    //    foreach (GameObject enemy in enemyLocations)
    //    {
    //        var diff = (enemy.transform.position - position);
    //        var curDistance = diff.sqrMagnitude;
    //    }

    //    if (float curDistance & lt; distance) 
    //{
    //        GameObject closest = enemy;
    //        distance = curDistance;
    //    }

    //    return closest;



    //}


//    //================================================
//    //Lock On Script
//    //================================================
//    private var current : int = 0; 
//private var locked : boolean = false; 
//var playerController : ThirdPersonController ;
//var enemyLocations : GameObject[];
//var closest : GameObject;
//var activeIcon : Transform; //Current targeted enemy indicator


//function Update()
//    {
//        var playerController : ThirdPersonController = GetComponent(ThirdPersonController);


//        if (closest != null & amp; &amp; locked)

//{
//            activeIcon.active = true;
//            activeIcon.transform.position.y = (closest.transform.position.y + 1);
//            activeIcon.transform.position.x = (closest.transform.position.x);
//            activeIcon.transform.position.z = (closest.transform.position.z);
//        }
//else
//        {
//            activeIcon.active = false;
//        }


//        if (Input.GetButtonDown("Lock"))
//        {
//            //Looks for the closest enemy
//            FindClosestEnemy();
//            locked = !locked;
//        }
//        if (locked)
//        {
//            //If there aren't any enemies (or the player killed the last one targeted) make sure that the lock is false
//            if (!closest)
//            {
//                activeIcon.active = false;
//                locked = false;
//                closest = null;
//            }
//            if (playerController.isAttacking)
//                transform.LookAt(Vector3(closest.transform.position.x, transform.position.y, closest.transform.position.z));
//        }

//    }


//    function FindClosestEnemy() : GameObject 
//    {
//        // Find all game objects with tag Enemy
//        enemyLocations = GameObject.FindGameObjectsWithTag("Enemy"); 
//        //var closest : GameObject; 
//        var distance = Mathf.Infinity;
//        var position = transform.position; 
//        // Iterate through them and find the closest one
//        for (var go : GameObject in enemyLocations) 
//            { 
//                var diff = (go.transform.position - position);
//        var curDistance = diff.sqrMagnitude; 


//             if (curDistance &lt; distance) 
//             { 
//                 closest = go; 
//                 distance = curDistance; 
//             } 
//         } 
//     return closest; 

//    }

}