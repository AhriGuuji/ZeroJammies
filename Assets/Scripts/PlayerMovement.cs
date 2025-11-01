using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Character
{
    [Header("Input Settings")]
    [SerializeField]
    private string _inputMove = "Move";
    private InputAction _inputMovement;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        _inputMovement = InputSystem.actions.FindAction(_inputMove);
    }

    // Update is called once per frame
    protected override void Update()
    {
        playerDir = _inputMovement.ReadValue<Vector2>();
        ComputeGroundState();

        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    /*private void OnEnable()
    {
        _inputMovement.Enable();
    }

    private void OnDisable()
    {
        _inputMovement.Disable();
    }*/

    protected override float GetDirection()
    {
        return playerDir.x;
    }

    protected override void ComputeGroundState()
    {
        base.ComputeGroundState();
    }
}