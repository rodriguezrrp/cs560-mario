using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFacingBehavior : MonoBehaviour
{
    public GameObject alwaysFaceTowards;

    // Update is called once per frame
    void Update()
    {
        if(alwaysFaceTowards != null)
        {
            Quaternion lookAtTarget = Quaternion.LookRotation(alwaysFaceTowards.transform.position - this.transform.position);
            this.transform.rotation = lookAtTarget;
            // fix horizontal on the right (red) and forward (blue) axes, so it doesn't tilt up or sideways
            this.transform.eulerAngles = new Vector3(0f, this.transform.eulerAngles.y, 0f);
        }
    }
}
