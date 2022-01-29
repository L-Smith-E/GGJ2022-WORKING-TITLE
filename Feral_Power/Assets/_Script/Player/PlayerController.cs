using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ProjectileManager PlayerProjectileManager;
    public float MoveSpeed;

    [SerializeField]
    private Rigidbody2D RB;

    [SerializeField]
    private SpriteRenderer SR;

    [SerializeField]
    private float Horizontal;

    [SerializeField]
    private float Vertical;

    [SerializeField]
    private bool IsNight = false;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        if (Input.GetMouseButtonDown(0) && !IsNight)
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 heading = mouseWorldPos - RB.position;
            float distance = heading.magnitude;
            Vector2 dir = heading / distance;

            if(PlayerProjectileManager != null)
                PlayerProjectileManager.SpawnProjectile((RB.position + (dir)), dir);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            IsNight = !IsNight;
            PlayerProjectileManager.DayNightCycle(IsNight);
        }
    }

    private void FixedUpdate()
    {
        RB.velocity = new Vector2(Horizontal * MoveSpeed, Vertical * MoveSpeed);
    }
}
