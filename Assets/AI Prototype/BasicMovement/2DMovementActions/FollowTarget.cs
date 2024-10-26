using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementVars))]
public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public float distanceToFollow = 1f;
    private MovementVars movementVars;

    void Start()
    {
        movementVars = GetComponent<MovementVars>();
    }

    void Update()
    {
        if (target != null)
        {
            MoveTowardsTarget(Time.deltaTime, GetFollowPosition());
            float distanceToTarget = (transform.position - GetFollowPosition()).magnitude;
            if (distanceToTarget > movementVars.MatchThreshold)
                movementVars.LookAt2D(movementVars.direction);

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

    private void MoveTowardsTarget(float deltaTime, Vector3 pos)
    {
        movementVars.direction = (pos - transform.position).normalized;

        movementVars.ApplyAcceleration(deltaTime);

        transform.Translate(movementVars.currentVelocity * deltaTime, Space.World);
    }
    private void MatchLook()
    {
        transform.rotation = Quaternion.Euler(0, 0, target.eulerAngles.z);
    }
}