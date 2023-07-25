using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float damage;

    public void CastSpell()
    {
        gameObject.SetActive(true);
        
    }

    public void AttackPlayer()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(transform.position, new Vector2(2, 3), 0, Vector2.down, 0.1f, playerMask);
        if (raycastHit.collider != null && raycastHit.collider.tag == "Player")
        {
            raycastHit.collider.GetComponent<Health>().TakeDamage(damage);
        }
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
