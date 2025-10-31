using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Character
{
    [Header("Input Settings")]
    [SerializeField] private InputAction _input;
    [SerializeField] private InputAction jumpAction;
    [SerializeField] private InputAction jumpReleaseAction;
    [SerializeField] private InputAction dodgeAction;

    [Header("Jump Settings")]
    [SerializeField] private float _jumpForce = 12f;
    [SerializeField][Range(0.0f, 1.0f)] private float _jumpCutMultiplier;
    [SerializeField] private float _jumpBufferTime;
    [SerializeField] private float _jumpCoyoteTime;
    [SerializeField] private float _fallGravityMultiplier;

    [Header("Dodge Settings")]
    [SerializeField] private float _dodgeForce;
    [SerializeField] private float _imunityTime;

    private Collider2D playerCollider;
    private float lastGroundedTime;
    private float lastJumpTime;
    private bool isJumping;
    private float gravityScale;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        gravityScale = rb.gravityScale;
        playerCollider = GetComponent<CapsuleCollider2D>();

        jumpAction.performed += ctx => OnJump();
        jumpReleaseAction.performed += ctx => OnJumpUp();
        dodgeAction.performed += ctx => Dodge();
    }

    // Update is called once per frame
    protected override void Update()
    {
        playerDir = _input.ReadValue<Vector2>();
        ComputeGroundState();

        #region Timer and Resets
        lastGroundedTime -= Time.deltaTime;
        lastJumpTime -= Time.deltaTime;

        if (rb.linearVelocityY == 0) isJumping = false;
        #endregion

        base.Update();
    }

    protected override void FixedUpdate()
    {
        #region Jump
        // Handle jump cooldown and jump execution
        if (lastGroundedTime > 0 && lastJumpTime > 0 && !isJumping)
        {
            Jump();
        }

        // Handle gravity scaling
        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = gravityScale * _fallGravityMultiplier;
        }
        else
        {
            rb.gravityScale = gravityScale;
        }
        #endregion

        base.FixedUpdate();
    }

    private void Jump()
    {
        //apply force, using impluse force mode
        rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        lastGroundedTime = 0;
        lastJumpTime = 0;
        isJumping = true;
        Debug.Log(isJumping);
    }

    public void Dodge()
    {
        Debug.Log("hey listen");
        playerCollider.enabled = false;
        rb.AddForce(Vector2.right * GetDirection() * _dodgeForce, ForceMode2D.Impulse);
        StartCoroutine(OnDodge());
    }

    private IEnumerator OnDodge()
    {
        yield return new WaitForSeconds(_imunityTime);

        playerCollider.enabled = true;
    }

    public void OnJump() => lastJumpTime = _jumpBufferTime;

    public void OnJumpUp()
    {
        if (rb.linearVelocityY > 0 && isJumping)
        {
            //reduce current y velocity by amount (0 to 1)
            rb.AddForce(Vector2.down * rb.linearVelocityY * (1 - _jumpCutMultiplier), ForceMode2D.Impulse);
        }
        lastJumpTime = 0;
    }

    private void OnEnable()
    {
        _input.Enable();
        jumpAction.Enable();
        jumpReleaseAction.Enable();
        dodgeAction.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
        jumpAction.Disable();
        jumpReleaseAction.Disable();
        dodgeAction.Disable();
    }

    protected override float GetDirection()
    {
        return playerDir.x;
    }

    protected override void ComputeGroundState()
    {
        base.ComputeGroundState();

        if (isGrounded)
        {
            lastGroundedTime = _jumpCoyoteTime;
        }
    }
}