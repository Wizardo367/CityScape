using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to switch between and play different audio tracks.
/// </summary>
public class AudioRandomiser : MonoBehaviour
{
	/// <summary>
	/// The sounds to play.
	/// </summary>
	public List<AudioClip> Sounds;

	/// <summary>
	/// Updates this instance.
	/// </summary>
	void Update()
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();

        // Check if a sound is already playing
        if (audioSource.isPlaying) return;

        // Select random audioclip
        audioSource.clip = Sounds[Random.Range(0, Sounds.Count)];
        // Play audio
        audioSource.Play();
    }
}
