using UnityEngine;

public class AudioZoom : MonoBehaviour
{
    public int MaxZoom;

    void Update()
    {
        // Use the size of the mainCamera or "zoom" to determine the volume of the audio source
        AudioSource source = gameObject.GetComponent<AudioSource>();
        source.volume = 1.1f - Camera.main.orthographicSize / MaxZoom;
    }
}
