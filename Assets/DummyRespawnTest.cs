using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyRespawnTest : MonoBehaviour
{

    public GameObject respawnObj;

    /*// Start is called before the first frame update
    void Start()
    {
        
    }*/

    // Update is called once per frame
//    void Update()
    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 vel = rb.velocity;
        if(vel.magnitude < 0.02)
        {
            rb.position = respawnObj.transform.position;
            Vector3 newVel = respawnObj.GetComponent<MarioRespawnPoint>().respawnAcceleration;
            rb.velocity = newVel;  //new Vector3(newVel.x, newVel.y, newVel.z);
        }
    }
}
