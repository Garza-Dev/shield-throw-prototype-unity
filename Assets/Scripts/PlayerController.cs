/*
 *  PlayerController.cs
 *  Description: Temporary player controller for prototype.
 *  Created By: Eduardo Jr Garza
 */

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovmentStats _moveStats;
    private Vector2 _frameVelocity;




    /* Everything above this is the start of the upgraded movement */

    [Header("Jump")]
    [Tooltip("The immediate velocity applied when jumping")] 
    public float _jumpPower = 4f;

    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody2D _rb;
    private float _moveInput;
    public Vector2 linearVelocity => _rb.linearVelocity; // Expose _rb.linearVelocity for PlayerAnimation
    public bool IsGrounded { get; private set; }

    private float _time;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        IsGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);

        _time += Time.deltaTime;
    }

    void FixedUpdate()
    {
        HandleJump();
        HandleMovement();
        HandleGravity();

        ApplyMovement();
    }

    private void HandleMovement()
    {
        if (_moveInput == 0)
        {
            // Decelerate
            var decel = IsGrounded ? _moveStats.GroundDeceleration : _moveStats.AirDeceleration;
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, decel * Time.fixedDeltaTime);
        }
        else
        {
            // Accelerate toward target speed
            float targetSpeed = _moveInput * _moveStats.MaxSpeed;
            _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, targetSpeed, _moveStats.Acceleration * Time.fixedDeltaTime);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        _moveInput = input.x;
    }

    private void ApplyMovement() => _rb.linearVelocity = _frameVelocity;

    #region Jumping

    private bool _jumpToConsume;
    private bool _bufferedJumpUsable;
    private bool _endedJumpEarly;
    private bool _coyoteUsable;
    private float _timeJumpWasPressed;

    private void HandleJump()
    {
        if (_jumpToConsume && IsGrounded)
        {
            ExecuteJump();
            _jumpToConsume = false;
        }
    }

    private void ExecuteJump()
    {
        _endedJumpEarly = false;

        // Apply jump force
        _frameVelocity.y = _moveStats.JumpPower;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // Player pressed jump
            _jumpToConsume = true;
        }
    }

    #endregion

    #region Apply Gravity

    private void HandleGravity()
    {
        if (IsGrounded && _frameVelocity.y <= 0f)
        {
            _frameVelocity.y = _moveStats.GroundingForce;
        }
        else
        {
            var inAirGravity = _moveStats.FallAcceleration;
            if (_endedJumpEarly && _frameVelocity.y > 0) inAirGravity *= _moveStats.JumpEndEarlyGravityModifier;
            _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, -_moveStats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
        }
    }

    #endregion
}
