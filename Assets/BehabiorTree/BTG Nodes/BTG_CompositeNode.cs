using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BTG_CompositeNode : BTG_Node
{
    public List<BTG_Node> Children = new List<BTG_Node>();
    public override BTG_Node Clone()
    {
        BTG_CompositeNode node = Instantiate(this);
        node.Children = Children.ConvertAll(c => c.Clone());
        return node;
    }
}
