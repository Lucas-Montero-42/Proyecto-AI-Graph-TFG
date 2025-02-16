using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BTG_ConditionList;

[CreateAssetMenu(fileName = "NewConditionList", menuName = "FSG_Condition List", order = 4)]
public class FSG_ConditionList : ScriptableObject
{
    [Serializable]
    public class FSG_condition
    {
        public string name;
        public bool value;
    }
    public List<FSG_condition> FSG_conditions = new List<FSG_condition>();
    public void ResetAllConditions()
    {
        foreach (var condition in FSG_conditions)
        {
            condition.value = false;
        }
    }
}

