using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR

using UnityEditor.AssetImporters;

#endif
using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    private Animator anim;
    private BoxCollider2D boxCollider;


    [SerializeField] private RangedEnemy enemy;

    private bool hit;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        boxCollider.enabled = true;
    }

    private void Update()
    {
        if (hit) return;

        if(enemy != null && Mathf.Sign(speed) != Mathf.Sign(enemy.transform.localScale.x))
        {
            Debug.Log(Mathf.Sign(enemy.transform.localScale.x));
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            speed = speed * -1;
        }

        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        base.OnTriggerEnter2D(collision);
        boxCollider.enabled = false;
        
        if (anim != null)
            anim.SetTrigger("explode"); // when projectile has animation 

        else gameObject.SetActive(false); // when projective has no animation

    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
