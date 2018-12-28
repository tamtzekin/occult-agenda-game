using UnityEngine;
using System.Collections;

public class CreditsView : MonoBehaviour {

	public void Show () {
		gameObject.SetActive(true);
		transform.Find("Restart Button").gameObject.SetActive(false);
		transform.Find("Quit Button").gameObject.SetActive(false);
	}

	public void ShowEndCredits()
	{
		gameObject.SetActive(true);
		transform.Find("Back Button").gameObject.SetActive(false);
		transform.Find("Restart Button").gameObject.SetActive(true);
		transform.Find("Quit Button").gameObject.SetActive(true);
	}

	public void Hide () {
		gameObject.SetActive(false);
	}

	public void OnClickBackButton () {
		Hide ();
	}
}
