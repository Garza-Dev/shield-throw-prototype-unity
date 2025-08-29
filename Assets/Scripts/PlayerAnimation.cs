using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private PlayerController _player;

    private bool wasGoingUp;

    private void Awake()
    {
        _player = GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        HandleSpriteFlip();
        HandleMovementAnimations();
        HandleJumpAnimations();
    }

    private void HandleSpriteFlip()
    {
        if (_player.MoveInput != 0)
        {
            _spriteRenderer.flipX = _player.MoveInput < 0;
        }
    }

    private void HandleMovementAnimations()
    {
        _animator.SetFloat("Speed", Mathf.Abs(_player.MoveInput));
    }

    private void HandleJumpAnimations()
    {
        _animator.SetBool("IsGrounded", _player.IsGrounded);
        _animator.SetFloat("VerticalVelocity", _player.VerticalVelocity);

        // Detect apex
        if (_player.VerticalVelocity > 0.1f)
        {
            wasGoingUp = true;
        }

        if (wasGoingUp && _player.VerticalVelocity <= 0.1f && !_player.IsGrounded)
        {
            _animator.SetTrigger("Apex"); // new trigger parameter
            wasGoingUp = false;
        }
    }

    public void TriggerJump()
    {
        _animator.SetTrigger("Jump");
    }

    public void PerformJump()
    {
        _player.DoJump();
    }
}
