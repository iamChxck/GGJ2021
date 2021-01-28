using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    PlayerMovement player;
    Soul soul;
    [SerializeField] public bool canTogglePlatform;

    [SerializeField] public int soulsHeld;
    [SerializeField] public int soulsReturned;
    [SerializeField] List<GameObject> heldSouls = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerMovement>();
        canTogglePlatform = false;
        soulsHeld = 0;
        soulsReturned = 0;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        DestroyPlatform();

        if(heldSouls[0] == null)
        {
            heldSouls.RemoveAt(0);
        }
    }

    public void DestroyPlatform()
    {
        if (player.doubleJump == true)
        {
            canTogglePlatform = true;
        }
    }

    public void FollowPlayer()
    {
        if (soul.withinRange)
        {
            if (Input.GetMouseButtonDown(0))
            {
                soul.gameObject.transform.parent = gameObject.transform;
                heldSouls.Add(soul.gameObject);
                soulsHeld++;
                soul.withinRange = false;
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.CompareTag("Lost Soul"))
        {
            soul = collision.gameObject.GetComponent<Soul>();
            Debug.Log("Lost Soul within range!");
            soul.withinRange = true;
        }

        if (collision.gameObject.CompareTag("Sign"))
        {
            
            soulsReturned += soulsHeld;
          
            for (int i = 0; i < soulsHeld; i ++)
            {
                Destroy(heldSouls[i]);
            }
            soulsHeld = 0;
            Debug.Log("Souls Returned!");
           
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        soul.withinRange = false;
       
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (canTogglePlatform)
        {
            if(collision.gameObject.tag == "Breakable Platform")
            {
                Destroy(collision.gameObject);
                canTogglePlatform = false;
            }
            else if (collision.gameObject.tag == "Movable Platform")
            {
                canTogglePlatform = false;
            }
        }
        if(collision.gameObject.tag == "Platform")
        {
            canTogglePlatform = false;
        }
    }
}
