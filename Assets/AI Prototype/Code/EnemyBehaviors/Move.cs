using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Move : MonoBehaviour
{
    public string TargetName;
    public Transform targetPos;
    public float Speed = 0;
   
    // Start is called before the first frame update
    void Start()
    {
        try{ targetPos = GameObject.Find(TargetName).transform; }
        catch { }
    }

    // Update is called once per frame
    void Update()
    {
            moveToTarget();
    }

    private void moveToTarget()
    {
        if (targetPos == null) { return; }
        Vector3 DistanceToTarget = targetPos.position - transform.position;
        Vector3 Direction = DistanceToTarget.normalized;
        gameObject.transform.position += Direction * Time.deltaTime * Speed;
    }
}
