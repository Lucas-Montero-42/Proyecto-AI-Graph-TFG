using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;

[CreateAssetMenu(fileName = "NewFiniteStateGraph", menuName = "FSG_FiniteStateGraph", order = 3)]
public class FSG_FiniteStateGraph : ScriptableObject
{
    public FSG_State Start;
    public FSG_State currentState;
    public FSG_State.State MachineState = FSG_State.State.Running;
    public List<FSG_State> states = new List<FSG_State>();
    public bool machineStarted = true;

    public FSG_State.State Update()
    {
        if (machineStarted)
        {
            currentState = Start;
            machineStarted = false;
        }


        if (currentState is StartState)
        {
            if (currentState.state != FSG_State.State.Running && currentState.rootChild != null)
            {
                currentState = currentState.rootChild;
                currentState.state = FSG_State.State.Running;
            }
        }
        else
        {
            if(currentState.state != FSG_State.State.Running && currentState.children.Count != 0)
            {
                if (currentState.children[0] != null)
                {

                    currentState = currentState.children[0];
                    currentState.started = false;
                    currentState.state = FSG_State.State.Running;
                }
            }
        }
        if (currentState.state == FSG_State.State.Running)
        {
            return currentState.Update();
        }
        return currentState.state;
    }
#if UNITY_EDITOR
    public FSG_State CreateState(System.Type type)
    {
        FSG_State state = ScriptableObject.CreateInstance(type) as FSG_State;
        state.name = type.Name;
        state.guid = GUID.Generate().ToString();

        Undo.RecordObject(this, "FSG_FiniteStateGraph (CreateState)");
        states.Add(state);

        if (!Application.isPlaying)
        {
            AssetDatabase.AddObjectToAsset(state, this);
        }

        Undo.RegisterCreatedObjectUndo(state, "FSG_FiniteStateGraph (CreateState)");

        AssetDatabase.SaveAssets();

        return state;
    }

    public void DeleteState (FSG_State state)
    {

        Undo.RecordObject(this, "FSG_FiniteStateGraph (DeleteState)");
        states.Remove(state);

        Undo.DestroyObjectImmediate(state);
        //AssetDatabase.RemoveObjectFromAsset(state);
        AssetDatabase.SaveAssets();
    }

    public void AddChild(FSG_State parent, FSG_State child)
    {
        FSG_State state = parent as FSG_State;
        StartState startState = state as StartState;

        foreach(FSG_State s in parent.children)
        {
            if (s == child)
            {
                return;
            }
        }

        if (state && !startState)
        {
            Undo.RecordObject(state, "FSG_FiniteStateGraph (AddChild)");
            state.children.Add(child);
            EditorUtility.SetDirty(state);
        }
        if (startState)
        {
            Undo.RecordObject(startState, "FSG_FiniteStateGraph (AddChild)");
            startState.rootChild = child;
            EditorUtility.SetDirty(startState);
            
        }
    }

    public void RemoveChild(FSG_State parent, FSG_State child)
    {
        FSG_State state = parent as FSG_State;
        StartState startState = parent as StartState;
        if (state && !startState)
        {
            Undo.RecordObject(state, "FSG_FiniteStateGraph (RemoveChild)");
            state.children.Remove(child);
            EditorUtility.SetDirty(state);
        }
        if (startState)
        {
            Undo.RecordObject(startState, "FSG_FiniteStateGraph (RemoveChild)");
            startState.rootChild = null;
            EditorUtility.SetDirty(startState);
        }
    }

    public List<FSG_State> GetChildren(FSG_State parent)
    {
        List<FSG_State> children = new List<FSG_State>();

        FSG_State state = parent as FSG_State;
        StartState startState = parent as StartState;

        if (state && !startState)
        {
            return state.children;
        }
        if (startState && startState.rootChild != null)
        {
            children.Add(startState.rootChild);
        }

        return children;
    }
#endif

    public void Traverse(FSG_State startState, System.Action<FSG_State> visiter)
    {
        if (startState == null)
            return;

        Queue<FSG_State> queue = new Queue<FSG_State>();
        HashSet<FSG_State> visited = new HashSet<FSG_State>();

        queue.Enqueue(startState);

        while (queue.Count > 0)
        {
            FSG_State current = queue.Dequeue();

            if (visited.Contains(current))
                continue;

            visiter.Invoke(current);
            visited.Add(current);

            var children = GetChildren(current);
            foreach (var child in children)
            {
                if (!visited.Contains(child))
                    queue.Enqueue(child);
            }
        }
    }
    public FSG_FiniteStateGraph Clone()
    {
        // Crear una nueva instancia de la máquina de estados
        FSG_FiniteStateGraph clonedGraph = Instantiate(this);
        clonedGraph.states = new List<FSG_State>(); // Inicializar la lista de estados en la máquina clonada

        // Diccionario para mapear estados originales a sus clones
        Dictionary<FSG_State, FSG_State> stateMapping = new Dictionary<FSG_State, FSG_State>();

        // Clonar todos los estados y crear el mapeo
        Traverse(Start, state =>
        {
            FSG_State clonedState = Instantiate(state);
            clonedState.children = new List<FSG_State>();
            stateMapping[state] = clonedState;
            clonedGraph.states.Add(clonedState); // Añadir el estado clonado a la lista de estados
        });

        // Asignar los hijos correctos a cada estado clonado
        Traverse(Start, state =>
        {
            FSG_State clonedState = stateMapping[state];
            foreach (var child in state.children)
            {
                clonedState.children.Add(stateMapping[child]);
            }
        });

        // Establecer el estado inicial del gráfico clonado
        clonedGraph.Start = stateMapping[Start];

        return clonedGraph;
    }


    public void Bind(FSG_Actor _actor)
    {
        Traverse(Start, state =>
        {
            state.actor = _actor;
        });
    }
}
