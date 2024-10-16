using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    BTG_Actor BTG_actor;
    FSG_Actor FSG_actor;

    public Transform CurrentTarget;

    public LineRenderer line;

    // Start is called before the first frame update
    private void Awake()
    {
        line.SetPosition(1, transform.position);
        if (GetComponent<BTG_Actor>()) { BTG_actor = GetComponent<BTG_Actor>(); }
        if (GetComponent<FSG_Actor>()) { FSG_actor = GetComponent<FSG_Actor>(); }
    }

    public void Shoot()
    {
        if (BTG_actor)
        {
            if (CurrentTarget == null && BTG_actor.targetObject != null) { CurrentTarget = BTG_actor.targetObject.transform; }
            else if (CurrentTarget == null) { CurrentTarget = BTG_actor.findTarget.GetTarget(); }
            else { CurrentTarget = BTG_actor.findTarget.GetTarget(); }
            if (CurrentTarget)
            {
                if (CurrentTarget.gameObject.GetComponent<HP>())
                {
                    CurrentTarget.gameObject.GetComponent<HP>().TakeDMG();
                    StartCoroutine(DrawLaser());
                }
            }
        }
        else if(FSG_actor)
        {
            if (CurrentTarget == null && FSG_actor.targetObject != null) { CurrentTarget = FSG_actor.targetObject.transform; }
            else if (CurrentTarget == null) { CurrentTarget = FSG_actor.findTarget.GetTarget(); }
            else { CurrentTarget = FSG_actor.findTarget.GetTarget(); }
            if (CurrentTarget)
            {
                if (CurrentTarget.gameObject.GetComponent<HP>())
                {
                    CurrentTarget.gameObject.GetComponent<HP>().TakeDMG();
                    StartCoroutine(DrawLaser());
                }
            }
        }
        
        
    }

    IEnumerator DrawLaser()
    {
        line.gameObject.SetActive(true);
        if (BTG_actor) { line.SetPosition(0, BTG_actor.targetObject.transform.position); }
        if (FSG_actor) { line.SetPosition(0, FSG_actor.targetObject.transform.position); }
        line.SetPosition(1, transform.position);
        yield return new WaitForSeconds(.2f);
        line.gameObject.SetActive(false);
    }
}
