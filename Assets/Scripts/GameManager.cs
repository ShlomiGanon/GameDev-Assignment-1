using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Hashtable Collected = new Hashtable();
    [SerializeField] private GameObject Player;
    [SerializeField] private Vector2 Death_Force = new(0f, 5f);
    [SerializeField] private float Death_Torque = 20f;
    [SerializeField] private float AfterDeathForceSeconds = 4f;
    public void CountCollectables(CollectableType type)
    {
        int lastValue = 0;
        
        if (Collected.Contains(type))
        {
            lastValue = (int)Collected[type];
        }
        Collected[type] = lastValue + 1;
    }


    public IEnumerator GameEnd()
    {

        if (Player != null)
        {
            PlayerInput playerInput = Player.GetComponent<PlayerInput>();
            if (playerInput != null)
            {
                playerInput.enabled = false;
            }

            SpriteRenderer spriteRenderer = Player.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                Color newColor = spriteRenderer.color;
                newColor.a = 0.5f;
                spriteRenderer.color = newColor;
            }
            Rigidbody2D PlayerRb = Player.GetComponent<Rigidbody2D>();
            if(PlayerRb != null)
            {
                PlayerRb.AddForce(Death_Force);
                PlayerRb.freezeRotation = false;
                PlayerRb.AddTorque(Death_Torque, ForceMode2D.Impulse);
            }
        }
        yield return new WaitForSeconds(AfterDeathForceSeconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
