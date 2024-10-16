using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public Transform targetPos;
    public float Speed = 10;
    public float arriveMargin = 0.05f;
    public bool arrived = false;
    // Start is called before the first frame update
    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //targetPos = actor.patrolPoints[actor.CurrentPoint];
        moveToTarget();
    }

    private void moveToTarget()
    {
        if (targetPos == null) { return; }
        Vector3 DistanceToTarget = targetPos.position - transform.position;
        Vector3 Direction = DistanceToTarget.normalized;
        gameObject.transform.position += Direction * Time.deltaTime * Speed;
        if (DistanceToTarget.magnitude < arriveMargin)
        {
            arrived = true;
        }
        else
        {
            arrived = false;
        }
    }
}
