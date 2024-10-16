using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopNode : BTG_DecoratorNode
{

    protected override void OnStart()
    {
        //Debug.Log("StartLoop");
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        child.Update();
        if (child.state == State.Failure) 
            return State.Failure;

        return State.Running;
    }
}
