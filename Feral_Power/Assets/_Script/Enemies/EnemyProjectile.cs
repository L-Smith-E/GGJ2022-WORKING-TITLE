using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Vector2 moveDirection;
    public float moveSpeed;

    private void Start()
    {
        
    }

    private void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction;
    }
}
