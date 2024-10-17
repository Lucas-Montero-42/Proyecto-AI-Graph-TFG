using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewConditionList", menuName = "BTG_Condition List", order = 2)]
public class BTG_ConditionList : ScriptableObject
{
    [Serializable]
    public class BTG_condition
    {
        public string name;
        public bool value;
    }
    public List<BTG_condition> BTG_conditions = new List<BTG_condition>();

    public void ResetAllConditions()
    {
        foreach (var condition in BTG_conditions)
        {
            condition.value = false;
        }
    }
}

