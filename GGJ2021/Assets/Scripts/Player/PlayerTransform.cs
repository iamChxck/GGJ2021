using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransform : MonoBehaviour
{
    [SerializeField] Sprite originalSprite = null;
    Sprite objectSprite = null;
    Rigidbody2D rb2D = null;
    SpriteRenderer playerSprite = null;
    PlayerAbilities player = null;
    public float colliderMass;
    
    bool canTransform;


    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        player = GetComponent<PlayerAbilities>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interactable")
        {
            objectSprite = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
            colliderMass = collision.attachedRigidbody.mass;
        }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interactable")
        {
            canTransform = true;
            
        }
    }

    
    private void OnTriggerExit2D(Collider2D collision)
    {
        canTransform = false;
    }

    private void Update()
    {
       
        if(canTransform)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                playerSprite.sprite = objectSprite;
                player.isSoulForm = false;
                
                player.morphedObject = player.currentObject;
                rb2D.mass = colliderMass;
            }
                
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                playerSprite.sprite = originalSprite;
                player.isSoulForm = true;
                player.morphedObject = null;
                player.isCircle = false;
                player.isBox = false;
                player.isBalloon = false;
                rb2D.mass = 1;
            }
        }
    }
}
