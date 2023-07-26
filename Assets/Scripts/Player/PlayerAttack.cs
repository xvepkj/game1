using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Fireball")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    [Header("melee attack params")] 
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float attackRange;
    [SerializeField] private float meleeCooldown;

    [Header("Sounds")]
    [SerializeField] private AudioClip fireballSound;
    [SerializeField] private AudioClip meleeSound;
    [SerializeField] private Transform attackPoint;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    private float meleeCooldownTimer = Mathf.Infinity;

    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.V) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            RangeAttack();
        }

        cooldownTimer += Time.deltaTime;
        meleeCooldownTimer += Time.deltaTime;   

        if (Input.GetMouseButtonDown(0) && playerMovement.canAttack() && meleeCooldownTimer > meleeCooldown) 
        {
            meleeAttack();
        }
    }

    private void RangeAttack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        anim.SetTrigger("rangeAttack");
        cooldownTimer = 0;

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for(int i = 0; i < fireballs.Length; i++) { 
            if(!fireballs[i].activeInHierarchy) return i;
        }
        return 0;
    }

    private void meleeAttack()
    {
        if (gameObject.GetComponent<Rigidbody2D>().velocity != Vector2.zero) return;
        SoundManager.instance.PlaySound(meleeSound);
        anim.SetTrigger("meleeAttack");
        meleeCooldownTimer = 0;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Health>().TakeDamage(2.5f);
        }
    }

    private void OnDrawGizmos()
    {
        if (firePoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
