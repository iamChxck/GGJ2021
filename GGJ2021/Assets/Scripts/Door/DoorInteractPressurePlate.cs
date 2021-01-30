using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractPressurePlate : MonoBehaviour
{
    [SerializeField] GameObject doorGameObject;
    DoorSetActive door;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        door = doorGameObject.GetComponent<DoorSetActive>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Small Circle")
        {
            //Debug.Log("Door opened!");
            door.OpenDoor();
            timer = 1f;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Small Circle")
        {
           // Debug.Log("Door opened!");
            door.OpenDoor();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Small Circle")
        {
            timer = 1f;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0f)
            {
                door.CloseDoor();
              //  Debug.Log("Door closed!");
            }
        }
    }
}
