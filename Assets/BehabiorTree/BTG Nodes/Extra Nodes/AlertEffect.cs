using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertEffect : BTG_ActionNode
{
    protected override void OnStart()
    {
        actor.AlertEffect();
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return State.Success;
    }


}
