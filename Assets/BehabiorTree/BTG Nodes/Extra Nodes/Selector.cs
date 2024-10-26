using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : BTG_CompositeNode
{
    [StringInList(typeof(PropertyDrawersHelper), "BTG_Condition")]
    public string stateConditions;

    [StringInList(typeof(PropertyDrawersHelper), "BTG_ConditionFloat")]
    public string stateConditions2;

    private int current;

    protected override void OnStart()
    {
        current = 0;
    }

    protected override void OnStop()
    {
        actor.SetBool(stateConditions, false);
    }

    protected override State OnUpdate()
    {
        if(actor.GetBool(stateConditions))
        {
            actor.Clean();
            Children[current].state = State.Success;
            Children[current].started = false;
            current = 0;
            return State.Failure;
        }

        var child = Children[current];
        //Debug.Log(current);
        switch (child.Update())
        {
            case State.Running:
                return State.Running;
            case State.Failure:
                current++;
                break;
            case State.Success:
                current++;
                break;

        }
        return current == Children.Count ? State.Success : State.Running;
    }
}