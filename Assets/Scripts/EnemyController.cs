using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private CharacterMovement characterMovement;
    private bool isEngaged;

    public void StartEngage()
    {
        isEngaged = true;
    }

    public void StopEngage()
    {
        isEngaged = false;
    }

    private void FixedUpdate()
    {
        if (!player.GetComponent<PlayerController>().IsAlive())
        {
            characterMovement.HandleHorizontalMove(0f);
            return;
        }

        Vector2 moveDirection = new Vector2(0f, 0f);

        if (isEngaged)
        {
            float playerX = player.transform.position.x;
            float currentEnemyX = transform.position.x;

            if (playerX > currentEnemyX) // The player is to the right of the enemy.
            {
                moveDirection.x = 1;
            }
            else if (playerX < currentEnemyX) // The player is to the left of the enemy.
            {
                moveDirection.x = -1;
            }
            else // The player and the enemy are at the same position.
            {
                moveDirection.x = 0;
            }
        }
        else
        {
            moveDirection.x = 0;
        }

        characterMovement.HandleHorizontalMove(moveDirection.x);
    }
}