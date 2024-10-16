using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor;

public class StateView : UnityEditor.Experimental.GraphView.Node
{
    public Action<StateView> OnStateSelected;
    public FSG_State state;
    public Port input;
    public Port output;
    public StateView(FSG_State state) : base("Assets/FiniteStateMachine/EditorWindow/StateView.uxml")
    {
        this.state = state;
        this.title = state.name;
        this.viewDataKey = state.guid;

        style.left = state.position.x;
        style.top = state.position.y;

        CreateInputPorts();
        CreateOutputPorts();
        SetupColors();

        Label descriptionLable = this.Q<Label>("description");
        descriptionLable.bindingPath = "description";
        descriptionLable.Bind(new SerializedObject(state)); 
    }

    public void SetupColors()
    {
        if (state is StartState)
        {
            AddToClassList("White");
        }
        else
        { 
            if (state.color == FSG_State.Color.Red)
                AddToClassList("Red");
            if (state.color == FSG_State.Color.Purple)
                AddToClassList("Purple");
            if (state.color == FSG_State.Color.Blue)
                AddToClassList("Blue");
            if (state.color == FSG_State.Color.Cyan)
                AddToClassList("Cyan");
            if (state.color == FSG_State.Color.Green)
                AddToClassList("Green");
            if (state.color == FSG_State.Color.Yellow)
                AddToClassList("Yellow");
            if (state.color == FSG_State.Color.Orange)
                AddToClassList("Orange");
        }
    }
    private void CreateInputPorts()
    {
        if (state is StartState)
        {

        }
        else
        {
            input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
        }
        if (input!=null)
        {
            input.portName = "";
            inputContainer.Add(input);
        }
    }

    private void CreateOutputPorts()
    {
        if (state is StartState)
        {
            output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
        }
        else
        {
            output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
        }
        
        if (output!= null)
        {
            output.portName = "";
            outputContainer.Add(output);
        }
    }

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        Undo.RecordObject(state, "FSG_FiniteStateGraph (Set Position)");
        state.position.x = newPos.xMin;
        state.position.y = newPos.yMin;
        EditorUtility.SetDirty(state);
    }
    public override void OnSelected()
    {
        base.OnSelected();
        if (OnStateSelected != null)
        {
            OnStateSelected.Invoke(this);
        }
    }

    public void UpdateState()
    {

        RemoveFromClassList("running");
        RemoveFromClassList("failure");
        RemoveFromClassList("success");
        if (Application.isPlaying)
        {
            switch (state.state)
            {
                case FSG_State.State.Running:
                    if (state.started)
                        AddToClassList("running");
                    break;
                case FSG_State.State.Failure:
                    AddToClassList("failure");
                    break;
                case FSG_State.State.Success:
                    AddToClassList("success");
                    break;
            }
        }
    }
}
