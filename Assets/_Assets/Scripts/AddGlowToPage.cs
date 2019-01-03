using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class AddGlowToPage : MonoBehaviour
{
	[SerializeField] Material glowMaterial;

	private FloatTween glowTween = new FloatTween();
	private FloatTween bloomTween = new FloatTween();

	[SerializeField] PostProcessVolume postProcessVolume;

	Bloom bloom;

	float startIntensity;

	void Start()
	{
		PostProcessProfile profile = postProcessVolume.profile;
		bloom = profile.GetSetting<Bloom>();
		startIntensity = bloom.intensity.value;
		bloomTween.OnChange += ChangeBloomTween;

		glowTween.OnChange += ChangeGlowTween;
	}

	public void AddGlow()
	{
		StartCoroutine(GlowIn(2f));
		
	}

	IEnumerator GlowIn(float fadeTime)
	{
		Main.Instance.gameState.contentManager.shadowImage.gameObject.SetActive(false);
		Main.Instance.gameState.contentManager.paperImage.material = new Material(glowMaterial);
		glowTween.Tween(2.2f, 1.2f, fadeTime);
		while (glowTween.tweening)
		{
			if (glowTween.tweening)
				glowTween.Loop();
			yield return null;
		}
	}

	void ChangeGlowTween(float currentValue)
	{
		Main.Instance.gameState.contentManager.paperImage.material.SetFloat("_OutlineSize", currentValue);
	}

	public void RemoveGlow()
	{
		// fade bloom intensity down then remove
		// not down to zero or the effect isn't there - maybe 0.4?
		StartCoroutine(BloomDown(2f));
	}

	IEnumerator BloomDown(float fadeTime)
	{
		bloomTween.Tween(0f, 1.0f, fadeTime);
		while (bloomTween.tweening)
		{
			if (bloomTween.tweening)
				bloomTween.Loop();
			yield return null;
		}
		Main.Instance.gameState.contentManager.paperImage.material = null;
		Main.Instance.gameState.contentManager.shadowImage.gameObject.SetActive(true);
		bloom.intensity.value = startIntensity;
	}

	void ChangeBloomTween(float currentValue)
	{
		bloom.intensity.Interp(startIntensity, 0.4f, currentValue);
	}
}
