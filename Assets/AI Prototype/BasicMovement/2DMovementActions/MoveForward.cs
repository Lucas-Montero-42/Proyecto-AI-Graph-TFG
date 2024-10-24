using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UIElements;

[RequireComponent(typeof(MovementVars))]
public class MoveForward : MonoBehaviour
{
    private MovementVars movementVars;

    void Start()
    {

        movementVars = GetComponent<MovementVars>();
    }

    void Update()
    {

        MoveInDirection(Time.deltaTime);
    }

    private void MoveInDirection(float deltaTime)
    {
        movementVars.direction = new Vector3(Mathf.Sin(-transform.eulerAngles.z * Mathf.Deg2Rad), Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad));

        movementVars.ApplyAcceleration(deltaTime);

        transform.Translate(movementVars.currentVelocity * deltaTime, Space.World);        
    }
}