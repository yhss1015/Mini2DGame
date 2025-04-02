using UnityEditor.Rendering;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private bool isMoving;
    private float xInput;

    private int facingDir = 1;
    private bool facingRight = true;

    [Header("Collision info")]    
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask LayerGround;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        CheckInput();
        Movement();

        GroundCheck();

        FlipController();
        AnimatorControllers();
        
    }
    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void GroundCheck()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, LayerGround);
    }

    private void Movement()
    {
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
    }

    private void AnimatorControllers()
    {
        isMoving = rb.linearVelocity.x != 0;
        anim.SetBool("isMoving", isMoving);

    }

    private void Jump()
    {
        if(isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        
    }

    private void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipController()
    {
        if (rb.linearVelocityX > 0 && !facingRight)
        {
            Flip();
        }
        else if(rb.linearVelocityX<0 && facingRight)
        {
            Flip();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
    }
}
