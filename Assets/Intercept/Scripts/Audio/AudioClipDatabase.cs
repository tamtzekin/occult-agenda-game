using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioClipDatabase : MonoSingleton<AudioClipDatabase> {
	public List<AudioClip> keySounds;
	public AudioClip attachingPaperSound;
	public AudioClip horrorSting;

	public AudioClip sanityIncreaseAudio;

	public AudioClip sanityDecreaseAudio;

	public float keyboardVolume = 1;

	public void PlayKeySound () {
		PlaySound(keySounds[Random.Range(0, keySounds.Count)], keyboardVolume);
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
