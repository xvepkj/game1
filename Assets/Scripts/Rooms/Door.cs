using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]private Transform previousRoom;
    [SerializeField]private Transform nextRoom;
    [SerializeField] private CameraController cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("player confirmed");
            if (collision.transform.position.x < transform.position.x)
            {
                Debug.Log("Room 1 to Room 2");
                cam.MovetoNewRoom(nextRoom);
                previousRoom.GetComponent<Room>().ActivateRoom(false);
                nextRoom.GetComponent<Room>().ActivateRoom(true);

            }
            else
            {
                Debug.Log("Room 2 to Room 1");
                cam.MovetoNewRoom(previousRoom);
                previousRoom.GetComponent<Room>().ActivateRoom(true);
                nextRoom.GetComponent<Room>().ActivateRoom(false);
            }
        }
    }
}
