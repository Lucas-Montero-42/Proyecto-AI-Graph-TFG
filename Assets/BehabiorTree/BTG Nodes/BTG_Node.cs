using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class BTG_Node : ScriptableObject
{
    public enum State
    {
        Running,
        Failure,
        Success
    }

    [HideInInspector] public State state = State.Running;
    [HideInInspector] public bool started = false;
    [HideInInspector] public string guid;
    [HideInInspector] public Vector2 position;
    [HideInInspector] public BTG_Actor actor;
    [TextArea] public string description;

    public State Update()
    {
        if (!started)
        {
            OnStart();
            started = true;
        }
        state = OnUpdate();

        if (state == State.Failure || state == State.Success)
        {
            OnStop();
            started = false;
        }
        return state;
    }

    public virtual BTG_Node Clone()
    {
        return Instantiate(this);
    }
    protected abstract void OnStart();
    protected abstract void OnStop();
    protected abstract State OnUpdate();
}
