using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine;

public class FiniteStateGraphView : GraphView
{
    public Action<StateView> OnStateSelected;
    public new class UxmlFactory : UxmlFactory<FiniteStateGraphView, GraphView.UxmlTraits> { } // con esto aparecen los custom controls ya que el sistema graph view es experimental
    FSG_FiniteStateGraph machine;

    public FiniteStateGraphView()
    {
        Insert(0, new GridBackground()); //Grid

        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/FiniteStateMachine/EditorWindow/FiniteStateGraphEditor.uss");
        styleSheets.Add(styleSheet);

        Undo.undoRedoPerformed += OnUndoRedo;
    }

    private void OnUndoRedo()
    {
        PopulateView(machine);
        AssetDatabase.SaveAssets();
    }

    StateView FindStateView(FSG_State state)
    {
        return GetNodeByGuid(state.guid) as StateView;
    }

    internal void PopulateView(FSG_FiniteStateGraph _machine)
    {
        this.machine = _machine;

        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChanged;

        if (machine.Start == null)
        {
            machine.Start = machine.CreateState(typeof(StartState)) as StartState;
            machine.currentState = machine.Start;
            EditorUtility.SetDirty(_machine);
            AssetDatabase.SaveAssets();
        }

        _machine.states.ForEach(s => CreateStateView(s));

        _machine.states.ForEach(s =>
        {
            var children = _machine.GetChildren(s);
            children.ForEach(c =>
            {
                StateView parentView = FindStateView(s);
                StateView childView = FindStateView(c);

                Edge edge = parentView.output.ConnectTo(childView.input);
                AddElement(edge);
            });
        });
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList().Where(endPort =>
        endPort.direction != startPort.direction &&
        endPort.node != startPort.node).ToList();
    }

    private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
    {
        if (graphViewChange.elementsToRemove != null)
        {
            graphViewChange.elementsToRemove.ForEach(elem =>
            {
                StateView stateView = elem as StateView;
                if (stateView != null)
                {
                    machine.DeleteState(stateView.state);
                }

                Edge edge = elem as Edge;
                if (edge != null)
                {
                    StateView parentView = edge.output.node as StateView;
                    StateView childView = edge.input.node as StateView;
                    machine.RemoveChild(parentView.state, childView.state);
                }
            });
        }

        if (graphViewChange.edgesToCreate != null)
        {
            graphViewChange.edgesToCreate.ForEach(edge => 
            {
                StateView parentView = edge.output.node as StateView;
                StateView childView = edge.input.node as StateView;
                machine.AddChild(parentView.state, childView.state);
            });
             
        }
        if (graphViewChange.movedElements != null)
        {
            //EditorUtility.SetDirty(machine);
            //AssetDatabase.SaveAssets();
        }

        return graphViewChange;
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        //base.BuildContextualMenu(evt);  
        {
            var types = TypeCache.GetTypesDerivedFrom<FSG_State>();
            foreach (var type in types)
            {
                evt.menu.AppendAction($"[{type.BaseType.Name}]{type.Name}", (a) => CreateState(type));
            }
        }
    }
    void CreateState(System.Type type)
    {
        FSG_State state = machine.CreateState(type);
        CreateStateView(state);
    }
    void CreateStateView(FSG_State state)
    {
        StateView stateView = new StateView(state);
        stateView.OnStateSelected = OnStateSelected;
        AddElement(stateView);
    }
    public void UpdateStatesState()
    {
        nodes.ForEach(s =>
        {
            StateView view = s as StateView;
            view.UpdateState();
        });
    }
    
}
