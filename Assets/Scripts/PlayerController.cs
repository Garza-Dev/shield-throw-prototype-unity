/*
 *  PlayerController.cs
 *  Description: Temporary player controller for prototype.
 *  Created By: Eduardo Jr Garza
 */

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 12f;

    [Header("Jump")]
    [Tooltip("The immediate velocity applied when jumping")] 
    public float _jumpPower = 4f;

    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody2D _rb;
    private float _moveInput;
    public float MoveInput => _moveInput; // Expose _moveInput for PlayerAnimation
    public float VerticalVelocity => _rb.linearVelocity.y; // Expose _rb.linearVelocity.y for PlayerAnimation
    public bool IsGrounded { get; private set; }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        IsGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
    }

    void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_moveInput * _moveSpeed, _rb.linearVelocity.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        _moveInput = input.x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started && IsGrounded)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpPower);
        }
    }

    public void DoJump()
    {
        
    }
}
