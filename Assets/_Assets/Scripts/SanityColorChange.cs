using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanityColorChange : SanityEffect
{
	ColorTween colorTween = new ColorTween();

	Image image;

	[SerializeField] Color effectColor;

	Color startColor;

	public override void ApplyEffect()
	{
		StartCoroutine(ChangeColor(2.0f, effectColor));
	}

	public override void RemoveEffect()
	{
		StartCoroutine(ChangeColor(2.0f, startColor));
	}

	void Awake ()
	{
		image = GetComponent<Image>();
		startColor = image.color;
		colorTween.OnChange += OnChangeColor;
	}

	protected virtual void OnChangeColor(Color currentValue)
	{
		image.color = currentValue;
	}

	private IEnumerator ChangeColor(float fadeTime, Color goalColor)
	{
		colorTween.Tween(new Color(image.color.r, image.color.g, image.color.b, 1), goalColor, fadeTime);
		while (colorTween.tweening)
		{
			colorTween.Loop();
			yield return null;
		}
	}
}
