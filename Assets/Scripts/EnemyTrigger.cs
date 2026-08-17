using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private EnemyController enemyToTrigger;

    private void Start()
    {
        enemyToTrigger.StopEngage();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemyToTrigger.StartEngage();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemyToTrigger.StopEngage();
        }
    }
}