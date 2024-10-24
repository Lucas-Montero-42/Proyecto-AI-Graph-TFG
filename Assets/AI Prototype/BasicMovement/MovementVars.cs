using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementVars : MonoBehaviour
{
    // Variables de movimiento
    public float moveSpeed = 5f;
    public float MatchThreshold = .5f;
    public float acceleration = 10f;
    public float deceleration = 10f;
    public Vector3 currentVelocity;
    public Vector3 direction;

    // Variables para plataformeros
    public float airControl = 1f;
    public float jumpForce = 10f;

    void Start()
    {
        currentVelocity = Vector3.zero;
    }

    public void ApplyAcceleration(float deltaTime)
    {
        currentVelocity = Vector3.Lerp(currentVelocity, direction * moveSpeed, deltaTime * acceleration);
    }

    public void ApplyDeceleration(float deltaTime)
    {
        currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, deltaTime * deceleration);
    }
    public void LookAt2D(Vector3 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
}