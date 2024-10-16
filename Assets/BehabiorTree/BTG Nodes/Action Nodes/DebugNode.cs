using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugNode : BTG_ActionNode
{
    public string Message = "Debug";
    protected override void OnStart()
    {
        Debug.Log(Message);
    }

    protected override void OnStop()
    {
        //Debug.Log($"OnStop{Message}");
    }

    protected override State OnUpdate()
    {
        //Debug.Log($"OnUpdate{Message}");
        return State.Success;
    }
}
