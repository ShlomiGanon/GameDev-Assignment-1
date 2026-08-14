using UnityEngine;


public enum CollectableType
{
    Coin,
    Dimond,
    Star
}

public class Collectable : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private CollectableType collectableType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (scoreManager)
            {
                scoreManager.CountCollectables(this.collectableType);
            }
            Destroy(gameObject);
        }
    }

}
