using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetNewPointState : FSG_State
{
    protected override void OnStart()
    {
        if (actor.CurrentPoint < actor.patrolPoints.Count - 1)
        {
            actor.CurrentPoint++;
        }
        else
        {
            actor.CurrentPoint = 0;
        }
        actor.targetObject = actor.patrolPoints[actor.CurrentPoint].gameObject;
        actor.moveTo.targetPos = actor.patrolPoints[actor.CurrentPoint];
        actor.targetObject = actor.patrolPoints[actor.CurrentPoint].gameObject;
        actor.moveTo.arrived = false;
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        Debug.Log("Got Point");
        return State.Success;
    }

}
