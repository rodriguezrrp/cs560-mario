using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateDestroyer : MonoBehaviour
{

    public GameObject gateObjToDestroy;
    public int hitsRequired = 3;
    int hitsTaken = 0;

    public float initialHeight = 1.5f;
    private float initialY;
    private bool retracting = false;

    // Start is called before the first frame update
    void Start()
    {
        initialY = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(retracting)
        {
            // handle retraction progress
            // ultimately, after all retractions, the Y position will be moved down by amount initialHeight
            float targetY = initialY - initialHeight * ((hitsTaken + 1) / hitsRequired);
            // lerp Y position downwards
            this.transform.Translate(0, targetY * Time.deltaTime, 0);

            // if just finished retracting
            if (this.transform.position.y <= targetY)
            {
                this.transform.position = new Vector3(this.transform.position.x, targetY, this.transform.position.z);
                retracting = false;
                hitsTaken++;
                // if last hit:
                // destroy this and gate
                if (hitsTaken >= hitsRequired)
                {
                    Destroy(gateObjToDestroy);
                    Destroy(this.gameObject);
                }
            }
        }
    }

    void TriggerHit()
    {
        retracting = true;
    }
}
