using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{

    // Start is called before the first frame update
    public Vector2 StartingPos;
    public Vector2 Dir;
    public float ProjectileSpeed;

    [SerializeField]
    private Rigidbody2D RB;

    [SerializeField]
    private SpriteRenderer SR;
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(6,3, true);
        RB = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
    }
    public void MoveProjectile()
    {
        RB.velocity = Dir * ProjectileSpeed;
    }

    public void StopProjectile()
    {
        RB.velocity = Vector2.zero;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.rigidbody.tag != "Player" && collision.rigidbody.tag != "PlayerProjectile")
        {
            transform.gameObject.SetActive(false);
        }
    }
}
