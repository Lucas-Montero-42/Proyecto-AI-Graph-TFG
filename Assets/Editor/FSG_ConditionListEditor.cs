using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomEditor(typeof(FSG_ConditionList))]
public class FSG_ConditionListEditor : Editor
{
    public VisualTreeAsset visualTree;

    private FSG_ConditionList FSGconditionList;
    private Button resetButton;

    private void OnEnable()
    {
        FSGconditionList = (FSG_ConditionList)target;
    }

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();

        // Añade los elementos de UIBuilder
        visualTree.CloneTree(root);

        //Busca el botón en la jerarquía
        resetButton = root.Q<Button>("ResetButton");
        resetButton.RegisterCallback<ClickEvent>(OnResetButton);

        return root;
    }

    private void OnResetButton(ClickEvent clic)
    {
        FSGconditionList.ResetAllConditions();
    }
}
