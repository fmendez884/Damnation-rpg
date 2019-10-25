using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerController : MonoBehaviour
{
   
    public float moveSpeed;
    public float jumpForce = 20;
    public float gravityScale;
    public CharacterController controller;
    public CharacterAnimator characterAnimator;
    public NavMeshAgent agent;
    public GameObject cam;
    public Animator animator;

    private Vector3 moveDirection;
    public float rotateSpeed;
    public GameObject playerModel;
    public float targetSelectRadius = 10f;
    public Transform target;

    public PlayerCombat combat;
    readonly KeyCode AttackKey = KeyCode.X;
    readonly KeyCode MagicKey = KeyCode.C;
    readonly KeyCode ResetKey = KeyCode.R;

    public bool action;


    public enum Controller

    {
        IDLE,
        AIR,
        RUNNING,
        ACTION,
        DAMAGE,
        DEAD
    }

    public Controller controllerState;

    
    void Start()
    {
        
        controller = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        cam = GameObject.Find("Main Camera");
        animator = GetComponentInChildren<Animator>();
        characterAnimator = GetComponent<CharacterAnimator>();
        combat = GetComponent<PlayerCombat>();
        action = false;

        controllerState = Controller.IDLE;
    }

   
    void Update()
    {
        
        float yStore = moveDirection.y;
        moveDirection = (cam.transform.forward * Input.GetAxis("Vertical") + (cam.transform.right * Input.GetAxis("Horizontal")));
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;


        //switch (controllerState)
        //{
        //    case Controller.IDLE:
        //        //EnablePlayerMovement();
        //        //controller.Move(moveDirection * Time.deltaTime);
        //        break;
        //    case Controller.RUNNING:

        //        break;
        //    case Controller.ACTION:
        //        StartCoroutine(AttackSequence());

        //        //moveDirection.y += (Physics.gravity.y * gravityScale * Time.deltaTime);

        //        IEnumerator AttackSequence()
        //        {
        //            float attackTime = .7f;

        //            //characterAnimator.Death();
        //            //gameObject.Disable();
        //            //agent.speed = 0;

        //            // Start function WaitAndPrint as a coroutine. And wait until it is completed.
        //            // the same as yield WaitAndPrint(2.0);
        //            yield return new WaitForSeconds(attackTime);
        //            //print("Done " + Time.time);

        //            // drop loot
        //            //gameObject.SetActive(false);

        //            controllerState = Controller.IDLE;

                    

        //        }
        //        break;
        //    case Controller.AIR:

        //        break;
        //    case Controller.DEAD:
        //        //agent.speed = 0;
        //        //enemyStats.Die();
        //        break;
        //}





        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
         
            // agent.isStopped = true;
            // agent.ResetPath();
            moveDirection.y = jumpForce;
        }




        if (target != null)
        {
            //Debug.Log(target);
            //transform.rotation = Quaternion.Euler(0f, cam.transform.rotation.eulerAngles.y, 0f);
            //// Debug.Log(cam.transform.rotation);
            //Quaternion newRotation = Quaternion.LookRotation(new Vector3(target.transform.position.x, 0f, target.transform.position.z));
            //transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotateSpeed * Time.deltaTime);

            transform.rotation = Quaternion.Euler(0f, cam.transform.rotation.eulerAngles.y, 0f);
            // Debug.Log(cam.transform.rotation);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            //FaceTarget();

        }
        else if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {

            transform.rotation = Quaternion.Euler(0f, cam.transform.rotation.eulerAngles.y, 0f);
            // Debug.Log(cam.transform.rotation);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotateSpeed * Time.deltaTime);

        }

        controller.Move(moveDirection * Time.deltaTime);

        //if (!action)
        //{

        //}

        // Move the player in different directions based on the camera direction



        //if (Input.GetKeyDown(AttackKey))
        //{
        //    //controllerState = Controller.ACTION;
        //    ResetAgent();

        //    //action = true;
        //    controller.Move(new Vector3(0, moveDirection.y * Time.deltaTime, 0));

        //    StartCoroutine(AttackSequence());

        //    //moveDirection.y += (Physics.gravity.y * gravityScale * Time.deltaTime);

        //    IEnumerator AttackSequence()
        //    {
        //        float attackTime = .7f;

        //        //characterAnimator.Death();
        //        //gameObject.Disable();
        //        //agent.speed = 0;

        //        // Start function WaitAndPrint as a coroutine. And wait until it is completed.
        //        // the same as yield WaitAndPrint(2.0);
        //        yield return new WaitForSeconds(attackTime);
        //        //print("Done " + Time.time);

        //        // drop loot
        //        //gameObject.SetActive(false);

        //        //controllerState = Controller.IDLE;



        //    }

        //    PlayerAttack();

        //    //action = false;

        //}

        if (Input.GetKeyDown(MagicKey))
        {
            ResetAgent();

            //action = true;

            PlayerCastMagic();

            //action = false;
        }

        if (Input.GetKeyDown(ResetKey))
        {
            ResetAgent();

            PlayerManager.instance.KillPlayer();
        }

        moveDirection.y += (Physics.gravity.y * gravityScale * Time.deltaTime);

        animator.SetBool("isGrounded", controller.isGrounded);
        animator.SetFloat("speedPercent", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
        


    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(AttackKey))
        {
            //controllerState = Controller.ACTION;
            ResetAgent();

            //action = true;
            controller.Move(new Vector3(0, moveDirection.y * Time.deltaTime, 0));

            StartCoroutine(AttackSequence());

            //moveDirection.y += (Physics.gravity.y * gravityScale * Time.deltaTime);

            IEnumerator AttackSequence()
            {
                float attackTime = .7f;

                //characterAnimator.Death();
                //gameObject.Disable();
                //agent.speed = 0;

                // Start function WaitAndPrint as a coroutine. And wait until it is completed.
                // the same as yield WaitAndPrint(2.0);
                yield return new WaitForSeconds(attackTime);
                //print("Done " + Time.time);

                // drop loot
                //gameObject.SetActive(false);

                //controllerState = Controller.IDLE;



            }

            PlayerAttack();

            //action = false;

        }
    }

    public void ResetAgent() 
    {
        if (agent.enabled == true)
        {
            //Debug.Log(agent);
            agent.isStopped = true;
            agent.ResetPath();
        }
    }

    public void FaceTarget() 
    {
        //Debug.Log(target);
        //Debug.Log(target.transform);
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetSelectRadius);
    }

    //private void OnTriggerEnter(Collider other) 
    //{
    //    switch (other.tag)
    //    {
    //        case ("Enemy"):
    //        {
    //            //Debug.Log("enemy");
    //            //Debug.Log(other);

    //            //target = other.transform;
    //                //FaceTarget();
    //        }
    //        break;
    //    }
    //}

    //public void TeleportPlayer(Collider other) 
    //{

    //}

    public void PlayerAttack()
    {
        
        combat.actionState = PlayerCombat.Action.ATTACK;


    }

    public void PlayerCastMagic()
    {
        
        combat.actionState = PlayerCombat.Action.MAGIC;


        
    }

    public void EnablePlayerMovement()
    {
        if (target != null)
        {
            //Debug.Log(target);
            //transform.rotation = Quaternion.Euler(0f, cam.transform.rotation.eulerAngles.y, 0f);
            //// Debug.Log(cam.transform.rotation);
            //Quaternion newRotation = Quaternion.LookRotation(new Vector3(target.transform.position.x, 0f, target.transform.position.z));
            //transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotateSpeed * Time.deltaTime);

            transform.rotation = Quaternion.Euler(0f, cam.transform.rotation.eulerAngles.y, 0f);
            // Debug.Log(cam.transform.rotation);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            //FaceTarget();

        }
        
        else if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {

            transform.rotation = Quaternion.Euler(0f, cam.transform.rotation.eulerAngles.y, 0f);
            // Debug.Log(cam.transform.rotation);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotateSpeed * Time.deltaTime);

        }

        moveDirection.y += (Physics.gravity.y * gravityScale * Time.deltaTime);
    }

}
