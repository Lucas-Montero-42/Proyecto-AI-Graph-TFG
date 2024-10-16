using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.Callbacks;
using System;
using Unity.VisualScripting;

public class BehaviorTreeEditor : EditorWindow
{
    BehaviorTreeView behaviorTreeView;
    SerializedObject behaviorTreeObject;
    
    IMGUIContainer blackboardView;
    SerializedProperty blackboardProperty;

    InspectorView windowInspectorView;

    // Menú desplegable en la barra de herramientas superior
    [MenuItem("/BehaviorTreeEditor/Editor Window")]
    
    [OnOpenAsset]
    public static bool OnOpenAssets(int instancedId, int line)
    {
        if (!(Selection.activeObject is BTG_BehaviorTree))
            return false;

        // Abririr la ventana personalizada AI graph
        BehaviorTreeEditor BTWindow = GetWindow<BehaviorTreeEditor>();
        BTWindow.titleContent = new GUIContent("BehaviorTreeEditor");
        return true;
    }

    public void CreateGUI()
    {
        // Añadir el nodo raíz al crear el BT
        VisualElement rootNode = rootVisualElement;
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/BehabiorTree/Editor Window/BehaviorTreeEditor.uxml");
        visualTree.CloneTree(rootNode);

        // Modificar el estilo visual de los nodos con el uxml y el uss
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/BehabiorTree/Editor Window/BehaviorTreeEditor.uss");
        rootNode.styleSheets.Add(styleSheet);

        behaviorTreeView = rootNode.Q<BehaviorTreeView>();
        blackboardView = rootNode.Q<IMGUIContainer>();
        windowInspectorView = rootNode.Q<InspectorView>();

        if (behaviorTreeObject != null)
        {
            blackboardView.onGUIHandler = () =>
            {
                // Actualizar el BT al abrirlo o modificarlo
                behaviorTreeObject.Update();
                EditorGUILayout.PropertyField(blackboardProperty);
                behaviorTreeObject.ApplyModifiedProperties();
            };
        }
        // Manejar la selección de los nodos
        behaviorTreeView.OnNodeSelected = OnNodeSelecionChanged;
        OnSelectionChange();

    }

    private void OnPlayModeStateChanged(PlayModeStateChange stateChange)
    {
        // Permirit modificar el esquema en modo edición y de juego
        if (stateChange == PlayModeStateChange.EnteredEditMode || stateChange == PlayModeStateChange.EnteredPlayMode)
        {
            OnSelectionChange();
        }
    }
    private void OnSelectionChange() 
    {
        // Comprovar que hay un asset de BT seleccionado (o un actor con un asset) para modificarlo
        BTG_BehaviorTree behaviorTree = Selection.activeObject as BTG_BehaviorTree;
        if (!behaviorTree && Selection.activeGameObject)
        {
            behaviorTree = Selection.activeGameObject?.GetComponent<BTG_Actor>().tree;
        }

        if (Application.isPlaying && behaviorTree)
        {
            behaviorTreeView?.PopulateView(behaviorTree);
        }
        else if(behaviorTree && AssetDatabase.CanOpenAssetInEditor(behaviorTree.GetInstanceID()))
        { 
            behaviorTreeView?.PopulateView(behaviorTree);
        }

        if (behaviorTree != null)
        {
            behaviorTreeObject = new SerializedObject(behaviorTree);
            blackboardProperty = behaviorTreeObject.FindProperty("blackboard");
        }
    }
    //Actualizar el Graph en función de la selección
    void OnNodeSelecionChanged(NodeView nodeView)
    {
        windowInspectorView.UpdateSelection(nodeView);
    }
    private void OnInspectorUpdate()
    {
        behaviorTreeView?.UpdateNodeStates();
    }
    // Actualizar el estado segun entra o sale del playmode
    private void OnEnable()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }
    private void OnDisable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
    }

    
}