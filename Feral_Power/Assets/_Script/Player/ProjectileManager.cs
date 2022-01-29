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
            else
            {
                p.StopProjectile();
            }
        }
    }
    public void SpawnProjectile(Vector2 StartPos, Vector2 Dir)
    {
        if (RecycleProjectile(StartPos, Dir) == 1)
        {
            GameObject TempProjectile = Instantiate(ProjectilePrefab);
            TempProjectile.transform.SetParent(Self);
            TempProjectile.transform.position = StartPos;
            TempProjectile.transform.rotation = Quaternion.FromToRotation(transform.right, Dir) * transform.rotation;
            TempProjectile.tag = "PlayerProjectile";
            ProjectileBehaviour p = TempProjectile.GetComponent<ProjectileBehaviour>();
            if (p != null)
            {
                p.StartingPos = StartPos;
                p.ProjectileSpeed = 20;
                p.Dir = Dir;
                Projectile.Add(p);
            }
        }
    }

    // Look for Deactived projectile and reactiving them
    // Return 0 if it reactive any projectile
    // Return 1 if no Deactived projectile found
    public int RecycleProjectile(Vector2 StartPos, Vector2 Dir)
    {
        foreach (ProjectileBehaviour p in Projectile)
        {
            if (!p.isActiveAndEnabled)
            {
                p.transform.gameObject.SetActive(true);
                p.enabled = true;
                p.transform.position = StartPos;
                p.transform.rotation = Quaternion.FromToRotation(transform.right, Dir) * transform.rotation;

                p.StartingPos = StartPos;
                p.ProjectileSpeed = 20;
                p.Dir = Dir;
                return 0;
            }

        }
        return 1;
    }
    public void DayNightCycle(bool State)
    {
        Debug.Log("THE WORLD");
        IsNight = State;
    }
}
