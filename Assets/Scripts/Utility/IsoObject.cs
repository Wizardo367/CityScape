using UnityEngine;

public class IsoObject : MonoBehaviour
{
    // Depth sorting
    void LateUpdate()
    {
        Isometric.DepthSort(gameObject);
    }
}