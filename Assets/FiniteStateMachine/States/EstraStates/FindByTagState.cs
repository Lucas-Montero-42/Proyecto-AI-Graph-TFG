using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindByTagState : FSG_State
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
