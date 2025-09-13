using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    private PlayerController rb;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    // StringToHash to prevent GC allocations every frame
    private readonly int SpeedHash = Animator.StringToHash("Speed");
    private readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");
    private readonly int VerticalVelocityHash = Animator.StringToHash("VerticalVelocity");

    private void Awake()
    {
        rb = GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        float speed = Mathf.Abs(rb.linearVelocity.x);
        animator.SetFloat(SpeedHash, speed);

        animator.SetBool(IsGroundedHash, rb.IsGrounded);

        animator.SetFloat(VerticalVelocityHash, rb.linearVelocity.y);

        HandleSpriteFlip();
    }

    private void HandleSpriteFlip()
    {
        if (rb.linearVelocity.x != 0)
        {
            _spriteRenderer.flipX = rb.linearVelocity.x < 0;
        }
    }
}
