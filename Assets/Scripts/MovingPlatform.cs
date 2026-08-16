using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform topPoint;
    [SerializeField] private Transform bottomPoint;
    [SerializeField] private float speed = 2f;

    private Rigidbody2D rb;
    private Transform target;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
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

        if (Vector2.Distance(rb.position, target.position) < 0.01f)
        {
            target = target == topPoint ? bottomPoint : topPoint;
        }
    }

}