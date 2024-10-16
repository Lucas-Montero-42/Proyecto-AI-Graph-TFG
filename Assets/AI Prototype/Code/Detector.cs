using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    BTG_Actor BTG_Actor;
    FSG_Actor FSG_Actor;

    [StringInList(typeof(PropertyDrawersHelper), "FSG_Condition")]
    public string FSG_Detect;
    [StringInList(typeof(PropertyDrawersHelper), "BTG_Condition")]
    public string BTG_Detect;

    private void Awake()
    {
        if(GetComponentInParent<BTG_Actor>())
            BTG_Actor = GetComponentInParent<BTG_Actor>();
        else if(GetComponentInParent<FSG_Actor>())
            FSG_Actor = GetComponentInParent<FSG_Actor>();
        else
            Debug.LogError("No actor detected, add a BTG_Actor or a FSG_actor to this Gameobject");
    }
    private void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FSG_Actor?.SetBool(FSG_Detect, true);
            BTG_Actor?.SetBool(BTG_Detect,true);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            FSG_Actor?.SetBool(FSG_Detect, false);
            BTG_Actor?.SetBool(BTG_Detect, false);
        }
    }
  
}
