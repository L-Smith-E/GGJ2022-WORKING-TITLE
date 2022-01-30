using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    public Transform target;
    public Transform spawner;

    public float speed = 200f;

    public float nextWaypointDistance = 3f;

    public float burstRate = 1.0f;

    
    private float projectileTimer;
    private float cooldownTime = 3.0f;
    private float fireTime = 2.0f;
    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    private ObjectPool objectPool;

    private int amountOfBulletsDay = 9;
    private int amountOfBulletsNight = 0;
    private float dayStartAngle = 90.0f;
    private float dayEndAngle = 270.0f;
    private float nightAngle = 180.0f;
    private bool shooting = false;

    private Vector2 bulletMoveDirection;

    private Seeker seeker;
    private Rigidbody2D rb;



    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        projectileTimer = cooldownTime;
        objectPool = ObjectPool.Instance;
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        //ProjectileManager = GameManager.GetEnemyProjectileManager();


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

        projectileTimer -= Time.fixedDeltaTime;

        if (projectileTimer <= 0.0f && !shooting && GameManager.IsDay())
        {
            StartCoroutine(burstFire(3));
            shooting = true;
        }

        if (projectileTimer <= 0.0f &&GameManager.IsNight())
        {
            fireTime -= 1 * Time.deltaTime;
            if(fireTime >= 0.0f)
            {
                shootProjectileNight();
            }
            else
            {
                projectileTimer = cooldownTime;
                fireTime = 2.0f;
            }
            
        }
    }

    void shootProjectileDay()
    {
        
            float angleStep = (dayEndAngle - dayStartAngle) / amountOfBulletsDay;
            float angle = dayStartAngle;

            for (int i = 0; i <= amountOfBulletsDay; i++)
            {
                float bulletDirectionX = transform.position.x + Mathf.Cos((angle * Mathf.PI) / 90.0f);
                float bulletDirectionY = transform.position.y + Mathf.Sin((angle * Mathf.PI) / 90.0f);

                Vector3 bulletMoveVector = new Vector3(bulletDirectionX, bulletDirectionY, 0.0f);
                Vector2 bulletDirection = (bulletMoveVector - transform.position).normalized;

            GameObject bullet = objectPool.SpawnFromPool("Day", spawner.position, spawner.rotation);
            bullet.GetComponent<EnemyProjectile>().SetMoveDirection(bulletDirection);
            //ProjectileManager.RecycleProjectile(spawner.position, bulletDirection, 0);
            angle += angleStep;
            }
        
    }

    void shootProjectileNight()
    {
        
            for (int i = 0; i <= amountOfBulletsNight; i++)
            {
                float bulletDirectionX = transform.position.x + Mathf.Sin((nightAngle * Mathf.PI) / 180.0f);
                float bulletDirectionY = transform.position.y + Mathf.Cos((nightAngle * Mathf.PI) / 180.0f);

                Vector3 bulletMoveVector = new Vector3(bulletDirectionX, bulletDirectionY, 0.0f);
                Vector2 bulletDirection = (bulletMoveVector - transform.position).normalized;

            GameObject bullet = objectPool.SpawnFromPool("Night", spawner.position, spawner.rotation);
            bullet.GetComponent<EnemyProjectile>().SetMoveDirection(bulletDirection);
            //ProjectileManager.RecycleProjectile(spawner.position, bulletDirection, 1);
            nightAngle += 10.0f;
            }
        
    }

    IEnumerator burstFire(int TimesToShoot)
    {
        for (int i = 0; i < TimesToShoot; i++)
        {
            shootProjectileDay();

            yield return new WaitForSeconds(1.0f / burstRate);
        }

        projectileTimer = cooldownTime;
        shooting = false;
    }
}
