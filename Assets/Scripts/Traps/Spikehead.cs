using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spikehead : EnemyDamage
{
    [Header("Spikehead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    private float checkTimer;
    private Vector3 destination;
    private bool attacking;

    private Vector3[] directions = new Vector3[4];

    [Header("Sound")]
    [SerializeField] private AudioClip spikedSound;

    private void Update()
    {
        
        if (attacking)
        {   
            Vector3 pos = transform.position;

            Vector3 trans = destination * speed * Time.deltaTime;

            if (transform.position.x + trans.x < leftEdge.position.x) pos.x = leftEdge.position.x;
            else if (transform.position.x + trans.x > rightEdge.position.x) pos.x = rightEdge.position.x;
            else pos.x += trans.x;

            pos.y += trans.y;
            pos.z += trans.z;

            transform.position = pos;
        }
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
                CheckForPlayer();

        }
    }

    private void CheckForPlayer()
    {
        CalculateDirections();

        for (int i = 0; i < directions.Length; i++)
        {   
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null  && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }

        }
    }

    private void OnEnable()
    {
        Stop();
    }

    private void CalculateDirections()
    {
        directions[0] = transform.right * range;
        directions[1] = -transform.right * range;
        directions[2] = transform.up * range;
        directions[3] = -transform.up * range;
    }

    private void Stop()
    {
        destination = transform.position;
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SoundManager.instance.PlaySound(spikedSound);
        }
        base.OnTriggerEnter2D(collision);
        Stop();

    }
}
