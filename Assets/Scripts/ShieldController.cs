using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float throwSpeed = 10f;
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private Transform player; // Assign in Inspector

    private bool isThrown = false;
    private Rigidbody2D rb;
    private Vector2 throwStartPosition;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Ensure gravity is off at start; shield is kinematic while attached to player
        rb.gravityScale = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void FixedUpdate()
    {
        if (isThrown)
        {
            // Check distance from throw start position
            float distance = Vector2.Distance(rb.position, throwStartPosition);

            if (distance > maxDistance)
            {
                // Stop manual movement & let gravity take over
                isThrown = false;
                rb.gravityScale = 1f;
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
            else
            {
                // Move forward along X using MovePosition for physics safety
                rb.MovePosition(rb.position + new Vector2(throwSpeed * Time.deltaTime, 0f));
            }
        }
    }

    public void Throw()
    {
        if (!isThrown)
        {
            isThrown = true;
            rb.gravityScale = 0f;
            rb.bodyType = RigidbodyType2D.Kinematic; // manual control while flying
            throwStartPosition = player.position;

            // Detach from player so it can move independently
            transform.parent = null;
        }
    }
}
