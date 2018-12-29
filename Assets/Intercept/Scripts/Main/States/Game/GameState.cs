using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine.UI;

using Debug = UnityEngine.Debug;

public class GameState : MainState {

	// The json compiled ink story
	public TextAsset storyJSON;
	// The ink runtime story object
	private Story story;

	public ContentManager contentManager;

	public ContentView contentViewPrefab;
	public ChoiceGroupView choiceGroupViewPrefab;
	public ChevronButtonView chevronViewPrefab;
	public EmptyView emptyViewPrefab;

	public Transform contentParent;
	public ChoiceGroupContainerView choiceContainerView;

	public ScrollToBottomButtonView scrollToBottomButton;

	public bool hasMadeAChoice = false;

	public SettingsView settingsView;
	public SettingsButton settingsButton;

    public Transform pagePrefab;

	[SerializeField] Transform policePagePrefab;

    Transform currentPage;

	int previousSanityValue = 0;

	public Text filenameText;

	[SerializeField] Text timeText;

	int hourTime = 0;
	int minuteTime = 0;
	bool outsideOfTime = false;

	[SerializeField] Text dateText;

	[SerializeField] Sprite[] sealImages;

	[SerializeField] List<GameObject> sanityObjects;

	[SerializeField] List<SanityEffect> sanityEffects;

	[SerializeField] Image endBackground;

	[SerializeField] Sprite endBlood;

	private void Awake () {
		//contentManager.enabled = false;
		settingsView.Hide();
		//settingsButton.Hide();
		#if !UNITY_EDITOR
		GameObject.Find("Sanity").SetActive(false);
		#endif
		foreach (GameObject sanityObject in sanityObjects)
		{
			sanityObject.SetActive(false);
		}
	}

	public override void Enter () {
		base.Enter ();

		currentPage = GameObject.Instantiate<Transform>(pagePrefab);
		currentPage.transform.SetParent(GameObject.Find("Game Canvas").transform, false);
		currentPage.transform.SetSiblingIndex(2);
		contentManager = currentPage.GetComponentInChildren<ContentManager>();
		contentParent = contentManager.layoutGroup.transform;

		contentManager.enabled = true;
		//settingsButton.FadeIn();

		if(storyJSON == null) {
			Debug.LogWarning("Drag a valid story JSON file into the StoryReader component.");
			enabled = false;
		}
		story = new Story(storyJSON.text);
		story.BindExternalFunction("getTime", () =>
		{
			string currentTime;
			if(outsideOfTime)
			{
				return timeText.text;
			}
			if (minuteTime < 10)
			{
				currentTime = hourTime + ":0" + minuteTime + "pm";
			}
			else
			{
				currentTime = hourTime + ":" + minuteTime + "pm";
			}
			return currentTime;
		});
		StartCoroutine(OnAdvanceStory());

		story.ObserveVariable("insanity", (string varName, object newValue) =>
		{
			Debug.Log("insanity changed to " + newValue);
			int newSanity = (int) newValue;
			#if UNITY_EDITOR
			GameObject.Find("Sanity").GetComponent<Text>().text = newSanity.ToString();
			#endif
			if (newSanity > previousSanityValue)
			{// sanity increased
				Debug.Log("Play sanity increase audio");
				AudioClipDatabase.Instance.PlaySanityIncrease();
				if(sanityEffects.Count >= newSanity && previousSanityValue >= 0)
				{
					sanityEffects[previousSanityValue].ApplyEffect();
				}
				
			}
			else
			{// sanity decreased
				Debug.Log("Play sanity decrease audio");
				AudioClipDatabase.Instance.PlaySanityDecrease();
				if (sanityEffects.Count >= previousSanityValue && newSanity >= 0)
				{
					sanityEffects[newSanity].RemoveEffect();
				}
			}
			previousSanityValue = newSanity;
		});
	}

	private IEnumerator FadeBetweenMeetings(string meetingName, Boolean ending = false)
	{
		CanvasGroup fadeCanvasGroup = Main.Instance.introState.group;

		fadeCanvasGroup.gameObject.SetActive(true);
		fadeCanvasGroup.alpha = 0;
		fadeCanvasGroup.interactable = true;
		fadeCanvasGroup.blocksRaycasts = true;

		Main.Instance.introState.inklePresentsText.gameObject.SetActive(false);
		Main.Instance.introState.theInterceptText.gameObject.SetActive(false);

		FloatTween fadeOutTween = new FloatTween();
		fadeOutTween.Tween(0, 1, 2);
		while (fadeOutTween.tweening)
		{
			fadeOutTween.Loop();
			if(fadeOutTween.currentValue > fadeCanvasGroup.alpha)
				fadeCanvasGroup.alpha = fadeOutTween.currentValue;
			yield return null;
		}
		fadeCanvasGroup.alpha = 1;
		yield return new WaitForSeconds(1.0f);

		currentPage.gameObject.SetActive(false);
		story.ChoosePathString(meetingName);

		Material oldPageMaterial = contentManager.paperImage.material;
		if (!ending)
		{
			currentPage = GameObject.Instantiate<Transform>(pagePrefab, GameObject.Find("Game Canvas").transform, false);
		}
		else
		{
			currentPage = GameObject.Instantiate<Transform>(policePagePrefab, GameObject.Find("Game Canvas").transform, false);
			GameObject.Find("Background").SetActive(false);
			GameObject.Find("DateTime").SetActive(false);
			GameObject.Find("TopBar").SetActive(false);
		}
		currentPage.transform.SetSiblingIndex(2);
		contentManager = currentPage.GetComponentInChildren<ContentManager>();
		contentParent = contentManager.layoutGroup.transform;
		StartCoroutine(OnAdvanceStory());

		if(oldPageMaterial && !ending)
		{
			contentManager.paperImage.material = oldPageMaterial;
		}

		if (meetingName.Equals("meetingthree"))
		{
			Debug.Log("Meeting Three");
			AudioClipDatabase.Instance.spooky = true;
			FindObjectOfType<BackgroundAmbienceController>().QuietMode();
			GameObject.Find("PactAudio").GetComponent<AudioSource>().Play();
		}

		FloatTween fadeInTween = new FloatTween();
		fadeInTween.Tween(1, 0, 2);
		while (fadeInTween.tweening)
		{
			fadeInTween.Loop();
			fadeCanvasGroup.alpha = fadeInTween.currentValue;
			yield return null;
		}
		fadeCanvasGroup.interactable = false;
		fadeCanvasGroup.blocksRaycasts = false;
	}

	public void Clear () {
		StopAllCoroutines();
		ClearContent();
		choiceContainerView.Clear();
		contentManager.enabled = false;
		settingsButton.Hide();
		settingsView.Hide();
	}

	private void ClearContent () {
		for (int i = contentParent.childCount-1; i >= 0; i--) {
			Destroy(contentParent.GetChild(i).gameObject);
		}
	}

	IEnumerator OnAdvanceStory () {
		if(story.canContinue) {
			ChoiceGroupView choiceView = null;
			ChevronButtonView chevronView = null;
			while(story.canContinue) {
				string content = story.Continue().Trim();
				if(content.StartsWith("FONT_OLD"))
				{
					content = content.Substring(9);
				}
				bool append = false;
				foreach (String tag in story.currentTags)
				{
					if (tag.Contains("="))
					{
						string[] tagValue = tag.Split('=');
						if (tagValue[0] == "Filename")
						{
							filenameText.text = tagValue[1];
						}
						if (tagValue[0] == "SetTime")
						{
							timeText.text = tagValue[1];
							string[] timeSplit = tagValue[1].Split(':');
							if (int.TryParse(timeSplit[0], out hourTime))
							{
								if(int.TryParse(timeSplit[1].Substring(0, 2), out minuteTime))
								{
									outsideOfTime = false;
								}
								else
								{// couldn't parse minutes
									outsideOfTime = true;
								}
							}
							else
							{
								outsideOfTime = true;
							}
						}
						if (tagValue[0] == "AddMinutes")
						{
							int newMinutes = 0;
							if (int.TryParse(tagValue[1], out newMinutes))
							{
								minuteTime = minuteTime + newMinutes;
								if (minuteTime > 60)
								{
									hourTime = hourTime + 1;
									minuteTime = minuteTime - 60;
								}
								if (minuteTime < 10)
								{
									timeText.text = hourTime + ":0" + minuteTime + "pm";
								}
								else
								{
									timeText.text = hourTime + ":" + minuteTime + "pm";
								}
							}
							else
							{
								Debug.LogWarning("AddMinutes not given a number! " + tagValue[1]);
							}
						}
						if (tagValue[0] == "SetDate")
						{
							dateText.text = tagValue[1];
						}
						if (tagValue[0] == "SetSeal")
						{
							int index = int.Parse(tagValue[1]);
							contentManager.sealImage.sprite = sealImages[index];
						}
						if (tagValue[0] == "EndBack")
						{
							if(tagValue[1] == "blood")
							{
								endBackground.sprite = endBlood;
							}
						}
					}
					else
					{
						if (tag == "Append")
						{
							append = true;
						}
					}
				}
				if (content.Length > 0)
				{
					ContentView contentView;
					if(!append)
					{
						contentView = CreateContentView(content);
					}
					else
					{
						contentView = AppendToLastContentView(content);
					}
					
					if (!story.canContinue)
					{
						if (story.currentChoices.Count > 0)
						{
							choiceView = CreateChoiceGroupView(story.currentChoices);
						}
						else
						{
							while (contentView.textTyper.typing)
							{
								Debug.Log("wait to make end chevron");
								yield return null;
							}
							CreateEmptyView(100f);
							yield return new WaitForSeconds(1.5f);
							chevronView = CreateChevronView();
						}

					}
					while (contentView.textTyper.typing)
						yield return null;
					if (story.canContinue)
						yield return new WaitForSeconds(Mathf.Min(1.0f, contentView.textTyper.targetText.Length * 0.01f));
				}
				else
				{
					if (!story.canContinue)
					{
						if (story.currentChoices.Count > 0)
						{
							choiceView = CreateChoiceGroupView(story.currentChoices);
						}
						else
						{
							//chevronView = CreateChevronView();
						}
					}
				}
			}
			if(story.currentChoices.Count > 0) {
				yield return new WaitForSeconds(1f);
				choiceView.RenderChoices();
				yield return new WaitForSeconds(0.5f);
			} else { // finished typing out, can't continue and no choices
				bool foundMeeting = false;
				foreach(String tag in story.currentTags)
				{
					Debug.Log("Read tag " + tag);
					if(tag.Contains("="))
					{
						string[] tagValue = tag.Split('=');
						if (tagValue[0] == "NextMeeting")
						{
							Debug.Log("Trying to go to " + tagValue[1]);
							StartCoroutine(FadeBetweenMeetings(tagValue[1]));
							foundMeeting = true;
						}
						if (tagValue[0] == "Ending")
						{
							Debug.Log("Trying to go to " + tagValue[1]);
							StartCoroutine(FadeBetweenMeetings(tagValue[1], true));
							foundMeeting = true;
						}
					}
				}
				if (!foundMeeting)
				{
					chevronView.Render();
				}
				yield return new WaitForSeconds(2);
			}
		} else {
			yield return new WaitForSeconds(2);
			CreateChevronView();
		}
	}

	public void ChooseChoiceIndex (int choiceIndex) {
		DestroyEmpties();
		story.ChooseChoiceIndex(choiceIndex);
		hasMadeAChoice = true;
		StartCoroutine(OnAdvanceStory());
	}

	private void DestroyEmpties () {
		EmptyView[] emptyViews = contentParent.GetComponentsInChildren<EmptyView>();
		for (int i = emptyViews.Length-1; i >= 0; i--) {
			Destroy(emptyViews[i].gameObject);
		}
	}

	public void ClickChevronButton () {
		Complete();
	}

	ContentView AppendToLastContentView(string content)
	{
		Transform lastChildTransform = contentParent.transform.GetChild(contentParent.transform.childCount - 2);
		ContentView contentView = lastChildTransform.GetComponent<ContentView>();
		contentView.AppendText(content);
		return contentView;
	}

	ContentView CreateContentView (string content) {
		ContentView contentView = Instantiate(contentViewPrefab);
		contentView.transform.SetParent(contentParent, false);
		contentView.LayoutText(content);
		return contentView;
	}

	ChoiceGroupView CreateChoiceGroupView (IList<Choice> choices) {
		ChoiceGroupView choiceGroupView = Instantiate(choiceGroupViewPrefab);
		choiceGroupView.transform.SetParent(choiceContainerView.transform, false);
		choiceGroupView.LayoutChoices(choices);
		CreateEmptyView(choiceGroupView.rectTransform.sizeDelta.y);
		return choiceGroupView;
	}

	ChevronButtonView CreateChevronView () {
		ChevronButtonView chevronView = Instantiate(chevronViewPrefab);
		chevronView.transform.SetParent(choiceContainerView.transform, false);
		//CreateEmptyView(chevronView.rectTransform.sizeDelta.y);
		return chevronView;
	}

	EmptyView CreateEmptyView (float height) {
		EmptyView emptyView = Instantiate(emptyViewPrefab);
		emptyView.transform.SetParent(contentParent, false);
		emptyView.layoutElement.preferredHeight = height;
		return emptyView;
	}
}