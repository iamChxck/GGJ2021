using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 200f;
    public float moveSpeed = 5.0f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private Rigidbody2D _rigidbody2D;

    [SerializeField] Animator animator;

    private float horizontalMovement;

    private bool canJump;
    private bool isRunning;

    public bool isFacingRight;




    private GameObject playerLives;

    private Vector3 playerStartPos;

    [SerializeField] protected SpriteRenderer spriteRenderer;

    protected float minX, maxX, minY, maxY;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        canJump = true;
        playerStartPos = transform.position;
        isFacingRight = true;
        isRunning = false;
    }

    private void FixedUpdate()
    {
            _rigidbody2D.gravityScale = 1;
            // For Movement
            horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed;

            _rigidbody2D.velocity = new Vector2(horizontalMovement, _rigidbody2D.velocity.y);

            // For jumping
            if (Input.GetKey(KeyCode.Space))
            {
                if (canJump == true)
                {
                    _rigidbody2D.AddForce(new Vector2(0, jumpForce));
                    canJump = false;
                    isRunning = true;
                }
            }

            // For a better jumping
            if (_rigidbody2D.velocity.y < 0)
            {
                // fallMultiplier - 1 because unity is already applying 1 multiple of the gravity so we just need 1.5 more to this
                // Why not just set it to 2.5?
                // So that we don't get confused while editing in the inspector
                _rigidbody2D.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
            }
            // This is for when the player chooses not to hold the jump button therefore having better control of jumping
            else if (_rigidbody2D.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
            {
                _rigidbody2D.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }
    


    private void Update()
    {
        animator.SetBool("isRunning", isRunning);

        if (_rigidbody2D.velocity.x > 0)
        {
            transform.localScale = new Vector2(+1, transform.localScale.y);
            isFacingRight = true;
            isRunning = true;
        }
        else if (_rigidbody2D.velocity.x < 0)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
            isFacingRight = false;
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }


        if (Input.GetKeyUp(KeyCode.Space))
        {
            isRunning = false;
        }
        RestrictMovement();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // Check if the player is back on the ground
        if (col.gameObject.tag == "Platform")
        {
            canJump = true;
        }
    }

  
    public virtual void RestrictMovement()
    {
        Vector3 upperRightCorner = Camera.main.ViewportToWorldPoint(new
            Vector3(1, 1, 0));
        Vector3 lowerLeftCorner = Camera.main.ViewportToWorldPoint(new
            Vector3(0, 0, 0));

        float xBound = spriteRenderer.bounds.extents.x;
        float yBound = spriteRenderer.bounds.extents.y;

        minX = lowerLeftCorner.x + xBound;
        maxX = upperRightCorner.x - xBound;
        minY = lowerLeftCorner.y + yBound;
        maxY = upperRightCorner.y - yBound;

        float restrictedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float restrictedY = Mathf.Clamp(transform.position.y, minY, maxY);

        transform.position = new Vector3(restrictedX, restrictedY, 0);
    }

   
}
