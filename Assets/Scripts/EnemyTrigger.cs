using UnityEngine;

public class EnemyTriger : MonoBehaviour
{
    [SerializeField] private EnemyController enemy_to_trig;

    private void Start()
    {
        enemy_to_trig.StopEngage();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy_to_trig.StartEngage();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy_to_trig.StopEngage();
        }
    }
}
