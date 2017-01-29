using UnityEngine;
using UnityEngine.EventSystems;

public class Marker : MonoBehaviour
{
    public GameObject markerObj;

    public void OnClick()
    {
        // Spawn marker at mouse location
        Instantiate(markerObj, Vector3.zero, Quaternion.identity);
    }
}