using System.Threading;
using UnityEngine;

public class Player : Entity
{
    [Header("이동동 정보")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [Header("대쉬 정보")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashCooldownTimer;

    [Header("공격 정보")]
    [SerializeField] private float comboTime = 0.3f;
    private float comboTimerCounter;
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
        comboTimerCounter -= Time.deltaTime;
        
        FlipController();
        AnimatorControllers();
    }
    // Velocity State
    private void Movement()
    {
        if(isAttacking)
        {
            rb.linearVelocity = Vector2.zero;
        }
        else if(dashTime > 0)
        {
            rb.linearVelocity = new Vector2(facingDir * dashSpeed, 0);
        }
        else
        {
            rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
        }
    }
    
    // Input
    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartAttackEvent();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();
        }
    }
    private void StartAttackEvent()
    {
        if(!isGrounded)
            return;
        
        if (comboTimerCounter < 0)
            comboCounter = 0;

        isAttacking = true;
        comboTimerCounter = comboTime;
    }
    private void Jump()
    {
        if(isGrounded)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }
    private void DashAbility()
    {
        if(dashCooldownTimer < 0 && !isAttacking)
        {
            dashCooldownTimer = dashCooldown;
            dashTime = dashDuration;
        }
    }

    // Flip
    private void FlipController()
    {
        if(rb.linearVelocityX > 0 && !facingRight)
        {
            Flip();
        }
        else if(rb.linearVelocityX <0 &&facingRight)
        {
            Flip();
        }
    }

    // Animation
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



    public void AttackOver()
    {
        isAttacking = false;

        comboCounter++;

        if(comboCounter > 2)
            comboCounter = 0;
    }

    protected override void CollisionChecks()
    {
        base.CollisionChecks();
    }

}