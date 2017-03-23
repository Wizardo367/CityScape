using UnityEngine;

/// <summary>
/// Adjusts the volume of an audio source depending on the main camera's zoom level.
/// </summary>
public class AudioZoom : MonoBehaviour
{
	/// <summary>
	/// The maximum zoom level.
	/// </summary>
	public int MaxZoom;

	/// <summary>
	/// Updates this instance.
	/// </summary>
	private void Update()
    {
        // Use the size of the mainCamera or "zoom" to determine the volume of the audio source
        AudioSource source = gameObject.GetComponent<AudioSource>();
        source.volume = 1.1f - Camera.main.orthographicSize / MaxZoom;
    }
}
