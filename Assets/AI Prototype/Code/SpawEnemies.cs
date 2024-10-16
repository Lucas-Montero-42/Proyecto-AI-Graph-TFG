using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawEnemies : MonoBehaviour
{
    public GameObject spawnPrefab;
    public Transform[] spawnPositions;
    public bool automatic = false;
    public float Cooldown = 1f;
    float startTime;
    public List<Transform> patrolPoints;
    private void Start()
    {
        startTime = Time.time;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !automatic)
        {
            SpawnOne();
        }
        if (automatic)
        {
            if (Time.time - startTime > Cooldown)
            {
                SpawnOne();
                startTime = Time.time;
            }
        }
    }

    private void SpawnOne()
    {
        if (!spawnPrefab) { Debug.LogError("Prefab not asigned to spaw"); return; }
        if (spawnPositions.Length == 0) { return; }
        int Rnd = UnityEngine.Random.Range(0, spawnPositions.Length);
        GameObject obj = Instantiate(spawnPrefab, spawnPositions[Rnd].position, spawnPositions[Rnd].rotation);
        if (obj.GetComponent<BTG_Actor>())
        {
            obj.GetComponent<BTG_Actor>().patrolPoints = patrolPoints;
        }
    }
}
