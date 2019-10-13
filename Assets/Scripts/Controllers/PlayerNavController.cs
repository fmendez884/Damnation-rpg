using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerNavController : MonoBehaviour
{
    // private string moveInputAxis = "Vertical";
    // private string turnInputAxis = "Horizontal";

    // public float rotationRate = 360;
    // public float moveSpeed = 2;
    public Interactable focus;
    public LayerMask movementMask;
    public Camera cam;
    public CharacterController controller;
    public PlayerController playerController;
    PlayerMotor motor;
    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        controller = GetComponent<CharacterController>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // float moveAxis = Input.GetAxis(moveInputAxis);
        // float turnAxis = Input.GetAxis(turnInputAxis);

        // ApplyInput(moveAxis, turnAxis);

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // if we press left mouse
        if (Input.GetMouseButtonDown(0))
        {   
            //we create a ray
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // if the ray hits
            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                // playerController.enabled = false;
                // Move our player to what we hit
                // Debug.Log(controller.enabled);
                Debug.Log("We hit" + hit.collider.name + " " + hit.point);
                motor.MoveToPoint(hit.point);
                // Stop focusing any objects
                RemoveFocus();
                // playerController.enabled = true;
            }
        }

        if (Input.GetKeyDown("space")) // GetMousButtonDown(1) ; right click on mouse
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                // Check if we hit an interactable
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                // If we did set it as our focus
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }

    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();
                
            focus = newFocus;
            newFocus.OnFocused(transform);
        }

        motor.FollowTarget(newFocus);
    }

    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
        motor.StopFollowingTarget();
    }

    // private void ApplyInput(float moveInput, float turnInput)
    // {
    //     Move(moveInput);
    //     Turn(turnInput);
    // }

    // private void Move(float input)
    // {
    //     transform.Translate(Vector3.forward * input * moveSpeed);
    // }

    // private void Turn(float input)
    // {
    //     transform.Rotate(0, input * rotationRate * Time.deltaTime, 0);
    // }
}
