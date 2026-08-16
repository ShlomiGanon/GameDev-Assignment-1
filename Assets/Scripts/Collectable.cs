using UnityEngine;


public enum CollectableType
{
    Coin,
    Diamond,
    Star
}

public class Collectable : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private CollectableType collectableType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameManager)
            {
                gameManager.CountCollectables(this.collectableType);
            }
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.y += 1;
        transform.eulerAngles = currentRotation;
    }
}
