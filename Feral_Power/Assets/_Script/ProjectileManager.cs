using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public GameObject ProjectilePrefab;

    [SerializeField]
    private List<ProjectileBehaviour> Projectile;

    [SerializeField]
    private bool IsNight = false;
    public float ProjectileRange = 100.0f;

    private Transform Self;

    // Start is called before the first frame update
    void Start()
    {
        Self = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(ProjectileBehaviour p in Projectile)
        {
            if (IsNight)
            {
                p.MoveProjectile();

                Vector2 currPos = p.transform.position;
                Vector2 heading = p.StartingPos - currPos;
                float distance = heading.magnitude;

                if(distance >= ProjectileRange)
                {
                    p.transform.gameObject.SetActive(false);
                }
            }
        }
    }

    public void SpawnProjectile(Vector2 StartPos, Vector2 Dir)
    {
        GameObject TempProjectile = Instantiate(ProjectilePrefab);
        TempProjectile.transform.SetParent(Self);
        TempProjectile.transform.position = StartPos;
        TempProjectile.transform.rotation = Quaternion.FromToRotation(transform.right, Dir) * transform.rotation; ;
        TempProjectile.tag = "PlayerProjectile";
        ProjectileBehaviour p = TempProjectile.GetComponent<ProjectileBehaviour>();
        if(p != null)
        {
            p.StartingPos = StartPos;
            p.ProjectileSpeed = 20;
            p.Dir = Dir;
            Projectile.Add(p);
        }
    }

    public void DayNightCycle(bool State)
    {
        Debug.Log("THE WORLD");
        IsNight = State;
    }
}
