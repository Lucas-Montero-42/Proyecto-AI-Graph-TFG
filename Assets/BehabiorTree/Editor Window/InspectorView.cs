using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor;

public class InspectorView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits> { }

    Editor editor;
    public InspectorView(){}

    // Modifica la ventana del inspector cuando se selecciona un asset
    internal void UpdateSelection<T>(T view) where T : class
    {
        Clear();

        UnityEngine.Object.DestroyImmediate(editor);

        var target = GetTargetFromView(view);
        editor = Editor.CreateEditor(target);
        IMGUIContainer container = new IMGUIContainer(() =>
        {
            if (editor.target)
            {
                editor.OnInspectorGUI();
            }
        });
        Add(container);
    }
    // Diferencia entre Nodos de Behavior tree y Estaos de Maquina de estados
    private UnityEngine.Object GetTargetFromView<T>(T view) where T : class
    {
        if (view is NodeView nodeView)
        {
            return nodeView.BTnode;
        }
        else if (view is StateView stateView)
        {
            return stateView.state;
        }

        return null;
    }
}
