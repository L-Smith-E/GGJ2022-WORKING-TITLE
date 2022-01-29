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

    private Transform Self;

    // Start is called before the first frame update
    void Start()
    {
        Self = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (ProjectileBehaviour p in Projectile)
        {
            if (IsNight)
                p.MoveProjectile();
        }
    }

    public void SpawnProjectile(Vector2 StartPos, Vector2 Dir)
    {
        GameObject TempProjectile = Instantiate(ProjectilePrefab);
        TempProjectile.transform.SetParent(Self);
        TempProjectile.transform.position = StartPos;
        TempProjectile.transform.rotation = Quaternion.FromToRotation(transform.right, Dir) * transform.rotation; ;
        ProjectileBehaviour p = TempProjectile.GetComponent<ProjectileBehaviour>();
        if (p != null)
        {
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
