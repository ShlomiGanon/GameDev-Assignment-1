using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement pm;
    private GameManager gameManager;
    private bool isAlive = true;
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        if (!pm) Debug.LogError("can't find PlayerMovement script!");
        gameManager = FindFirstObjectByType<GameManager>();
        if (!gameManager) Debug.LogError("can't find GameManager script!");
    }
    public bool IsAlive()
    {
        return isAlive;
    }

    void OnMove(InputValue value)
    {
        float XValue = value.Get<Vector2>().x;
        pm.HandleHorizontalMove(XValue);
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            pm.HandleJump();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Trap"))
        {
            isAlive = false;
            TriggerGameOver();
        }
    }

    void TriggerGameOver()
    {
        if (gameManager != null)
        {
            gameManager.StartCoroutine(gameManager.GameEnd());
        }
        else
        {
            Debug.LogError("cant end the game , can't find GameManager script");
        }
    }

}
