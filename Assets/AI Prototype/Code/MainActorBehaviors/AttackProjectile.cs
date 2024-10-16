using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackProjectile : MonoBehaviour
{
    BTG_Actor BTG_actor;
    FSG_Actor FSG_actor;
    public float AttackForce = 50f;
    public Transform CurrentTarget;

    public GameObject projectile;

    // Start is called before the first frame update
    private void Awake()
    {
        if (GetComponent<BTG_Actor>()) { BTG_actor = GetComponent<BTG_Actor>(); }
        if (GetComponent<FSG_Actor>()) { FSG_actor = GetComponent<FSG_Actor>(); }
    }

    public void Shoot()
    {
        if (BTG_actor)
        {
            if (CurrentTarget == null && BTG_actor.targetObject != null) { CurrentTarget = BTG_actor.targetObject.transform; }
            else if (CurrentTarget == null) { CurrentTarget = BTG_actor.findTarget.GetTarget(); }
            else { CurrentTarget = BTG_actor.findTarget.GetTarget(); }
            if (CurrentTarget)
            {
                ShootBullet();
            }
        }
        else if (FSG_actor)
        {
            if (CurrentTarget == null && FSG_actor.targetObject != null) { CurrentTarget = FSG_actor.targetObject.transform; }
            else if (CurrentTarget == null) { CurrentTarget = FSG_actor.findTarget.GetTarget(); }
            else { CurrentTarget = FSG_actor.findTarget.GetTarget(); }
            if (CurrentTarget)
            {
                ShootBullet();
            }
        }
    }
    private void ShootBullet()
    {
        GameObject _projectile = Instantiate(projectile,transform);
        _projectile.transform.parent = null;
        _projectile.GetComponent<Rigidbody>().AddForce(transform.forward * AttackForce * 10000 * Time.deltaTime, ForceMode.Force);
        StartCoroutine(DestroyProjectile(_projectile));

    }

    IEnumerator DestroyProjectile(GameObject p)
    {
        yield return new WaitForSeconds(1f);
        Destroy(p);
    }
}
