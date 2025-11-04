using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Character
{
    [Header("Input Settings")]
    [SerializeField]
    private string _inputMove = "Move";
    private InputAction _inputMovement;
    [SerializeField] private float _dodgeForce;

    [SerializeField] private string _sprint = "Sprint";
    [SerializeField] private string _idleAnimation = "IdleAnim";
     private InputAction _inputDash;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        _inputDash = InputSystem.actions.FindAction(_sprint);
        _inputMovement = InputSystem.actions.FindAction(_inputMove);
    }

    // Update is called once per frame
    protected override void Update()
    {
        playerDir = _inputMovement.ReadValue<Vector2>();
        ComputeGroundState();

        if (_inputDash.WasPressedThisFrame())
        {
            Dodge();
        }

        base.Update();

        _anim.SetFloat(_idleAnimation,rb.linearVelocityX);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override float GetDirection()
    {
        return playerDir.x;
    }

    protected override void ComputeGroundState()
    {
        base.ComputeGroundState();
    }
    private void Dodge()
    {
        Debug.Log("clean");
        rb.AddForce(Vector2.right * GetDirection() * _dodgeForce, ForceMode2D.Impulse);
    }


}