using UnityEngine;

public enum CollectableType
{
    Coin,
    Diamond,
    Star
}

public class Collectable : MonoBehaviour
{
    [SerializeField] private CollectableType collectableType;

    private void FixedUpdate()
    {
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.y += 1f;
        transform.eulerAngles = currentRotation;
    }

    public CollectableType GetCollectableType()
    {
        return collectableType;
    }
}