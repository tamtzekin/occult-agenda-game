﻿using UnityEngine;
using System.Collections;

public class ContentView : StoryElementView {

	public TypedText textTyper = new TypedText();
	private RichTextSubstring richText;

	public Color driedColor;
	public Color wetColor;

	string startText;

	protected override void Awake () {
		text.color = wetColor;
		base.Awake();
	}

	protected override void Update () {
		base.Update ();
		if(textTyper.typing) {
			textTyper.Loop();
			if((Main.Instance.gameState.hasMadeAChoice || Application.isEditor) && Input.GetMouseButtonDown(0)) {
				textTyper.ShowInstantly();
			}
			else if(Main.Instance.fastText && Application.isEditor)
			{
				textTyper.ShowInstantly();
			}
		}
	}

	public override void LayoutText (string content) {
		base.LayoutText (content);

		TypedText.TypedTextSettings textTyperSettings = new TypedText.TypedTextSettings();
		textTyperSettings.customPostTypePause.Add(new TypedText.CustomStringTimeDelay(",", new TypedText.RandomTimeDelay(0.075f,0.1f)));
		textTyperSettings.customPostTypePause.Add(new TypedText.CustomStringTimeDelay(":", new TypedText.RandomTimeDelay(0.125f,0.175f)));
		textTyperSettings.customPostTypePause.Add(new TypedText.CustomStringTimeDelay("-", new TypedText.RandomTimeDelay(0.125f,0.175f)));
		textTyperSettings.customPostTypePause.Add(new TypedText.CustomStringTimeDelay(".", new TypedText.RandomTimeDelay(0.3f,0.4f)));
		textTyperSettings.customPostTypePause.Add(new TypedText.CustomStringTimeDelay("\n", new TypedText.RandomTimeDelay(0.5f,0.6f)));
		if(Main.Instance.gameState.hasMadeAChoice) {
			textTyperSettings.splitMode = TypedText.TypedTextSettings.SplitMode.Word;
			textTyperSettings.defaultTypeDelay = new TypedText.RandomTimeDelay(0.04f,0.065f);
		} else {
			textTyperSettings.splitMode = TypedText.TypedTextSettings.SplitMode.Character;
			textTyperSettings.defaultTypeDelay = new TypedText.RandomTimeDelay(0.03f,0.0425f);
		}

		richText = new RichTextSubstring (content);
		textTyper = new TypedText();
		textTyper.OnTypeText += OnTypeText;
		textTyper.OnCompleteTyping += CompleteTyping;
		textTyper.TypeText(richText.plainText, textTyperSettings);
	}

	void OnTypeText (string newText) {
		text.text = richText.Substring(0, textTyper.text.Length);
		if(newText != " ")
			AudioClipDatabase.Instance.PlayKeySound ();
	}

	public void AppendText(string content)
	{
		base.LayoutText(text.text + " " + content);

		TypedText.TypedTextSettings textTyperSettings = new TypedText.TypedTextSettings();
		textTyperSettings.customPostTypePause.Add(new TypedText.CustomStringTimeDelay(",", new TypedText.RandomTimeDelay(0.075f, 0.1f)));
		textTyperSettings.customPostTypePause.Add(new TypedText.CustomStringTimeDelay(":", new TypedText.RandomTimeDelay(0.125f, 0.175f)));
		textTyperSettings.customPostTypePause.Add(new TypedText.CustomStringTimeDelay("-", new TypedText.RandomTimeDelay(0.125f, 0.175f)));
		textTyperSettings.customPostTypePause.Add(new TypedText.CustomStringTimeDelay(".", new TypedText.RandomTimeDelay(0.3f, 0.4f)));
		textTyperSettings.customPostTypePause.Add(new TypedText.CustomStringTimeDelay("\n", new TypedText.RandomTimeDelay(0.5f, 0.6f)));
		if (Main.Instance.gameState.hasMadeAChoice)
		{
			textTyperSettings.splitMode = TypedText.TypedTextSettings.SplitMode.Word;
			textTyperSettings.defaultTypeDelay = new TypedText.RandomTimeDelay(0.04f, 0.065f);
		}
		else
		{
			textTyperSettings.splitMode = TypedText.TypedTextSettings.SplitMode.Character;
			textTyperSettings.defaultTypeDelay = new TypedText.RandomTimeDelay(0.03f, 0.0425f);
		}

		startText = text.text;
		richText = new RichTextSubstring(content);
		textTyper = new TypedText();
		textTyper.OnTypeText += OnAppendText;
		textTyper.OnCompleteTyping += CompleteTyping;
		textTyper.TypeText(richText.plainText, textTyperSettings);
	}

	void OnAppendText(string newText)
	{
		text.text = startText + " " + richText.Substring(0, textTyper.text.Length);
		if (newText != " ")
			AudioClipDatabase.Instance.PlayKeySound();
	}

	public void DeleteText(string content)
	{
		base.LayoutText(text.text);

		TypedText.TypedTextSettings textTyperSettings = new TypedText.TypedTextSettings();
		textTyperSettings.customPostTypePause.Add(new TypedText.CustomStringTimeDelay(",", new TypedText.RandomTimeDelay(0.075f, 0.1f)));
		textTyperSettings.customPostTypePause.Add(new TypedText.CustomStringTimeDelay(":", new TypedText.RandomTimeDelay(0.125f, 0.175f)));
		textTyperSettings.customPostTypePause.Add(new TypedText.CustomStringTimeDelay("-", new TypedText.RandomTimeDelay(0.125f, 0.175f)));
		textTyperSettings.customPostTypePause.Add(new TypedText.CustomStringTimeDelay(".", new TypedText.RandomTimeDelay(0.3f, 0.4f)));
		textTyperSettings.customPostTypePause.Add(new TypedText.CustomStringTimeDelay("\n", new TypedText.RandomTimeDelay(0.5f, 0.6f)));

		textTyperSettings.splitMode = TypedText.TypedTextSettings.SplitMode.Character;
		textTyperSettings.defaultTypeDelay = new TypedText.RandomTimeDelay(0.03f, 0.0425f);

		startText = text.text;
		richText = new RichTextSubstring(text.text);
		textTyper = new TypedText();
		textTyper.OnTypeText += OnDeleteText;
		textTyper.OnCompleteTyping += CompleteTyping;
		textTyper.TypeText(richText.plainText, textTyperSettings);
	}

	void OnDeleteText(string newText)
	{
		int amountLeft = startText.Length - textTyper.text.Length;
		text.text = richText.Substring(0, amountLeft);
		if (newText != " ")
			AudioClipDatabase.Instance.PlayKeySound();
	}

	protected override void CompleteTyping () {
		colorTween.Tween(text.color, driedColor, 8);
		base.CompleteTyping();
	}
}
