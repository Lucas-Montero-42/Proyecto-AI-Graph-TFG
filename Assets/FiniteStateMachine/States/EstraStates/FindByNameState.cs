using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindByNameState : FSG_State
{
    public string findName;
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (GameObject.Find(findName))
        {
            actor.targetObject = GameObject.Find(findName);
            return State.Success;
        }
        else
        {
            Debug.LogError("Object not Found");
            return State.Failure;
        }
    }


}
