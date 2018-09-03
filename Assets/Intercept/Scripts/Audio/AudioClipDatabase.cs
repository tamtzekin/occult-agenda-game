using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioClipDatabase : MonoSingleton<AudioClipDatabase> {
	public List<AudioClip> keySounds;
	[SerializeField] List<AudioClip> spookyKeySounds;

	public AudioClip attachingPaperSound;
	public AudioClip horrorSting;

	public AudioClip sanityIncreaseAudio;

	public AudioClip sanityDecreaseAudio;

	public float keyboardVolume = 1;

	public bool spooky = false;

	public void PlayKeySound () {
		if(!spooky)
			PlaySound(keySounds[Random.Range(0, keySounds.Count)], keyboardVolume);
		else
			PlaySound(spookyKeySounds[Random.Range(0, spookyKeySounds.Count)], keyboardVolume);
	}

	public void PlaySpookyKeySound()
	{
		PlaySound(keySounds[Random.Range(0, spookyKeySounds.Count)], keyboardVolume);
	}

	public void PlayAttachingPaperSound () {
		PlaySound(attachingPaperSound);
	}

	public void PlayHorrorSting () {
		PlaySound(horrorSting);
	}

	public void PlaySanityIncrease()
	{
		PlaySound(sanityIncreaseAudio);
	}

	public void PlaySanityDecrease()
	{
		PlaySound(sanityDecreaseAudio);
	}

	private void PlaySound (AudioClip audioClip, float volume = 1) {
		GameObject tempGO = new GameObject("Audio: "+audioClip.name); // create the temp object
		tempGO.transform.SetParent(transform);
		AudioSource audioSource = tempGO.AddComponent<AudioSource>(); // add an audio source
		audioSource.clip = audioClip; // define the clip
		audioSource.spatialBlend = 0;
		audioSource.Play(); // start the sound
		Destroy(tempGO, audioClip.length); // destroy object after clip duration
	}
}
