using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float wallJumpForceX = 5f; 
    [SerializeField] private float wallJumpForceY = 10f;
    [SerializeField] private bool canJump;
    [SerializeField] private bool canMove;

    [Header("Ground and Wall Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Respawn")]
    [SerializeField] private Transform startSpawnPoint;
    [SerializeField] private Transform respawnPoint;

    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance = 0.5f;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isFacingRight = true;
    private bool isGrounded;
    private bool isTouchingWall;

    public bool isWallJumping;

    private float horizontalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (canMove)
        {
            horizontalInput = Input.GetAxis("Horizontal");

            if (!isWallJumping)
            {
                //  if (isGrounded || isTouchingWall)
                //  {
                if (horizontalInput > 0 && !isFacingRight)
                    Flip();
                else if (horizontalInput < 0 && isFacingRight)
                    Flip();
                //  }
            }

            if (Input.GetButtonDown("Jump") && canJump)
            {
                CancelInvoke(nameof(ResetWallJump));
                isWallJumping = false;
                if (isGrounded)
                {
                    Jump();
                }
                else if (isTouchingWall)
                {
                    WallJump();
                }
            }
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontalInput)); // For blend tree
        animator.SetBool("isGrounded", isGrounded); // For Jump and Fall animations
        animator.SetFloat("VerticalVelocity", rb.linearVelocity.y); // To differentiate between jump and fall
        animator.SetBool("isTouchingWall", isTouchingWall); // Add wall cling animation
    }

    void FixedUpdate()
    {
        if (!isWallJumping)
        {
          //  if (isGrounded || isTouchingWall)
                rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, isFacingRight ? Vector2.right : Vector2.left, wallCheckDistance, wallLayer);
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    void WallJump()
    {
        // Add a slight delay to prevent immediate input during the wall jump
        isWallJumping = true;

        // Calculate jump direction
        float jumpDirection = isFacingRight ? -1 : 1;

        // Apply jump force
        rb.linearVelocity = new Vector2(jumpDirection * wallJumpForceX, wallJumpForceY);

        // Flip player direction after the jump
        if (isFacingRight && jumpDirection < 0 || !isFacingRight && jumpDirection > 0)
        {
            Flip();
        }

        // Reset wall jumping state after a delay
        Invoke(nameof(ResetWallJump), 0.6f);
    }

    void ResetWallJump()
    {
        isWallJumping = false;
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    public void RespawnPlayer()
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = respawnPoint.position;
    }

    public void CanMove()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        canMove = true;
    }

    void StopMove()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        canMove = false;
    }

    public void CanJump(bool newState)
    {
        canJump = newState;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        if (wallCheck != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + (isFacingRight ? Vector3.right : Vector3.left) * wallCheckDistance);
        }
    }


}//class
