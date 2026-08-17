using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (!rb)
        {
            Debug.LogError("the script can't find any Rigidbody2D!");
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, rb.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void HandleHorizontalMove(float horizontalDirection)
    {
        moveDirection = new Vector2(horizontalDirection, 0f);
    }

    public void HandleJump()
    {
        if (!isGrounded)
        {
            return;
        }

        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }
}