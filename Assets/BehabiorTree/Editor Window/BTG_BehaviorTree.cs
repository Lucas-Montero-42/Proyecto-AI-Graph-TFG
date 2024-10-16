using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;

[CreateAssetMenu(fileName = "NewBehaviorTree", menuName = "BTG_BehaviorTree", order = 1)]
public class BTG_BehaviorTree : ScriptableObject
{
    public BTG_Node rootNode;
    public BTG_Node.State treeState = BTG_Node.State.Running;
    public List<BTG_Node> nodes = new List<BTG_Node>();

    public BTG_Node.State Update()
    {
        if (rootNode.state == BTG_Node.State.Running )
        {
            return rootNode.Update();
        }
        return treeState;
    }

    public BTG_Node CreateNode(System.Type type)
    {
        BTG_Node node = ScriptableObject.CreateInstance(type) as BTG_Node;
        node.name = type.Name;
        node.guid = GUID.Generate().ToString();

        Undo.RecordObject(this, "BTG Behavior Tree (CreateNode)");
        nodes.Add(node);

        if (!Application.isPlaying)
        {
            AssetDatabase.AddObjectToAsset(node, this);
        }
        Undo.RegisterCreatedObjectUndo(node, "BTG Behavior Tree (CreateNode)");

        AssetDatabase.SaveAssets();
        return node;
    }
    public void DeleteNode(BTG_Node node)
    {
        Undo.RecordObject(this, "BTG Behavior Tree (DeleteNode)");
        nodes.Remove(node);

        //AssetDatabase.RemoveObjectFromAsset(node);
        Undo.DestroyObjectImmediate(node);
        AssetDatabase.SaveAssets();
    }
    public void AddChild(BTG_Node parent, BTG_Node child)
    {
        BTG_RootNode rootNode = parent as BTG_RootNode;
        if (rootNode)
        {
            Undo.RecordObject(rootNode, "BTG Behavior Tree (AddChild)");
            rootNode.child = child;
            EditorUtility.SetDirty(rootNode);
        }
        BTG_DecoratorNode decorator = parent as BTG_DecoratorNode;
        if (decorator)
        {
            Undo.RecordObject(decorator, "BTG Behavior Tree (AddChild)");
            decorator.child = child;
            EditorUtility.SetDirty(decorator);
        }
        BTG_CompositeNode composite = parent as BTG_CompositeNode;
        if (composite)
        {
            Undo.RecordObject(composite, "BTG Behavior Tree (AddChild)");
            composite.Children.Add(child);
            EditorUtility.SetDirty(composite);
        }
    }
    public void RemoveChild(BTG_Node parent, BTG_Node child)
    {
        BTG_RootNode rootNode = parent as BTG_RootNode;
        if (rootNode)
        {
            Undo.RecordObject(rootNode, "BTG Behavior Tree (RemoveChild)");
            rootNode.child = null;
            EditorUtility.SetDirty(rootNode);
        }
        BTG_DecoratorNode decorator = parent as BTG_DecoratorNode;
        if (decorator)
        {
            Undo.RecordObject(decorator, "BTG Behavior Tree (RemoveChild)");
            decorator.child = null;
            EditorUtility.SetDirty(decorator);
        }
        BTG_CompositeNode composite = parent as BTG_CompositeNode;
        if (composite)
        {
            Undo.RecordObject(composite, "BTG Behavior Tree (RemoveChild)");
            composite.Children.Remove(child);
            EditorUtility.SetDirty(composite);
        }
    }
    public List<BTG_Node> GetChildren(BTG_Node parent)
    {
        List<BTG_Node> children = new List<BTG_Node>();

        BTG_RootNode rootNode = parent as BTG_RootNode;
        if (rootNode && rootNode.child != null)
        {
            children.Add(rootNode.child);
        }
        BTG_DecoratorNode decorator = parent as BTG_DecoratorNode;
        if (decorator && decorator.child != null)
        {
            children.Add(decorator.child);
        }
        BTG_CompositeNode composite = parent as BTG_CompositeNode;
        if (composite)
        {
            return composite.Children;
        }
        return children;
    }

    public void Traverse(BTG_Node node, System.Action<BTG_Node> visitor)
    {
        if (node)
        {
            visitor.Invoke(node);
            var children = GetChildren(node);
            children.ForEach((n) => Traverse(n, visitor));
        }
    }

    public BTG_BehaviorTree Clone()
    {
        BTG_BehaviorTree tree = Instantiate(this);
        tree.rootNode = tree.rootNode.Clone();
        tree.nodes = new List<BTG_Node>();
        Traverse(tree.rootNode, (n) => {
            tree.nodes.Add(n);
        });

        return tree;
    }
    public void Bind(BTG_Actor agent)
    {
        Traverse(rootNode, node =>
        {
            node.actor = agent;
            //node.blackboard = blackboard;
        });
    }
}
