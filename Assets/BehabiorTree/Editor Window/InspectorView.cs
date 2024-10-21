using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEngine;

public class InspectorView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits> { }

    Editor editor;
    public InspectorView(){}

    internal void UpdateSelection<T>(T view) where T : class
    {
        Clear();

        UnityEngine.Object.DestroyImmediate(editor);

        var target = GetTargetFromView(view);
        editor = Editor.CreateEditor(target);
        IMGUIContainer container = new IMGUIContainer(() =>
        {
            // Personalización de estilo
            var originalColor = GUI.color;
            var originalFontStyle = EditorStyles.label.fontStyle;

            EditorStyles.label.fontSize = 14;
            EditorStyles.label.fontStyle = FontStyle.Bold;
            EditorStyles.label.alignment = TextAnchor.MiddleLeft; // Alinear a la izquierda

            // Cambiar el color del texto
            GUI.color = new Color(250f / 255f, 280f / 255f, 300f / 255f);
            // Cambiar el color de fondo
            GUI.backgroundColor = new Color(150f / 255f, 140f / 255f, 180f / 255f);


            // Iterar sobre las propiedades del objeto y personalizar su visualización
            SerializedObject serializedObject = editor.serializedObject;
            SerializedProperty property = serializedObject.GetIterator();
            property.NextVisible(true);
            while (property.NextVisible(false))
            {
                EditorGUILayout.PropertyField(property, new GUIContent(property.displayName), true);
            }

            // Restablecer estilos después de la personalización
            GUI.color = originalColor;
            EditorStyles.label.fontStyle = originalFontStyle;

            serializedObject.ApplyModifiedProperties();
        });
        Add(container);
    }
    // Dibuja la ventana del inspector cuando se selecciona un asset
    /*
    internal void UpdateSelection<T>(T view) where T : class
    {
        Clear();

        UnityEngine.Object.DestroyImmediate(editor);

        var target = GetTargetFromView(view);
        editor = Editor.CreateEditor(target);
        IMGUIContainer container = new IMGUIContainer(() =>
        {
            //Debug
            GUI.Label(new Rect(50, 30, 200, 16), "Hello World!");
            //Debug
            editor.DrawDefaultInspector();
             
            if (editor.target)
            {
                editor.OnInspectorGUI();
            }
        });
        Add(container);
    }
     */
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
