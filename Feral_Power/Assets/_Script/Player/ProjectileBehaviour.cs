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
    public List<string> IgnoreTagList;
    public List<int> IgnoreLayer;

    protected Rigidbody2D RB;
    protected SpriteRenderer SR;

    [SerializeField]
    protected float ExistTimer;

    // Start is called before the first frame update
    void Start()
    {
        foreach(int i in IgnoreLayer)
            Physics2D.IgnoreLayerCollision(transform.gameObject.layer, i, true);


        RB = GetComponent<Rigidbody2D>();
        RB.velocity = Vector2.zero;
        SR = GetComponent<SpriteRenderer>();
    }
    virtual public void MoveProjectile()
    {
        ExistTimer += Time.fixedDeltaTime;
        if (RB)
            RB.velocity = Dir * ProjectileSpeed;
    }
    virtual public void StopProjectile()
    {
        ExistTimer += Time.fixedDeltaTime;
        if (RB)
            RB.velocity = Vector2.zero;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(!IgnoreTagList.Contains(collision.transform.tag))
        {
            ResetExistTime();
            transform.gameObject.SetActive(false);
            collision.transform.SendMessage("TakeDamage", Damage, SendMessageOptions.DontRequireReceiver);
            this.SendMessageUpwards("HitEvent", null, SendMessageOptions.DontRequireReceiver);
            return;
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
