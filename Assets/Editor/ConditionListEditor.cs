using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomEditor(typeof (BTG_ConditionList))]
public class ConditionListEditor : Editor
{
    public VisualTreeAsset visualTree;
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();

        // Añade los elementos de UIBuilder
        visualTree.CloneTree(root);

        return root;
    }
}
