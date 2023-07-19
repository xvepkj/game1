using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    [Header("Sounds")]
    [SerializeField] private AudioClip fireballSound;
    [SerializeField] private AudioClip meleeSound;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

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

        if (Input.GetMouseButtonDown(0) && playerMovement.canAttack()) 
        {
            SoundManager.instance.PlaySound(meleeSound);
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
        anim.SetTrigger("meleeAttack");
    }
}
