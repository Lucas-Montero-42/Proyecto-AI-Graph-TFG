using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.IMGUI.Controls;
using UnityEditor.Callbacks;
using System;


public class FiniteStateGraphEditor : EditorWindow
{
    FiniteStateGraphView machineView;
    InspectorView inspectorView;

    [MenuItem("FiniteStateGraphEditor/Editor Window")]
    public static void OpenWindow()
    {
        FiniteStateGraphEditor wnd = GetWindow<FiniteStateGraphEditor>();
        wnd.titleContent = new GUIContent("FiniteStateGraphEditor");
    }

    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        if (Selection.activeObject is FSG_FiniteStateGraph)
        {
            OpenWindow();
            return true;
        }
        return false;        
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;


        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/FiniteStateMachine/EditorWindow/FiniteStateGraphEditor.uxml");
        visualTree.CloneTree(root);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/FiniteStateMachine/EditorWindow/FiniteStateGraphEditor.uss");
        root.styleSheets.Add(styleSheet);

        machineView = root.Q<FiniteStateGraphView>();
        inspectorView = root.Q<InspectorView>();
        machineView.OnStateSelected = OnStateSelectionChanged;
        OnSelectionChange();
    }

    private void OnEnable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }
    private void OnDisable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
    }
    private void OnPlayModeStateChanged(PlayModeStateChange change)
    {
        switch (change)
        {
            case PlayModeStateChange.EnteredEditMode:
                OnSelectionChange();
                break;
            case PlayModeStateChange.ExitingEditMode:
                break;
            case PlayModeStateChange.EnteredPlayMode:
                OnSelectionChange();
                break;
            case PlayModeStateChange.ExitingPlayMode:
                break;
        }
    }


    private void OnSelectionChange()
    {
        FSG_FiniteStateGraph machine = Selection.activeObject as FSG_FiniteStateGraph;
        if (!machine)
        {
            if (Selection.activeObject)
            {
                if (Selection.activeGameObject)
                {
                    FSG_Actor runner = Selection.activeGameObject.GetComponent<FSG_Actor>();
                    if (runner)
                    {
                        machine = runner.machine;
                    }
                }
            }
        }
        if (Application.isPlaying)
        {
            if (machine)
            {
                machineView.PopulateView(machine);
            }
        }
        else
        {
            if (machine && AssetDatabase.CanOpenAssetInEditor(machine.GetInstanceID()))
            {
                machineView.PopulateView(machine);
            }
        }
    }
     void OnStateSelectionChanged(StateView state)
    {
        inspectorView.UpdateSelection(state);
    }
    private void OnInspectorUpdate()
    {
        machineView?.UpdateStatesState();
    }
}