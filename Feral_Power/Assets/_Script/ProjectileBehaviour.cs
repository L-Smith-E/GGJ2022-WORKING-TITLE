using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{

    // Start is called before the first frame update
    public Vector2 Dir;
    public float ProjectileSpeed;

    [SerializeField]
    private Rigidbody2D RB;

    [SerializeField]
    private SpriteRenderer SR;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
    }
    public void MoveProjectile()
    {
        RB.velocity = Dir * ProjectileSpeed;
    }
}
