using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkPointSound;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UIManager uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();  
    }

    private void CheckRespawn()
    {
        //Check if check point available 
        if(currentCheckpoint == null)
        {
            uiManager.GameOver();

            return;
        }

        transform.position = currentCheckpoint.position;
        playerHealth.Respawn();

        Camera.main.GetComponent<CameraController>().MovetoNewRoom(currentCheckpoint.parent);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            SaveCheckPointToPref(currentCheckpoint.position);
            SoundManager.instance.PlaySound(checkPointSound);
            collision.GetComponent<Collider2D>().enabled = false;
        }
    }

    private void SaveCheckPointToPref(Vector3 checkPointPos)
    {
        PlayerPrefs.SetFloat("checkpointX", checkPointPos.x);  
        PlayerPrefs.SetFloat("checkpointY", checkPointPos.y);
        PlayerPrefs.SetFloat("checkpointZ", checkPointPos.z);
    }
}
