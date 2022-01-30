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

            if(PlayerProjectileManager != null)
                PlayerProjectileManager.RecycleProjectile((RB.position + (dir)), dir);
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            //IsNight = !IsNight;
            //PlayerProjectileManager.DayNightCycle(IsNight);

            GameManager.TimeChange();
        }

        if(Input.GetKeyDown(KeyCode.Space) && DashTimer >= DashCoolDownTime)
        {
            DashNextFrame = true;
        }
            
        //IDLE Animation
        //if (Horizontal == 0 && Vertical == 0 && DashForce == 0)
        //{
        //   //m_animator.SetInteger("AnimState", (int)PlayerAnimationType.IDLE);
        //}

        if (Input.anyKeyDown)
        {
        //No Vertical Input
            if (Vertical == 0 && !DashNextFrame)
            {
            //Move Left
                if (Horizontal < 0)
                {
                 
                 m_animator.SetInteger("AnimState", (int)PlayerAnimationType.WALK_LEFT);
                 Debug.Log("Walkleft");
                }
            //Move Right
                if (Horizontal > 0)
                {
                    SR.flipX = true;
                  m_animator.SetInteger("AnimState", (int)PlayerAnimationType.WALK_LEFT);
                  Debug.Log("Walkright");
                }

                    //if (DashForce >= 0)
                    //{

                    //}
                }
            //Vertical input
            if (Vertical > 0 || Vertical < 0 && !DashNextFrame)
                Debug.Log("Vertical Input");
            {
            //Move Straight Down
               if (Vertical < 0 && Horizontal == 0)
                {
                    m_animator.SetInteger("AnimState", (int)PlayerAnimationType.WALK_DOWN);
                    Debug.Log("WalkDown");
                    Debug.Log("Vertical Value:" + Vertical);
                }
                //Move Straight Up
                if (Vertical > 0 && Horizontal == 0)
                {
                    m_animator.SetInteger("AnimState", (int)PlayerAnimationType.WALK_UP);
                    Debug.Log("Walkup");
                }
                //Move Up Left
                if (Vertical > 0 && Horizontal < 0)
                {

                    m_animator.SetInteger("AnimState", (int)PlayerAnimationType.WALK_UP_LEFT);
                    Debug.Log("Walkupleft");
                }
                //Move Up Right
                if (Vertical > 0 && Horizontal > 0)
                {
                    SR.flipX = true;
                    m_animator.SetInteger("AnimState", (int)PlayerAnimationType.WALK_UP_LEFT);
                    Debug.Log("Walkupright");
                }
                //Move Down Left
                if (Vertical < 0 && Horizontal < 0 )
                {
                    m_animator.SetInteger("AnimState", (int)PlayerAnimationType.WALK_DOWN_LEFT);
                    Debug.Log("WalkDownleft");
                }
                //Move Down Right
                if (Vertical < 0 && Horizontal > 0)
                {
                    SR.flipX = true;
                    m_animator.SetInteger("AnimState", (int)PlayerAnimationType.WALK_DOWN_LEFT);
                }

                //DASHES


            }
        }
        
        }

    private void FixedUpdate()
    {
        IsNight = GameManager.IsNight();
        RB.AddForce(new Vector2(Horizontal * MoveSpeed, Vertical * MoveSpeed));

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

    

