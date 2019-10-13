using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Transform enemyTarget;
    public Vector3 offset;
    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;
    public float pitch = 2f;
    public float yawSpeed = 100f;
    public float currentYaw = 0f;
    private float currentZoom = 6f;

    // public Transform pivot;

    void Start() 
    {
        currentZoom = maxZoom - minZoom;
    }
    void Update ()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        // currentYaw += Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentYaw += -1f * yawSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            currentYaw += 1f * yawSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            //Debug.Log("KeyUp  " + currentZoom);
            currentZoom -= 1f * zoomSpeed * Time.deltaTime;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            //Debug.Log("KeyDown  " + currentZoom);
            currentZoom -= -1f * zoomSpeed * Time.deltaTime;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        }

        // Debug.Log("currentZoom   " + currentZoom);
        // Debug.Log("scroll  " + Input.GetAxis("Mouse ScrollWheel"));

        
    }
    void LateUpdate() {
        {
            if (enemyTarget == null)
            {
                transform.position = target.position - offset * currentZoom;
                transform.LookAt(target.position + Vector3.up * pitch);

                transform.RotateAround(target.position, Vector3.up, currentYaw);
            }
            else
            {
                transform.position = target.position - offset * currentZoom;
                transform.LookAt(enemyTarget.position + Vector3.up * pitch);

                transform.RotateAround(target.position, Vector3.up, currentYaw);
            }
        }
    }

}
