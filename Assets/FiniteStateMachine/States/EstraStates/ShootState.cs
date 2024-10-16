using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : FSG_State
{
    public float Cooldown = 1f;
    float startTime;
    protected override void OnStart()
    {
        startTime = Time.time;
        actor.attack.Shoot();
        orientation(true);
        FixTarget(true);

    }

    protected override void OnStop()
    {
        orientation(false);
        FixTarget(false);
    }

    protected override State OnUpdate()
    {
        if (!actor.targetObject)
        {
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
            actor.orient.enabled = on;

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
