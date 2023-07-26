using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTouchDamage : MonoBehaviour
{
    [SerializeField] private float damage;
    private Vector2 velocity;
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        velocity = body.velocity;

        if (collision.collider.tag == "Player")
        {
            collision.collider.GetComponent<Health>().TakeDamage(damage);
            velocity = Vector2.zero;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        body.velocity = velocity;
    }
}
