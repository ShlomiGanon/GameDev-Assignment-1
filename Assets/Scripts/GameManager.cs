using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cinemachineCamera;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector2 deathForce = new(0f, 5f);
    [SerializeField] private float deathTorque = 20f;
    [SerializeField] private float sceneReloadDelay = 4f;
    [SerializeField] private Color winPlayerColor;

    private readonly Hashtable collected = new();

    public void CountCollectables(CollectableType type)
    {
        int lastValue = 0;

        if (collected.Contains(type))
        {
            lastValue = (int)collected[type];
        }

        collected[type] = lastValue + 1;
    }

    public IEnumerator GameEnd(bool finishedSuccessfully = false)
    {
        //--- summary print
        string summaryLog = $"End Game Status: {(finishedSuccessfully ? "Success" : "Failed")}\n";

        foreach (CollectableType type in Enum.GetValues(typeof(CollectableType)))
        {
            if (collected.ContainsKey(type))
            {
                int count = (int)collected[type];
                summaryLog += $"collected {count} of {type}\n";
            }
            else
            {
                summaryLog += $"not collected {type}\n";
            }
        }

        Debug.Log(summaryLog);
        //---

        if (cinemachineCamera != null)
        {
            cinemachineCamera.Follow = null;
        }

        if (player != null)
        {
            PlayerInput playerInput = player.GetComponent<PlayerInput>();

            if (playerInput != null)
            {
                playerInput.enabled = false;
            }

            SpriteRenderer playerSpriteRenderer = player.GetComponent<SpriteRenderer>();

            if (playerSpriteRenderer != null)
            {
                Color currentColor = playerSpriteRenderer.color;

                if (!finishedSuccessfully)
                {
                    currentColor.a = 0.5f;
                    playerSpriteRenderer.color = currentColor;

                    Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();

                    if (playerRb != null)
                    {
                        playerRb.AddForce(deathForce);
                        playerRb.freezeRotation = false;
                        playerRb.AddTorque(deathTorque, ForceMode2D.Impulse);
                    }
                }
                else
                {
                    playerSpriteRenderer.color = winPlayerColor;
                }
            }
        }
        yield return new WaitForSeconds(sceneReloadDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}