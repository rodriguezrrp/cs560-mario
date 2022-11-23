using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderSpawner : MonoBehaviour
{
    // Configurable public attributes
    public GameObject boulderPrefab;
    public float spawnDelay;  // I recommend default of 10 seconds.
    public Vector3 spawnVelocity;

    void Start()
    {
        if(spawnDelay <= 0)
        {
            // In case of invalid value, default to 10 seconds
            spawnDelay = 10.0f;
        }
        InvokeRepeating("SpawnBoulder", 1.0f, spawnDelay);
    }

    // Spawns the configured prefab (Intended to be a Boulder)
    void SpawnBoulder()
    {
        GameObject spawnedBoulder = Instantiate(boulderPrefab,
            GetComponent<Transform>().position, Quaternion.identity
            );
        Rigidbody rb = spawnedBoulder.GetComponent<Rigidbody>();
        rb.velocity = spawnVelocity;
    }

}
