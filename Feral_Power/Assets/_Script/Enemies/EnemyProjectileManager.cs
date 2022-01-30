using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileManager : MonoBehaviour
{
    public List<GameObject> ProjectilePrefab;
    public int ProjectileCount;

    [SerializeField]
    private List<ProjectileBehaviour> Projectile;

    [SerializeField]
    private bool IsNight = false;
    private Transform Self;

    public float ExistTime = 10;
    // Start is called before the first frame update

    private void Awake()
    {
        if(GameManager.EnemiesProjectileManager == null)
            GameManager.EnemiesProjectileManager = this;
    }
    void Start()
    {
        CreateBullets();
        Self = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        IsNight = GameManager.IsNight();
        foreach (ProjectileBehaviour p in Projectile)
        {
            if (p.isActiveAndEnabled)
            {
                //if (IsNight)
                //{
                    p.MoveProjectile();
                //}
                //else
                //{
                //    p.StopProjectile();
                //}

                if (p.GetExistTime() >= ExistTime)
                {
                    p.transform.gameObject.SetActive(false);
                    p.ResetExistTime();
                }
            }
        }
    }
    public void SpawnProjectile(Vector2 StartPos, Vector2 Dir)
    {
        if (RecycleProjectile(StartPos, Dir) == 1)
        {
            GameObject TempProjectile = Instantiate(ProjectilePrefab[0]);
            TempProjectile.transform.SetParent(Self);
            TempProjectile.transform.position = StartPos;
            TempProjectile.transform.rotation = Quaternion.FromToRotation(transform.right, Dir) * transform.rotation;
            ProjectileBehaviour p = TempProjectile.GetComponent<ProjectileBehaviour>();
            if (p != null)
            {
                p.ResetExistTime();
                p.StartingPos = StartPos;
                p.Dir = Dir;
                Projectile.Add(p);
            }
        }
    }

    // Look for Deactived projectile and reactiving them
    // Return 0 if it reactive any projectile
    // Return 1 if no Deactived projectile found
    public int RecycleProjectile(Vector2 StartPos, Vector2 Dir, int I = 0)
    {
        for (int i = 0; i < ProjectileCount; i++)
        {
            if (i + (ProjectileCount * I) > Projectile.Count)
            {
                Debug.LogError("RecycleProjectile Error");
                return -1;
            }

            ProjectileBehaviour p = Projectile[i + (ProjectileCount * I)];
            if (!p.isActiveAndEnabled)
            {
                p.transform.gameObject.SetActive(true);
                p.enabled = true;
                p.transform.position = StartPos;
                p.transform.rotation = Quaternion.FromToRotation(transform.up, Dir) * transform.rotation;

                p.StartingPos = StartPos;
                p.Dir = Dir;
                p.ResetExistTime();
                return 0;
            }

        }
        return 1;
    }
    private void CreateBullets()
    {
        for (int i = 0; i < ProjectilePrefab.Count; i++)
        {
            for (int i2 = 0; i2 < ProjectileCount; i2++)
            {
                var TempProjectile = Instantiate(ProjectilePrefab[i]);
                TempProjectile.transform.SetParent(this.transform);
                ProjectileBehaviour p = TempProjectile.GetComponent<ProjectileBehaviour>();
                if (p != null)
                {
                    Projectile.Add(p);
                    TempProjectile.SetActive(false);
                }
            }
        }
    }
}
