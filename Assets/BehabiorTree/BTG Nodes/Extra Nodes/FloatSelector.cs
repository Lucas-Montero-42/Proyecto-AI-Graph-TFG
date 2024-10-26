using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatSelector : BTG_CompositeNode
{

    [StringInList(typeof(PropertyDrawersHelper), "BTG_ConditionFloat")]
    public string SelectorValue;


    public float Threshold;
    public enum Condition
    {
        GreaterThan,
        GreaterOrEqualTo,
        LowerThan,
        LowerOrEqualTo,
        EqualTo
    }
    public Condition condition;
    //Variable para decidir si es mayor o menor igual un enum
    
    
    private int current;

    protected override void OnStart()
    {
        current = 0;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {       

        if (condition == Condition.GreaterThan && actor.GetFloat(SelectorValue)>Threshold)
        {
            actor.Clean();
            Children[current].state = State.Success;
            Children[current].started = false;
            current = 0;
            Debug.Log("NO");
            return State.Failure;
        }
        if (condition == Condition.GreaterOrEqualTo && actor.GetFloat(SelectorValue) >= Threshold)
        {
            actor.Clean();
            Children[current].state = State.Success;
            Children[current].started = false;
            current = 0;
            Debug.Log("NO");
            return State.Failure;
        }
        else if (condition == Condition.LowerThan && actor.GetFloat(SelectorValue) < Threshold)
        {
            actor.Clean();
            Children[current].state = State.Success;
            Children[current].started = false;
            current = 0;
            Debug.Log("NO");
            return State.Failure;
        }
        else if (condition == Condition.LowerOrEqualTo && actor.GetFloat(SelectorValue) <= Threshold)
        {
            actor.Clean();
            Children[current].state = State.Success;
            Children[current].started = false;
            current = 0;
            Debug.Log("Si");
            return State.Failure;
        }
        else if (condition == Condition.EqualTo && actor.GetFloat(SelectorValue) == Threshold)
        {
            actor.Clean();
            Children[current].state = State.Success;
            Children[current].started = false;
            current = 0;
            Debug.Log("NO");
            return State.Failure;
        }
        else
        {
            Debug.LogWarning("Float Selector: " + name + ". Was not properly set up");
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