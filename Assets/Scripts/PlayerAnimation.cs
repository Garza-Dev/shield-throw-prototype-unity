using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private PlayerController _player;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _player = GetComponentInParent<PlayerController>();
        _rb = _player.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleSpriteFlip();
        HandleSpeed();
    }

    private void HandleSpriteFlip()
    {
        if (_player.MoveInput != 0)
        {
            _spriteRenderer.flipX = _player.MoveInput < 0;
        }
    }

    private void HandleSpeed()
    {
        _animator.SetFloat("Speed", Mathf.Abs(_rb.linearVelocity.x)); // Parameter for Player_Idle -> Player_Run transition
    }
}
