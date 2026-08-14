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

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scoreManager.CountCollectables(this.collectableType);
            Destroy(gameObject);
        }
    }

}
