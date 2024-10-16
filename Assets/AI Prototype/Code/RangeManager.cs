using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeManager : MonoBehaviour
{
    BTG_Actor BTG_Actor;
    FSG_Actor FSG_Actor;
    
    [StringInList(typeof(PropertyDrawersHelper), "BTG_Condition")]
    public string BTG_Aproach;
    [StringInList(typeof(PropertyDrawersHelper), "BTG_Condition")]
    public string BTG_StopShoot;



    [StringInList(typeof(PropertyDrawersHelper), "FSG_Condition")]
    public string FSG_Aproach;
    [StringInList(typeof(PropertyDrawersHelper), "FSG_Condition")]
    public string FSG_StopShoot;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<BTG_Actor>()) { BTG_Actor = GetComponent<BTG_Actor>(); }
        if (GetComponent<FSG_Actor>()) { FSG_Actor = GetComponent<FSG_Actor>(); }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (BTG_Actor)
        {
            BTG_Range();
        }
        if (FSG_Actor)
        {
            FSG_Range();
        }
    }
    public void BTG_Range()
    {
        if (BTG_Actor.targetObject == null) 
            return; 
        
        if ((BTG_Actor.transform.position - BTG_Actor.targetObject.transform.position).magnitude < BTG_Actor.insideAttackRange)
            BTG_Actor.SetBool(BTG_Aproach, true);
        else
            BTG_Actor.SetBool(BTG_Aproach, false);

        if ((BTG_Actor.transform.position - BTG_Actor.targetObject.transform.position).magnitude > BTG_Actor.outsideAttackRange)
            BTG_Actor.SetBool(BTG_StopShoot, true);
        else
            BTG_Actor.SetBool(BTG_StopShoot, false);
    }
    public void FSG_Range()
    {
        // si la distancia es mayor al anillo interior, acercate
        if ((FSG_Actor.transform.position - FSG_Actor.targetObject.transform.position).magnitude > FSG_Actor.insideAttackRange)
            FSG_Actor.SetBool(FSG_Aproach, false);
        else
            FSG_Actor.SetBool(FSG_Aproach, true);

        // si la distancia es menor al anillo exteriror, dispara
        if ((FSG_Actor.transform.position - FSG_Actor.targetObject.transform.position).magnitude < FSG_Actor.outsideAttackRange)
            FSG_Actor.SetBool(FSG_StopShoot, true);
        else
            FSG_Actor.SetBool(FSG_StopShoot, false);
    }
}
