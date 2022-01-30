using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Vector2 moveDirection;
    public float dayMoveSpeed;
    public float nightMoveSpeed;

    private void Start()
    {

    }

    private void Update()
    {
        if (GameManager.IsDay())
        {
            transform.Translate(moveDirection * dayMoveSpeed * Time.deltaTime);
        }
        else if (GameManager.IsNight())
        {
            transform.Translate(moveDirection * nightMoveSpeed * Time.deltaTime);
        }

    }

    public void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction;
    }
}
