using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanityFader : MonoBehaviour
{
	private FloatTween alphaTween = new FloatTween();

	[SerializeField]
	Image targetImage;

	public void StartFadeIn()
	{
		alphaTween.OnChange += ChangeAlphaTween;
		Debug.Log("Fade in " + name);
		targetImage.gameObject.SetActive(true);
		StartCoroutine(FadeIn(2f));
	}

	void ChangeAlphaTween(float currentValue)
	{
		targetImage.color = new Color(targetImage.color.r, targetImage.color.g, targetImage.color.b, currentValue);
	}

	IEnumerator FadeIn(float fadeTime)
	{
		alphaTween.Tween(0, 1, fadeTime);
		while (alphaTween.tweening)
		{
			Debug.Log("fading");
			if (alphaTween.tweening)
				alphaTween.Loop();
			yield return null;
		}
		Debug.Log("fade done");
	}

	public void StartFadeOut()
	{
		StartCoroutine(FlickerOut(2f));
	}

	IEnumerator FlickerOut(float fadeTime)
	{
		targetImage.gameObject.SetActive(false);
		yield return new WaitForSeconds(0.2f);
		targetImage.gameObject.SetActive(true);
		yield return new WaitForSeconds(0.2f);
		targetImage.gameObject.SetActive(false);
	}
}
