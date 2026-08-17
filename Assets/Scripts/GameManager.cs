using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;


public class GameManager : MonoBehaviour
{
    private Hashtable Collected = new Hashtable();
    [SerializeField] private CinemachineCamera cinemachineCamera;
    [SerializeField] private GameObject Player;
    [SerializeField] private Vector2 Death_Force = new(0f, 5f);
    [SerializeField] private float Death_Torque = 20f;
    [SerializeField] private float AfterDeathForceSeconds = 4f;
    [SerializeField] private Color PlayerFinishedSuccessfullyColor;
    public void CountCollectables(CollectableType type)
    {
        int lastValue = 0;
        
        if (Collected.Contains(type))
        {
            lastValue = (int)Collected[type];
        }
        Collected[type] = lastValue + 1;
    }


    public IEnumerator GameEnd(bool finishedSuccessfully = false)
    {
        if (cinemachineCamera != null)
        {
            cinemachineCamera.Follow = null;
        }


        if (Player != null)
        {
            PlayerInput playerInput = Player.GetComponent<PlayerInput>();
            if (playerInput != null)
            {
                playerInput.enabled = false;
            }

            SpriteRenderer PlayerSpriteRenderer = Player.GetComponent<SpriteRenderer>();
            if (PlayerSpriteRenderer != null)
            {
                Color currentColor = PlayerSpriteRenderer.color;
                if (!finishedSuccessfully)
                {

                    currentColor.a = 0.5f;
                    PlayerSpriteRenderer.color = currentColor;

                    Rigidbody2D PlayerRb = Player.GetComponent<Rigidbody2D>();
                    if (PlayerRb != null)
                    {
                        PlayerRb.AddForce(Death_Force);
                        PlayerRb.freezeRotation = false;
                        PlayerRb.AddTorque(Death_Torque, ForceMode2D.Impulse);
                    }
                }
                else
                {
                    PlayerSpriteRenderer.color = PlayerFinishedSuccessfullyColor;
                }
            }
        }
        yield return new WaitForSeconds(AfterDeathForceSeconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
