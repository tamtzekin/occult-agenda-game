using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanityFader : MonoBehaviour
{
	private FloatTween alphaTween = new FloatTween();

	Image image;

	void Awake()
	{
		image = GetComponent<Image>();
	}

	public void StartFadeIn()
	{
		alphaTween.OnChange += ChangeAlphaTween;
		Debug.Log("Fade in " + name);
		gameObject.SetActive(true);
		StartCoroutine(FadeIn(2f));
	}

	void ChangeAlphaTween(float currentValue)
	{
		image.color = new Color(image.color.r, image.color.g, image.color.b, currentValue);
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
}
