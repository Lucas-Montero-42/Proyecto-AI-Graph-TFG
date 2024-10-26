using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class BTG_Actor : MonoBehaviour
{
    public BTG_BehaviorTree tree;
    [HideInInspector] public Attack attack;
    [HideInInspector] public AttackProjectile attackProjectile;
    [HideInInspector] public Orient orient;
    [HideInInspector] public Orient3D orient3D;
    [HideInInspector] public FindTarget findTarget;
    [HideInInspector] public FindNearestTarget findNearestTarget;
    [HideInInspector] public MoveTo moveTo;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animation alertAnim;
    [HideInInspector] public RangeManager rangeManager;
    public List<Transform> patrolPoints;
    [HideInInspector] public int CurrentPoint = 0;
    public GameObject targetObject;
    public float outsideAttackRange = 1f;
    public float insideAttackRange = .7f;

    //
    public BTG_ConditionList conditionList;

    public void SetBool(string _name, bool _value)
    {
        foreach (var i in conditionList.BTG_conditions)
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
        foreach (var i in conditionList.BTG_conditions)
        {
            if (i.name == _name)
            {
                return i.value;
            }
        }
        Debug.LogError("Variable '" + _name + "' not found");
        return false;
    }

    public void SetFloat(string _name, float _value)
    {
        foreach (var i in conditionList.BTG_floats)
        {
            if (i.name == _name)
            {
                i.value = _value;
                return;
            }
        }
        Debug.LogError("Variable '" + _name + "' not found");
    }
    public float GetFloat(string _name)
    {
        foreach (var i in conditionList.BTG_floats)
        {
            if (i.name == _name)
            {
                return i.value;
            }
        }
        Debug.LogError("Variable '" + _name + "' not found");
        return 0;
    }
    //

    void Start()
    {
        tree = tree.Clone();
        tree.Bind(GetComponent<BTG_Actor>());
        Clean();
        foreach (BTG_ConditionList.BTG_condition c in conditionList.BTG_conditions)
        {
            c.value = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        tree.Update();
    }
    public void Clean()
    {
        if (attack = GetComponent<Attack>()) { attack.enabled = true; }
        if (attackProjectile = GetComponent<AttackProjectile>()) { attackProjectile.enabled = true; }
        if (orient = GetComponent<Orient>()) { orient.enabled = false; }
        if (orient3D = GetComponent<Orient3D>()) { orient3D.enabled = false; }
        if (findTarget = GetComponent<FindTarget>()) { findTarget.enabled = true; }
        if (findNearestTarget = GetComponent<FindNearestTarget>()) { findNearestTarget.enabled = true; }
        if (moveTo = GetComponent<MoveTo>()) { moveTo.enabled = false; }
        if (agent = GetComponent<NavMeshAgent>()) { agent.enabled = false; }
        if (alertAnim = GetComponent<Animation>()) {  alertAnim.enabled = true; }
        if (rangeManager = GetComponent<RangeManager>()) { rangeManager.enabled = true; }
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,outsideAttackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,insideAttackRange);
    }
    public void AlertEffect()
    {
        alertAnim.Play();
    }
}
