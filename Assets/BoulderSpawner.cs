using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderSpawner : MonoBehaviour
{

    public GameObject boulderPrefab;
    public float spawnDelay;  // I recommend default of 10 seconds probably.
    public Vector3 spawnVelocity;

    // Start is called before the first frame update
    void Start()
    {
        if(spawnDelay <= 0)
        {
            spawnDelay = 10.0f;  // I recommend a default of 10 seconds probably.
        }
        //StartCoroutine(SpawnerCoroutine());
        InvokeRepeating("SpawnBoulder", 1.0f, spawnDelay);
    }

    IEnumerator SpawnerCoroutine()
    {
//        while ()
        {
            // spawn new boulder
            SpawnBoulder();
            // wait
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnBoulder()
    {
        GameObject spawnedBoulder = Instantiate(boulderPrefab, GetComponent<Transform>().position, Quaternion.identity);
        Rigidbody rb = spawnedBoulder.GetComponent<Rigidbody>();
        rb.velocity = spawnVelocity;
    }

}
