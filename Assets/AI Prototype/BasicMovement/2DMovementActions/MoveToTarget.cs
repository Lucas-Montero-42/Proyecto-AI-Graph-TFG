using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(MovementVars))]
public class MoveToTarget : MonoBehaviour
{
    public Transform target;
    private MovementVars movementVars;

    void Start()
    {
        movementVars = GetComponent<MovementVars>();
    }

    void Update()
    {
        if (target != null)
        {
            MoveTowardsTarget(Time.deltaTime);
            float distanceToTarget = (transform.position - target.position).magnitude;
            if (distanceToTarget > movementVars.MatchThreshold)
                movementVars.LookAt2D(movementVars.direction);

            else
                MatchLook();
        }
    }

    private void MoveTowardsTarget(float deltaTime)
    {
        movementVars.direction = (target.position - transform.position).normalized;

        movementVars.ApplyAcceleration(deltaTime);

        transform.Translate(movementVars.currentVelocity * deltaTime, Space.World);        
    }
    private void MatchLook()
    {
        transform.rotation = Quaternion.Euler(0, 0, target.eulerAngles.z);
    }
}