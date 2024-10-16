using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt3D : BTG_ActionNode
{
    public float _AngularSpeed = 180;
    [StringInList(typeof(PropertyDrawersHelper), "BTG_Condition")]
    public string BTG_Oriented;

    protected override void OnStart()
    {
        actor.SetBool(BTG_Oriented, false);
        actor.orient3D.enabled = true;
        actor.orient3D.AngularSpeed = _AngularSpeed;
    }

    protected override void OnStop()
    {
       
        actor.orient3D.enabled = false;
        actor.SetBool(BTG_Oriented, false);
    }

    protected override State OnUpdate()
    {
        //Debug.Log(started);
        if (actor.targetObject)
        {
            if(actor.GetBool(BTG_Oriented))
            {
                actor.SetBool(BTG_Oriented, true);
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
