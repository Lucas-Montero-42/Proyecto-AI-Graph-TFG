using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public static class PropertyDrawersHelper
{
#if UNITY_EDITOR
    /// <summary>
    /// Obtiene los nombres de las escenas del proyecto que estén en la lista de Build (File > Build Settings).
    /// </summary>
    /// <returns></returns>
    public static string[] BTG_Condition()
    {
        var temp = new List<string>();
        var asset = (BTG_ConditionList)AssetDatabase.LoadAssetAtPath("Assets/BehabiorTree/BTConditions.asset", typeof(BTG_ConditionList));

        foreach (BTG_ConditionList.BTG_condition c in asset.BTG_conditions)
        {
            temp.Add(c.name);
        }

        return temp.ToArray();
    }

    public static string[] BTG_ConditionFloat()
    {
        var temp = new List<string>();
        var asset = (BTG_ConditionList)AssetDatabase.LoadAssetAtPath("Assets/BehabiorTree/BTConditions.asset", typeof(BTG_ConditionList));

        foreach (BTG_ConditionList.BTG_float c in asset.BTG_floats)
        {
            temp.Add(c.name);
        }

        return temp.ToArray();
    }

    public static string[] FSG_Condition()
    {
        var temp = new List<string>();
        var asset = (FSG_ConditionList)AssetDatabase.LoadAssetAtPath("Assets/FiniteStateMachine/FSMConditions.asset", typeof(FSG_ConditionList));

        foreach (FSG_ConditionList.FSG_condition c in asset.FSG_conditions)
        {
            temp.Add(c.name);
        }

        return temp.ToArray();
    }
#endif
}