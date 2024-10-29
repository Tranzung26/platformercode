using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // tao bien luu tru audioSource
    public AudioSource musicAudioSource;
	public AudioSource sfxAudioSource;

	// tao bien luu tru audio Clip
	public AudioClip musicClip;
	public AudioClip coinClip;
	public AudioClip winClip;
	// Start is called before the first frame update
	void Start()
    {
        musicAudioSource.clip = musicClip;
		musicAudioSource.Play();
    }

	public void PlaySFX(AudioClip sfxClip)
	{
		sfxAudioSource.clip = sfxClip;
		sfxAudioSource.PlayOneShot(sfxClip);
	}

}
