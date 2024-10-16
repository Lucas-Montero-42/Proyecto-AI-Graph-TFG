using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor;

public class NodeView : UnityEditor.Experimental.GraphView.Node
{
    public Action<NodeView> OnNodeSelected;
    public BTG_Node BTnode;
    public Port inputPort;
    public Port outputPort;
    public NodeView(BTG_Node node) : base("Assets/BehabiorTree/Editor Window/NodeView.uxml")
    {
        this.BTnode = node;
        this.title = node.name;
        this.viewDataKey = node.guid;
        style.left = node.position.x;
        style.top = node.position.y;

        CreateInputPorts();
        CreateOutputPorts();
        SetupClasses();

        Label descriptionLable = this.Q<Label>("description");
        descriptionLable.bindingPath = "description";
        descriptionLable.Bind(new SerializedObject(node));
    }
    // Añade Puerto de entrada
    private void CreateInputPorts()
    {
        switch (BTnode)
        {
            case BTG_ActionNode:
                inputPort = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
                break;
            case BTG_CompositeNode:
                inputPort = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
                break;
            case BTG_DecoratorNode:
                inputPort = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
                break;
            case BTG_RootNode:
                break; // Al ser el nodo raíz no tiene input
            default:
                break;
        }
        if (inputPort != null)
        {
            inputPort.portName = "";
            inputContainer.Add(inputPort);
        }
    }
    // Añade Puerto de salida
    private void CreateOutputPorts()
    {
        switch (BTnode)
        {
            case BTG_ActionNode:
                break; // Al ser un nodo acción no tiene output
            case BTG_CompositeNode:
                outputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));
                break;
            case BTG_DecoratorNode:
                outputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
                break;
            case BTG_RootNode:
                outputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
                break; 
            default:
                break;
        }
        if (outputPort != null)
        {
            outputPort.portName = "";
            outputContainer.Add(outputPort);
        }
    }
    // Añade información a los nodos según su tipo
    private void SetupClasses()
    {
        switch (BTnode)
        {
            case BTG_ActionNode:
                AddToClassList("action");
                break;
            case BTG_CompositeNode:
                AddToClassList("composite");
                break;
            case BTG_DecoratorNode:
                AddToClassList("decorator");
                break;
            case BTG_RootNode:
                AddToClassList("root");
                break;
            default:
                break;
        }
    }
    // Registra y cambia la posición de cada nodo
    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        UpdateNodePosition(newPos);
    }
    private void UpdateNodePosition(Rect newPos)
    {
        Undo.RecordObject(BTnode, "BTG Behavior Tree (Set Position)");
        BTnode.position = new Vector2(newPos.xMin, newPos.yMin);
        EditorUtility.SetDirty(BTnode);
    }
    public override void OnSelected()
    {
        base.OnSelected();
        if (OnNodeSelected != null)
        {
            OnNodeSelected.Invoke(this);
        }
    }
    // Cambia el orden de ejecución según la posición en el Graph
    public void SortChildren()
    {
        BTG_CompositeNode composite = BTnode as BTG_CompositeNode;
        if (composite)
        {
            composite.Children.Sort(SortByHorizontalPosition);
        }
    }
    private int SortByHorizontalPosition(BTG_Node left, BTG_Node right)
    {
        return left.position.x < right.position.x ? -1 : 1;
    }

    public void UpdateState()
    {
        RemoveFromClassList("failure");
        RemoveFromClassList("success");
        RemoveFromClassList("running");
        if (!Application.isPlaying)
            return;

        // Actualiza las clases según el estado del nodo
        if (BTnode.state == BTG_Node.State.Running && BTnode.started)
        {
            AddToClassList("running");
        }
        else if (BTnode.state == BTG_Node.State.Success)
        {
            AddToClassList("success");
        }
        else if (BTnode.state == BTG_Node.State.Failure)
        {
            AddToClassList("failure");
        }

    }
}
