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
    [Serializable]
    public class BTG_float
    {
        public string name;
        public float value;
        public float max;
        public float min;
    }
    public List<BTG_condition> BTG_conditions = new List<BTG_condition>();
    public List<BTG_float> BTG_floats = new List<BTG_float>();

    public void ResetAllConditions()
    {
        foreach (var condition in BTG_conditions)
        {
            condition.value = false;
        }
    }
}

