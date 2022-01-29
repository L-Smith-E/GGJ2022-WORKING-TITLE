using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class ProjectileBehaviour : MonoBehaviour
{

    // Start is called before the first frame update
    public Vector2 StartingPos;
    public Vector2 Dir;
    public float ProjectileSpeed;
    public float Damage = 0.0f;

    private Rigidbody2D RB;
    private SpriteRenderer SR;

    [SerializeField]
    private float ExistTimer;


    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(6,3, true);
        RB = GetComponent<Rigidbody2D>();
        RB.velocity = Vector2.zero;
        SR = GetComponent<SpriteRenderer>();
    }
    public void MoveProjectile()
    {
        ExistTimer += Time.fixedDeltaTime;
        if (RB)
            RB.velocity = Dir * ProjectileSpeed;
    }
    public void StopProjectile()
    {
        ExistTimer += Time.fixedDeltaTime;
        if (RB)
            RB.velocity = Vector2.zero;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.rigidbody.tag != "Player" && collision.rigidbody.tag != "PlayerProjectile")
        {
            ResetExistTime();
            transform.gameObject.SetActive(false);
            collision.transform.SendMessage("TakeDamage", Damage, SendMessageOptions.DontRequireReceiver);
        }
    }

    public float GetExistTime()
    {
        return ExistTimer;
    }

    public void ResetExistTime()
    {
        ExistTimer = 0;
    }
}
