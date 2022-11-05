using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderRemovalTrigger : MonoBehaviour
{
  /*  // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/

    void OnTriggerEnter(Collider coll)
    {
        // When boulder collides, remove it
        if(coll.CompareTag("boulders"))
        {
            // remove boulder
            Destroy(coll.gameObject);
        }
    }
}
