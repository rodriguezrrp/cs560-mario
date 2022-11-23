using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderRemovalTrigger : MonoBehaviour
{

    // Whenever a Boulder comes into the trigger zone, destroy the Boulder
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
