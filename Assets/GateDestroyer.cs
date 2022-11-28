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
    private float retractStartY;
    private float deltaY;
    private bool retracting = false;

    // Start is called before the first frame update
    void Start()
    {
        initialY = this.transform.position.y;
        deltaY = initialHeight / hitsRequired;
    }

    // Update is called once per frame
    void Update()
    {
        if(retracting)
        {
            // handle retraction progress
            // ultimately, after all retractions, the Y position will be moved down by amount initialHeight
            //float targetY = initialY - initialHeight * ((hitsTaken + 1) / hitsRequired);
            //float deltaY = retractStartY - targetY;
            float targetY = retractStartY - deltaY;
            // lerp Y position downwards
            this.transform.Translate(0, - deltaY * Time.deltaTime, 0);
            Debug.Log("hitsTaken+1: " + (hitsTaken+1) + "  translate y: " + this.transform.position.y + " targetY: " + targetY + "  deltaY: " + deltaY);

            // if just finished retracting
            if (this.transform.position.y <= targetY)
            {
                // snap y to the targetY
                this.transform.position = new Vector3(this.transform.position.x, targetY, this.transform.position.z);
                // reset retraction state
                retracting = false;
                hitsTaken++;
                Debug.Log("finished retracting");
                // if last hit:
                // destroy this and gate
                if (hitsTaken >= hitsRequired)
                    PerformDestruction();
            }
        }
    }

    void PerformDestruction()
    {
        // destroy this and gate
        Destroy(gateObjToDestroy);
        Destroy(this.gameObject);
    }

    public void OnHeadTriggerHit()
    {
        if(!retracting)
            retractStartY = this.transform.position.y;
        retracting = true;
    }
}
