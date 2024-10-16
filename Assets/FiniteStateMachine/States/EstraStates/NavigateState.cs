using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateState : FSG_State
{
    protected override void OnStart()
    {
        actor.agent.enabled = true;
        actor.agent.destination = actor.targetObject.transform.position;
    }

    protected override void OnStop()
    {
        actor.agent.enabled = false;
    }

    protected override State OnUpdate()
    {
        //Utilidando el sistema remaining distance da positvo nada mas empezar, y luego da negativo hasta que llega. Lo cual, rompe la causalidad del nodo
        if ((actor.targetObject.transform.position-actor.transform.position).magnitude < actor.agent.radius)
        {
            return State.Success;
        }
        return State.Running;
    }

}
