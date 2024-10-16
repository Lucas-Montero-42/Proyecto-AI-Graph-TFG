using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencerNode : BTG_CompositeNode
{
    int current;

    protected override void OnStart()
    {
        current = 0;
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        var child = Children[current];
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
