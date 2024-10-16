using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTG_RootNode : BTG_Node
{
    public BTG_Node child;
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return child.Update();
    }

    public override BTG_Node Clone()
    {
        BTG_RootNode node = Instantiate(this);
        node.child = child.Clone();
        return node;
    }
}
