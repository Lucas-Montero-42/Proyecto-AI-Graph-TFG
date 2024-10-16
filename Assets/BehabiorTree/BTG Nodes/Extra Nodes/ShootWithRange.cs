using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWithRange : BTG_ActionNode
{
    public float Cooldown = 1f;
    float startTime;
    public enum LookProtocol { LookAtTarget, NoAiming }
    public LookProtocol lookProtocol;
    protected override void OnStart()
    {
        startTime = Time.time;
        actor.attack.Shoot();
        if (lookProtocol == LookProtocol.LookAtTarget)
        {
            orientation(true);
            FixTarget(true);
        }
    }

    protected override void OnStop()
    {
        if (lookProtocol == LookProtocol.LookAtTarget)
        {
            orientation(false);
            FixTarget(false);
        }
    }

    protected override State OnUpdate()
    {
        if ((actor.targetObject.transform.position - actor.transform.position).magnitude > actor.outsideAttackRange)
        {
            Debug.Log("Fail by distance");
            return State.Failure;
        }
        if (!actor.targetObject)
        {
            Debug.Log("Fail by target object");
            return State.Failure;
        }
        if (Time.time - startTime > Cooldown)
        {
            if (actor.targetObject)
            {
                return State.Success;
            }
        }
        return State.Running;
    }
    private void orientation(bool on)
    {
        if (actor.orient)
            actor.orient3D.enabled = on;

        if (actor.orient3D)
            actor.orient3D.enabled = on;
    }
    private void FixTarget(bool on)
    {
        if (actor.orient)
        {
            if (on)
                actor.orient.AngularSpeed = 1000;
            else
                actor.orient.AngularSpeed = 0;
        }
        if (actor.orient3D)
        {
            if (on)
                actor.orient3D.AngularSpeed = 1000;
            else
                actor.orient3D.AngularSpeed = 0;
        }
    }
}
