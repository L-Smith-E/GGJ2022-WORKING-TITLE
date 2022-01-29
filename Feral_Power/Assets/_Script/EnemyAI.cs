using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    public Transform target;
    public GameObject Projectile;
    private float projectileTimer;
    private float firingTime = 3.0f;
    private float cooldownTime = 3.0f;

    public float speed = 200f;

    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        projectileTimer = cooldownTime;

        InvokeRepeating("UpdatePath", 0f, .5f);

        
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
        
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector3 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        projectileTimer -= 1.0f * Time.deltaTime;

        if (projectileTimer <= 0.0f)
        {
            firingTime -= 1.0f;
            shootProjectile();
            if(firingTime <= 0.0f)
            {
                projectileTimer = cooldownTime;
                firingTime = 3.0f;
            }
            
        }
    }

    void shootProjectile()
    {
        GameObject spawnedBullet = Instantiate(Projectile);
        spawnedBullet.transform.position = transform.position;
        spawnedBullet.transform.rotation = transform.rotation;
    }
}
