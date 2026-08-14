using System.Collections;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private Hashtable Collected = new Hashtable();
    public void CountCollectables(CollectableType type)
    {
        int lastValue = 0;
        
        if (Collected.Contains(type))
        {
            lastValue = (int)Collected[type];
        }
        Collected[type] = lastValue + 1;
    }
}
