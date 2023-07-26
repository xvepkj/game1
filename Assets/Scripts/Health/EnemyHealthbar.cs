using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{

    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;
    [SerializeField] private float offsetX;
    [SerializeField] private float offsetY;

    [SerializeField] 


    // Start is called before the first frame update
    void Start()
    {
        totalhealthBar.fillAmount = playerHealth.currentHealth / (playerHealth.GetComponent<Health>().startingHealth);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(new Vector3(playerHealth.transform.position.x + offsetX, playerHealth.transform.position.y + offsetY, playerHealth.transform.position.z));
        currenthealthBar.fillAmount = playerHealth.currentHealth / (playerHealth.GetComponent<Health>().startingHealth);
    }
}
