using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StanderEnemyProjectile : ProjectileBehaviour
{

    void Start()
    {
        foreach (int i in IgnoreLayer)
            Physics2D.IgnoreLayerCollision(transform.gameObject.layer, i, true);


        RB = GetComponent<Rigidbody2D>();
        RB.velocity = Vector2.zero;
        SR = GetComponent<SpriteRenderer>();
    }
    virtual public void StopProjectile()
    {
        ExistTimer += Time.fixedDeltaTime;
        if (RB)
            RB.velocity = Dir * ProjectileSpeed;
    }
    virtual public void MoveProjectile()
    {
        ExistTimer += Time.fixedDeltaTime;
        if (RB)
            RB.velocity = Vector2.zero;
    }
}
