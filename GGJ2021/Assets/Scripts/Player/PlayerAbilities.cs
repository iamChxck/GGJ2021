using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    PlayerMovement player = null;
    BoxCollider2D boxCollider = null;
    MovablePlatform movablePlatform = null;
    PlayerTransform playerTransform = null;
    Soul soul;
    public bool canTogglePlatform;
    public bool isSoulForm;

    public bool isBox;
    public bool isBalloon;
    public bool isCircle;

    Rigidbody2D rb2D;

    public GameObject currentObject = null;
    public GameObject morphedObject = null;
    public int soulsHeld;
    public int soulsReturned;
    [SerializeField] List<GameObject> heldSouls = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerMovement>();
        playerTransform = GetComponent<PlayerTransform>();
        boxCollider = GetComponent<BoxCollider2D>();
        isSoulForm = true;
        canTogglePlatform = false;
        soulsHeld = 0;
        soulsReturned = 0;
        isBox = false;
        isBalloon = false;
        isCircle = false;
    }

    
    // Update is called once per frame
    void Update()
    {
        
        FollowPlayer();   
        CurrentForm();

        if (player.doubleJump == true)
        {
            canTogglePlatform = true;
        }

        if (heldSouls[0] == null)
        {
            heldSouls.RemoveAt(0);
        }
       
    }

    public void CurrentForm()
    {
        if (morphedObject.name == "Big Heavy Box")
        {
            isBox = true;
           // isCircle = false;
           // isBalloon = false;
        }
        else if (morphedObject.name == "Small Circle")
        {
            isCircle = true;
           // isBox = false;
           // isBalloon = false;
        }
        else if (morphedObject.name == "Balloon")
        {
            isBalloon = true;
           // isBox = false;
           // isCircle = false;
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
        if (collision.gameObject.tag == "Interactable")
        {
            if (isSoulForm)
            {
                Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
            }
            else
            {
                Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), false);
            }
            currentObject = collision.gameObject;
        }

        if (collision.gameObject.CompareTag("Lost Soul"))
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
        if (collision.gameObject.tag == "Interactable")
        {
            currentObject = null;
        }
        
        
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        
        canTogglePlatform = false;
        if(collision.gameObject.tag == "Movable Platform")
        {
            movablePlatform.hasCollided = false;
        }
        if (collision.gameObject.tag == "Interactable")
        {
            currentObject = null;
            collision.rigidbody.isKinematic = false;
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Interactable")
        {
            if (rb2D.mass < collision.rigidbody.mass)
            {
                collision.rigidbody.velocity = new Vector2(0, 0);
                collision.rigidbody.isKinematic = true;
            }
           
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            canTogglePlatform = false;
        }
        if (collision.gameObject.tag == "Interactable")
        {
            if (rb2D.mass < collision.rigidbody.mass)
            {
                collision.rigidbody.velocity = new Vector2(0, 0);
                collision.rigidbody.isKinematic = true;
            }

        }

        if (collision.gameObject.tag == "Interactable")
        {
            playerTransform.colliderMass = collision.rigidbody.mass;
        }

        if (canTogglePlatform)
        {
            if(collision.gameObject.tag == "Breakable Platform")
            {
                Destroy(collision.gameObject);
                canTogglePlatform = false;
                
            }
            else if (collision.gameObject.tag == "Movable Platform")
            {
                movablePlatform = collision.gameObject.GetComponent<MovablePlatform>();
                if (isBox)
                {
                    if (movablePlatform.hasCollided == true)
                    {
                        canTogglePlatform = false;
                    }
                }
                
            }
        }
       
    }
}
