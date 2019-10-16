﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{

    string enemyName;
    public targetController targetController;
    public CharacterAnimator characterAnimator;
    float deathTime = 4f;



    private void Start()
    {
        enemyName = name;
        targetController = GameObject.Find("GameManager").GetComponentInChildren<targetController>();
        characterAnimator = GetComponent<CharacterAnimator>();

    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (currentHealth >= 0 || currentHealth == 0)
        {
            Die();
        }

        else
        {
            characterAnimator.Damage();

            damage = Mathf.Clamp(damage, 0, int.MaxValue);
            currentHealth -= damage;
            Debug.Log("Player Damages " + name + " for " + damage + " damage!");
            Debug.Log(name + " HP: " + currentHealth);

        }

    }

    //public override void Die()  
    //{
    //    base.Die();

    //    // Add ragdoll effect / death animation
    //    //characterAnimator.Death();
    //    //targetController.removeTarget();;
    //    targetController.target = null;
    //    targetController.nearByEnemies.Remove(gameObject);

    //    StartCoroutine(Death(deathTime));

    //    IEnumerator Death(float deathTime)
    //    {
    //        //dying = true;
    //        yield return new WaitForSeconds(deathTime);
    //        //dieing animation here
    //        characterAnimator.Death();
    //        //dying = !dying;
    //    }
    //    gameObject.SetActive(false);
        
    //    //loot

    //}

    

    public override void Die()
    {
        base.Die();
        float deathTime = 5f;

        targetController.target = null;
        targetController.nearByEnemies.Remove(gameObject);

        StartCoroutine(DeathSequence());

        // Add ragdoll effect / death animation
        //characterAnimator.Death();
        //targetController.removeTarget();;
        //targetController.target = null;
        //targetController.nearByEnemies.Remove(gameObject);

        IEnumerator DeathSequence()
        {
            // - After 0 seconds, prints "Starting 0.0"
            // - After 2 seconds, prints "WaitAndPrint 2.0"
            // - After 2 seconds, prints "Done 2.0"
            characterAnimator.Death();

            // Start function WaitAndPrint as a coroutine. And wait until it is completed.
            // the same as yield WaitAndPrint(2.0);
            yield return new WaitForSeconds(deathTime);
            //print("Done " + Time.time);
            gameObject.SetActive(false);
            
        }

        //// suspend execution for waitTime seconds
        //IEnumerator DeathAnimation(float waitTime)
        //{
        //    yield return new WaitForSeconds(waitTime);
        //    //print("WaitAndPrint " + Time.time);
        //    characterAnimator.Death();
        //}

        //IEnumerator PerformPlayerMotion()
        //{
        //    player.ChangeAnimatorState(PlayerAnimator.AnimatorState.Move);
        //    PlayerControl.RaiseOnChangeCameraMode(CameraScenematic.CameraMode.PlayerMovement);
        //    Yield return StartCoroutine(player.PerformMovement());
        //    player.ChangeAnimatorState(PlayerAnimator.AnimatorState.Idle);
        //    Player.StopNow();
        //    Yield return new WaitForSeconds(1.0f);
        //}

    }

    //public void Death()
    //{
    //    targetController.target = null;
    //    targetController.nearByEnemies.Remove(gameObject);
    //}


    // // time to wait
    //bool dying;

    //void Dead()
    //{
    //    if (!dying)
    //        StartCoroutine(Death(deathTime));  // StartCoroutine(dying(4f));
    //}
    //IEnumerator Death(float deathTime)
    //{
    //    //dying = true;
    //    yield return new WaitForSeconds(deathTime);
    //    //dieing animation here
    //    characterAnimator.Death();
    //    //dying = !dying;
    //}


    //IEnumerator PerformPlayerMotion()
    //{
    //    player.ChangeAnimatorState(PlayerAnimator.AnimatorState.Move);
    //    PlayerControl.RaiseOnChangeCameraMode(CameraScenematic.CameraMode.PlayerMovement);
    //    Yield return StartCoroutine(player.PerformMovement());
    //    player.ChangeAnimatorState(PlayerAnimator.AnimatorState.Idle);
    //    Player.StopNow();
    //    Yield return new WaitForSeconds(1.0f);
    //}


    //public override void Die()
    //{
    //    base.Die();
    //    float deathTime = 5f;
    //    StartCoroutine(DeathSequence());

    //    // Add ragdoll effect / death animation
    //    //characterAnimator.Death();
    //    //targetController.removeTarget();;
    //    //targetController.target = null;
    //    //targetController.nearByEnemies.Remove(gameObject);

    //    IEnumerator DeathSequence()
    //    {
    //        // - After 0 seconds, prints "Starting 0.0"
    //        // - After 2 seconds, prints "WaitAndPrint 2.0"
    //        // - After 2 seconds, prints "Done 2.0"
    //        //characterAnimator.Death();

    //        // Start function WaitAndPrint as a coroutine. And wait until it is completed.
    //        // the same as yield WaitAndPrint(2.0);
    //        yield return StartCoroutine(DeathAnimation(deathTime));
    //        //print("Done " + Time.time);
    //        gameObject.SetActive(false);

    //    }

    //    // suspend execution for waitTime seconds
    //    IEnumerator DeathAnimation(float waitTime)
    //    {
    //        yield return new WaitForSeconds(waitTime);
    //        //print("WaitAndPrint " + Time.time);
    //        characterAnimator.Death();
    //    }

    //    //IEnumerator PerformPlayerMotion()
    //    //{
    //    //    player.ChangeAnimatorState(PlayerAnimator.AnimatorState.Move);
    //    //    PlayerControl.RaiseOnChangeCameraMode(CameraScenematic.CameraMode.PlayerMovement);
    //    //    Yield return StartCoroutine(player.PerformMovement());
    //    //    player.ChangeAnimatorState(PlayerAnimator.AnimatorState.Idle);
    //    //    Player.StopNow();
    //    //    Yield return new WaitForSeconds(1.0f);
    //    //}

    //}







}