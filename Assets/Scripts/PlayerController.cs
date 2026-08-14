using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement pm;
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        if (!pm) Debug.LogError("can't find PlayerMovement script!");
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

}
