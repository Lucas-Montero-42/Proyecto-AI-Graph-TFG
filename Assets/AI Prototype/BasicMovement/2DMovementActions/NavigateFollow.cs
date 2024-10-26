using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(MovementVars))]
public class NavigateFollow : MonoBehaviour
{
    public Transform target;
    public float distanceToFollow = 1f;
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
            agent.SetDestination(GetFollowPosition());
            float distanceToTarget = (transform.position - target.position).magnitude;
            if (distanceToTarget > movementVars.MatchThreshold)
                movementVars.LookAt2D(agent.velocity.normalized);

            else
                MatchLook();
        }
    }
    private Vector3 GetFollowPosition()
    {
        Vector3 direction = target.GetComponent<MovementVars>().currentVelocity.normalized;

        Vector3 positionBehind = target.position - direction * distanceToFollow;

        return positionBehind;
    }
    private void MatchLook()
    {
        transform.rotation = Quaternion.Euler(0, 0, target.eulerAngles.z);
    }
}
