using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public Interactable focus;
    public LayerMask movementMask;
    public Camera cam;
    PlayerMotor motor;
    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
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
                // Move our player to what we hit
                Debug.Log("We hit" + hit.collider.name + " " + hit.point);
                motor.MoveToPoint(hit.point);
                // Stop focusing any objects
                RemoveFocus();
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
}
