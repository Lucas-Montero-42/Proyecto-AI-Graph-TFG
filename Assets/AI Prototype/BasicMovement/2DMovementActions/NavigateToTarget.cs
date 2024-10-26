using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(MovementVars))]
public class NavigateToTarget : MonoBehaviour
{
    public Transform target;
    private MovementVars movementVars;
    private NavMeshAgent agent;

    void Start()
    {
        movementVars = GetComponent<MovementVars>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = true;
        agent.updateUpAxis = false;
        agent.speed = movementVars.moveSpeed;
        agent.acceleration = movementVars.acceleration;
        
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            float distanceToTarget = (transform.position - target.position).magnitude;
            if (distanceToTarget > movementVars.MatchThreshold)
                movementVars.LookAt2D(agent.velocity.normalized);

            else
                MatchLook();
        }
    }
    private void MatchLook()
    {
        transform.rotation = Quaternion.Euler(0, 0, target.eulerAngles.z);
    }
}