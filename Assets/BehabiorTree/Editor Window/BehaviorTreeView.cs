using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine;

public class BehaviorTreeView : GraphView
{
    public Action<NodeView> OnNodeSelected;
    // Con esto aparecen los custom controls ya que el sistema graph view es experimental
    public new class UxmlFactory : UxmlFactory<BehaviorTreeView, GraphView.UxmlTraits> { } 
    BTG_BehaviorTree behaviorTree;

    public BehaviorTreeView()
    {
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/BehabiorTree/Editor Window/BehaviorTreeEditor.uss");
        // Funciones basicas para el Graph: selección, arrastrar y hacer zoom
        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        styleSheets.Add(styleSheet);
        Undo.undoRedoPerformed += OnUndoRedo;
        Insert(0, new GridBackground());
    }

    internal void PopulateView(BTG_BehaviorTree _behaviorTree)
    {
        this.behaviorTree = _behaviorTree;
        graphViewChanged -= OnGraphViewChanged;

        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChanged;

        if (behaviorTree.rootNode == null)
        {
            behaviorTree.rootNode = behaviorTree.CreateNode(typeof(BTG_RootNode)) as BTG_RootNode;
            EditorUtility.SetDirty(_behaviorTree);
            AssetDatabase.SaveAssets();
        }

        ConnectNodes(_behaviorTree);
        
    }

    // Permite unicamente conectar puertos compatibles (Entrada -> Salida -/> Salida -> Entrada)
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList().Where(endPort => endPort.direction != startPort.direction && endPort.node != startPort.node).ToList();
    }

    private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
    {
        if (graphViewChange.elementsToRemove != null)
            HandleElementsToRemove(graphViewChange.elementsToRemove);

        if (graphViewChange.edgesToCreate != null)
            HandleEdgesToCreate(graphViewChange.edgesToCreate);

        if (graphViewChange.movedElements != null)
            HandleMovedElements();

        return graphViewChange;
    }

    // Elimina los nodos y conexiones borrados al modificar el Graph
    private void HandleElementsToRemove(List<GraphElement> elementsToRemove)
    {
        foreach (var elem in elementsToRemove)
        {
            if (elem is NodeView nodeView)
            {
                behaviorTree.DeleteNode(nodeView.BTnode);
            }

            if (elem is Edge edge)
            {
                var parentView = edge.output.node as NodeView;
                var childView = edge.input.node as NodeView;
                if (parentView != null && childView != null)
                {
                    behaviorTree.RemoveChild(parentView.BTnode, childView.BTnode);
                }
            }
        }
    }

    // Añade las conexiones entre nodos al modificar el Graph
    private void HandleEdgesToCreate(List<Edge> edgesToCreate)
    {
        foreach (var edge in edgesToCreate)
        {
            var parentView = edge.output.node as NodeView;
            var childView = edge.input.node as NodeView;
            if (parentView != null && childView != null)
            {
                behaviorTree.AddChild(parentView.BTnode, childView.BTnode);
            }
        }
    }

    // Actualiza la posición de los nodos del Graph
    private void HandleMovedElements()
    {
        nodes.ForEach(n =>
        {
            if (n is NodeView view)
            {
                view.SortChildren();
            }
        });
    }

    // Crea un menú desplegable para añadir nodos en el Graph
    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        AddMenuItemsForType<BTG_ActionNode>(evt, "[Action]");
        AddMenuItemsForType<BTG_CompositeNode>(evt, "[Composite]");
        AddMenuItemsForType<BTG_DecoratorNode>(evt, "[Decorator]");
    }

    private void AddMenuItemsForType<T>(ContextualMenuPopulateEvent evt, string labelPrefix) where T : BTG_Node
    {
        var types = TypeCache.GetTypesDerivedFrom<T>();
        foreach (var type in types)
        {
            evt.menu.AppendAction($"{labelPrefix} {type.Name}", (a) => CreateNode(type));
        }
    }

    // Crea los nodos desde el menú contextual
    void CreateNode(System.Type type)
    {
        BTG_Node node = behaviorTree.CreateNode(type);
        CreateNodeView(node);
    }

    // Al abrir un asset, añade los nodos al arbol y los muestra conectados en el Graph
    void ConnectNodes(BTG_BehaviorTree _behaviorTree)
    {
        _behaviorTree.nodes.ForEach(n => CreateNodeView(n));
        _behaviorTree.nodes.ForEach(n =>
        {
            var children = _behaviorTree.GetChildren(n);
            children.ForEach(c =>
            {
                NodeView parentView = FindNodeView(n);
                NodeView childView = FindNodeView(c);

                Edge edge = parentView.outputPort.ConnectTo(childView.inputPort);
                AddElement(edge);
            });
        });
    }

    // Busca el Node View
    NodeView FindNodeView(BTG_Node node)
    {
        return GetNodeByGuid(node.guid) as NodeView;
    }

    // Si no hay un Node View lo crea
    void CreateNodeView(BTG_Node node)
    {
        NodeView nodeView = new NodeView(node);
        nodeView.OnNodeSelected = OnNodeSelected;
        AddElement(nodeView);
    }

    // Actualiza los nodos al modificar el editor
    public void UpdateNodeStates()
    {
        nodes.ForEach(n =>
        {
            NodeView view = n as NodeView;
            view.UpdateState();
        });
    }

    // Guarda los cambios y actualiza el arbol del Graph
    private void OnUndoRedo()
    {
        PopulateView(behaviorTree);
        AssetDatabase.SaveAssets();
    }
    
}
