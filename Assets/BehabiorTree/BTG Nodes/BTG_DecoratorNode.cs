using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class BTG_DecoratorNode : BTG_Node
{
    [HideInInspector] public BTG_Node child;
    public override BTG_Node Clone()
    {
        BTG_DecoratorNode node = Instantiate(this);
        node.child = child.Clone();
        return node;
    }
}
