using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : BTG_ActionNode
{
    protected override void OnStart()
    {
        actor.moveTo.enabled = true;
        actor.moveTo.targetPos = actor.patrolPoints[actor.CurrentPoint];
        actor.targetObject = actor.patrolPoints[actor.CurrentPoint].gameObject;
    }

    protected override void OnStop()
    {
        actor.moveTo.enabled = false;
    }

    protected override State OnUpdate()
    {
        if (actor.moveTo.arrived)
        {
            return State.Success;
        }
        return State.Running;
    }
}
