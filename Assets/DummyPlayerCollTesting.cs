using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlayerCollTesting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("player dummy coll -- " + collision.gameObject);
        if (collision.gameObject.CompareTag("WeakArea"))
        {
            Debug.Log("player dummy coll testing: hit a weak area!");
            IEnemyComponent enemyComp = collision.gameObject.GetComponentInParent<IEnemyComponent>();
            enemyComp.OnWeakAreaHit();
        }
        else if (collision.gameObject.CompareTag("chomp_log"))
        {
            GateDestroyer enemyComp = collision.gameObject.GetComponentInParent<GateDestroyer>();
            enemyComp.OnHeadTriggerHit();
        }
    }
}
