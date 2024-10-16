using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orient3D : MonoBehaviour
{
    //BTG_Actor BTGActor;
    //FSG_Actor FSGActor;
    //public bool Oriented = false;

    BTG_Actor BTG_Actor;
    FSG_Actor FSG_Actor;

    [StringInList(typeof(PropertyDrawersHelper), "FSG_Condition")]
    public string FSG_Orient;
    [StringInList(typeof(PropertyDrawersHelper), "BTG_Condition")]
    public string BTG_Orient;

    public float AngularSpeed = 180;
    private Vector3 _direction;


    // Start is called before the first frame update
    private void Awake()
    {
        if (GetComponentInParent<BTG_Actor>())
            BTG_Actor = GetComponentInParent<BTG_Actor>();
        else if (GetComponentInParent<FSG_Actor>())
            FSG_Actor = GetComponentInParent<FSG_Actor>();
        else
            Debug.LogError("No actor detected, add a BTG_Actor or a FSG_actor to this Gameobject");

    }

    // Update is called once per frame
    void Update()
    {
        if (BTG_Actor)
        {
            RotateBTG();
        }
        else if (FSG_Actor)
        {
            RotateFSG();
        }
        
    }
    private void RotateBTG()
    {
        if (BTG_Actor.targetObject)
        {
            _direction = (BTG_Actor.targetObject.transform.position - transform.position).normalized;
            Quaternion toRotation = Quaternion.LookRotation(_direction, Vector3.up);
            if (transform.rotation == toRotation)
            {
                BTG_Actor.SetBool(BTG_Orient, true);  
            }
            else
            {
                BTG_Actor.SetBool(BTG_Orient, false);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, AngularSpeed * Time.deltaTime);
            }
        }
        else
            BTG_Actor.SetBool(BTG_Orient, false);
    }
    private void RotateFSG()
    {
        if (FSG_Actor.targetObject)
        {
            _direction = (FSG_Actor.targetObject.transform.position - transform.position).normalized;
            Quaternion toRotation = Quaternion.LookRotation(_direction, Vector3.up);
            if (transform.rotation == toRotation)
            {
                FSG_Actor.SetBool(FSG_Orient, true);
            }
            else
            {
                FSG_Actor.SetBool(FSG_Orient, false);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, AngularSpeed * Time.deltaTime);
            }
        }
        else
            FSG_Actor.SetBool(FSG_Orient, false);
    }
}
