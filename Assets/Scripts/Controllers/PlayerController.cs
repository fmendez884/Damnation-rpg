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


    
    void Start()
    {
        
        controller = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        cam = GameObject.Find("Main Camera");
        animator = GetComponentInChildren<Animator>();
        characterAnimator = GetComponent<CharacterAnimator>();
        combat = GetComponent<PlayerCombat>();
       
    }

   
    void Update()
    {
        
        float yStore = moveDirection.y;
        moveDirection = (cam.transform.forward * Input.GetAxis("Vertical") + (cam.transform.right * Input.GetAxis("Horizontal")));
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;
        

        
        
        moveDirection.y += (Physics.gravity.y * gravityScale * Time.deltaTime);

        

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

        // Move the player in different directions based on the camera direction

        

        if (Input.GetKeyDown(AttackKey))
        {
            ResetAgent();

            PlayerAttack();
    
        }

        if (Input.GetKeyDown(MagicKey))
        {
            ResetAgent();

            PlayerCastMagic();
        }

        if (Input.GetKeyDown(ResetKey))
        {
            ResetAgent();

            PlayerManager.instance.KillPlayer();
        }

        animator.SetBool("isGrounded", controller.isGrounded);
        animator.SetFloat("speedPercent", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
        


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
}
