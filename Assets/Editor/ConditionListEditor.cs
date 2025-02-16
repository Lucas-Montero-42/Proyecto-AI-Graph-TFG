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

    private BTG_ConditionList BTGconditionList;
    private Button resetButton;

    private void OnEnable()
    {
        BTGconditionList = (BTG_ConditionList)target;
    }

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();

        // A�ade los elementos de UIBuilder
        visualTree.CloneTree(root);

        //Busca el bot�n en la jerarqu�a
        resetButton = root.Q<Button>("ResetButton");
        resetButton.RegisterCallback<ClickEvent>(OnResetButton);

        return root;
    }

    private void OnResetButton(ClickEvent clic)
    {
        BTGconditionList.ResetAllConditions();
    }
}
