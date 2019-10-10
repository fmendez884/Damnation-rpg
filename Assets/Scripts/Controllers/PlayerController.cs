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

    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("SUP");
        // theRB = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(moveSpeed);
        // theRB.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, theRB.velocity.y, Input.GetAxis("Vertical") * moveSpeed);

        // if (Input.GetButtonDown("Jump"))
        // {
        //     theRB.velocity = new Vector3(theRB.velocity.x, jumpForce, theRB.velocity.z);
        //     Debug.Log();
        // }

        moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 0f, Input.GetAxis("Vertical") * moveSpeed);

        if (Input.GetButtonDown("Jump"))
        {
            // Debug.Log("jump");
            moveDirection.y = jumpForce;
        }

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);

        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {
            agent.isStopped = true;
            agent.ResetPath();
            controller.Move(moveDirection * Time.deltaTime);
        }
        
    }
}
