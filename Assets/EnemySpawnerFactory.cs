using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerFactory : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public int maxNum = 1;
    public float spawnDelay;
    public GameObject defaultTarget;
    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        if (maxNum != 1 && spawnDelay <= 0)
        {
            // In case of invalid value, default to 10 seconds
            spawnDelay = 10.0f;
        }
        if(maxNum < 1)
        { // do nothing if maxNum is < 1
        }
        else
        { // maxNum >= 1; start spawner coroutine
            coroutine = EnemySpawning(/*prefabToSpawn, maxNum, spawnDelay*/);
            StartCoroutine(coroutine);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator EnemySpawning(/*GameObject prefabToSpawn, int maxNum, float spawnDelay*/)
    {
        int num = 0;
        while(num < maxNum)
        {
            yield return new WaitForSeconds(spawnDelay);
            // instantiate the object
            GameObject obj = Instantiate(prefabToSpawn,
                //GetComponent<Transform>().position, Quaternion.identity
                transform.position, transform.rotation
                );
            // hook up obj's target
            if(defaultTarget != null)
            {
                IEnemyComponent objCompEnemy = obj.GetComponent<IEnemyComponent>();
                objCompEnemy.SetDefaultTarget(defaultTarget);
            }
            // hook up obj's home point with this, if applicable
            HomePointComponent objCompHomePoint = obj.GetComponent<HomePointComponent>();
            if(objCompHomePoint != null)
            {
                // set home point object to be this gameObject
                // set home point radius to be this' sphere collider's radius, if applicable
                objCompHomePoint.homePoint = this.gameObject;
                SphereCollider thisColl = this.GetComponent<SphereCollider>();
                if (thisColl != null)
                    objCompHomePoint.homeRadius = thisColl.radius;
            }
            num++;
        }
    }
}
