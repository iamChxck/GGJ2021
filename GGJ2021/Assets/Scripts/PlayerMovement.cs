using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 200f;
    public float moveSpeed = 5.0f;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private Rigidbody2D _rigidbody2D;

    [SerializeField] Animator animator;

    private float horizontalMovement;

    [SerializeField] private bool canJump;
    [SerializeField] public bool doubleJump;
    [SerializeField] private bool isRunning;

    [SerializeField] private bool isGrounded;

    public bool isFacingRight;
    private int extraJumps;

    public int extraJumpsValue;

    private GameObject playerLives;

    private Vector3 playerStartPos;

    [SerializeField] protected SpriteRenderer spriteRenderer;

    protected float minX, maxX, minY, maxY;

    private void Start()
    {
        extraJumps = extraJumpsValue;
    }
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        playerStartPos = transform.position;
        isFacingRight = true;
        isRunning = false;
    }

    private void FixedUpdate()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        
            // For Movement
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed;

        _rigidbody2D.velocity = new Vector2(horizontalMovement, _rigidbody2D.velocity.y);

       
    }
    


    private void Update()
    {
        if (extraJumps == 0)
        {
            doubleJump = true;
        }
        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
            doubleJump = false;
        }

       
        Jump();


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
       
    }

  
    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            Debug.Log("PRESSING SPACE");
            _rigidbody2D.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded)
        {
            _rigidbody2D.velocity = Vector2.up * jumpForce;
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
