using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeesawStabilizer : MonoBehaviour
{
    float homeYRotation;
    public float playerRange = 30f;
    public GameObject playerRef;

    // Start is called before the first frame update
    void Start()
    {
        homeYRotation = transform.eulerAngles.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb.velocity.sqrMagnitude > 0.001 || rb.angularVelocity.sqrMagnitude > 0.001)
            // still moving some, let it continue moving
            return;
        // otherwise, attempt to reset position
        if(playerRange < Vector3.Distance(playerRef.transform.position, this.transform.position))
        { // player out of range; snap back to horizontal
            this.transform.eulerAngles = new Vector3(
                transform.rotation.eulerAngles.x,
                1f,
                transform.rotation.eulerAngles.z);
        }
    }
}
