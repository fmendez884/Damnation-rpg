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
    public NavMeshAgent agent;
    public GameObject cam;
    public Animator animator;

    private Vector3 moveDirection;
    public float rotateSpeed;
    public GameObject playerModel;

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("SUP");
        // theRB = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        cam = GameObject.Find("Main Camera");
        animator = GetComponentInChildren<Animator>();
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
        
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);

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
            Debug.Log("grounded");
            Debug.Log("jump");
            // agent.isStopped = true;
            // agent.ResetPath();
            moveDirection.y = jumpForce;
        }
        
        
        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {

            transform.rotation = Quaternion.Euler(0f, cam.transform.rotation.eulerAngles.y, 0f);
            // Debug.Log(cam.transform.rotation);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotateSpeed * Time.deltaTime);

        }

        controller.Move(moveDirection * Time.deltaTime);

        // Move the player in different directions based on the camera directionf

        if (Input.GetKeyDown(KeyCode.LeftCommand) || Input.GetKeyDown(KeyCode.RightCommand))
        {
            ResetAgent();

            Debug.Log("Attacks");
            animator.SetTrigger("attack");
        }
        
        animator.SetBool("isGrounded", controller.isGrounded);
        animator.SetFloat("speedPercent", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
        
    }

    public void ResetAgent() 
    {
        if (agent.enabled = true)
        {
            Debug.Log(agent);
            agent.isStopped = true;
            agent.ResetPath();
        }
    }
}
