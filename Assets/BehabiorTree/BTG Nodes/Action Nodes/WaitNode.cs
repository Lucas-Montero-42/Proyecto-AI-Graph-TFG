using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitNode : BTG_ActionNode
{
    public float duration = 1f;
    float startTime;

    public int a;
    public string b;
    public bool c;
    public char d;
    public enum E
    {
        ab,bc,cd
    };
    public Vector2 f;
    public List<int> g;
    public GameObject h;

    protected override void OnStart()
    {
        startTime = Time.time;
        
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (Time.time - startTime > duration)
        {
            return State.Success;
        }
        return State.Running;
    }
}
