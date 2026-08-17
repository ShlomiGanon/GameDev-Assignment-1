using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private const float SwitchTargetThreshold = 0.01f;

    [SerializeField] private Transform topPoint;
    [SerializeField] private Transform bottomPoint;
    [SerializeField] private float speed = 2f;

    private Rigidbody2D rb;
    private Transform target;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = topPoint;
    }

    private void FixedUpdate()
    {
        Vector2 newPosition = Vector2.MoveTowards(
            rb.position,
            target.position,
            speed * Time.fixedDeltaTime
        );

        rb.MovePosition(newPosition);

        if (Vector2.Distance(rb.position, target.position) < SwitchTargetThreshold)
        {
            target = target == topPoint ? bottomPoint : topPoint;
        }
    }
}