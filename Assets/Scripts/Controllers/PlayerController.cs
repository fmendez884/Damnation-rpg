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

    public GameObject player;
    public Vector3 currentPosition, lastPosition;

    public bool grounded;

    public PlayerCombat combat;
    readonly KeyCode AttackKey = KeyCode.X;
    readonly KeyCode MagicKey = KeyCode.C;
    readonly KeyCode ResetKey = KeyCode.R;

    //public bool action;


    public enum Controller

    {
        IDLE,
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

        player = GameObject.FindWithTag("Player");
        //currentPosition = player.transform.position;

        //action = false;

        controllerState = Controller.IDLE;
    }

   
    void Update()
    {

        
        float yStore = moveDirection.y;
        moveDirection = (cam.transform.forward * Input.GetAxis("Vertical") + (cam.transform.right * Input.GetAxis("Horizontal")));
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;


        switch (controllerState)
        {
            case Controller.IDLE:
                EnablePlayerMovement();
                
                break;
            
            case Controller.ACTION:
               

                if (controller.isGrounded)
                {
                    
                }
                else
                {
                    EnablePlayerMovement();
                 
                    
                }

                break;
            
            case Controller.DEAD:
                
                break;
        }





        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
         
            
            moveDirection.y = jumpForce;
        }




        //if (target != null)
        //{
            
        //    transform.rotation = Quaternion.Euler(0f, cam.transform.rotation.eulerAngles.y, 0f);
        //    // Debug.Log(cam.transform.rotation);
        //    Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
        //    transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        //    //FaceTarget();

        //}
        //else if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        //{

        //    transform.rotation = Quaternion.Euler(0f, cam.transform.rotation.eulerAngles.y, 0f);
        //    // Debug.Log(cam.transform.rotation);
        //    Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
        //    transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotateSpeed * Time.deltaTime);

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
            PlayerAttack();
            controllerState = Controller.ACTION;
           

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
        controller.Move(moveDirection * Time.deltaTime);
    }

}
