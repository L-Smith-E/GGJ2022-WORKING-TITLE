using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MoleAI : MonoBehaviour
{
    public GameObject target;
    public Transform spawner;

    public float speed = 200f;

    public float nextWaypointDistance = 3f;

    public float burstRate;

    public int amountOfBullets;

    public float StartAngle = 90.0f;
    public float EndAngle = 270.0f;

    private float projectileTimer;
    private float cooldownTime = 3.0f;
    private float fireTime = 2.0f;
    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    private ObjectPool objectPool;
    private bool shooting = false;

    private Vector2 bulletMoveDirection;

    private Seeker seeker;
    private Rigidbody2D rb;



    // Start is called before the first frame update
    void Start()
    {
        
        target = GameObject.FindGameObjectWithTag("Player");
        spawner = transform.GetChild(0);
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        projectileTimer = cooldownTime;
        objectPool = ObjectPool.Instance;
        InvokeRepeating("UpdatePath", 0f, .5f);


    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
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
        Vector3 dir = target.transform.position - transform.position;
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

        if (projectileTimer <= 0.0f && !shooting)
        {
            StartCoroutine(burstFire(3));
            shooting = true;
        }

        if (UserHUD.timeUp == true)
        {
            Destroy(gameObject);
        }
    }

    void shootProjectile()
    {
        float angleStep = (EndAngle - StartAngle) / amountOfBullets;
        float angle = StartAngle;

        for (int i = 0; i < amountOfBullets + 1; i++)
        {
            float bulletDirectionX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180.0f);
            float bulletDirectionY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180.0f);

            Vector3 bulletMoveVector = new Vector3(bulletDirectionX, bulletDirectionY, 0.0f);
            Vector2 bulletDirection = (bulletMoveVector - transform.position).normalized;

            GameObject bullet = objectPool.SpawnFromPool("Mole", spawner.position, spawner.rotation);
            bullet.GetComponent<EnemyProjectile>().SetMoveDirection(bulletDirection);

            angle += angleStep;
        }
    }


    IEnumerator burstFire(int TimesToShoot)
    {
        for (int i = 0; i < TimesToShoot; i++)
        {
            shootProjectile();

            yield return new WaitForSeconds(1.0f / burstRate);
        }

        projectileTimer = cooldownTime;
        shooting = false;
    }
}
