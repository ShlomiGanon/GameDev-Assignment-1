using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterMovement characterMovement;
    private GameManager gameManager;
    private bool isAlive = true;

    private void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();

        if (!characterMovement)
        {
            Debug.LogError("can't find CharacterMovement script!");
        }

        gameManager = FindFirstObjectByType<GameManager>();

        if (!gameManager)
        {
            Debug.LogError("can't find GameManager script!");
        }
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    private void OnMove(InputValue value)
    {
        float horizontalDirection = value.Get<Vector2>().x;
        characterMovement.HandleHorizontalMove(horizontalDirection);
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            characterMovement.HandleJump();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isAlive)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Trap"))
        {
            isAlive = false;
            TriggerGameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAlive)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Collectable"))
        {
            Collectable collectable = collision.gameObject.GetComponent<Collectable>();

            if (collectable != null && gameManager != null)
            {
                CollectableType type = collectable.GetCollectableType();
                gameManager.CountCollectables(type);
            }

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("FinishLine"))
        {
            TriggerGameOver(true);
        }
    }

    private void TriggerGameOver(bool finishedSuccessfully = false)
    {
        if (gameManager != null)
        {
            gameManager.StartCoroutine(gameManager.GameEnd(finishedSuccessfully));
        }
        else
        {
            Debug.LogError("cant end the game , can't find GameManager script");
        }
    }
}