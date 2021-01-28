using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransform : MonoBehaviour
{
    [SerializeField] Sprite originalSprite = null;
    Sprite objectSprite = null;
    SpriteRenderer playerSprite = null;

    bool canTransform;


    private void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            objectSprite = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
        }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Interactable"))
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
                playerSprite.sprite = objectSprite;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                playerSprite.sprite = originalSprite;
            }
        }
    }
}
