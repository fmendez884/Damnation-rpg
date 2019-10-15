using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerController : MonoBehaviour
{
    // public Rigidbody theRB;
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
    public HitDetection hitDetection;
    public Collider hitCollider;

    KeyCode AttackKey = KeyCode.X;
    KeyCode MagicKey = KeyCode.C;

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("SUP");
        // theRB = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        cam = GameObject.Find("Main Camera");
        animator = GetComponentInChildren<Animator>();
        characterAnimator = GetComponent<CharacterAnimator>();
        combat = GetComponent<PlayerCombat>();
        //hitDetection = GetComponentInChildren<HitDetection>();
        //hitCollider = hitDetection.GetComponent<Collider>();
        //hitDetection.enabled = false;
        //hitCollider.enabled = false;
        //Debug.Log("Hit Detection:  " + hitDetection.enabled);
        //Debug.Log("Mesh Collider:  " + hitDetection.GetComponent<Collider>().enabled);
    }

    // Update is called once per frame
    // Late Update once NavMesh is fixed for smoother movement
    void Update()
    {
        // cam.transform.position = target.transform.position;
        // Debug.Log(moveSpeed);
        // theRB.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, theRB.velocity.y, Input.GetAxis("Vertical") * moveSpeed);

        // if (Input.GetButtonDown("Jump"))
        // {
        //     theRB.velocity = new Vector3(theRB.velocity.x, jumpForce, theRB.velocity.z);
        //     Debug.Log();
        // }

        // moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 0f, Input.GetAxis("Vertical") * moveSpeed);
        float yStore = moveDirection.y;
        moveDirection = (cam.transform.forward * Input.GetAxis("Vertical") + (cam.transform.right * Input.GetAxis("Horizontal")));
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;
        

        // if (Input.GetAxis("Horizontal") == 0f || Input.GetAxis("Vertical") == 0f)
        // {
        //     if (Input.GetButtonDown("Jump") && controller.isGrounded)
        //     {
        //         Debug.Log("grounded");
        //         Debug.Log("jump");
        //         // agent.isStopped = true;
        //         // agent.ResetPath();
        //         moveDirection.y = jumpForce;
        //     }
        //     controller.Move(moveDirection * Time.deltaTime);
        // }
        
        moveDirection.y += (Physics.gravity.y * gravityScale * Time.deltaTime);

        // if (agent.enabled = true)
        // {
        //     if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        //     {
        //         ResetAgent();

        //         transform.rotation = Quaternion.Euler(0f, cam.transform.rotation.eulerAngles.y, 0f);
        //         // Debug.Log(cam.transform.rotation);
        //         Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
        //         transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotateSpeed * Time.deltaTime);

        //         controller.Move(moveDirection * Time.deltaTime);
        //     }
        // }

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            //Debug.Log("grounded");
            //Debug.Log("jump");
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

        // Move the player in different directions based on the camera directionf

        //if (Input.GetKeyDown(KeyCode.LeftCommand) || Input.GetKeyDown(KeyCode.RightCommand))
        if (Input.GetKeyDown(AttackKey))
        {
            ResetAgent();

            PlayerAttack();
            //animator.SetTrigger("attack");
        }

        if (Input.GetKeyDown(MagicKey))
        {
            ResetAgent();

            PlayerCastMagic();
            //animator.SetTrigger("attack");
        }

        animator.SetBool("isGrounded", controller.isGrounded);
        animator.SetFloat("speedPercent", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
        

        // float distance = Vector3.Distance(target.position, transform.position);
        
        // if (distance <= targetSelectRadius)
        // {
        //     FaceTarget();
        // }

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

    private void OnTriggerEnter(Collider other) 
    {
        switch (other.tag)
        {
            case ("Enemy"):
            {
                //Debug.Log("enemy");
                //Debug.Log(other);

                //target = other.transform;
                    //FaceTarget();
            }
            break;
        }
    }

    public void TeleportPlayer(Collider other) 
    {

    }

    public void PlayerAttack()
    {
        //Debug.Log("Player Attacks");
        //transform.GetComponentInChildren<HitDetection>().enabled = true;
        //animator.SetTrigger("attack");
        //characterAnimator.Attack();
        combat.actionState = PlayerCombat.Action.ATTACK;


        //transform.GetComponentInChildren<HitDetection>().enabled = false;
    }

    public void PlayerCastMagic()
    {
        //Debug.Log("Player Attacks");
        //transform.GetComponentInChildren<HitDetection>().enabled = true;
        //animator.SetTrigger("attack");
        //characterAnimator.Attack();
        combat.actionState = PlayerCombat.Action.MAGIC;


        //transform.GetComponentInChildren<HitDetection>().enabled = false;
    }
}
