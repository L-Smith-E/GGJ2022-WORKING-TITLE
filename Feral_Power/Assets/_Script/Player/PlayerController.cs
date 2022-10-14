using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public ProjectileManager PlayerProjectileManager;
    private Rigidbody2D RB;
    private SpriteRenderer SR;
    private Animator m_animator;

    [SerializeField]
    private bool IsNight = false;

    [Header("Movement")]
    public float MoveSpeed;

    [SerializeField]
    private float Horizontal;

    [SerializeField]
    private float Vertical;

    [SerializeField]
    private Vector2 FacingDir;

    [SerializeField]
    private Vector2 PrevFacingDir;

    private Vector2 AimDirection;

    [Header("Dash")]
    public float DashForce = 100.0f;
    public float DashCoolDownTime = 1.0f;
    private bool DashNextFrame;
    private float DashTimer;
    // Start is called before the first frame update
    void Start()
    {
        IsNight = false;
        RB = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();

        GameManager.Player = this.transform.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        DashTimer += Time.deltaTime;

        // Get the Direction the player is facing
        var currentfacing = RB.velocity.normalized;
        if (currentfacing != Vector2.zero)
            PrevFacingDir = currentfacing;

        m_animator.SetFloat("Horizontal", PrevFacingDir.y);
        m_animator.SetFloat("Vertical", PrevFacingDir.x);

        float speed = RB.velocity.magnitude;
        m_animator.SetFloat("Speed", speed);

        SR.flipX = false;
        if (PrevFacingDir.x >= 1)
        {
            //Going Right
            SR.flipX = true;
        }
    }



    private void FixedUpdate()
    {
        RB.velocity = FacingDir * MoveSpeed;

        if (DashNextFrame)
        {
            if (RB.bodyType == RigidbodyType2D.Kinematic)
                RB.velocity = new Vector2(Horizontal * DashForce, Vertical * DashForce);
            else if (RB.bodyType == RigidbodyType2D.Dynamic)
               RB.AddForce(new Vector2(Horizontal * DashForce, Vertical * DashForce), ForceMode2D.Impulse);
            DashNextFrame = false;
            DashTimer = 0;
        }

    }

    public void OnMovement(InputValue var)
    {
        if(var.Get<Vector2>().GetType() == typeof(Vector2))
            FacingDir = var.Get<Vector2>();
    }

    public void OnAttack()
    {
        if (!GameManager.IsNight())
        {
            if (PlayerProjectileManager != null)
                PlayerProjectileManager.RecycleProjectile((RB.position + (AimDirection * 1.5f)), AimDirection);
        }
    }

    public void OnUpdateAimPos(InputValue var)
    {
        if (var.Get<Vector2>().GetType() == typeof(Vector2))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(var.Get<Vector2>());
            Vector2 heading = mouseWorldPos - RB.position;
            float distance = heading.magnitude;
            AimDirection = heading / distance;
        }
    }

    public void OnTimeChange()
    {
        GameManager.TimeChange();
    }
}

    

