using UnityEngine;

public class HappinessBooster : MonoBehaviour
{
    [Tooltip("Amount to boost happiness by.")]
    public int Boost;

    public void Set(int amount)
    {
        Boost = amount;
    }
}