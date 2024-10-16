using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchTarget : BTG_ActionNode
{
    protected override void OnStart()
    {
       
    }

    protected override void OnStop()
    {
        actor.targetObject = actor.findTarget.GetTarget().gameObject;
    }

    protected override State OnUpdate()
    {
        if (actor.findTarget.GetTarget())
        {
            return State.Success;
        }
        return State.Running;
    }
}
