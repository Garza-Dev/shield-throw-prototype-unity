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
    [Tooltip("The immediate velocity applied when jumping")] public float _jumpPower = 36f;

    private Rigidbody2D _rb;
    private float _moveInput;
    public float MoveInput => _moveInput; // Expose moveInput for PlayerAnimation

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
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
        if (context.started)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpPower);
        }
    }
}
