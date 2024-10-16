using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugState : FSG_State
{
    public string debugMessage = "Debug";
    protected override void OnStart()
    {
        Debug.Log(debugMessage);
        
    }

    protected override void OnStop()
    {
        // manda a tu hijo, al FSG para que lo ponga como siguiente
        //actor.NextChildren(this.children[0]); //o child
       
    }

    protected override State OnUpdate()
    {
        return State.Success;
    }
}
