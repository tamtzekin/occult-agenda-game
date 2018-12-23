using UnityEngine;
using System.Collections;

public class CreditsView : MonoBehaviour {

	public void Show () {
		gameObject.SetActive(true);
		transform.Find("Restart Button").gameObject.SetActive(false);
	}

	public void ShowEndCredits()
	{
		gameObject.SetActive(true);
		transform.Find("Back Button").gameObject.SetActive(false);
	}

	public void Hide () {
		gameObject.SetActive(false);
	}

	public void OnClickBackButton () {
		Hide ();
	}
}
