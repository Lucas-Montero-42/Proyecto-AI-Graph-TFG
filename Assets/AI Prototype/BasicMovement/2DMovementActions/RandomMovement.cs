using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    private MovementVars movementVars;
    public float lerpSpeed = 5f; 
    public float minWaitTime = 1f; 
    public float maxWaitTime = 2f;
    private float targetAngle;

    void Start()
    {
        movementVars = GetComponent<MovementVars>();
        StartCoroutine(ChangeRotationRoutine());
    }

    void Update()
    {
        MoveInDirection(Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle), lerpSpeed * Time.deltaTime);
    }
    private void MoveInDirection(float deltaTime)
    {
        movementVars.direction = new Vector3(Mathf.Sin(-transform.eulerAngles.z * Mathf.Deg2Rad), Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad));

        movementVars.ApplyAcceleration(deltaTime);

        transform.Translate(movementVars.currentVelocity * deltaTime, Space.World);
    }
    private IEnumerator ChangeRotationRoutine()
    {
        while (true)
        {
            // Generar un nuevo ángulo aleatorio entre 0 y 360
            targetAngle = UnityEngine.Random.Range(0f, 360f);

            // Esperar hasta alcanzar la rotación objetivo
            while (Mathf.Abs(NormalizeAngle(transform.eulerAngles.z - targetAngle)) > 1f)
            {
                yield return null; // Esperar hasta el siguiente frame
            }

            // Esperar entre 1 y 2 segundos
            yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 2f));
        }
    }

    private float NormalizeAngle(float angle)
    {
        while (angle > 180) angle -= 360;
        while (angle < -180) angle += 360;
        return angle;
    }
}
