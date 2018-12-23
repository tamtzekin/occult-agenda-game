using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndRestartButton : MonoBehaviour {

	public void OnClickRestartButton ()
	{
		Main.Instance.creditsView.ShowEndCredits();
	}

	public void OnClickCreditsRestartButton()
	{
		SceneManager.LoadScene(0);
	}
}
