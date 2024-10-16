using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateTo : BTG_ActionNode
{
    public float MinDistance = .1f;
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
    actor.agent.destination = actor.targetObject.transform.position;
    //Utilidando el sistema remaining distance da positvo nada mas empezar, y luego da negativo hasta que llega. Lo cual, rompe la causalidad del nodo
    if ((actor.targetObject.transform.position - actor.transform.position).magnitude < MinDistance)
    {
        return State.Success;
    }
    return State.Running;
}

}
