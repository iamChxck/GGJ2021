using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : MonoBehaviour
{
    PlayerAbilities player;
    Rigidbody2D _rigidbody2D;
    [SerializeField] public bool hasCollided;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        hasCollided = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.GetComponent<PlayerAbilities>();
            
            if (player.canTogglePlatform == true && player.isBox)
            {
                hasCollided = true;
                _rigidbody2D.isKinematic = false;
            }
            //Debug.Log("ToggleValue: " + player.canTogglePlatform);
        }
        else if(collision.gameObject.tag == "Platform")
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
        }
       
    }
}
