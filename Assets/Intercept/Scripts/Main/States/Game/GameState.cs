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

	[SerializeField] Text dateText;

	[SerializeField] Sprite[] sealImages;

	private void Awake () {
		//contentManager.enabled = false;
		settingsView.Hide();
		//settingsButton.Hide();
		#if UNITY_STANDALONE
		GameObject.Find("Sanity").SetActive(false);
		#endif
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
				
			}
			else
			{// sanity decreased
				Debug.Log("Play sanity decrease audio");
				AudioClipDatabase.Instance.PlaySanityDecrease();
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

		currentPage.gameObject.SetActive(false);
		story.ChoosePathString(meetingName);
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
		CreateEmptyView(chevronView.rectTransform.sizeDelta.y);
		return chevronView;
	}

	EmptyView CreateEmptyView (float height) {
		EmptyView emptyView = Instantiate(emptyViewPrefab);
		emptyView.transform.SetParent(contentParent, false);
		emptyView.layoutElement.preferredHeight = height;
		return emptyView;
	}
}