﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntroState : MainState {

	public CanvasGroup group;
	public IntroTypedText inklePresentsText;
	public IntroTypedText theInterceptText;

	[SerializeField] Text beginText;
	[SerializeField] Text quitText;

	public override void Enter () {
		group.gameObject.SetActive(true);
		group.alpha = 1;
		group.interactable = true;
		group.blocksRaycasts = true;
		inklePresentsText.gameObject.SetActive(false);
		theInterceptText.gameObject.SetActive(false);
		group.gameObject.SetActive(true);
		beginText.gameObject.SetActive(false);
		quitText.gameObject.SetActive(false);
		base.Enter ();
	}

	public override void Exit () {
		group.interactable = false;
		group.blocksRaycasts = false;
//		group.gameObject.SetActive(false);
		base.Exit ();
	}

	public void PlayLongIntro () {
		StartCoroutine(DoLongIntro());
	}

	public void PlayShortIntro () {
		StartCoroutine(DoShortIntro());
	}

	// Type Inkle, type game title, and fade in from black
	private IEnumerator DoLongIntro () {
		yield return new WaitForSeconds(2f);

		/*
		inklePresentsText.gameObject.SetActive(true);
		while(inklePresentsText.typedText.typing) {
			yield return null;
		}
		yield return new WaitForSeconds(2f);
		inklePresentsText.gameObject.SetActive(false);

		yield return new WaitForSeconds(2);
		*/



		theInterceptText.gameObject.SetActive(true);
		while(theInterceptText.typedText.typing) {
			yield return null;
		}
		theInterceptText.gameObject.gameObject.GetComponent<ScalePulser>().enabled = true;

		AudioClipDatabase.Instance.PlayHorrorSting();
		yield return new WaitForSeconds(2.5f);
		//theInterceptText.gameObject.SetActive(false);

		//yield return new WaitForSeconds(0.5f);
		//yield return StartCoroutine(DoShortIntro());
		StartCoroutine(FadeButton(beginText, 1f, true));
		StartCoroutine(FadeButton(quitText, 1f, true));
	}

	public void Begin()
	{
		StartCoroutine(FadeButton(beginText, 1f, false));
		StartCoroutine(FadeButton(quitText, 1f, false));
		StartCoroutine(DoShortIntro());
	}

	public IEnumerator FadeButton(Text fadeText, float fadeTime, bool fadeIn)
	{
		FloatTween alphaTween = new FloatTween();
		Button button = fadeText.GetComponent<Button>();
		if (fadeIn)
		{
			alphaTween.Tween(0, 1, fadeTime);
			fadeText.gameObject.SetActive(true);
		}
		else
		{
			button.interactable = false;
			alphaTween.Tween(1, 0, fadeTime);
		}
		while (alphaTween.tweening)
		{
			fadeText.color = new Color(fadeText.color.r, fadeText.color.g, fadeText.color.b, alphaTween.currentValue);
			alphaTween.Loop();
			yield return null;
		}
	}

	// Just fade in from black
	private IEnumerator DoShortIntro () {
		yield return new WaitForSeconds(0.5f);
		AudioClipDatabase.Instance.PlayAttachingPaperSound();
		yield return new WaitForSeconds(1);

		FloatTween opacityTween = new FloatTween();
		opacityTween.Tween(1, 0, 5);
		while(opacityTween.tweening) {
			opacityTween.Loop();
			group.alpha = opacityTween.currentValue;
			if(Main.Instance.currentState == this && opacityTween.tweenTimer.GetNormalizedTime() > 0.45f) {
				Complete();
			}
			yield return null;
		}
	}
}

