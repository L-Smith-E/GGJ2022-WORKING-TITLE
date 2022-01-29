using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossToad : MonoBehaviour
{
    private GameObject _target;
    public GameObject Projectile;
    private float projectileTimer;


    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = _target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.Translate(Vector3.right * Time.deltaTime * 3.0f);

        if (projectileTimer <= 0.0f)
        {
            shootProjectile();
        }

        
    }

    void shootProjectile()
    {
        GameObject spawnedBullet = Instantiate(Projectile);
        spawnedBullet.transform.position = transform.position;
        spawnedBullet.transform.rotation = transform.rotation;
    }
}
