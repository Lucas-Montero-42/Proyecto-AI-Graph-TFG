using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoTo : BTG_ActionNode
{
    public enum LookProtocol {LookWherYouGo, LookAtTarget}
    public LookProtocol lookProtocol;
    protected override void OnStart()
    {
        actor.moveTo.enabled = true;
        actor.moveTo.targetPos = actor.targetObject.transform;
        orientation(true);
    }

    protected override void OnStop()
    {
        actor.moveTo.enabled = false;
        orientation(false);

    }

    protected override State OnUpdate()
    {
        if (actor.moveTo.arrived)
        {
            return State.Success;
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

}
