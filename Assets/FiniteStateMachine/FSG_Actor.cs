using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSG_Actor : MonoBehaviour
{
    public FSG_FiniteStateGraph machine;

    [HideInInspector] public Attack attack;
    [HideInInspector] public Orient orient;
    [HideInInspector] public Orient3D orient3D;
    [HideInInspector] public FindTarget findTarget;
    [HideInInspector] public FindNearestTarget findNearestTarget;
    [HideInInspector] public MoveTo moveTo;
    [HideInInspector] public NavMeshAgent agent;
    public List<Transform> patrolPoints;
    [HideInInspector] public int CurrentPoint = 0;
    public GameObject targetObject;
    //public bool Oriented = true;
    //public bool Detected = false;

    public float outsideAttackRange = 1f;
    public float insideAttackRange = .7f;

    //
    public FSG_ConditionList conditionList;

    public void SetBool(string _name, bool _value)
    {
        foreach (var i in conditionList.FSG_conditions)
        {
            if (i.name == _name)
            {
                i.value = _value;
                return;
            }
        }
        Debug.LogError("Variable '" + _name + "' not found");
    }
    public bool GetBool(string _name)
    {
        foreach (var i in conditionList.FSG_conditions)
        {
            if (i.name == _name)
            {
                return i.value;
            }
        }
        Debug.LogError("Variable '" + _name + "' not found");
        return false;
    }
    //

    void Start()
    {
        machine = machine.Clone();
        machine.Bind(this);
        Clean();
    }

    // Update is called once per frame
    void Update()
    {
        machine.Update();
    }
    public void NextChildren(FSG_State nextState)
    {
        machine.currentState = nextState;
    }

    public void Clean()
    {
        if (attack = GetComponent<Attack>()) { attack.enabled = true; }
        if (orient = GetComponent<Orient>()) { orient.enabled = false; }
        if (orient3D = GetComponent<Orient3D>()) { orient3D.enabled = false; }
        if (findTarget = GetComponent<FindTarget>()) { findTarget.enabled = true; }
        if (findNearestTarget = GetComponent<FindNearestTarget>()) { findNearestTarget.enabled = true; }
        if (moveTo = GetComponent<MoveTo>()) { moveTo.enabled = false; }
        if (agent = GetComponent<NavMeshAgent>()) { agent.enabled = false; }
    }

}
