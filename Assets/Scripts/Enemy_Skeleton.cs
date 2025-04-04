using UnityEngine;

public class Enemy_Skeleton : Entity
{

    [Header("이동 정보")]
    [SerializeField] private float moveSpeed;

    [Header("Player detection")]
    [SerializeField] private float playerCheckDistance;
    [SerializeField] private LayerMask whatIsPlayer;
    bool isAttacking;


    private RaycastHit2D isPlayerDetected;



    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (isPlayerDetected)
        {
            if (isPlayerDetected.distance > 1)
            {
                //추적
                rb.linearVelocity = new Vector2(moveSpeed * 3.0f * facingDir, rb.linearVelocityY);

                Debug.Log("플레이어를 인지함");
            }
            else
            {
                Debug.Log("사거리 내로 들어와 공격" + isPlayerDetected.collider.gameObject.name);
                isAttacking = true;
            }
        }

        if (!isGrounded || isWallDetected)
        {
            Flip();
        }

        Movement();
    }

    private void Movement()
    {
        if(!isAttacking)
        {
            rb.linearVelocity = new Vector2(moveSpeed * facingDir, rb.linearVelocity.y);
        }        
    }

    protected override void CollisionChecks()
    {
        base.CollisionChecks();
        isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right, playerCheckDistance * facingDir,whatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + playerCheckDistance * facingDir, transform.position.y));
    }

}
