using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGlowToPage : MonoBehaviour
{
	[SerializeField] Material glowMaterial;

	private FloatTween glowTween = new FloatTween();

	public void AddGlow()
	{
		glowTween.OnChange += ChangeGlowTween;
		StartCoroutine(GlowIn(2f));
		
	}

	IEnumerator GlowIn(float fadeTime)
	{
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
		Main.Instance.gameState.contentManager.paperImage.material = null;
	}
}
