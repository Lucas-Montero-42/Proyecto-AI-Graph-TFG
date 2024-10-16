using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class FindTarget : MonoBehaviour
{

    BTG_Actor actor;
    public string targetTag;
    [SerializeField] private GameObject[] Targets;
    [SerializeField] private Transform CurrentTarget;

    private void Awake()
    {
        actor = GetComponent<BTG_Actor>();
    }
    // Update is called once per frame
    void Update()
    {
        
        // asign target
        CurrentTarget = FindNeatestTarget();
        /*
        if (CurrentTarget)
        {
            actor.targetObject = CurrentTarget.gameObject;
        }
         */
    }
    private Transform FindNeatestTarget()
    {
        try 
        {
            // fill a list of all posible targets
            Targets = GameObject.FindGameObjectsWithTag(targetTag);
            // return the first on the list
            return Targets[0].transform;
        }

        catch { return null;  }
    }
    public Transform GetTarget()
    {
        return CurrentTarget;
    }
}
