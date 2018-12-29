using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BloomBlowOut : MonoBehaviour
{
	private FloatTween bloomTween = new FloatTween();

	[SerializeField] PostProcessVolume postProcessVolume;

	Bloom bloom;

	float blowOut;
	float normal;

	// Use this for initialization
	void Start ()
	{
		PostProcessProfile profile = postProcessVolume.profile;
		bloom = profile.GetSetting<Bloom>();
		normal = bloom.intensity.value;

		bloomTween.OnChange += ChangeBloomTween;
	}

	public void TriggerBloomOut(float blowOutAmount)
	{
		blowOut = blowOutAmount;
		StartCoroutine(BloomOut(1.5f));
	}

	IEnumerator BloomOut(float fadeTime)
	{
		bloomTween.Tween(0f, 1f, fadeTime);
		while (bloomTween.tweening)
		{
			if (bloomTween.tweening)
				bloomTween.Loop();
			yield return null;
		}
	}

	void ChangeBloomTween(float currentValue)
	{
		Debug.Log("bloom " + currentValue);
		bloom.intensity.Interp(blowOut, normal, currentValue);
	}
}
