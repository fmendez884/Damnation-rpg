using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerController = GetComponent<PlayerController>();
        // Debug.Log("sup");
    }

    // Update is called once per frame

    void Update() 
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            FaceTarget();
        }
    }
    public void MoveToPoint(Vector3 point)
    {
        // agent.velocity = playerController.velocity;
        agent.SetDestination(point);
        

        // if (!agent.pathPending)
        // {
        //     Debug.Log("test");
        //     Debug.Log(agent.remainingDistance);
        //     Debug.Log(agent.stoppingDistance);
        //     if (agent.remainingDistance <= agent.stoppingDistance)
        //     {
        //         Debug.Log("test 2");
        //         if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
        //         {
        //             Debug.Log(agent.pathStatus);
        //             playerController.enabled = true;
        //         }
        //     }
        // }

        
    }

    public void FollowTarget(Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.radius * .8f;
        agent.updateRotation = false;
        target = newTarget.interactionTransform;
    }

    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
        target = null;
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3 (direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
