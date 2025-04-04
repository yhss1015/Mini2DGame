using UnityEngine;

public class Player : Entity
{

    [Header("�̵� ����")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [Header("�뽬 ����")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    private float dashTime;

    [SerializeField] private float dashCooldown;
    private float dashCooldownTimer;

    [Header("���� ����")]

    [SerializeField] private float comboTime = 0.3f;
    private float comboTimeWindow;
    private bool isAttacking;
    private int comboCounter;



    private float xInput;

    

   
    

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        CheckInput();
        Movement();
        


        dashTime -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;
        comboTimeWindow -= Time.deltaTime;



        FlipController();
        AnimatorControllers();

    }

    public void AttackOver()
    {
        isAttacking = false;


        comboCounter++;


        if (comboCounter > 2)
            comboCounter = 0;




    }




   

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartAttackEvent();
        }




        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();
        }


    }

    private void StartAttackEvent()
    {
        if (!isGrounded)
        {
            return;
        }

        if (comboTimeWindow < 0)
            comboCounter = 0;

        isAttacking = true;
        comboTimeWindow = comboTime;
    }

    private void DashAbility()
    {

        if (dashCooldownTimer < 0 )
        {
            dashCooldownTimer = dashCooldown;
            dashTime = dashDuration;
            isAttacking = false;
        }
    }

    private void Movement()
    {
        if(isAttacking)
        {
            rb.linearVelocity = new Vector2(0, 0);
        }
        else if (dashTime > 0)
        {
            rb.linearVelocity = new Vector2(xInput * dashSpeed, 0);
        }
        else
        {
            rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
        }


    }

    private void Jump()
    {
        if (isGrounded)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }





    //Alt + ȭ��ǥŰ
    private void AnimatorControllers()
    {

        bool isMoving = rb.linearVelocity.x != 0;


        anim.SetFloat("yVelocity", rb.linearVelocityY);

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDashing", dashTime > 0);
        anim.SetBool("isAttacking", isAttacking);
        anim.SetInteger("comboCounter", comboCounter);
    }

    


    private void FlipController()
    {
        if (rb.linearVelocityX > 0 && !facingRight)
        {
            Flip();
        }
        else if (rb.linearVelocityX < 0 && facingRight)
        {
            Flip();
        }
    }


    protected override void CollisionChecks()
    {
        base.CollisionChecks();


    }



}