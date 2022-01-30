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
    private Animator m_animator;
    Vector2 moveDirection;

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
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        if (Input.GetMouseButtonDown(0) && !IsNight)
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 heading = mouseWorldPos - RB.position;
            float distance = heading.magnitude;
            Vector2 dir = heading / distance;

            if (PlayerProjectileManager != null)
                PlayerProjectileManager.RecycleProjectile((RB.position + (dir)), dir);
        }

        //Movement
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * MoveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            //IsNight = !IsNight;
            //PlayerProjectileManager.DayNightCycle(IsNight);

            GameManager.TimeChange();
        }

        if (Input.GetKeyDown(KeyCode.Space) && DashTimer >= DashCoolDownTime)
        {
            DashNextFrame = true;
        }

        //IDLE Animation
        //if (Horizontal == 0 && Vertical == 0 && DashForce == 0)
        //{
        //   //m_animator.SetInteger("AnimState", (int)PlayerAnimationType.IDLE);
        //}

        //No Vertical Input
        //Move Left
        if (Input.GetKey(KeyCode.A))
        {
            SR.flipX = false;
            m_animator.SetInteger("AnimState", (int)PlayerAnimationType.WALK_LEFT);
            Debug.Log("Walkleft");
        }
        //Move Right
        if (Input.GetKey(KeyCode.D))
        {
            SR.flipX = true;
            m_animator.SetInteger("AnimState", (int)PlayerAnimationType.WALK_LEFT);
            Debug.Log("Walkright");
        }


        //Move Straight Down
        if (Input.GetKey(KeyCode.S))
        {
            m_animator.SetInteger("AnimState", (int)PlayerAnimationType.WALK_DOWN);
            Debug.Log("WalkDown");
            Debug.Log("Vertical Value:" + Vertical);
        }
        //Move Straight Up
        if (Input.GetKey(KeyCode.W))
        {
            m_animator.SetInteger("AnimState", (int)PlayerAnimationType.WALK_UP);
            Debug.Log("Walkup");
        }
        //Move Up Left
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            SR.flipX = false;
            m_animator.SetInteger("AnimState", (int)PlayerAnimationType.WALK_UP_LEFT);
            Debug.Log("Walkupleft");
        }
        //Move Up Right
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            SR.flipX = true;
            m_animator.SetInteger("AnimState", (int)PlayerAnimationType.WALK_UP_LEFT);
            Debug.Log("Walkupright");
        }
        //Move Down Left
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            SR.flipX = true;
            m_animator.SetInteger("AnimState", (int)PlayerAnimationType.WALK_DOWN_LEFT);
            Debug.Log("WalkDownleft");
        }
        //Move Down Right
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            SR.flipX = false;
            m_animator.SetInteger("AnimState", (int)PlayerAnimationType.WALK_DOWN_LEFT);
            Debug.Log("WalkDownRight");
        }

        //DASHES

    }



    private void FixedUpdate()
    {
        IsNight = GameManager.IsNight();
        //RB.AddForce(new Vector2(Horizontal * MoveSpeed, Vertical * MoveSpeed));


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
}

    

