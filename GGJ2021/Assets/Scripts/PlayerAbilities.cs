using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    PlayerMovement player;
    [SerializeField] private bool canBreakPlatform;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerMovement>();
        canBreakPlatform = false;
    }

    // Update is called once per frame
    void Update()
    {
        DestroyPlatform();
    }

    public void DestroyPlatform()
    {
        if (player.doubleJump == true)
        {
            canBreakPlatform = true;
        }
        else
            canBreakPlatform = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (canBreakPlatform)
        {
            if(collision.gameObject.tag == "Breakable Platform")
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
