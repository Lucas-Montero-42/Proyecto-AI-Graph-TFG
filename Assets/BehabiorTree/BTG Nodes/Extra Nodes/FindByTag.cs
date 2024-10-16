using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindByTag : BTG_ActionNode
{
    public string tag;
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (GameObject.Find(tag))
        {
            actor.targetObject = GameObject.FindGameObjectWithTag(tag);
            return State.Success;
        }
        else
        {
            Debug.LogError("Tag not Found");
            return State.Failure;
        }
    }


}
