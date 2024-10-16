using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class FSG_State : ScriptableObject
{
    [HideInInspector] public FSG_State rootChild;
    //[HideInInspector]
    public List<FSG_State> children = new List<FSG_State>();
    [HideInInspector] public FSG_Actor actor;
    public enum Color
    {
        Red,
        Purple,
        Blue,
        Cyan,
        Green, 
        Yellow,
        Orange
    }
    public Color color;

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

    protected abstract void OnStart();
    protected abstract void OnStop();
    protected abstract State OnUpdate();

}
