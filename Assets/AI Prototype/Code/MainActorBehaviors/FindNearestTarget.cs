using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNearestTarget : MonoBehaviour
{

    BTG_Actor actor;
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
        actor.targetObject = CurrentTarget.gameObject;
    }
    private Transform FindNeatestTarget()
    {
        try
        {
            // fill a list of all posible targets
            Targets = GameObject.FindGameObjectsWithTag("Enemy");
            // find the nearest target
            GameObject nearestObject = Targets[0];
            for (int i = 0; i < Targets.Length; i++)
            {
                float distance = (nearestObject.transform.position - transform.position).magnitude;
                float newdistance = (Targets[i].transform.position - transform.position).magnitude;
                if (distance > newdistance)
                {
                    nearestObject = Targets[i];
                }
            }
            return nearestObject.transform; // return the nearest target
        }

        catch { return null; }
    }
    public Transform GetTarget()
    {
        return CurrentTarget;
    }
}
