using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    private PlayerController _playerController;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    // StringToHash to prevent GC allocations every frame
    private readonly int SpeedHash = Animator.StringToHash("Speed");
    private readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");
    private readonly int VerticalVelocityHash = Animator.StringToHash("VerticalVelocity");
    private readonly int AttackHash = Animator.StringToHash("IsAttacking");

    private void Awake()
    {
        _playerController = GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        float speed = Mathf.Abs(_playerController.linearVelocity.x);
        animator.SetFloat(SpeedHash, speed);

        animator.SetBool(IsGroundedHash, _playerController.IsGrounded);

        animator.SetFloat(VerticalVelocityHash, _playerController.linearVelocity.y);

        HandleSpriteFlip();
    }

    private void HandleSpriteFlip()
    {
        if (_playerController.linearVelocity.x != 0)
        {
            _spriteRenderer.flipX = _playerController.linearVelocity.x < 0;
        }
    }

    private void OnEnable()
    {
        _playerController.OnAttack += HandleAttack;
    }

    private void OnDisable()
    {
        _playerController.OnAttack -= HandleAttack;
    }

    private void HandleAttack()
    {
        animator.SetTrigger(AttackHash);
    }
}
