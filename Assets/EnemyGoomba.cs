using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HomePointComponent))]
public class EnemyGoomba : MonoBehaviour, IEnemyComponent
{

    public GameObject target; // if null, attemts to initialize to player on Start
    public float targetViewDistance = 12f;
    public float moveSpeed = 3f;
    public float rotateSpeed = 0.72f; //0.4f
    private GameObject homePoint;
    //private float homeRadius = 18f;

    GameObject coinPrefab;

    // Start is called before the first frame update
    void Start()
    {
        coinPrefab = GameObject.Find("Coin");

        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
            if(target == null)
                target = GameObject.Find("Player");
        }

        HomePointComponent comp = GetComponent<HomePointComponent>();
        homePoint = comp.homePoint;
        //homeRadius = comp.homeRadius;
    }

    public void SetDefaultTarget(GameObject targetObj)
    {
        this.target = targetObj;
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetInSight())
        { // go after the target
            // directly set the angle, no lerping
            this.transform.LookAt(target.transform);
            // fix horizontal on the right (red) and forward (blue) axes, so it doesn't tilt up or sideways
            this.transform.eulerAngles = new Vector3(0f, this.transform.eulerAngles.y, 0f);
            // move forwards
            float moveZ = moveSpeed * Time.deltaTime;
            this.transform.Translate(0, 0, moveZ);
        }
        else
        { // wander around home point
            Quaternion lookAtPoint = Quaternion.LookRotation(homePoint.transform.position - this.transform.position);
            // slerp rotation each time
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                lookAtPoint, rotateSpeed * Time.deltaTime
                );
            // fix horizontal on the right (red) and forward (blue) axes, so it doesn't tilt up or sideways
            this.transform.eulerAngles = new Vector3(0f, this.transform.eulerAngles.y, 0f);
            // move towards
            float moveZ = moveSpeed * Time.deltaTime;
            this.transform.Translate(0, 0, moveZ);
        }
    }

    bool TargetInSight()
    {
        return targetViewDistance > Vector3.Distance(this.transform.position, target.transform.position);
    }

    public void OnWeakAreaHit()
    {
        Debug.Log("Weak area of goomba hit; destroying?");
        Defeat();
    }

    void Defeat()
    {
        GameObject obj = Instantiate(coinPrefab,
                transform.position, Quaternion.identity
                );
        // obj.transform.localScale = Vector3.one * 0.5f;
        obj.transform.localScale = this.transform.localScale * 0.9f;
        Destroy(gameObject);
    }
}
