using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform platform;
    [SerializeField] private Transform topPoint;
    [SerializeField] private Transform BottomPoint;
    [SerializeField] private float speed = 2f;

    private Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = topPoint;
    }

    private void FixedUpdate()
    {
        platform.position = Vector3.MoveTowards(
            platform.position,
            target.position,
            speed * Time.deltaTime
            );
        if (Vector3.Distance(platform.position, target.position ) < 0.01f)
        {
            target = target == topPoint ? BottomPoint : topPoint;
        }
    }
}
