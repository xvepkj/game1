using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomRegion : MonoBehaviour
{
    [SerializeField] private GameObject enemyHealthBar;

    private void Awake()
    {
        enemyHealthBar.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            enemyHealthBar.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enemyHealthBar.SetActive(false);
        }
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
