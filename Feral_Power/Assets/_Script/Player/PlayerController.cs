using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    public ProjectileManager PlayerProjectileManager;
    private Rigidbody2D RB;
    private SpriteRenderer SR;

    [SerializeField]
    private bool IsNight = false;
    
    [Header("Movement")]
    public float MoveSpeed;

    [SerializeField]
    private float Horizontal;

    [SerializeField]
    private float Vertical;


    [Header("Dash")]
    public float DashForce = 100.0f;
    public float DashCoolDownTime = 1.0f;
    private  bool DashNextFrame;
    private float DashTimer;
    // Start is called before the first frame update
    void Start()
    {
        IsNight = false;
        RB = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        DashTimer += Time.deltaTime;
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        if (Input.GetMouseButtonDown(0) && !IsNight)
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 heading = mouseWorldPos - RB.position;
            float distance = heading.magnitude;
            Vector2 dir = heading / distance;

            if(PlayerProjectileManager != null)
                PlayerProjectileManager.RecycleProjectile((RB.position + (dir)), dir);
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            IsNight = !IsNight;
            PlayerProjectileManager.DayNightCycle(IsNight);
        }

        if(Input.GetKeyDown(KeyCode.Space) && DashTimer >= DashCoolDownTime)
        {
            DashNextFrame = true;
        }
    }

    private void FixedUpdate()
    {
        RB.velocity = new Vector2(Horizontal * MoveSpeed, Vertical * MoveSpeed);

        if (DashNextFrame)
        {
            if(RB.bodyType == RigidbodyType2D.Kinematic)
                RB.velocity = new Vector2(Horizontal * DashForce, Vertical * DashForce);
            else if(RB.bodyType == RigidbodyType2D.Dynamic)
                RB.AddForce(new Vector2(Horizontal * DashForce, Vertical * DashForce), ForceMode2D.Impulse);

            DashNextFrame = false;
            DashTimer = 0;
        }
    }
}
