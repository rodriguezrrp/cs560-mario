using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HomePointComponent))]
public class EnemyBobomb : MonoBehaviour, IEnemyComponent
{
    public int health = 3;
    private float invulnerableTimer = 0f;
    public GameObject target;
    public float moveSpeed = 3f; //1.5f
    public float rotateSpeed = 3f; //0.3f
    public float targetAggroDistance = 25f;
    // these home point vals get 'inherited' (in Start) from the required home point component
    private GameObject homePoint;
    private float homeRadius = 18f;

    GameObject starPrefab;

    // Start is called before the first frame update
    void Start()
    {
        starPrefab = GameObject.Find("Star");
        HomePointComponent comp = GetComponent<HomePointComponent>();
        homePoint = comp.homePoint;
        homeRadius = comp.homeRadius;
    }

    public void SetDefaultTarget(GameObject targetObj)
    {
        this.target = targetObj;
    }

    // Update is called once per frame
    void Update()
    {
        if(invulnerableTimer > 0f)
        { // deal with invulnerability laying on ground
            // lean back
            this.transform.eulerAngles = new Vector3(-40f, this.transform.eulerAngles.y, 0f);
            // dec invuln timer
            invulnerableTimer -= Time.deltaTime;
            Debug.Log("health: " + health + "  invulnerableTimer: " + invulnerableTimer);
            if (invulnerableTimer <= 0f)
            {
                if(health == 0)
                    Defeat();
                // lean horizontal again
                this.transform.eulerAngles = new Vector3(0f, this.transform.eulerAngles.y, 0f);
            }
        }
        else
        { // not invulnerable down on ground
            if(Vector3.Distance(this.transform.position, target.transform.position) < targetAggroDistance)
            { // target should be chased after
                if(Vector3.Distance(this.transform.position, target.transform.position) < 2)
                //if(Vector3.Distance(this.transform.position, new Vector3(0f, target.transform.position.y, 0f)) < 2)
                { // target close
                }
                else
                { // follow
                    Quaternion lookAtTarget = Quaternion.LookRotation(target.transform.position - this.transform.position);
                    // slerp rotation each time
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                        lookAtTarget, rotateSpeed * Time.deltaTime
                        );
                    // fix horizontal on the right (red) and forward (blue) axes, so it doesn't tilt up or sideways
                    this.transform.eulerAngles = new Vector3(0f, this.transform.eulerAngles.y, 0f);
                    // move towards
                    float moveZ = moveSpeed * Time.deltaTime;
                    if (IsInHomeRange())
                    {
                        this.transform.Translate(0, 0, moveZ);
                        // ensure it does not go out of home range
                        if (!IsInHomeRange())
                        { // if out of home range, back it up ('undo')
                            this.transform.Translate(0, 0, -moveZ);
                        }
                    }
                    /*Vector3 displacement = new Vector3(0, 0, moveSpeed * Time.deltaTime);
                    if(WouldBeInHomeRangeAfterMove(displacement))
                    {
                        this.transform.Translate(displacement);
                    }*/
                }
            }
        }
    }
    bool IsInHomeRange()
    {
        return Vector3.Distance(this.transform.position, homePoint.transform.position) < homeRadius;
    }

    public void OnWeakAreaHit()
    {
        if(invulnerableTimer <= 0f)
        {
            if (this.health >= 1)
            {
                this.health--;
                this.invulnerableTimer = 1f;
            }
            else
                Defeat();
        }
    }

    void Defeat()
    {
        GameObject obj = Instantiate(starPrefab,
                transform.position, Quaternion.identity
                );
        StarFacingBehavior starComp = obj.GetComponent<StarFacingBehavior>();
        starComp.starName = "Big Bob-omb on the Summit";
        Destroy(gameObject);
    }
}
