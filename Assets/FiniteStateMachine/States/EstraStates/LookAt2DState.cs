using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt2DState : FSG_State
{
    public float AngularSpeed = 10;
    [StringInList(typeof(PropertyDrawersHelper), "FSG_Condition")]
    public string FSG_Oriented;
    protected override void OnStart()
    {
        actor.orient.enabled = true;
        actor.SetBool(FSG_Oriented, false);
    }

    protected override void OnStop()
    {
        actor.orient.enabled = false;
        actor.SetBool(FSG_Oriented, false);
    }

    protected override State OnUpdate()
    {
        if (actor.targetObject)
        {
            if (actor.GetBool(FSG_Oriented))
            {
                return State.Success;
            }

            return State.Running;
        }
        else
        {
            return State.Failure;
        }

    }

}
