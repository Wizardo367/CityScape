using System.Collections.Generic;
using UnityEngine;

public class AudioRandomiser : MonoBehaviour
{
    public List<AudioClip> Sounds;

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
