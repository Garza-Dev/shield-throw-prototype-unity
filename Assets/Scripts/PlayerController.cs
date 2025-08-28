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
    [SerializeField] private float moveSpeed = 6.2f;

    private Rigidbody2D rb;
    private float moveInput;
    public float MoveInput => moveInput; // Expose moveInput for PlayerAnimation

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        moveInput = input.x;
    }
}
