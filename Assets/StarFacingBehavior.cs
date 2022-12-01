using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFacingBehavior : MonoBehaviour
{
    public GameObject alwaysFaceTowards;
    public string starName;
    [SerializeField] float speedX;
    [SerializeField] float speedY = 0.5f;
    [SerializeField] float speedZ;

    void Update()
    {
        if(alwaysFaceTowards == null)
        {  // just rotate
            transform.Rotate(360 * speedX * Time.deltaTime, 360 * speedY * Time.deltaTime, 360 * speedZ * Time.deltaTime);
        }
        else
        {
            Quaternion lookAtTarget = Quaternion.LookRotation(alwaysFaceTowards.transform.position - this.transform.position);
            this.transform.rotation = lookAtTarget;
            // fix horizontal on the right (red) and forward (blue) axes, so it doesn't tilt up or sideways
            this.transform.eulerAngles = new Vector3(0f, this.transform.eulerAngles.y, 0f);
        }
    }
}
